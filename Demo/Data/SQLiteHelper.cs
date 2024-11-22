using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using System.Data;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace DataBaseManager
{


    public class SQLiteHelper : IDisposable, DataBaseHelper
    {
        private bool _autoCommit;
        public SQLiteConnection _connection;
        private string _connectionString;
        private bool _disposed;
        private bool _transacted;
        private SQLiteTransaction _transaction;
        private object lockSQLite;
        public SQLiteDataAdapter sqlAdapter;
        public bool AutoCommit
        {
            get { return _autoCommit; }
            set { _autoCommit = value; }
        }
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        public SQLiteHelper(string connectionString)
        {

            if (connectionString == null)
            {
                throw new ArgumentException("数据库路径为空,connectionString==null");
            }
            if (!File.Exists(connectionString))
            {
                SQLiteConnection.CreateFile(connectionString);
                Thread.Sleep(500);
            }

            _autoCommit = false;
            _connection = null;
            _connectionString = string.Empty;
            _disposed = false;
            _transacted = false;
            _transaction = null;
            lockSQLite = RuntimeHelpers.GetObjectValue(new object());
            _connectionString = new SQLiteConnectionStringBuilder
            {
                DataSource = connectionString
            }.ToString();
            _connection = new SQLiteConnection(_connectionString);
            _connection.Commit += Transaction_Commit;
            _connection.RollBack += Transaction_RollBack;
        }

        public SQLiteHelper(string connectionString, string Passward)
        {

            if (connectionString == null)
            {
                throw new ArgumentException("The database path is empty,connectionString==null");
            }
            if (!File.Exists(connectionString))
            {
                SQLiteConnection.CreateFile(connectionString);
                Thread.Sleep(500);
                using (SQLiteConnection cnn = new SQLiteConnection("Data Source=" + connectionString))
                {
                    cnn.Open();
                    cnn.ChangePassword(Passward);
                    cnn.Close();
                }

            }

            _autoCommit = false;
            _connection = null;
            _connectionString = string.Empty;
            _disposed = false;
            _transacted = false;
            _transaction = null;
            lockSQLite = RuntimeHelpers.GetObjectValue(new object());
            _connectionString = new SQLiteConnectionStringBuilder
            {
                DataSource = connectionString,
                Password = Passward
            }.ToString();

            _connection = new SQLiteConnection(_connectionString);
            _connection.Commit += Transaction_Commit;
            _connection.RollBack += Transaction_RollBack;
        }
        public void BeginTransaction()
        {
            _connection.BeginTransaction();
            _transacted = true;
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _connection.BeginTransaction(isolationLevel);
            _transacted = true;
        }

        public void Close()
        {
            if (_connection.State > ConnectionState.Closed)
            {
                if (_transacted && _autoCommit)
                {
                    Commit();
                }
                _connection.Close();
            }
        }

        public void Commit()
        {
            if (_transacted)
            {
                _transaction.Commit();
                _transacted = false;
            }
        }

        public virtual void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }
                _disposed = true;
            }
        }

        public int ExecuteNonQuery(string commandText)
        {
            int result = 0;
            object objectValue = RuntimeHelpers.GetObjectValue(lockSQLite);
            object obj = objectValue;
            lock (obj)
            {
                bool lockTaken = false;
                using (SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, _connection))
                {
                    try
                    {
                        Monitor.Enter(obj, ref lockTaken);
                        Open();
                        SQLiteTransaction sQLiteTransaction = _connection.BeginTransaction();
                        result = sQLiteCommand.ExecuteNonQuery();
                        sQLiteTransaction.Commit();
                        Close();
                    }
                    finally
                    {
                        Close();
                        if (lockTaken)
                        {
                            Monitor.Exit(obj);
                        }
                    }
                }

            }
            return result;
        }

        public int ExecuteNonQuery(string commandText, SQLiteParameter[] parmeters)
        {
            int result = 0;
            object objectValue = RuntimeHelpers.GetObjectValue(lockSQLite);
            object obj = objectValue;
            lock (obj)
            {
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj, ref lockTaken);
                    Open();
                    using (SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, _connection))
                    {
                        sQLiteCommand.Parameters.AddRange(parmeters);
                        result = sQLiteCommand.ExecuteNonQuery();
                    }
                    Close();
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(obj);
                    }
                }
            }
            return result;
        }

        public object ExecuteScalar(string commandText)
        {
            object obj = null;
            object objectValue = RuntimeHelpers.GetObjectValue(lockSQLite);
            object obj2 = objectValue;
            lock (obj2)
            {
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj2, ref lockTaken);
                    Open();
                    using (SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, _connection))
                    {
                        obj = RuntimeHelpers.GetObjectValue(sQLiteCommand.ExecuteScalar());
                    }
                    Close();
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(obj2);
                    }
                }
            }
            if (obj == null)
            {
                return "";
            }
            return obj;
        }

        public object ExecuteScalar(string commandText, SQLiteParameter[] parmeters)
        {
            object result = null;
            object objectValue = RuntimeHelpers.GetObjectValue(lockSQLite);
            object obj = objectValue;
            lock (obj)
            {
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj, ref lockTaken);
                    Open();
                    using (SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, _connection))
                    {
                        sQLiteCommand.Parameters.AddRange(parmeters);
                        result = RuntimeHelpers.GetObjectValue(sQLiteCommand.ExecuteScalar());
                    }
                    Close();
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(obj);
                    }
                }
            }
            return result;
        }

        public SQLiteDataAdapter GetAdapter(string commandText)
        {
            return new SQLiteDataAdapter(commandText, _connection);
        }

        public void Open()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        private bool QueryTran(List<string> queryList)
        {
            SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=DataBase;Version=3");
            SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteConnection.Open();
            SQLiteTransaction sQLiteTransaction = sQLiteConnection.BeginTransaction();
            bool result = false;
            try
            {
                using (List<string>.Enumerator enumerator = queryList.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        string text = (sQLiteCommand.CommandText = enumerator.Current);
                        sQLiteCommand.ExecuteNonQuery();
                    }
                }
                sQLiteTransaction.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                //ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                sQLiteTransaction.Rollback();
                result = false;
                throw ex2;
            }
            finally
            {
                sQLiteConnection.Close();
            }
            return result;
        }

        public void Rollback()
        {
            if (_transacted)
            {
                _transaction.Rollback();
                _transacted = false;
            }
        }

        public SQLiteCommand updParmeter(SQLiteCommand upd, string[] items, string[] type)
        {
            int num = 0;
            foreach (string text in type)
            {
                switch (text.ToLower())
                {
                    case "int":
                        upd.Parameters.Add($"@{items[num]}", DbType.Int16, 4, items[num]);
                        break;
                    case "double":
                        upd.Parameters.Add($"@{items[num]}", DbType.Double, 8, items[num]);
                        break;
                    case "string":
                        upd.Parameters.Add($"@{items[num]}", DbType.String, 16, items[num]);
                        break;
                    case "bool":
                        upd.Parameters.Add($"@{items[num]}", DbType.Boolean, 2, items[num]);
                        break;
                    case "boolean":
                        upd.Parameters.Add($"@{items[num]}", DbType.Boolean, 2, items[num]);
                        break;
                }
                num = checked(num + 1);
            }
            return upd;
        }

        private void IDisposable_Dispose()
        {

            ((IDisposable)sqlAdapter).Dispose();
        }

        void IDisposable.Dispose()
        {
            //ILSpy generated this explicit interface implementation from .override directive in IDisposable_Dispose
            this.IDisposable_Dispose();
        }

        private void Transaction_Commit(object sender, CommitEventArgs e)
        {
            _transacted = false;
        }

        private void Transaction_RollBack(object sender, EventArgs e)
        {
            _transacted = false;
        }

        public int Update(DataSet dataSet, string sqlCommand)
        {
            return Update(dataSet, string.Empty, sqlCommand);
        }

        public int Update(DataSet dataSet, string tableName, string sqlCommand)
        {
            int result = 0;
            object objectValue = RuntimeHelpers.GetObjectValue(lockSQLite);
            object obj = objectValue;
            lock (obj)
            {
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj, ref lockTaken);
                    Open();
                    using (SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(sqlCommand, _connection))
                    {
                        using (SQLiteCommandBuilder sQLiteCommandBuilder = new SQLiteCommandBuilder(sQLiteDataAdapter))
                        {
                            sQLiteDataAdapter.UpdateCommand = sQLiteCommandBuilder.GetUpdateCommand();
                            result = ((!string.Empty.Equals(tableName)) ? sQLiteDataAdapter.Update(dataSet, tableName) : sQLiteDataAdapter.Update(dataSet));
                            dataSet.AcceptChanges();
                        }
                    }
                    Close();
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(obj);
                    }
                }
            }
            return result;
        }

        public DataSet GetDataSet(string commandText, string tableName)
        {
            DataSet dataSet = new DataSet();
            object objectValue = RuntimeHelpers.GetObjectValue(lockSQLite);
            object obj = objectValue;
            lock (obj)
            {
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj, ref lockTaken);

                    if (_connection.State != ConnectionState.Open)
                    {
                        _connection.Open();
                    }
                    //Console.WriteLine(_connection.ConnectionString);
                    //Console.WriteLine(_connection.ToString());
                    //Open();
                   // commandText = "SELECT * from machinedata.unitall";
                    using (SQLiteCommand cmd = new SQLiteCommand(commandText, _connection))
                    {
                        using (SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(cmd))
                        {
                            if (string.Empty.Equals(tableName))
                            {
                                sQLiteDataAdapter.Fill(dataSet);
                            }
                            else
                            {
                                sQLiteDataAdapter.Fill(dataSet, tableName);
                            }
                        }
                    }
                    Close();
                }

                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(obj);
                    }
                }
            }
            return dataSet;
        }

        public DataSet GetDataSet(string commandText, out SQLiteCommand sqlCommand)
        {
            return GetDataSet(commandText, string.Empty, out sqlCommand);
        }

        public DataSet GetDataSet(string commandText, DataSet dataSet, string tableName)
        {
            object objectValue = RuntimeHelpers.GetObjectValue(lockSQLite);
            object obj = objectValue;
            lock (obj)
            {
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj, ref lockTaken);
                    Open();
                    sqlAdapter = new SQLiteDataAdapter(commandText, _connection);
                    if (string.Empty.Equals(tableName))
                    {
                        sqlAdapter.Fill(dataSet);
                    }
                    else
                    {
                        sqlAdapter.Fill(dataSet, tableName);
                    }
                    Close();
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(obj);
                    }
                }
            }
            return dataSet;
        }


        public DataSet GetDataSet(string commandText, string tableName, out SQLiteCommand sqlCommand)
        {
            DataSet dataSet = new DataSet();
            object objectValue = RuntimeHelpers.GetObjectValue(lockSQLite);
            object obj = objectValue;
            lock (obj)
            {
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj, ref lockTaken);
                    Open();
                    SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, _connection);
                    using (SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(sQLiteCommand))
                    {
                        sQLiteDataAdapter.Fill(dataSet);
                    }
                    sqlCommand = sQLiteCommand;
                    Close();
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(obj);
                    }
                }
            }
            return dataSet;
        }

        public DataSet GetDataSet(string commandText)
        {
            return GetDataSet(commandText, string.Empty);
        }

        /// <summary>
        /// 创建表名  一一对应
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名</param>
        /// <param name="colTypes">列类型</param>
        /// <returns></returns>
        public int CreateTable(string tableName, string[] colNames, string[] colTypes)
        {
            object obj = lockSQLite;
            //lock(obj)
            //{
            string text;
            bool lockTaken = false;
            checked
            {
                try
                {
                    Monitor.Enter(obj, ref lockTaken);
                    Open();
                    text = "CREATE TABLE IF NOT EXISTS " + tableName + "( " + colNames[0] + " " + colTypes[0];
                    int num = colNames.Length - 1;
                    for (int i = 1; i <= num; i++)
                    {
                        text = text + ", " + colNames[i] + " " + colTypes[i];
                    }
                    text += "  ); ";
                    return ExecuteNonQuery(text);
                    //Close();

                }
                finally
                {
                    Close();
                    if (lockTaken)
                    {
                        Monitor.Exit(obj);
                    }
                }

            }

            //}
        }

        public int CreateStandardTable(string tableName, string HappenTime, string EndTime, string[] colNames, string[] colLength)
        {
            object obj = lockSQLite;
            //lock(obj)
            //{
            string text;
            bool lockTaken = false;
            checked
            {
                try
                {
                    Monitor.Enter(obj, ref lockTaken);
                    Open();
                    text = "CREATE TABLE IF NOT EXISTS " + tableName + " ( Serial_ID INTEGER PRIMARY KEY AUTOINCREMENT ";//"( " + colNames[0] + " " + colTypes[0];
                    text += "," + HappenTime + " DATETIME ," + EndTime + " DATETIME ";

                    int num = colNames.Length - 1;
                    for (int i = 0; i <= num; i++)
                    {
                        text = text + ", " + colNames[i] + " " + "Char[" + colLength[i] + "]";
                    }
                    text += "  ); ";
                    return ExecuteNonQuery(text);
                    //Close();

                }
                finally
                {
                    Close();
                    if (lockTaken)
                    {
                        Monitor.Exit(obj);
                    }
                }

            }
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public int DeleteTable(string tableName)
        {
            object obj = lockSQLite;
            lock (obj)
            {
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj, ref lockTaken);
                    Open();
                    string commandText = "DROP TABLE IF EXISTS " + tableName;
                    return ExecuteNonQuery(commandText);
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(obj);
                    }
                }
            }
        }



        public static void CreateNewDatabase(string DatabaseFileName)
        {
            SQLiteConnection.CreateFile(DatabaseFileName);
        }

        public int DeleteValues(string tableName, string WhereClause)
        {
            string commandText = ((WhereClause.Length <= 0) ? ("DELETE FROM " + tableName) : ("DELETE FROM " + tableName + " WHERE " + WhereClause));
            return ExecuteNonQuery(commandText);
        }

        public int DeleteValues(string tableName, string WhichCol, string operateStr, string condition)
        {
            string commandText = "DELETE FROM " + tableName + " WHERE " + WhichCol + " " + operateStr + " '" + condition + "'";
            return ExecuteNonQuery(commandText);
        }

        public int InsertValues(string tableName, string[] colNames, string[] colValues)
        {
            //if(!TableExists(tableName))
            //{
            //    string[] colType = new string[colNames.Length];
            //    CreateTable(tableName, colNames, colType);
            //    Thread.Sleep(500);
            //}

            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException(tableName + " InsertValues colNames.Length!=colValues.Length");
            }
            string[] values = new string[5]
            {
            "INSERT INTO ",
            tableName,
            " ('",
            colNames[0],
            "'"
            };
            string str = string.Concat(values);
            checked
            {
                int num = colNames.Length - 1;
                for (int i = 1; i <= num; i++)
                {
                    str = str + ", '" + colNames[i] + "'";
                }
                str = str + ") VALUES ('" + colValues[0] + "'";
                int num2 = colValues.Length - 1;
                for (int j = 1; j <= num2; j++)
                {
                    str = str + ", '" + colValues[j] + "'";
                }
                str += " )";
                return ExecuteNonQuery(str);
            }
        }

        public int InsertValues(string tableName, string[] colValues)
        {
            //if(!TableExists(tableName))
            //{
            //    string[] colType = new string[colNames.Length];
            //    CreateTable(tableName, colNames, colType);
            //    Thread.Sleep(500);
            //}         
            string str = "INSERT INTO " + tableName;

            checked
            {

                str = str + " VALUES ('" + colValues[0] + "'";
                int num2 = colValues.Length - 1;
                for (int j = 1; j <= num2; j++)
                {
                    str = str + ", '" + colValues[j] + "'";
                }
                str += " )";
                return ExecuteNonQuery(str);
            }
        }


        public int InsertStandardValues(string tableName, string[] colNames, string[] colValues)
        {
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException(tableName + " InsertValues colNames.Length!=colValues.Length");
            }
            string[] values = new string[5]
            {
            "INSERT INTO ",
            tableName,
            " ('",
            "Serial_ID",
            "'"
            };
            string str = string.Concat(values);
            checked
            {
                int num = colNames.Length - 1;
                for (int i = 0; i <= num; i++)
                {
                    str = str + ", '" + colNames[i] + "'";
                }
                str = str + ") VALUES (" + "NULL,'" + colValues[0] + "'";
                int num2 = colValues.Length - 1;
                for (int j = 1; j <= num2; j++)
                {
                    str = str + ", '" + colValues[j] + "'";
                }
                str += " )";
                return ExecuteNonQuery(str);
            }
        }
        public int InsertStandardValues(string tableName, string[] colValues)
        {
            string str = "INSERT INTO " + tableName;

            checked
            {

                str = str + " VALUES (" + "NULL,'" + colValues[0] + "'";
                int num2 = colValues.Length - 1;
                for (int j = 1; j <= num2; j++)
                {
                    str = str + ", '" + colValues[j] + "'";
                }
                str += " )";
                return ExecuteNonQuery(str);
            }
        }

        public int InsertStandardValues(string tableName, string HappenTime, string EndTime, string[] colValues)
        {
            string str = "INSERT INTO " + tableName;

            checked
            {

                str = str + " VALUES (" + "NULL,'" + HappenTime + "','" + EndTime + "'";
                int num2 = colValues.Length - 1;
                for (int j = 0; j <= num2; j++)
                {
                    str = str + ", '" + colValues[j] + "'";
                }
                str += " )";
                return ExecuteNonQuery(str);
            }
        }

        public bool TableExists(string TableName)
        {
            string Count = "0";
            if (TableName == null)
            {
                throw new ArgumentException("TableName=null");
            }
            string existSql = String.Format("select count(*)  from sqlite_master where type='table' and name = '{0}'", TableName);

            object objectValue = RuntimeHelpers.GetObjectValue(lockSQLite);
            object obj = objectValue;
            lock (obj)
            {
                bool lockTaken = false;
                using (SQLiteCommand sQLiteCommand = new SQLiteCommand(existSql, _connection))
                {
                    try
                    {
                        Monitor.Enter(obj, ref lockTaken);
                        //Monitor.Enter(obj, ref lockTaken);
                        Open();
                        SQLiteTransaction sQLiteTransaction = _connection.BeginTransaction();
                        using (SQLiteDataReader reader = sQLiteCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Count = reader[0].ToString();
                            }
                        }
                        sQLiteTransaction.Commit();
                        Close();
                    }
                    finally
                    {
                        Close();
                        if (lockTaken)
                        {
                            Monitor.Exit(obj);
                        }
                    }
                }
            }
            if (Count == "0")
            {
                return false;
            }
            else
                return true;
        }


        public int UpdateValues(string tableName, string[] colNames, string[] colValues, string WhereClause)
        {
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException(tableName + " UpdateValues colNames.Length!=colValues.Length");
            }
            string[] values = new string[7]
            {
            "UPDATE ",
            tableName,
            " SET ",
            colNames[0],
            "='",
            colValues[0],
            "'"
            };
            string text = string.Concat(values);
            checked
            {
                int num = colValues.Length - 1;
                for (int i = 1; i <= num; i++)
                {
                    string[] values2 = new string[6]
                    {
                    text,
                    ", ",
                    colNames[i],
                    "='",
                    colValues[i],
                    "'"
                    };
                    text = string.Concat(values2);
                }
                if (WhereClause.Length > 0)
                {
                    text = text + " WHERE " + WhereClause;
                }
                return ExecuteNonQuery(text);
            }
        }

        public int UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string value, string operation = "=")
        {
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException(tableName + " UpdateValues colNames.Length!=colValues.Length");
            }
            string[] values = new string[7]
            {
            "UPDATE ",
            tableName,
            " SET ",
            colNames[0],
            "='",
            colValues[0],
            "'"
            };
            string text = string.Concat(values);
            checked
            {
                int num = colValues.Length - 1;
                for (int i = 1; i <= num; i++)
                {
                    string[] values2 = new string[6]
                    {
                    text,
                    ", ",
                    colNames[i],
                    "='",
                    colValues[i],
                    "'"
                    };
                    text = string.Concat(values2);
                }
                string[] values3 = new string[7]
                {
                text,
                " WHERE ",
                key,
                operation,
                "'",
                value,
                "'"
                };
                text = string.Concat(values3);
                return ExecuteNonQuery(text);
            }
        }



        public DataSet ExecuteQuery(string commandText)
        {
            return GetDataSet(commandText);
        }
        public DataSet SelectValues(string tableName, string WhereClause)
        {
            string commandText = ((WhereClause.Length <= 0) ? ("DELETE FROM " + tableName) : ("SELECT * FROM " + tableName + " WHERE " + WhereClause));
            return GetDataSet(commandText);
        }
        public DataSet SelectValues(string Command)
        {
            return GetDataSet(Command);
        }

        public DataSet SelectValues(string tableName, string key, string value, string operation = "=")
        {
            string commandText = ((key.Length <= 0) ? ("DELETE FROM " + tableName) : ("SELECT * FROM " + tableName + " WHERE " + key + operation + "'" + value + "'"));
            return GetDataSet(commandText);
        }

        public string ClauseAND(string[] colNames, string[] colValues, string[] operations)
        {
            string[] values = new string[5]
            {
            colNames[0],
            operations[0],
            "'",
            colValues[0],
            "'"
            };
            string text = string.Concat(values);
            checked
            {
                int num = colValues.Length - 1;
                for (int i = 1; i <= num; i++)
                {
                    string[] values2 = new string[7]
                    {
                    text,
                    " AND ",
                    colNames[i],
                    operations[i],
                    "'",
                    colValues[i],
                    "'"
                    };
                    text = string.Concat(values2);
                }
                return text;
            }
        }

        public string ClauseOR(string[] colNames, string[] colValues, string[] operations)
        {
            string[] values = new string[5]
            {
            colNames[0],
            operations[0],
            "'",
            colValues[0],
            "'"
            };
            string text = string.Concat(values);
            checked
            {
                int num = colValues.Length - 1;
                for (int i = 1; i <= num; i++)
                {
                    string[] values2 = new string[7]
                    {
                    text,
                    " OR ",
                    colNames[i],
                    operations[i],
                    "'",
                    colValues[i],
                    "'"
                    };
                    text = string.Concat(values2);
                }
                return text;
            }
        }

        public string ClauseBetween(string ColName, string LowValue, string HighValue)
        {
            string[] values = new string[7]
            {
            ColName,
            " BETWEEN ",
            "'",
            LowValue,
            "' AND '",
            HighValue,
            "'"
            };
            return string.Concat(values);
        }

        public string ClauseNotBetween(string ColName, string LowValue, string HighValue)
        {
            string[] values = new string[7]
            {
            ColName,
            " BETWEEN ",
            "'",
            LowValue,
            "' AND '",
            HighValue,
            "'"
            };
            return string.Concat(values);
        }

        public DataSet SelectValuesAND(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException(tableName + " Select ValuesAND colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }
            string[] values = new string[8]
            {
            "SELECT FROM ",
            tableName,
            " WHERE ",
            colNames[0],
            operations[0],
            "'",
            colValues[0],
            "'"
            };
            string text = string.Concat(values);
            checked
            {
                int num = colValues.Length - 1;
                for (int i = 1; i <= num; i++)
                {
                    string[] values2 = new string[7]
                    {
                    text,
                    " AND ",
                    colNames[i],
                    operations[i],
                    "'",
                    colValues[i],
                    "'"
                    };
                    text = string.Concat(values2);
                }
                return GetDataSet(text);
            }
        }

        public DataSet SelectValuesOR(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException(tableName + " Select ValuesOR colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }
            string[] values = new string[8]
            {
            "SELECT FROM ",
            tableName,
            " WHERE ",
            colNames[0],
            operations[0],
            "'",
            colValues[0],
            "'"
            };
            string text = string.Concat(values);
            checked
            {
                int num = colValues.Length - 1;
                for (int i = 1; i <= num; i++)
                {
                    string[] values2 = new string[7]
                    {
                    text,
                    "OR ",
                    colNames[i],
                    operations[0],
                    "'",
                    colValues[i],
                    "'"
                    };
                    text = string.Concat(values2);
                }
                return GetDataSet(text);
            }
        }

        public int DeleteValuesAND(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException(tableName + " DeleteValuesAND colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }
            string[] values = new string[8]
            {
            "DELETE FROM ",
            tableName,
            " WHERE ",
            colNames[0],
            operations[0],
            "'",
            colValues[0],
            "'"
            };
            string text = string.Concat(values);
            checked
            {
                int num = colValues.Length - 1;
                for (int i = 1; i <= num; i++)
                {
                    string[] values2 = new string[7]
                    {
                    text,
                    " AND ",
                    colNames[i],
                    operations[i],
                    "'",
                    colValues[i],
                    "'"
                    };
                    text = string.Concat(values2);
                }
                return ExecuteNonQuery(text);
            }
        }

        public int DeleteValuesOR(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException(tableName + " DeleteValuesOR colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }
            string[] values = new string[8]
            {
            "DELETE FROM ",
            tableName,
            " WHERE ",
            colNames[0],
            operations[0],
            "'",
            colValues[0],
            "'"
            };
            string text = string.Concat(values);
            checked
            {
                int num = colValues.Length - 1;
                for (int i = 1; i <= num; i++)
                {
                    string[] values2 = new string[7]
                    {
                    text,
                    "OR ",
                    colNames[i],
                    operations[0],
                    "'",
                    colValues[i],
                    "'"
                    };
                    text = string.Concat(values2);
                }
                return ExecuteNonQuery(text);
            }
        }

        public string StrJoin(string[] items)
        {
            return string.Join(",", items);
        }
        #region 查询

        public DataSet SelectTables(string tableName)
        {
            string commandText = "SELECT * FROM " + tableName;
            return GetDataSet(commandText);
        }
        /// <summary>
        /// 选择整张表   tableName=   dsName
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ds"></param>
        /// <param name="dsName">表名</param>
        /// <returns></returns>
        public DataSet SelectTables(string tableName, DataSet ds, string dsName)
        {
            DataSet dst = new DataSet();
            string commandText = "SELECT * FROM " + tableName;
            return GetDataSet(commandText, dst, tableName);
        }
        /// <summary>
        /// 按条件查询 
        /// </summary>
        /// <param name="queryString">查询命令</param>
        /// <param name="ds"></param>
        /// <param name="dsName">表名</param>
        /// <returns></returns>
        public DataSet SelectValues(string queryString, DataSet ds, string dsName)
        {
            return GetDataSet(queryString, ds, dsName);
        }

        /// <summary>
        /// 按条件查询  查询表里面某一行数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colName">筛选列名</param>
        /// <param name="key">筛选值</param>
        /// <returns></returns>
        public DataSet SelectValues(string tableName, string colName, string key)
        {
            DataSet dst = new DataSet();
            string commandText = "SELECT * FROM " + tableName + " WHERE " + colName + "='" + key + "'";
            return GetDataSet(commandText, dst, tableName);
        }

        public DataSet SelectValues(string tableName, string[] colNames, string colName, DateTime startTime, DateTime endTime)
        {
            string queryString = "";
            DataSet ds = new DataSet();
            //拼接查询命令
            queryString = "SELECT ";
            if (colNames.Length <= 1)
                queryString = queryString + "*";
            else
            {
                queryString = queryString + colNames[0];
                for (int i = 1; i < colNames.Length; i++)
                {
                    queryString = queryString + "," + colNames[i];
                }
            }
            queryString += " FROM " + tableName;
            queryString += " WHERE " + colName + ">='" + startTime.ToString(DTFormat.YMDHmsf) + "' AND " + colName + "<='" + endTime.ToString(DTFormat.YMDHmsf) + "'";


            return GetDataSet(queryString, ds, tableName);
        }

        public DataSet SelectValues(string tableName, string[] colNames, string colName, string startTime, string endTime)
        {
            string queryString = "";
            DataSet ds = new DataSet();
            //拼接查询命令
            queryString = "SELECT ";
            if (colNames.Length <= 1)
                queryString = queryString + "*";
            else
            {
                queryString = queryString + colNames[0];
                for (int i = 1; i < colNames.Length; i++)
                {
                    queryString = queryString + "," + colNames[i];
                }
            }
            queryString += " FROM " + tableName;
            queryString += " WHERE " + colName + ">='" + startTime + "' AND " + colName + "<='" + endTime + "'";


            return GetDataSet(queryString, ds, tableName);
        }

        public DataSet SelectValues(string tableName, string[] colNames, string colName, DateTime startTime, DateTime endTime, string[] orderColNames, bool isAscend)
        {
            string queryString = "";
            DataSet ds = new DataSet();
            //拼接查询命令
            queryString = "SELECT ";
            if (colNames.Length <= 1)
                queryString = queryString + "*";
            else
            {
                queryString = queryString + colNames[0];
                for (int i = 1; i < colNames.Length; i++)
                {
                    queryString = queryString + "," + colNames[i];
                }
            }
            queryString += " FROM " + tableName;
            queryString += " WHERE " + colName + ">='" + startTime.ToString(DTFormat.YMDHmsf) + "' AND " + colName + "<='" + endTime.ToString(DTFormat.YMDHmsf) + "'";
            if (orderColNames.Length >= 1)
            {
                queryString += " ORDER BY " + orderColNames[0];
                for (int i = 1; i < orderColNames.Length; i++)
                {
                    queryString += "," + orderColNames[i];
                }
                if (isAscend)
                    queryString += " ASC";
                else
                    queryString += " DESC";
            }

            return GetDataSet(queryString, ds, tableName);
        }

        public DataSet SelectValues(string tableName, string[] colNames, string colName, string startTime, string endTime, string[] orderColNames, bool isAscend)
        {
            string queryString = "";
            DataSet ds = new DataSet();
            //拼接查询命令
            queryString = "SELECT ";
            if (colNames.Length <= 1)
                queryString = queryString + "*";
            else
            {
                queryString = queryString + colNames[0];
                for (int i = 1; i < colNames.Length; i++)
                {
                    queryString = queryString + "," + colNames[i];
                }
            }
            queryString += " FROM " + tableName;
            queryString += " WHERE " + colName + ">='" + startTime + "' AND " + colName + "<='" + endTime + "'";
            if (orderColNames.Length >= 1)
            {
                queryString += " ORDER BY " + orderColNames[0];
                for (int i = 1; i < orderColNames.Length; i++)
                {
                    queryString += "," + orderColNames[i];
                }
                if (isAscend)
                    queryString += " ASC";
                else
                    queryString += " DESC";
            }

            return GetDataSet(queryString, ds, tableName);
        }

        public DataSet SelectValues(string tableName, string colNames, string colValues, DataSet ds, string dsName, string operations = "=")
        {
            string[] values = new string[8]
            {
            "SELECT *  FROM ",
            tableName,
            " WHERE ",
            colNames,
            " ",
            operations,
            " ",
            colValues
            };
            string commandText = string.Concat(values);
            return GetDataSet(commandText, ds, dsName);
        }




        public DataSet SelectValuesAND(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues, DataSet ds, string dsName)
        {
            string text = "SELECT " + items[0];
            checked
            {
                int num = items.Length - 1;
                for (int i = 1; i <= num; i++)
                {
                    text = text + ", " + items[i];
                }
                string[] values = new string[9]
                {
                text,
                " FROM ",
                tableName,
                " WHERE ",
                colNames[0],
                " ",
                operations[0],
                " ",
                colValues[0]
                };
                text = string.Concat(values);
                int num2 = colNames.Length - 1;
                for (int j = 0; j <= num2; j++)
                {
                    string[] values2 = new string[8]
                    {
                    text,
                    " AND ",
                    colNames[j],
                    " ",
                    operations[j],
                    " ",
                    colValues[0],
                    " "
                    };
                    text = string.Concat(values2);
                }
                return GetDataSet(text, ds, dsName);
            }
        }

        public DataSet SelectValuesOR(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues, DataSet ds, string dsName)
        {
            string text = "SELECT " + items[0];
            checked
            {
                int num = items.Length - 1;
                for (int i = 1; i <= num; i++)
                {
                    text = text + ", " + items[i];
                }
                string[] values = new string[9]
                {
                text,
                " FROM ",
                tableName,
                " WHERE ",
                colNames[0],
                " ",
                operations[0],
                " ",
                colValues[0]
                };
                text = string.Concat(values);
                int num2 = colValues.Length - 1;
                for (int j = 0; j <= num2; j++)
                {
                    string[] values2 = new string[8]
                    {
                    text,
                    " OR ",
                    colNames[j],
                    " ",
                    operations[j],
                    " ",
                    colValues[0],
                    " "
                    };
                    text = string.Concat(values2);
                }
                return GetDataSet(text, ds, dsName);
            }
        }

        public int CreateStandardTable(string tableName, string[] colNames, string[] colTypes)
        {
            string text;

            checked
            {
                try
                {
                    text = "CREATE TABLE IF NOT EXISTS " + tableName + " ( Serial_ID integer PRIMARY KEY AUTOINCREMENT ";//"( " + colNames[0] + " " + colTypes[0];
                    //text += "," + HappenTime + " DATETIME ," + EndTime + " DATETIME ";

                    int num = colNames.Length - 1;
                    for (int i = 0; i <= num; i++)
                    {
                        text = text + ", " + colNames[i] + " " + colTypes[i];
                    }
                    text += " )  ; ";       /*DEFAULT CHARSET=utf8mb4*/
                    return ExecuteNonQuery(text);
                    //Close();

                }
                finally
                {

                }

            }
        }
        #endregion
    }
    /// <summary>
    /// Y 年 M 月 D 日 H 时 m分 s 秒 f 毫秒
    /// 数据库时间格式较固定，填数据时按照一种数据格式填入
    /// </summary>
    public class DTFormat
    {
        public static string YMD = "yyyy-MM-dd";
        public static string YMDHm = "yyyy-MM-dd HH:mm";
        public static string YMDHmsf = "yyyy-MM-dd HH:mm:ss.fff";
        public static string mYMDHms = "yyyy-MM-dd HH:mm:ss";
        public static string MDYHm = "MM-DD-yyyy HH:mm";
        public static string Hm = "HH:mm";
        public static string YMDTHm = "yyyy-MM-ddTHH:mm";
        public static string Hms = "HH:mm:ss";
        public static string YMDHms = "yyyyMMdd HHmmss";

    }
}
