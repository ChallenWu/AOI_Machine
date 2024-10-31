using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager
{
    /// <summary>
    /// 定义数据库统一接口。统合不同数据库。各数据库继承此类，并重写相应接扣内容。
    /// 目前已暂时完成 MySQL SQLite
    /// </summary>
    public interface DataBaseHelper : IDisposable
    {
        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名数组</param>
        /// <param name="colTypes">列类型数组</param>
        /// <returns></returns>
        int CreateTable(string tableName, string[] colNames, string[] colTypes);
        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名数组</param>
        /// <param name="colTypes">列类型数组</param>
        /// <returns></returns>
        int CreateStandardTable(string tableName, string[] colNames, string[] colTypes);
        /// <summary>
        /// 创建标准数据表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dT">时间名称数组 建议HappenTime 、 EndTime，只取前两项有效</param>
        /// <param name="colNames">列名数组</param>
        /// <param name="colLength">数据列长度 根据实际需求定义</param>
        /// <returns></returns>
        int CreateStandardTable(string tableName, string HappenTime, string EndTime, string[] colNames, string[] colLength);

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        int DeleteTable(string tableName);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="WhereClause">删除条件</param>
        /// <returns></returns>
        int DeleteValues(string tableName, string WhereClause);
        /// <summary>
        /// 按条件删除表中的数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="WhichCol">条件列名</param>
        /// <param name="operateStr">条件 > = >= </param>
        /// <param name="condition">条件值</param>
        /// <returns></returns>
        int DeleteValues(string tableName, string WhichCol, string operateStr, string condition);
        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名数组</param>
        /// <param name="colValues">对应列名的数值</param>
        /// <returns></returns>
        int InsertValues(string tableName, string[] colNames, string[] colValues);

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名数组</param>
        /// <param name="colValues">对应列名的数值</param>
        /// <returns></returns>
        int InsertStandardValues(string tableName, string[] colNames, string[] colValues);
        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名数组</param>
        /// <param name="colValues">对应列名的数值</param>
        /// <returns></returns>
        int InsertStandardValues(string tableName, string[] colValues);

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="HappenTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名数组</param>
        /// <param name="colValues">对应列名的数值</param>
        /// <returns></returns>
        int InsertStandardValues(string tableName, string HappenTime, string EndTime, string[] colValues);

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名数组</param>
        /// <param name="colValues">对应列名的数值</param>
        /// <returns></returns>
        int InsertValues(string tableName, string[] colValues);
        /// <summary>
        /// 更新单条数据，改数据  UPDATE tableName SET colNames[i]=colValues[i] ... WHERE key operate value
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名</param>
        /// <param name="colValues">列对应的值</param>
        /// <param name="key">根据谁修改 筛选列名</param>
        /// <param name="value">对应的筛选值</param>
        /// <param name="operation">条件</param>
        /// <returns></returns>
        int UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string value, string operation = "=");

        /// <summary>
        /// 选择整张数据表中数据
        /// </summary>
        /// <param name="tableName">数据表名</param>   
        /// <returns></returns>
        DataSet SelectTables(string tableName);


        /// <summary>
        /// 选择整张数据表中数据
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <param name="ds">置空</param>
        /// <param name="dsName">置空</param>
        /// <returns></returns>
        DataSet SelectTables(string tableName, DataSet ds, string dsName);

        /// <summary>
        /// commandText  自己写全部命令,有返回值
        /// </summary>
        /// <param name="commandText">sql 命令行</param>
        /// <returns></returns>
        DataSet ExecuteQuery(string commandText);

        /// <summary>
        /// commandText  自己写全部命令，无返回值
        /// </summary>
        /// <param name="commandText"></param>
        int ExecuteNonQuery(string commandText);
        int ExecuteNonQuery(string commandText, SQLiteParameter[] parmeters);
        object ExecuteScalar(string commandText);
        object ExecuteScalar(string commandText, SQLiteParameter[] parmeters);
        /// <summary>
        /// 查询一段时间内的 时间DateTime
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">要查询的列名 可为空 为空时查询所有列</param>
        /// <param name="colName">筛查列名</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        DataSet SelectValues(string tableName, string[] colNames, string colName, DateTime startTime, DateTime endTime);

        /// <summary>
        /// 查询一段时间内的 时间string
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">要查询的列名</param>
        /// <param name="colName">筛查列名</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        DataSet SelectValues(string tableName, string[] colNames, string colName, string startTime, string endTime);
        /// <summary>
        /// 根据命令筛查数据
        /// </summary>
        /// <param name="queryString">查询语句</param>
        /// <param name="ds">置空</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>                                                                
        DataSet SelectValues(string queryString, DataSet ds, string tableName);
        DataSet SelectValues(string tableName, string WhereClause);
        DataSet SelectValues(string Command);


        /// <summary>
        /// 查询一段时间内的 时间DateTime  按指定列顺序
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">要查询的列名 可为空 为空时查询所有列</param>
        /// <param name="colName">筛查列名</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param> 
        /// <returns></returns>
        DataSet SelectValues(string tableName, string[] colNames, string colName, DateTime startTime, DateTime endTime, string[] orderColNames, bool isAscend);

        /// <summary>
        /// 查询数据库某一行数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colname">筛查列名</param>
        /// <param name="key">筛查值</param>
        /// <returns></returns>
        DataSet SelectValues(string tableName, string colname, string key);
        /// <summary>
        /// 查询一段时间内的 时间DateTime  按指定列顺序
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">要查询的列名 可为空 为空时查询所有列</param>
        /// <param name="colName">筛查列名</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param> 
        /// <returns></returns>
        DataSet SelectValues(string tableName, string[] colNames, string colName, string startTime, string endTime, string[] orderColNames, bool isAscend);

        void Open();
        void Close();

    }
}
