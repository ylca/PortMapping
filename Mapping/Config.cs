using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mapping
{
    public class Config
    {
        private List<WorkItemXml> mWorkItemList;

        public Config()
        {
            mWorkItemList = new List<WorkItemXml>();
        }
        public void Add(WorkItemXml w)
        {
            mWorkItemList.Add(w);
        }

        public void Clear()
        {
            mWorkItemList.Clear();

        }
        public List<WorkItemXml> GetList()
        {
           
            return mWorkItemList;
        }
    }

    public class WorkItemXml
    {
        public WorkItemXml() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExternalPort">对外开放端口</param>
        /// <param name="Ip_out">目标IP</param>
        /// <param name="Ip_out_port">目标端口</param>
        public WorkItemXml(ushort ExternalPort,string Ip_out, ushort Ip_out_port)
        {
            externalPort = ExternalPort;
            ip_out = Ip_out;
            ip_out_port = Ip_out_port;
        }

        private ushort externalPort;
        /// <summary>
        /// 对外开放端口
        /// </summary>
        public ushort ExternalPort
        {
            get { return externalPort; }
            set { externalPort = value; }
        }


        private string ip_out;
        /// <summary>
        /// 目标IP
        /// </summary>
        public string Ip_out
        {
            get { return ip_out; }
            set { ip_out = value; }
        }


        private ushort ip_out_port;
        /// <summary>
        /// 目标端口
        /// </summary>
        public ushort Ip_out_port
        {
            get { return ip_out_port; }
            set { ip_out_port = value; }
        }

    }

}
