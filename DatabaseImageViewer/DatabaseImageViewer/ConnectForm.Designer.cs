namespace DatabaseImageViewer
{
    partial class ConnectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.databaseComboBoxEx = new System.Windows.Forms.ComboBox();
            this.ipAddressInput = new IPAddressControlLib.IPAddressControl();
            this.userNameTextBoxX = new System.Windows.Forms.TextBox();
            this.passWordTextBoxX = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "密码:";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(30, 144);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "连接";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(155, 143);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "数据库:";
            // 
            // databaseComboBoxEx
            // 
            this.databaseComboBoxEx.FormattingEnabled = true;
            this.databaseComboBoxEx.Location = new System.Drawing.Point(106, 95);
            this.databaseComboBoxEx.Name = "databaseComboBoxEx";
            this.databaseComboBoxEx.Size = new System.Drawing.Size(116, 20);
            this.databaseComboBoxEx.TabIndex = 9;
            this.databaseComboBoxEx.DropDown += new System.EventHandler(this.databaseDropDown_Click);
            this.databaseComboBoxEx.SelectedValueChanged += new System.EventHandler(this.databaseDropDown_ValueChanged);
            // 
            // ipAddressInput
            // 
            this.ipAddressInput.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressInput.Location = new System.Drawing.Point(106, 13);
            this.ipAddressInput.MinimumSize = new System.Drawing.Size(116, 21);
            this.ipAddressInput.Name = "ipAddressInput";
            this.ipAddressInput.ReadOnly = false;
            this.ipAddressInput.Size = new System.Drawing.Size(116, 21);
            this.ipAddressInput.TabIndex = 10;
            // 
            // userNameTextBoxX
            // 
            this.userNameTextBoxX.Location = new System.Drawing.Point(106, 40);
            this.userNameTextBoxX.Name = "userNameTextBoxX";
            this.userNameTextBoxX.Size = new System.Drawing.Size(116, 21);
            this.userNameTextBoxX.TabIndex = 11;
            // 
            // passWordTextBoxX
            // 
            this.passWordTextBoxX.Location = new System.Drawing.Point(106, 67);
            this.passWordTextBoxX.Name = "passWordTextBoxX";
            this.passWordTextBoxX.PasswordChar = '*';
            this.passWordTextBoxX.Size = new System.Drawing.Size(116, 21);
            this.passWordTextBoxX.TabIndex = 12;
            // 
            // ConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(261, 178);
            this.Controls.Add(this.passWordTextBoxX);
            this.Controls.Add(this.userNameTextBoxX);
            this.Controls.Add(this.ipAddressInput);
            this.Controls.Add(this.databaseComboBoxEx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectForm";
            this.Text = "连接";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox databaseComboBoxEx;
        private IPAddressControlLib.IPAddressControl ipAddressInput;
        private System.Windows.Forms.TextBox userNameTextBoxX;
        private System.Windows.Forms.TextBox passWordTextBoxX;
    }
}