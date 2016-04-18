using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DatabaseImageViewer
{
    public partial class ConnectForm : Form
    {
        public SqlConnection conn { get; set; }
        public string ip { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string database { get; set; }
        private SqlConnection tmp_conn = new SqlConnection();
        public ConnectForm()
        {
            InitializeComponent();
            readSettings();
        }

        private bool CheckValue()
        {
            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("请输入服务器IP地址！");
                return false;
            }
            if (string.IsNullOrEmpty(user))
            {
                MessageBox.Show("请输入用户名！");
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入密码！");
                return false;
            }
            if (string.IsNullOrEmpty(database))
            {
                MessageBox.Show("请选择连接数据库！");
                return false;
            }
            return true;
        }

        private void readControlValue()
        {
            ip = ipAddressInput.Text;
            user = userNameTextBoxX.Text;
            password = passWordTextBoxX.Text;
            database = databaseComboBoxEx.Text;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                readControlValue();
                saveSettings();
                if (!CheckValue())
                {
                    return;
                }
                conn.ConnectionString = string.Format("Server={0};Database={1};uid={2};pwd={3}", ip, database, user, password);
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    MessageBox.Show("连接成功！");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("连接失败！");
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("连接失败！");
            }
        }

      
        private void readSettings()
        {
            ipAddressInput.Text = DatabaseImageViewer.Properties.Settings.Default.Ip;
            userNameTextBoxX.Text = DatabaseImageViewer.Properties.Settings.Default.UserName;
            passWordTextBoxX.Text = DatabaseImageViewer.Properties.Settings.Default.Password;
            databaseComboBoxEx.Text = DatabaseImageViewer.Properties.Settings.Default.Database;
        }

        private void databaseDropDown_ValueChanged(object sender, EventArgs e)
        {
            //database = databaseComboBoxEx.Text;
            //DatabaseImageViewer.Properties.Settings.Default.Database = database;
        }

        private void databaseDropDown_Click(object sender, EventArgs e)
        {
            try
            {
                readControlValue();
                databaseComboBoxEx.Items.Clear();
                string str = string.Format("Server={0};Database=master;uid={1};pwd={2}", ip, user, password);
                tmp_conn.ConnectionString = str;
                tmp_conn.Open();
                if (tmp_conn.State == ConnectionState.Open)
                {
                    DataTable tblDatabases = tmp_conn.GetSchema("Databases");
                    foreach (DataRow row in tblDatabases.Rows)
                    {
                        databaseComboBoxEx.Items.Add(row["database_name"]);
                    }
                    tmp_conn.Close();
                }
                else
                {
                    MessageBox.Show("配置错误！");
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }

        private void saveSettings()
        {
            DatabaseImageViewer.Properties.Settings.Default.Ip = ip;
            DatabaseImageViewer.Properties.Settings.Default.UserName = user;
            DatabaseImageViewer.Properties.Settings.Default.Password = password;
            DatabaseImageViewer.Properties.Settings.Default.Database = database;
            DatabaseImageViewer.Properties.Settings.Default.Save();
        }

        private void passWordTextBoxX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
            {
                this.buttonOk_Click(sender, e);//触发button事件  
            }
        }

    }
}
