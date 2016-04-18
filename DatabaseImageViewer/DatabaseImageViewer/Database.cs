using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DatabaseImageViewer
{
    /// <summary>
    /// 
    /// </summary>
    public enum DatabaseType
    {
        Unknown = 0,
        SqlServer = 1,
        Oracle = 2,
        Access = 3
    }
    /// <summary>
    /// 
    /// </summary>
    public class Database
    {
        /// <summary>
        /// 
        /// </summary>
        private string connectionString = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private string providerName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private DbProviderFactory providerFactory = null;

        /// <summary>
        /// 
        /// </summary>
        private DbConnection connection = null;

        /// <summary>
        /// 
        /// </summary>
        private DbTransaction transaction = null;

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; private set; }

        public DatabaseType DbType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Database()
        {

        }

        private static string GetProviderName(DatabaseType dbType)
        {
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    return "System.Data.SqlClient";
                case DatabaseType.Oracle:
                    return "Oracle.DataAccess.Client";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connstr"></param>
        /// <returns></returns>
        public bool Connect(DatabaseType dbType, string connstr)
        {
            this.DbType = dbType;

            connectionString = connstr;

            providerName = GetProviderName(dbType);

            if (string.IsNullOrEmpty(providerName))
            {
                ErrorMessage = "未知的数据库类型！";
                return false;
            }

            try
            {
                providerFactory = null;

                connection = null;

                transaction = null;

                providerFactory = DbProviderFactories.GetFactory(providerName);
                if (providerFactory == null)
                {
                    ErrorMessage = "系统没有安装数据提供程序！";
                    return false;
                }

                connection = providerFactory.CreateConnection();
                if (connection == null)
                {
                    ErrorMessage = "创建数据库连接失败！";
                    return false;
                }

                connection.ConnectionString = connectionString;

                connection.Open();

                return true;
            }
            catch (ArgumentException e)
            {
                ErrorMessage = e.InnerException == null ? e.Message : e.InnerException.Message;
                DisConnect();
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void DisConnect()
        {
            try
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }
            }
            catch (Exception)
            {
            }

            try
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception)
            {
            }

            providerFactory = null;

            connection = null;

            transaction = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            try
            {
                if (connection == null)
                {
                    ErrorMessage = "数据库未连接！";
                    return false;
                }

                if (connection.State == ConnectionState.Closed)
                {
                    ErrorMessage = "数据库未连接！";
                    DisConnect();
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                ErrorMessage = "数据库未连接！";
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool BeginTransaction()
        {
            if (!IsConnected())
            {
                return false;
            }

            try
            {
                if (transaction != null)
                {
                    ErrorMessage = "前一个数据库事务还没有结束！";
                    return false;
                }

                transaction = connection.BeginTransaction();

                return transaction != null;
            }
            catch (Exception e)
            {
                ErrorMessage = e.InnerException == null ? e.Message : e.InnerException.Message;
                transaction = null;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CommitTransaction()
        {
            if (!IsConnected())
            {
                return false;
            }

            try
            {
                if (transaction != null)
                {
                    transaction.Commit();

                    return true;
                }

                ErrorMessage = "没有启动数据库事务！";

                return false;
            }
            catch (Exception e)
            {
                ErrorMessage = e.InnerException == null ? e.Message : e.InnerException.Message;
                RollbackTransaction();
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RollbackTransaction()
        {
            if (!IsConnected())
            {
                return;
            }

            try
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                    transaction = null;
                }
            }
            catch (Exception)
            {
                transaction = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ExecuteSql(string sql)
        {
            return ExecuteSQL(sql, null, CommandType.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public bool ExecuteSQL(string sql, IList<DbParameter> parameters, CommandType commandType)
        {
            if (!IsConnected())
            {
                return false;
            }

            try
            {
                DbCommand command = CreateDbCommand(sql, parameters, commandType);
                command.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.InnerException == null ? e.Message : e.InnerException.Message;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, IList<DbParameter> parameters, CommandType commandType)
        {
            if (!IsConnected())
            {
                return null;
            }

            try
            {
                DbCommand command = CreateDbCommand(sql, parameters, commandType);

                DbDataAdapter adapter = providerFactory.CreateDataAdapter();

                adapter.SelectCommand = command;

                DataTable data = new DataTable("t");

                adapter.Fill(data);

                return data;
            }
            catch (Exception e)
            {
                ErrorMessage = e.InnerException == null ? e.Message : e.InnerException.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public DbCommand CreateDbCommand(string sql, IList<DbParameter> parameters, CommandType commandType)
        {
            DbCommand command = providerFactory.CreateCommand();
            command.CommandText = sql;
            command.CommandType = commandType;
            command.Connection = connection;
            if (this.transaction != null)
            {
                command.Transaction = this.transaction;
            }
            if (parameters != null)
            {
                foreach (DbParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DbParameter CreateDbParameter()
        {
            return providerFactory.CreateParameter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DbDataAdapter CreateDataAdapter()
        {
            return providerFactory.CreateDataAdapter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DbCommandBuilder CreateCommandBuilder()
        {
            return providerFactory.CreateCommandBuilder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTableReader OpenReader(string sql)
        {
            DataTable result = ExecuteDataTable(sql, null, CommandType.Text);

            return result.CreateDataReader();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTableReader OpenSingleReader(string sql)
        {
            DataTableReader reader = OpenReader(sql);
            if (reader == null)
            {
                throw new Exception("查询出错！");
            }

            if (!reader.HasRows)
            {
                throw new Exception("没有记录！");
            }

            reader.Read();

            return reader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string QueryString(string sql)
        {
            try
            {
                DataTableReader reader = OpenSingleReader(sql);
                return reader.GetString(0);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<string> QueryStrings(string sql)
        {
            try
            {
                DataTableReader reader = OpenReader(sql);
                List<string> result = new List<string>();
                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public double QueryDouble(string sql)
        {
            try
            {
                DataTableReader reader = OpenSingleReader(sql);
                object obj = reader.GetValue(0);
                return Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int QueryInt(string sql)
        {
            try
            {
                DataTableReader reader = OpenSingleReader(sql);
                object obj = reader.GetValue(0);
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}