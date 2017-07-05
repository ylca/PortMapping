using NATUPNPLib;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Mapping
{
    public delegate void MappingAddSuccess();
    public partial class Form1 : Form
    {
        Config mConfig = new Config();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            //创建COM类型
            var upnpnat = new UPnPNAT();
            var mappings = upnpnat.StaticPortMappingCollection;
            //错误判断
            if (mappings == null)
            {
                MessageBox.Show("没有检测到路由器，或者路由器不支持UPnP功能。");
            }
            loadConfig();
        }

        private void loadConfig()
        {
            try
            {
                //从配置文件中加载
                List<WorkItemXml> list = XmlSerializeUtil.Deserialize(mConfig.GetList().GetType(), "Config.xml") as List<WorkItemXml>;
                foreach (WorkItemXml item in list)
                {
                    dataGridView1.Rows.Add(new object[] { item.ExternalPort, item.Ip_out, item.Ip_out_port });
                    Mapping.Add(new WorkItem
                    {
                        lExternalPort = item.ExternalPort,
                        ip_out = item.Ip_out,
                        ip_out_port = item.Ip_out_port,
                        workState = new WorkState(" ", " ", " ", false, 0, 0, 0)
                    });
                }
                //刷新表格数据
                RefreshDatagridViewData();
            }
            catch (Exception)
            {


            }


        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(start);
            t.Start();

        }


        public void start()
        {
            // Mapping.Add(new WorkItem { lExternalPort = 8351, ip_out = "192.168.2.12", ip_out_port = 1433 });
            Mapping.mConnectionState = new Mapping.ConnectionState(connectionState);
            Mapping.Start();
        }
        public void connectionState(List<WorkItem> list)
        {
            try
            {
                this.Invoke(new MappingAddSuccess(RefreshDatagridViewData));
            }
            catch (Exception)
            {


            }

        }


        private void button_Add_Click(object sender, EventArgs e)
        {
            FormAdd fa = new FormAdd(new MappingAddSuccess(RefreshDatagridViewData));
            fa.Show();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshDatagridViewData()
        {
            dataGridView1.Rows.Clear();
            foreach (var item in Mapping.Get())
            {
                dataGridView1.Rows.Add(new object[] {
                    item._id, item.lExternalPort,
                    item.ip_out, item.ip_out_port,
                    item.workState._running?"运行中":"未运行",
                    item.workState._connect_cnt,
                    item.workState._bytes_recv /1024+ "k/" + item.workState._bytes_send/1024+"k"

                });

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            mConfig.Clear();
            foreach (var item in Mapping.Get())
            {
                //添加到配置文件中
                mConfig.Add(new WorkItemXml(item.lExternalPort, item.ip_out, item.ip_out_port));
                //序列号保存配置文件
                XmlSerializeUtil.Serializer(mConfig.GetList().GetType(), mConfig.GetList(), "Config.xml");
            }
            System.Environment.Exit(0);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            mConfig.Clear();
            foreach (var item in Mapping.Get())
            {
                //添加到配置文件中
                mConfig.Add(new WorkItemXml(item.lExternalPort, item.ip_out, item.ip_out_port));
                //序列号保存配置文件
                XmlSerializeUtil.Serializer(mConfig.GetList().GetType(), mConfig.GetList(), "Config.xml");
            }
            System.Environment.Exit(0);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                Mapping.Removed(ushort.Parse(dataGridView1.SelectedRows[0].Cells[Column_lExternalPort.Name].Value.ToString()));
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
            catch (Exception)
            {


            }
            // RefreshDatagridViewData();
        }

        private void button_about_Click(object sender, EventArgs e)
        {
            FormAbout fa =    new FormAbout();
           // fa.Parent = this;
            fa.Show();
        }
    }
}
