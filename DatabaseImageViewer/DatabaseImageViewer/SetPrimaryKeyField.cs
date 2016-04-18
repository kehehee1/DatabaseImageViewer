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
    public partial class SetPrimaryKeyField : Form
    {
        private List<string> m_fields;
        public string primaryKeyField { get; set; }

        public SetPrimaryKeyField(List<string> fields)
        {
            InitializeComponent();
            m_fields = fields;
            InitFieldCombobox();
        }

        void InitFieldCombobox()
        {
            fieldComboBox.Items.Clear();
            foreach (string field in m_fields)
            {
                fieldComboBox.Items.Add(field);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string text = fieldComboBox.Text;
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("请指定字段名称！");
            }
            else
            {
                primaryKeyField = text;
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fieldComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
            {
                this.buttonOK_Click(sender, e);//触发button事件  
            }
        }

       

    }
}
