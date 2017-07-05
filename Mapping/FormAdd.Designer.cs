namespace Mapping
{
    partial class FormAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_lExternalPort = new System.Windows.Forms.TextBox();
            this.textBox_IpOut = new System.Windows.Forms.TextBox();
            this.textBox_IpOutPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_Add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_lExternalPort
            // 
            this.textBox_lExternalPort.Location = new System.Drawing.Point(87, 6);
            this.textBox_lExternalPort.Name = "textBox_lExternalPort";
            this.textBox_lExternalPort.Size = new System.Drawing.Size(100, 21);
            this.textBox_lExternalPort.TabIndex = 0;
            // 
            // textBox_IpOut
            // 
            this.textBox_IpOut.Location = new System.Drawing.Point(87, 33);
            this.textBox_IpOut.Name = "textBox_IpOut";
            this.textBox_IpOut.Size = new System.Drawing.Size(100, 21);
            this.textBox_IpOut.TabIndex = 1;
            // 
            // textBox_IpOutPort
            // 
            this.textBox_IpOutPort.Location = new System.Drawing.Point(87, 60);
            this.textBox_IpOutPort.Name = "textBox_IpOutPort";
            this.textBox_IpOutPort.Size = new System.Drawing.Size(100, 21);
            this.textBox_IpOutPort.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "开放端口:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "目标端口:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "目标IP:";
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(66, 90);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(75, 23);
            this.button_Add.TabIndex = 6;
            this.button_Add.Text = "添加";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // FormAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 125);
            this.Controls.Add(this.button_Add);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_IpOutPort);
            this.Controls.Add(this.textBox_IpOut);
            this.Controls.Add(this.textBox_lExternalPort);
            this.Name = "FormAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAdd";
            this.Load += new System.EventHandler(this.FormAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_lExternalPort;
        private System.Windows.Forms.TextBox textBox_IpOut;
        private System.Windows.Forms.TextBox textBox_IpOutPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Add;
    }
}