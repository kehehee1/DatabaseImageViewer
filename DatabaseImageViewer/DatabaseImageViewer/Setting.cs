using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DatabaseImageViewer
{
    public partial class Setting : Form
    {
        public string default_ext { get; set; }
        public Setting()
        {
            InitializeComponent();
            readSettings();
        }

        private void readSettings()
        {
            defaultExtTextBox.Text = DatabaseImageViewer.Properties.Settings.Default.DefaultExt;
        }

        private void saveSettings()
        {
            default_ext = defaultExtTextBox.Text;
            DatabaseImageViewer.Properties.Settings.Default.DefaultExt = default_ext;
            DatabaseImageViewer.Properties.Settings.Default.Save();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            saveSettings();
            this.Close();
        }

        private void defaultExtTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
            {
                this.buttonOK_Click(sender, e);//触发button事件  
            }
        }
    }
}
