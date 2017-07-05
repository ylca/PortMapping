using NATUPNPLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

namespace Mapping
{
    public class Mapping
    {
        public delegate void ConnectionState(List<WorkItem> list);
        /// <summary>
        /// 启动状态
        /// </summary>
        private static bool startState = false;
        /// <summary>
        /// 映射器集合
        /// </summary>
        private static List<WorkItem> mWorkItemList = new List<WorkItem>();
        /// <summary>
        /// 映射状态回调
        /// </summary>
        public static ConnectionState mConnectionState = null;
        /// <summary>
        /// 外网IP
        /// </summary>
        private static string mExternalIP = OtherUtils.getExtranetlIP();

        /// <summary>
        /// 开启所有映射器
        /// </summary>
        public static void Start()
        {
            //启动映射器
            foreach (WorkItem item in mWorkItemList)
            {
                map_start(item);
            }
            while (true)
            {
                //每2秒更新一次数据
                //委托调用
                mConnectionState?.Invoke(mWorkItemList);
                Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// 添加映射器  已经启动无法再添加
        /// </summary>
        /// <param name="wi"></param>
        public static bool Add(WorkItem wi)
        {
            //如果已经被启动则不能添加
            if (startState)
            {
                return false;
            }
            else
            { 
                //用户不能设置ID
                if (wi._id != null)
                {
                    return false;
                }
                //设置顺序ID
                wi._id = OtherUtils.getGUID();
                if (wi.ip_local == null)
                {
                    //设置本地IP
                    wi.ip_local = new IPEndPoint(OtherUtils.getLoaclIP(), OtherUtils.GetFirstAvailablePort() + mWorkItemList.Count);
                }
                MyEndPoint mep = OtherUtils.FormatEndPoint(wi.ip_local.ToString());
                //如果添加Upnp失败直接返回 false
                if (!OtherUtils.AddUpnp(wi.lExternalPort, "TCP", mep.port, mep.ip, true, "Mapping Y1ca"))
                {
                    return false;
                }
               
                mWorkItemList.Add(wi);
                return true;
            }
        }
        /// <summary>
        /// 清除所有映射器
        /// </summary>
        public static void Clear()
        {
            
            //删除所有对象
            mWorkItemList.Clear();
        }
        /// <summary>
        ///  删除映射器
        /// </summary>
        public static void Removed(ushort port)
        {
            for (int i = 0; i < mWorkItemList.Count; i++)
            {
                if (mWorkItemList[i].lExternalPort == port)
                {
                    mWorkItemList.Remove(mWorkItemList[i]);
                }
            }
    
        }
        /// <summary>
        /// 检查是否存在相同映射  外网端口只能一个占用  存在返回true 不存在返回 false
        /// </summary>
        /// <param name="lExternalPort"></param>
        /// <returns></returns>
        public static bool Contains(int lExternalPort)
        {

            //如果集合中没有数据 直接不存在
            if (mWorkItemList.Count == 0)
            {
                return false;
            }
            foreach (var item in mWorkItemList)
            {
                if (item.lExternalPort == lExternalPort)
                {
                    return true;
                }
            }

            return false;


        }
        /// <summary>
        /// 获取所有映射器
        /// </summary>
        /// <returns></returns>
        public static List<WorkItem> Get()
        {
            
            return mWorkItemList;
        }
        /// <summary>
        /// 启动映射器
        /// </summary>
        /// <param name="work"></param>
        private static void map_start(WorkItem work)
        {
            //Socket sock_svr = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            work.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            bool start_error = false;
            try
            {
                work.socket.Bind(work.ip_local);//绑定本机ip
                work.socket.Listen(10);
                work.socket.BeginAccept(on_local_connected, new object[] { work.socket, work });//接受connect
            }
            catch (Exception)
            {
                start_error = true;
            }
            finally
            {
                work.workState._point_external = mExternalIP + ":" + work.lExternalPort;
                work.workState._point_in = work.ip_local.ToString();
                work.workState._point_out = work.ip_out + ":" + work.ip_out_port;
                work.workState._running = !start_error;
                work.workState._connect_cnt = 0;
                work.workState._bytes_send = 0;
                work.workState._bytes_recv = 0;
            }
        }

        /// <summary>
        /// 收到connect
        /// </summary>
        /// <param name="ar"></param>
        private static void on_local_connected(IAsyncResult ar)
        {
            ///获取用户传入对象
            object[] ar_arr = ar.AsyncState as object[];
            //获取Socket
            Socket sock_svr = ar_arr[0] as Socket;
            //获取Work
            WorkItem work = (WorkItem)ar_arr[1];
            //增加连接数
            ++work.workState._connect_cnt;
            Socket sock_cli = sock_svr.EndAccept(ar);
            sock_svr.BeginAccept(on_local_connected, ar.AsyncState);
            Socket sock_cli_remote = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sock_cli_remote.Connect(work.ip_out, work.ip_out_port);
            }
            catch (Exception)
            {
                try
                {
                    sock_cli.Shutdown(SocketShutdown.Both);
                    sock_cli_remote.Shutdown(SocketShutdown.Both);
                    sock_cli.Close();
                    sock_cli_remote.Close();
                }
                catch (Exception)
                { }
               --work.workState._connect_cnt;
                return;
            }
            //线程: 接受本地数据 转发至远程
            Thread t_send = new Thread(recv_and_send_caller) { IsBackground = true };
            //线程: 接受远程数据 转发至本地connect 端
            Thread t_recv = new Thread(recv_and_send_caller) { IsBackground = true };
            t_send.Start(new object[] { sock_cli, sock_cli_remote, work, true });
            t_recv.Start(new object[] { sock_cli_remote, sock_cli, work, false });
            //线程同步
            t_send.Join();
            //--_state_dic[work._id]._connect_cnt;
            --work.workState._connect_cnt;
            t_recv.Join();
            //已断开, 连接数-1

        }

        /// <summary>
        /// 数据转发
        /// </summary>
        /// <param name="from_sock"></param>
        /// <param name="to_sock"></param>
        /// <param name="send_complete"></param>
        private static void recv_and_send(Socket from_sock, Socket to_sock, Action<int> send_complete)
        {
            byte[] recv_buf = new byte[4096];
            int recv_len;
            while ((recv_len = from_sock.Receive(recv_buf)) > 0)
            {
                to_sock.Send(recv_buf, 0, recv_len, SocketFlags.None);
                send_complete(recv_len);
            }
        }

        private static void recv_and_send_caller(object thread_param)
        {
            object[] param_arr = thread_param as object[];
            Socket sock1 = param_arr[0] as Socket;
            Socket sock2 = param_arr[1] as Socket;
            try
            {
                recv_and_send(sock1, sock2, bytes =>
                {
                    WorkItem work = (WorkItem)param_arr[2];
                    if ((bool)param_arr[3])
                        work.workState._bytes_send += bytes;
                    else
                        work.workState._bytes_recv += bytes;
                });
            }
            catch (Exception)
            {
                try
                {
                    sock1.Shutdown(SocketShutdown.Both);
                    sock2.Shutdown(SocketShutdown.Both);
                    sock1.Close();
                    sock2.Close();
                }
                catch (Exception) { }
            }
        }


    }

    /// <summary>
    /// 映射器线程所需数据
    /// </summary>
    public struct WorkItem
    {

        /// <summary>
        /// 工作状态
        /// </summary>
        public WorkState workState;
        public string _id;
        /// <summary>
        /// socket  
        /// </summary>
        public Socket socket;
        /// <summary>
        /// In IP
        /// </summary>
        public EndPoint ip_local;
        /// <summary>
        /// 目标地址
        /// </summary>
        public string ip_out;
        /// <summary>
        /// 目标端口
        /// </summary>
        public ushort ip_out_port;
        /// <summary>
        /// 对外开放端口
        /// </summary>
        public ushort lExternalPort;

    }
    public class WorkState
    {
        public const string _print_head = "外部IP              中转IP              目标IP              状态    连接数    接收/发送";
        public int _connect_cnt;
        public string _point_external;
        public string _point_in;
        public string _point_out;
        public bool _running;
        public long _bytes_send;
        public long _bytes_recv;
        public WorkState(string point_external, string point_in, string point_out, bool running, int connect_cnt, int bytes_send, int bytes_recv)
        {
            _point_external = point_external;
            _point_in = point_in;
            _point_out = point_out;
            _running = running;
            _connect_cnt = connect_cnt;
            _bytes_recv = bytes_recv;
            _bytes_send = bytes_send;
        }
        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}", _point_external.PadRight(20, ' '), _point_in.PadRight(20, ' '), _point_out.PadRight(20, ' '), (_running ? "运行中  " : "启动失败"), _connect_cnt.ToString().PadRight(10, ' '), Math.Round((double)_bytes_recv / 1024) + "k/" + Math.Round((double)_bytes_send / 1024) + "k");
        }
    }


    public  class OtherUtils
    {
        /// <summary>
        /// 获取本地IP
        /// </summary>
        /// <returns></returns>
        public static IPAddress getLoaclIP()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(i => i.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
        }
        /// <summary>
        /// 获取外网IP
        /// </summary>
        /// <returns></returns>
        public static string getExtranetlIP()
        {
            var regex = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
            using (var webclient = new WebClient())
            {
                var rawRes = webclient.DownloadString("http://www.3322.org/dyndns/getip");
                return Regex.Match(rawRes, regex).Value;
            }
        }
        /// <summary>
        /// 检查IP是否合法
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool checkIP(string ip)
        {
            IPAddress ipa;
            return IPAddress.TryParse(ip,out ipa);
        }


        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lExternalPort">外部端口</param>
        /// <param name="bstrProtocol">协议类型</param>
        /// <param name="lInternalPort">内部端口</param>
        /// <param name="bstrInternalClient">内网IP</param>
        /// <param name="bEnabled"></param>
        /// <param name="bstrDescription">名称</param>
        /// <returns></returns>
        public static bool AddUpnp(int lExternalPort, string bstrProtocol, int lInternalPort, string bstrInternalClient, bool bEnabled, string bstrDescription)
        {

            //创建COM类型
            var upnpnat = new UPnPNAT();
            var mappings = upnpnat.StaticPortMappingCollection;
          
            //错误判断
            if (mappings == null)
            {
                Console.WriteLine("没有检测到路由器，或者路由器不支持UPnP功能。");
                return false;
            }

            try
            {
                //添加之前的ipv4变量（内网IP），内部端口，和外部端口
                mappings.Add(lExternalPort, bstrProtocol, lInternalPort, bstrInternalClient, bEnabled, bstrDescription);
            }
            catch (Exception)
            {
              //  return false;

            }
          
            
            return true;
        }

        public static MyEndPoint FormatEndPoint(string endPoint)
        {
            MyEndPoint mep = new MyEndPoint();
            mep.port = int.Parse(endPoint.Substring(endPoint.IndexOf(':') + 1));
            mep.ip = endPoint.Substring(0, endPoint.IndexOf(':'));
            return mep;
        }


        /// <summary> 
        /// 获取第一个可用的端口号 
        /// </summary> 
        /// <returns></returns> 
        public static int GetFirstAvailablePort()
        {
            int MAX_PORT = 65535; //系统tcp/udp端口数最大是65535 
            int BEGIN_PORT = 5000;//从这个端口开始检测 

            for (int i = BEGIN_PORT; i < MAX_PORT; i++)
            {
                if (PortIsAvailable(i)) return i;
            }

            return -1;
        }

        /// <summary> 
        /// 获取操作系统已用的端口号 
        /// </summary> 
        /// <returns></returns> 
        public static IList PortIsUsed()
        {
            //获取本地计算机的网络连接和通信统计数据的信息 
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //返回本地计算机上的所有Tcp监听程序 
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();

            //返回本地计算机上的所有UDP监听程序 
            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

            //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。 
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            IList allPorts = new ArrayList();
            foreach (IPEndPoint ep in ipsTCP) allPorts.Add(ep.Port);
            foreach (IPEndPoint ep in ipsUDP) allPorts.Add(ep.Port);
            foreach (TcpConnectionInformation conn in tcpConnInfoArray) allPorts.Add(conn.LocalEndPoint.Port);

            return allPorts;
        }

        /// <summary> 
        /// 检查指定端口是否已用
        /// </summary> 
        /// <param name="port"></param> 
        /// <returns></returns> 
        public static bool PortIsAvailable(int port)
        {
            bool isAvailable = true;
            IList portUsed = PortIsUsed();

            foreach (int p in portUsed)
            {
                if (p == port)
                {
                    isAvailable = false; break;
                }
            }
            return isAvailable;
        }

        /// <summary>
        ///获取全球唯一ID
        /// </summary>
        /// <returns></returns>
        public static string getGUID()
        {
            return Guid.NewGuid().ToString();
        }

    }
    public class MyEndPoint
    {
        public int port = 8888;
        public string ip = "0.0.0.0";
    }
}
