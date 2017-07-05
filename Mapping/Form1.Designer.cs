namespace Mapping
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_start = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_Add = new System.Windows.Forms.Button();
            this.button_exit = new System.Windows.Forms.Button();
            this.button_removed = new System.Windows.Forms.Button();
            this.button_about = new System.Windows.Forms.Button();
            this.Column_guid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_lExternalPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Ip_out = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Ip_out_port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Running = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_ConnectCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Receive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(689, 133);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 0;
            this.button_start.Text = "开始";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_guid,
            this.Column_lExternalPort,
            this.Column_Ip_out,
            this.Column_Ip_out_port,
            this.Column_Running,
            this.Column_ConnectCount,
            this.Column_Receive});
            this.dataGridView1.Location = new System.Drawing.Point(3, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(680, 308);
            this.dataGridView1.TabIndex = 4;
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(689, 59);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(75, 23);
            this.button_Add.TabIndex = 5;
            this.button_Add.Text = "添加";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // button_exit
            // 
            this.button_exit.Location = new System.Drawing.Point(689, 170);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(75, 23);
            this.button_exit.TabIndex = 6;
            this.button_exit.Text = "退出";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button5_Click);
            // 
            // button_removed
            // 
            this.button_removed.Location = new System.Drawing.Point(689, 96);
            this.button_removed.Name = "button_removed";
            this.button_removed.Size = new System.Drawing.Size(75, 23);
            this.button_removed.TabIndex = 7;
            this.button_removed.Text = "删除";
            this.button_removed.UseVisualStyleBackColor = true;
            this.button_removed.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button_about
            // 
            this.button_about.Location = new System.Drawing.Point(689, 207);
            this.button_about.Name = "button_about";
            this.button_about.Size = new System.Drawing.Size(75, 23);
            this.button_about.TabIndex = 8;
            this.button_about.Text = "关于";
            this.button_about.UseVisualStyleBackColor = true;
            this.button_about.Click += new System.EventHandler(this.button_about_Click);
            // 
            // Column_guid
            // 
            this.Column_guid.HeaderText = "Column_guid";
            this.Column_guid.Name = "Column_guid";
            this.Column_guid.ReadOnly = true;
            this.Column_guid.Visible = false;
            // 
            // Column_lExternalPort
            // 
            this.Column_lExternalPort.HeaderText = "ExternalPort";
            this.Column_lExternalPort.Name = "Column_lExternalPort";
            this.Column_lExternalPort.ReadOnly = true;
            // 
            // Column_Ip_out
            // 
            this.Column_Ip_out.HeaderText = "Target IP";
            this.Column_Ip_out.Name = "Column_Ip_out";
            this.Column_Ip_out.ReadOnly = true;
            this.Column_Ip_out.Width = 150;
            // 
            // Column_Ip_out_port
            // 
            this.Column_Ip_out_port.HeaderText = "Target Port";
            this.Column_Ip_out_port.Name = "Column_Ip_out_port";
            this.Column_Ip_out_port.ReadOnly = true;
            // 
            // Column_Running
            // 
            dataGridViewCellStyle1.NullValue = "未运行";
            this.Column_Running.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column_Running.HeaderText = "Running";
            this.Column_Running.Name = "Column_Running";
            this.Column_Running.ReadOnly = true;
            // 
            // Column_ConnectCount
            // 
            this.Column_ConnectCount.HeaderText = "ConnectCount";
            this.Column_ConnectCount.Name = "Column_ConnectCount";
            this.Column_ConnectCount.ReadOnly = true;
            // 
            // Column_Receive
            // 
            this.Column_Receive.HeaderText = "Receive out/in";
            this.Column_Receive.Name = "Column_Receive";
            this.Column_Receive.ReadOnly = true;
            this.Column_Receive.Width = 120;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 316);
            this.Controls.Add(this.button_about);
            this.Controls.Add(this.button_removed);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.button_Add);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PortMapping";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Button button_exit;
        private System.Windows.Forms.Button button_removed;
        private System.Windows.Forms.Button button_about;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_guid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_lExternalPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Ip_out;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Ip_out_port;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Running;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_ConnectCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Receive;
    }
}

