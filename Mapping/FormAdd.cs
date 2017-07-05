using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace Mapping
{

    public partial class FormAdd : Form
    {
        MappingAddSuccess mMappingAddSuccess;


        public FormAdd(MappingAddSuccess mas)
        {
            mMappingAddSuccess = mas;
            InitializeComponent();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {

            try
            {
                if (Mapping.Contains(ushort.Parse(textBox_lExternalPort.Text)))
                {
                    MessageBox.Show("端口已经存在");
                    return;
                }
                //如果能格式化就说明没问题
                ushort.Parse(textBox_IpOutPort.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("请填写正确的端口号");
                return;
            }

           
          
            if (Mapping.Add(new WorkItem { lExternalPort = ushort.Parse(textBox_lExternalPort.Text), ip_out = textBox_IpOut.Text, ip_out_port = ushort.Parse(textBox_IpOutPort.Text),workState = new WorkState(" ", " ", " ", false, 0, 0, 0)
        }))
            {
                MessageBox.Show("添加成功");
                this.Close();
                mMappingAddSuccess.Invoke();
            }
            else
            {
                MessageBox.Show("添加失败!请确认已经停止工作");
            }



            
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
            textBox_IpOut.Text = OtherUtils.getLoaclIP().ToString();
        }
    }
}
