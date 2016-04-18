using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;  //连接SQLServer 数据库专用
using System.IO;
using System.Data.Common;
using System.Data.OleDb;
namespace DatabaseImageViewer
{
    public partial class Main : Form
    {
        private string ip = "";
        private string user = "";
        private string password = "";
        private string database = "";
        private SqlConnection conn = new SqlConnection();
        string current_table = "";
        private string outputpath = "";
        private string default_ext = "";
        string where_text = "";
        List<string> all_fields = new List<string>();
        List<string> primary_keys = new List<string>();
        private string file_name_field = "";        // 文件名对应字段
        private BackgroundWorker bkWorker = new BackgroundWorker();
        int percentValue = 0;

        private void InitProgressBar()
        {
            bkWorker.WorkerReportsProgress = true;
            bkWorker.WorkerSupportsCancellation = true;
            bkWorker.DoWork += new DoWorkEventHandler(DoWork);
            bkWorker.ProgressChanged += new ProgressChangedEventHandler(ProgessChanged);
            bkWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);  
        }

        private void RunProgress()
        {
            percentValue = 10;
            this.progressBar.Maximum = 1000;
            // 执行后台操作  
            bkWorker.RunWorkerAsync();
            this.progressBar.Value = percentValue;  
        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            // 事件处理，指定处理函数  
            e.Result = ProcessProgress(bkWorker, e);
        }

        public void ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            // bkWorker.ReportProgress 会调用到这里，此处可以进行自定义报告方式  
            this.progressBar.Value = e.ProgressPercentage;
            int percent = (int)(e.ProgressPercentage / percentValue);
            this.label1.Text = "处理进度:" + Convert.ToString(percent) + "%";
        }

        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            this.label1.Text = "处理完毕!";
        }

        private int ProcessProgress(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 1000; i++)
            {
                if (bkWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return -1;
                }
                else
                {
                    // 状态报告  
                    bkWorker.ReportProgress(i);

                    // 等待，用于UI刷新界面，很重要  
                    System.Threading.Thread.Sleep(1);
                }
            }

            return -1;
        }  

        public Main()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            ReadSetting();
            //ConnectLast();
            
            InitFieldGrid();
        }

        private void ReadSetting()
        {
            outputpath = DatabaseImageViewer.Properties.Settings.Default.OutPutPath;
            outPutPathTextBox.Text = outputpath;
            default_ext = DatabaseImageViewer.Properties.Settings.Default.DefaultExt;
            current_table = DatabaseImageViewer.Properties.Settings.Default.CurTable;
            tableComboBoxEx.Text = current_table;
            where_text = DatabaseImageViewer.Properties.Settings.Default.WhereText;
            filterTextBoxX.Text = where_text;
        }

        void InitFieldGridHeader()
        {
            imageFieldGrid.Columns.Add("Binary", "二进制字段");
            imageFieldGrid.Columns.Add("Ext", "文件后缀");
            imageFieldGrid.Columns[0].ReadOnly = true;
        }

        private void SaveSetting()
        {
            DatabaseImageViewer.Properties.Settings.Default.OutPutPath = outputpath;
            DatabaseImageViewer.Properties.Settings.Default.CurTable = current_table;
            DatabaseImageViewer.Properties.Settings.Default.WhereText = where_text;
            DatabaseImageViewer.Properties.Settings.Default.Save();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConnect();
            SaveSetting();
        }

        private bool ConnectLast()
        {
            ip = DatabaseImageViewer.Properties.Settings.Default.Ip;
            user = DatabaseImageViewer.Properties.Settings.Default.UserName;
            password = DatabaseImageViewer.Properties.Settings.Default.Password;
            database = DatabaseImageViewer.Properties.Settings.Default.Database;
            if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(user) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(database))
            {
                return false;
            }
            conn.ConnectionString = string.Format("Server={0};Database={1};uid={2};pwd={3}", ip, database, user, password);
            try
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("默认连接失败！");
                return false;
            }
            return true;
        }

        void SaveConnect()
        {
            DatabaseImageViewer.Properties.Settings.Default.Ip = ip;
            DatabaseImageViewer.Properties.Settings.Default.UserName = user;
            DatabaseImageViewer.Properties.Settings.Default.Password = password;
            DatabaseImageViewer.Properties.Settings.Default.Save();
        }

        private bool ConnectDatabase()
        {
            ConnectForm conn_form = new ConnectForm();
            conn_form.StartPosition = FormStartPosition.CenterScreen;
            conn_form.conn = conn;
            DialogResult result = conn_form.ShowDialog();
            if (conn.State != ConnectionState.Open)
            {
                return false;
            }
            
            ip = DatabaseImageViewer.Properties.Settings.Default.Ip;
            user = DatabaseImageViewer.Properties.Settings.Default.UserName;
            password = DatabaseImageViewer.Properties.Settings.Default.Password;
            database = DatabaseImageViewer.Properties.Settings.Default.Database;
            DataTable dt = conn.GetSchema("Tables");
            foreach (DataRow dataRow in dt.Rows)
            {
                string tableName = dataRow["TABLE_NAME"].ToString();
                tableComboBoxEx.Items.Add(tableName);
            }
            if (tableComboBoxEx.Items.Count != 0)
            {
                tableComboBoxEx.SelectedIndex = 0;
            }
            return true;
        }
        private void 连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableComboBoxEx.Items.Clear();
            ConnectDatabase();
        }

        private void InitFieldGrid()
        {
            InitFieldGridHeader();
            if (conn.State != ConnectionState.Open)
            {
                return;
            }
            current_table = tableComboBoxEx.Text;
            if (string.IsNullOrEmpty(current_table))
            {
                return;
            }
            imageFieldGrid.Rows.Clear();
            List<string> fields_list = new List<string>();
            if (GetAllImageFieldNames(ref fields_list))
            {
                foreach (string field_name in fields_list)
                {
                    string[] row = new string[] { field_name, default_ext };
                    imageFieldGrid.Rows.Add(row);
                }
            }
            GetPrimaryKeys(ref primary_keys);
        }

        private void tableSelectValue_Changed(object sender, EventArgs e)
        {
            recordGrid.DataSource = null;  
            current_table = tableComboBoxEx.Text;
            imageFieldGrid.Rows.Clear();
            List<string> fields_list = new List<string>();
            if (GetAllImageFieldNames(ref fields_list))
            {
                foreach (string field_name in fields_list)
                {
                    string[] row = new string[] { field_name, default_ext};
                    imageFieldGrid.Rows.Add(row);
                }
            }
            GetPrimaryKeys(ref primary_keys);
            GetAllFieldNames(ref all_fields);
        }

        private void tableDropDown_Click(object sender, EventArgs e)
        {
            if (conn.State != ConnectionState.Open)
            {
                tableComboBoxEx.Items.Clear();
                if (!ConnectDatabase())
                {
                    return;
                }
            }
        }

        private bool GetAllFieldNames(ref List<string> fields)
        {
            if (conn.State != ConnectionState.Open)
            {
                return false;
            }
            current_table = tableComboBoxEx.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            adapter.SelectCommand = cmd;
            adapter.SelectCommand.CommandText = string.Format("select * from  {0} where 1=2", current_table);
            DataSet ds = new DataSet();
            adapter.FillSchema(ds, System.Data.SchemaType.Mapped);
            if (ds == null)
            {
                return false;
            }
            DataTable dt = ds.Tables[0];
            foreach (DataColumn dc in dt.Columns)
            {
                fields.Add(dc.ColumnName);
            }
            return true;
        }

        private bool GetAllImageFieldNames(ref List<string> image_fields)
        {
            if (conn.State != ConnectionState.Open)
            {
                return false;
            }
            current_table = tableComboBoxEx.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            adapter.SelectCommand = cmd;
            adapter.SelectCommand.CommandText = string.Format("select * from  {0} where 1=2", current_table);
            DataSet ds = new DataSet();
            adapter.FillSchema(ds, System.Data.SchemaType.Mapped);
            if (ds == null)
            {
                return false;
            }
            DataTable dt = ds.Tables[0];
            foreach (DataColumn dc in dt.Columns)
            {
                string str = string.Format("{0}:{1}", dc.ColumnName, dc.DataType.ToString());
                if (0 == string.Compare(dc.DataType.ToString(), "System.Byte[]"))
                {
                    image_fields.Add(dc.ColumnName);
                }
                
            }
            return true;
        }

        private DataTable QuerySql(string sql)
        {
            if (conn.State != ConnectionState.Open)
            {
                return null;
            }
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        private bool GetPrimaryKeys(ref List<string> primaryKeys)
        {
            primaryKeys.Clear();
            string sql = string.Format("select * from {0}", current_table);
            string str_conn = string.Format("Provider=SQLOLEDB;server={0};database={1};uid={2};pwd={3};", ip, database, user, password);
            OleDbDataAdapter da = new OleDbDataAdapter(sql, str_conn);
            DataTable dt = new DataTable();
            da.FillSchema(dt, SchemaType.Source);
            foreach (DataColumn dc in dt.PrimaryKey)
            {
                primaryKeys.Add(dc.ColumnName);
            }
            return true;
        }


        private MemoryStream DownloadFromTable(int sel_row_index, string field_name, string id_name = "id")
        {
            //id_name = "HouseId";
            string col_name = id_name;
            
            string id_value = recordGrid.Rows[sel_row_index].Cells[col_name].Value.ToString();
            string sql = ""; ;
            sql = string.Format("select {0}, {1} from {2} where {3}='{4}'", id_name, field_name, current_table, id_name, id_value);
            DataTable result = QuerySql(sql);
            if (result == null)
            {
                return null;
                throw new Exception("数据库未获取到记录!");
            }
            byte[] buffer = new byte[1];
            DataTableReader reader = result.CreateDataReader();
            if (reader.HasRows)
            {
                reader.Read();

                if (reader.IsDBNull(1))
                {
                    return null;
                    throw new Exception("数据库文件内容为空!");
                }

                buffer = reader.GetValue(1) as byte[];
                if (buffer == null)
                {
                    throw new Exception("字段不是二进制类型!");
                }
                return new MemoryStream(buffer);
            }
            else
            {
                return null;
                throw new Exception("数据库文件内容为空!");
            }
        }


        private void queryButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(current_table))
            {
                MessageBox.Show("未选择表格！");
                return;
            }
            string sql = "";
            where_text = filterTextBoxX.Text;
            if (string.IsNullOrEmpty(where_text))
            {
                sql = string.Format("select * from {0}", current_table);
            }
            else
            {
                sql = string.Format("select * from {0} where {1}", current_table, filterTextBoxX.Text);
            }
            try
            {
                DataTable dt = QuerySql(sql);
                if (dt == null)
                {
                    return;
                }
                else
                {
                    // 二进制显示时会崩溃，这里先简单移除二进制字段，后面完善
                    List<int> banary_col_index_list = new List<int>();
                    for (int i = 0; i < dt.Columns.Count; ++i)
                    {
                        DataColumn col = dt.Columns[i];
                        string col_name = col.ColumnName;
                        if (0 == string.Compare(col.DataType.ToString(), "System.Byte[]"))
                        {
                            dt.Columns.Remove(col);
                            col = dt.Columns.Add(col_name);
                            col.SetOrdinal(i);
                            banary_col_index_list.Add(i);
                        }
                        for (int j = 0; j < primary_keys.Count; ++j)
                        {
                            string primary_key = primary_keys[j];
                            if (0 == string.Compare(primary_key, col_name))
                            {
                                //col_name += " *";
                                col.ColumnName = col_name;
                            }
                        }
                    }
                    recordGrid.DataSource = dt.DefaultView;
                    recordGrid.AutoGenerateColumns = true;

                    for (int i = 0; i < banary_col_index_list.Count; ++i)
                    {
                        int index = banary_col_index_list[i];
                        for (int j = 0; j < recordGrid.RowCount - 1; ++j)
                        {
                            recordGrid.Rows[j].Cells[index].Value = "<二进制>";
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void filterTextBoxX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
            {
                this.queryButton_Click(sender, e);//触发button事件  
            }
        }

        private void outPutPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dilog = new FolderBrowserDialog();
            dilog.Description = "请选择文件夹";
            if (dilog.ShowDialog() == DialogResult.OK || dilog.ShowDialog() == DialogResult.Yes)
            {
                outputpath = dilog.SelectedPath;
                outPutPathTextBox.Text = outputpath;
            }
        }

        private void openPath_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(outputpath))
            {
                System.Diagnostics.Process.Start(outputpath);
            }
        }

        private void CopyStream(Stream source, Stream dest)
        {
            const int bufferSize = 524288;
            byte[] buffer = new byte[bufferSize];
            int count = 0;
            while ((count = source.Read(buffer, 0, bufferSize)) > 0)
            {
                dest.Write(buffer, 0, count);
            }
        }


        private bool SaveStreamToFile(Stream stream, string file_path)
        {
            FileStream fileStream = null;
            try
            {
                if (stream == null)
                {
                    return false;
                }
                fileStream = new FileStream(file_path, FileMode.Create, FileAccess.Write, FileShare.None);
                CopyStream(stream, fileStream);
                fileStream.Close();
                fileStream = null;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        private bool GetPrimaryKeyValue(DataGridViewRow row, ref string value)
        {
            if (primary_keys.Count == 0)
            {
                return false;
            }
            foreach (string primary_key in primary_keys)
            {
                string tmp_value = "";
                string primary_key_col_name = primary_key ;
                object cell_object_value = row.Cells[primary_key_col_name].Value;
                if (cell_object_value == null)
                {
                    return false;
                }
                tmp_value = cell_object_value.ToString();
                value += tmp_value;
            }
            return true;
        }

        private bool GetCellValue(DataGridViewRow row, string col_name, ref string value)
        {
            if (string.IsNullOrEmpty(col_name))
            {
                return false;
            }
            string tmp_value = "";
            object cell_object_value = row.Cells[col_name].Value;
            if (cell_object_value == null)
            {
                return false;
            }
            tmp_value = cell_object_value.ToString();
            value = tmp_value;
            return true;
        }

        private void outPutButton_Click(object sender, EventArgs e)
        {
            if (imageFieldGrid.RowCount == 1)   
            {
                MessageBox.Show("表格内没有二进制字段！");
                return;
            }
            progressBar.Value = 0;
            Stream stream = null;
            bool has_data = false;
            progressBar.Maximum = recordGrid.SelectedRows.Count;
            // 执行后台操作  
            bkWorker.RunWorkerAsync();
            int cur_row = 1;
            string table_path = Path.Combine(outputpath, current_table);
            if (!Directory.Exists(table_path))
            {
                Directory.CreateDirectory(table_path);
            }
            if (primary_keys.Count == 0)
            {
                SetPrimaryKeyField keyFieldForm = new SetPrimaryKeyField(all_fields);
                keyFieldForm.StartPosition = FormStartPosition.CenterScreen;
                DialogResult result = keyFieldForm.ShowDialog();
                file_name_field = keyFieldForm.primaryKeyField;
            }
            else
            {
                file_name_field = primary_keys[0];
            }
          
            foreach (DataGridViewRow record_row in this.recordGrid.SelectedRows)
            {                
                string file_name_value = "";
                // 默认取关键字为文件名
                GetPrimaryKeyValue(record_row, ref file_name_value);
                // 没有关键字取选择的字段名
                if (string.IsNullOrEmpty(file_name_value))
                {
                    GetCellValue(record_row, file_name_field, ref file_name_value);
                }
                // 还没有就取GUID吧
                if (string.IsNullOrEmpty(file_name_value))
                {
                    file_name_value = Guid.NewGuid().ToString();
                }
                foreach (DataGridViewRow field_row in this.imageFieldGrid.Rows)
                {
                    object field_object = field_row.Cells[0].Value;
                    object ext_object = field_row.Cells[1].Value;
                    if (field_object == null)
                    {
                        continue;
                    }
                    string banary_field_name = field_object.ToString();
                    string ext = "";
                    if (ext_object == null)
                    {
                        ext = "";
                    }
                    else
                    {
                        ext = ext_object.ToString();
                    }
                    
                    stream = DownloadFromTable(record_row.Index, banary_field_name, file_name_field);
                    string file_name = string.Format("{0}_{1}.{2}", file_name_value, banary_field_name, ext);
                    string full_path = Path.Combine(table_path, file_name);
                    SaveStreamToFile(stream, full_path);
                    has_data = true;
                }
                progressBar.Value = cur_row;
                ++cur_row;
            }
            if (has_data)
            {
                MessageBox.Show("输出完成！");
                System.Diagnostics.Process.Start(table_path);
            }
            else
            {
                MessageBox.Show("没有输出，请选择输出记录！");
            }
            
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting setting_form = new Setting();
            setting_form.StartPosition = FormStartPosition.CenterScreen;
            DialogResult result = setting_form.ShowDialog();
            default_ext = DatabaseImageViewer.Properties.Settings.Default.DefaultExt;
        }
    }
}
