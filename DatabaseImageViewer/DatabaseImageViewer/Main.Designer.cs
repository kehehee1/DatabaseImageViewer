namespace DatabaseImageViewer
{
    partial class Main
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.queryButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.outPutPathButton = new System.Windows.Forms.Button();
            this.outPutButton = new System.Windows.Forms.Button();
            this.openPath = new System.Windows.Forms.Button();
            this.imageFieldGrid = new System.Windows.Forms.DataGridView();
            this.recordGrid = new System.Windows.Forms.DataGridView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tableComboBoxEx = new System.Windows.Forms.ComboBox();
            this.filterTextBoxX = new System.Windows.Forms.TextBox();
            this.outPutPathTextBox = new System.Windows.Forms.TextBox();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageFieldGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1206, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连接ToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 连接ToolStripMenuItem
            // 
            this.连接ToolStripMenuItem.Name = "连接ToolStripMenuItem";
            this.连接ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.连接ToolStripMenuItem.Text = "连接";
            this.连接ToolStripMenuItem.Click += new System.EventHandler(this.连接ToolStripMenuItem_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.设置ToolStripMenuItem.Text = "设置";
            this.设置ToolStripMenuItem.Click += new System.EventHandler(this.设置ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "select * from ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "where ";
            // 
            // queryButton
            // 
            this.queryButton.Location = new System.Drawing.Point(859, 25);
            this.queryButton.Name = "queryButton";
            this.queryButton.Size = new System.Drawing.Size(75, 23);
            this.queryButton.TabIndex = 11;
            this.queryButton.Text = "查询";
            this.queryButton.UseVisualStyleBackColor = true;
            this.queryButton.Click += new System.EventHandler(this.queryButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "输出路径：";
            // 
            // outPutPathButton
            // 
            this.outPutPathButton.Location = new System.Drawing.Point(531, 51);
            this.outPutPathButton.Name = "outPutPathButton";
            this.outPutPathButton.Size = new System.Drawing.Size(75, 23);
            this.outPutPathButton.TabIndex = 14;
            this.outPutPathButton.Text = "浏览";
            this.outPutPathButton.UseVisualStyleBackColor = true;
            this.outPutPathButton.Click += new System.EventHandler(this.outPutPathButton_Click);
            // 
            // outPutButton
            // 
            this.outPutButton.Location = new System.Drawing.Point(612, 51);
            this.outPutButton.Name = "outPutButton";
            this.outPutButton.Size = new System.Drawing.Size(75, 23);
            this.outPutButton.TabIndex = 15;
            this.outPutButton.Text = "输出";
            this.outPutButton.UseVisualStyleBackColor = true;
            this.outPutButton.Click += new System.EventHandler(this.outPutButton_Click);
            // 
            // openPath
            // 
            this.openPath.Location = new System.Drawing.Point(694, 51);
            this.openPath.Name = "openPath";
            this.openPath.Size = new System.Drawing.Size(75, 23);
            this.openPath.TabIndex = 16;
            this.openPath.Text = "打开目录";
            this.openPath.UseVisualStyleBackColor = true;
            this.openPath.Click += new System.EventHandler(this.openPath_Click);
            // 
            // imageFieldGrid
            // 
            this.imageFieldGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.imageFieldGrid.Location = new System.Drawing.Point(14, 82);
            this.imageFieldGrid.Name = "imageFieldGrid";
            this.imageFieldGrid.RowTemplate.Height = 23;
            this.imageFieldGrid.Size = new System.Drawing.Size(284, 588);
            this.imageFieldGrid.TabIndex = 17;
            // 
            // recordGrid
            // 
            this.recordGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.recordGrid.Location = new System.Drawing.Point(304, 82);
            this.recordGrid.Name = "recordGrid";
            this.recordGrid.ReadOnly = true;
            this.recordGrid.RowTemplate.Height = 23;
            this.recordGrid.Size = new System.Drawing.Size(890, 588);
            this.recordGrid.TabIndex = 18;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(14, 691);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1180, 11);
            this.progressBar.TabIndex = 19;
            // 
            // tableComboBoxEx
            // 
            this.tableComboBoxEx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tableComboBoxEx.FormattingEnabled = true;
            this.tableComboBoxEx.Location = new System.Drawing.Point(97, 26);
            this.tableComboBoxEx.Name = "tableComboBoxEx";
            this.tableComboBoxEx.Size = new System.Drawing.Size(154, 20);
            this.tableComboBoxEx.Sorted = true;
            this.tableComboBoxEx.TabIndex = 20;
            this.tableComboBoxEx.DropDown += new System.EventHandler(this.tableDropDown_Click);
            this.tableComboBoxEx.SelectedValueChanged += new System.EventHandler(this.tableSelectValue_Changed);
            // 
            // filterTextBoxX
            // 
            this.filterTextBoxX.Location = new System.Drawing.Point(304, 25);
            this.filterTextBoxX.Name = "filterTextBoxX";
            this.filterTextBoxX.Size = new System.Drawing.Size(540, 21);
            this.filterTextBoxX.TabIndex = 21;
            this.filterTextBoxX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filterTextBoxX_KeyDown);
            // 
            // outPutPathTextBox
            // 
            this.outPutPathTextBox.Location = new System.Drawing.Point(73, 52);
            this.outPutPathTextBox.Name = "outPutPathTextBox";
            this.outPutPathTextBox.ReadOnly = true;
            this.outPutPathTextBox.Size = new System.Drawing.Size(442, 21);
            this.outPutPathTextBox.TabIndex = 22;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 709);
            this.Controls.Add(this.outPutPathTextBox);
            this.Controls.Add(this.filterTextBoxX);
            this.Controls.Add(this.tableComboBoxEx);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.recordGrid);
            this.Controls.Add(this.imageFieldGrid);
            this.Controls.Add(this.openPath);
            this.Controls.Add(this.outPutButton);
            this.Controls.Add(this.outPutPathButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.queryButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "数据库二进制查看器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageFieldGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 连接ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button queryButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button outPutPathButton;
        private System.Windows.Forms.Button outPutButton;
        private System.Windows.Forms.Button openPath;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.DataGridView imageFieldGrid;
        private System.Windows.Forms.DataGridView recordGrid;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ComboBox tableComboBoxEx;
        private System.Windows.Forms.TextBox filterTextBoxX;
        private System.Windows.Forms.TextBox outPutPathTextBox;
    }
}

