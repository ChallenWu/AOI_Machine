using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseManager;
using System.Data;
using NPOI.SS.Formula.PTG;
using System.Data.SQLite;
using BoTech;
using NPOI.SS.UserModel;
using NPOI.SS.Formula.Functions;
using System.Net.NetworkInformation;
using Demo;

namespace BoTech
{
    /// <summary>
    /// 此类用于相应项目软件调用 数据库资源， 具体项目与 《数据库》  断开直接连接   ，方便切换不同数据库
    /// 中间类， 
    /// </summary>
    public class DataServerManager
    {

        private static DataServerManager instance;
        public static DataServerManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataServerManager();
                return instance;
            }
        }
        public DataServerManager()
        {
            DBH = new SQLiteHelper($"{System.Windows.Forms.Application.StartupPath}\\Machinedata.db");
        }

        #region 内部变量
        private DataBaseHelper DBH;
        private string alarmTableName = "alarmhistory";
        private string unitTableName = "unitall";
        private string machineStateTableName = "machinestate";
        private string operationLogTableName = "operationlog";
        private string usersTableName = "users";

        #endregion
        /// <summary>
        /// 初始化 Audio 相应数据表 Alarm Unit MachineState OperationLog
        /// </summary>
        public void Load()
        {
            IniAlarmDataBsaseTable();
            IniUnitDataBsaseTable();
            IniMachineStateDataBsaseTable();
            IniOperationLogBsaseTable();
            IniUsersBsaseTable();
        }
        #region Alarm
        /// <summary>
        /// 初始化Alarm数据表 创建
        /// </summary>
        private void IniAlarmDataBsaseTable()
        {
            string[] colName = new string[] { "AlarmSerialNumber", "EffectiveHappenTime", "HappenTime", "EndTime", "Days", "ErrorCode", "ErrorCategory", "Duration", "DurationCategory", "ErrorMessageE", "ErrorMessageC", "DealtMethod" };
            string[] colTypes = new string[] { "varchar(25)", "DATETIME", "DATETIME", "DATETIME", "date", "varchar(45)", "varchar(45)", "double ", "int", "varchar(45)", "varchar(45)", "varchar(45)" };
            //创建数据表
            //dbh.CreateTable("test001", colName, colTypes);    
            DBH.CreateStandardTable(alarmTableName, colName, colTypes);
        }

        public void InsertAlarmON(AlarmMessage AM)
        {
            string[] colName = new string[] { "AlarmSerialNumber", "HappenTime", "EffectiveHappenTime", "EndTime", "Days", "ErrorCode", "ErrorCategory", "Duration", "DurationCategory", "ErrorMessageE", "ErrorMessageC", "DealtMethod" };
            string[] colValues = new string[] { AM.AlarmSerialNumber,AM.HappenTime.ToString("yyyy-MM-dd HH:mm:ss"),AM.EffectiveHappenTime.ToString("yyyy-MM-dd HH:mm:ss"), AM.EndTime.ToString("yyyy-MM-dd HH:mm:ss"),  AM.EndTime.ToString("yyyy-MM-dd"),
                                                AM.NowAlarm.ErrorCode, AM.NowAlarm.ErrorCategory, AM.Duration.ToString(),AM.DurationCatigory.ToString(), AM.NowAlarm.MessageEn, AM.NowAlarm.MessageCn, AM.NowAlarm.DealtMethod };

            DBH.InsertStandardValues(alarmTableName, colName, colValues);
        }

        public void UpdateAlarm(AlarmMessage AM)
        {
            AM.Duration = (AM.EndTime - AM.EffectiveHappenTime).TotalMinutes;
            if (AM.Duration < 5)
                AM.DurationCatigory = 1;
            else if (AM.Duration < 10)
                AM.DurationCatigory = 5;
            else if (AM.Duration < 15)
                AM.DurationCatigory = 10;
            else if (AM.Duration < 20)
                AM.DurationCatigory = 15;
            else if (AM.Duration < 25)
                AM.DurationCatigory = 20;
            else
                AM.DurationCatigory = 25;
            string[] colName = new string[] { "AlarmSerialNumber", "HappenTime", "EffectiveHappenTime", "EndTime", "Days", "ErrorCode", "ErrorCategory", "Duration", "DurationCategory", "ErrorMessageE", "ErrorMessageC", "DealtMethod" };
            string[] colValues = new string[] { AM.AlarmSerialNumber,AM.HappenTime.ToString("yyyy-MM-dd HH:mm:ss"),AM.EffectiveHappenTime.ToString("yyyy-MM-dd HH:mm:ss"), AM.EndTime.ToString("yyyy-MM-dd HH:mm:ss"),  AM.EndTime.ToString("yyyy-MM-dd"),
                                                AM.NowAlarm.ErrorCode, AM.NowAlarm.ErrorCategory, AM.Duration.ToString(),AM.DurationCatigory.ToString(), AM.NowAlarm.MessageEn, AM.NowAlarm.MessageCn, AM.NowAlarm.DealtMethod };

            DBH.UpdateValues(alarmTableName, colName, colValues, "AlarmSerialNumber", AM.AlarmSerialNumber);
        }
        public DataTable SelectDT_Statistic(DateTime start, DateTime end, bool DP = true)
        {
            DataTable dt = null;
            string Command = "";
            if (DP)
            {
                Command = $"select ErrorCategory,sum(Duration),count(*) from {alarmTableName} where HappenTime between '{start.ToString("yyyy-MM-dd HH:mm:ss")}' and '{end.ToString("yyyy-MM-dd HH:mm:ss")}' group by ErrorCategory order by sum(Duration)  ";

            }
            else
            {
                Command = $"select ErrorCategory,sum(Duration),count(*) from {alarmTableName} where HappenTime between '{start.ToString("yyyy-MM-dd HH:mm:ss")}' and '{end.ToString("yyyy-MM-dd HH:mm:ss")}' group by ErrorCategory order by count(*)  ";

            }

            dt = DBH.SelectValues(Command).Tables[0];
            return dt;
        }

        public DataTable SelectAlarmDurationCategory(DateTime start, DateTime end)
        {
            DataTable dt = null;
            string Command = "";
            Command = $"select DurationCategory,count(*) from {alarmTableName} where HappenTime between '{start.ToString("yyyy-MM-dd HH:mm:ss")}' and '{end.ToString("yyyy-MM-dd HH:mm:ss")}' group by Days,DurationCategory order by Days,DurationCategory  ";

            dt = DBH.SelectValues(Command).Tables[0];
            return dt;
        }

        // 增加一个 手动不完整报警信息 结束   读取数据库，并手动补齐数据
        // 增加读取数据库最后一条信息，手动补齐建议在外部构建
        public DataTable SelectLastAlarm()
        {
            DataTable dt = null;
            string Command = "";

            Command = $"select * from {alarmTableName}  order by HappenTime desc limit 1";

            dt = DBH.SelectValues(Command).Tables[0];
            return dt;
        }

        public DataTable SelectAlarmAll(DateTime start, DateTime end)
        {
            DataTable dt = null;
            string Command = "";
            Command = $"select * from {alarmTableName} where HappenTime between '{start.ToString("yyyy-MM-dd HH:mm:ss")}' and '{end.ToString("yyyy-MM-dd HH:mm:ss")}' order by  HappenTime  desc";

            dt = DBH.SelectValues(Command).Tables[0];
            return dt;
        }
        public DataTable SelectAlarmLog(DateTime start, DateTime end)
        {
            DataTable dt = null;
            string Command = "";
            Command = $"select Days,EffectiveHappenTime,EndTime,Duration,ErrorCode,ErrorMessageE,DealtMethod from {alarmTableName} where HappenTime between '{start.ToString("yyyy-MM-dd HH:mm:ss")}' and '{end.ToString("yyyy-MM-dd HH:mm:ss")}' order by  HappenTime  desc";

            dt = DBH.SelectValues(Command).Tables[0];
            return dt;
        }

        public DataTable SelectAlarmAll()
        {
            DataTable dt = null;

            dt = DBH.SelectTables(alarmTableName).Tables[0];
            return dt;
        }
        /// <summary>
        /// 删除 Alarm相关过期数据
        /// </summary>
        /// <param name="StaleDays">过期时间</param>
        public void DeleteAlarmStaleData(double StaleDays = 7.0)
        {
            DBH.DeleteValues(alarmTableName, "HappenTime", "<=", DateTime.Now.AddDays(-StaleDays).ToString("yyyy-MM-dd HH:mm:ss"));
        }



        #endregion

        #region Unit
        private void IniUnitDataBsaseTable()
        {
            //强调哪些不需要动
            string[] colName = new string[] { "HappenTime", "Days", "Hours", "Shift", "Unit_SN", "Component_SN", "Start_Time", "End_Time", "Pass", "CT", "Hive_State" };
            string[] colTypes = new string[] { "DATETIME", "date", "int", "varchar(5)", "varchar(45)", "varchar(45)", "DATETIME", "DATETIME", "varchar(10)", "varchar(10)", "varchar(5)" };
            //创建数据表
            //dbh.CreateTable("test001", colName, colTypes);    
            DBH.CreateStandardTable(unitTableName, colName, colTypes);

        }

        public void InsertUnitMessage(UCMessage ucm, int unitIndes)
        {
            try
            {
                UnitMessage UM = ucm.Units[unitIndes];
                DateTime now = DateTime.Now;
                string[] colName = new string[] { "HappenTime", "Days", "Hours", "Shift", "Unit_SN", "Component_SN", "Start_Time", "End_Time", "Pass", "CT", "Hive_State" };
                string[] colValues = new string[] { now.ToString("yyyy-MM-dd HH:mm:ss"),now.ToString("yyyy-MM-dd") , now.ToString("HH"),
                                               UM.Shift, UM.UnitSN, ucm.UC_SN, UM.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),UM.EndTime.ToString("yyyy-MM-dd HH:mm:ss"), UM.Pass, UM.CT.ToString("f2"), UM.HiveState.ToString() };
                //创建数据表
                //dbh.CreateTable("test001", colName, colTypes);    
                DBH.InsertStandardValues(unitTableName, colName, colValues);
            }
            catch (Exception ex)
            {

            }

        }
        public void InsertUnitMessage(ProductMessage ucm, int unitIndes)
        {
            try
            {
                ProductInformation UM = ucm.Unit;
                DateTime now = DateTime.Now;
                string[] colName = new string[] { "Model","HappenTime", "Days", "Hours", "Shift", "Unit_SN", "Component_SN", "SerialNumber1", "SerialNumber2", "SerialNumber3", "Start_Time", "End_Time", "Pass", "CT", "Hive_State" };
                string[] colValues = new string[] {UM.ModelProduct, now.ToString("yyyy-MM-dd HH:mm:ss"),now.ToString("yyyy-MM-dd") , now.ToString("HH"),
                                                   UM.Shift, UM.UnitSN, ucm.UC_SN, UM.serialNumber1, UM.serialNumber2, UM.serialNumber3,
                                                    UM.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),UM.EndTime.ToString("yyyy-MM-dd HH:mm:ss"), UM.Pass, UM.CT.ToString("f2"), UM.HiveState.ToString() };
                //创建数据表
                //dbh.CreateTable("test001", colName, colTypes);    
                DBH.InsertStandardValues(unitTableName, colName, colValues);
            }
            catch (Exception ex)
            {

            }

        }


        public DataTable Select_8H(DateTime start, DateTime end)
        {
            DataTable dt = null;
            string Command = "";

            Command = $"select Hours,count(*),Pass from {unitTableName} where HappenTime between " +
                $"'{start.ToString("yyyy-MM-dd HH:00:00")}' and '{end.ToString("yyyy-MM-dd HH:00:00")}' group by Hours order by Hours";

            dt = DBH.SelectValues(Command).Tables[0];
            return dt;
        }


        /****************************************************/

        //CycleTime
        public DataTable Select7DavgCT(DateTime startTime)
        {
            //   7D  CT trung bình hàng giờ
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"select avg(CT),Shift from {unitTableName} where  Days='{startTime.ToString("yyyy-MM-dd")}' and Shift='DS' ");
            for (int i = 1; i < 7; i++)
            {
                sb.Append($"union all select avg(CT),Shift from {unitTableName} where  Days='{startTime.AddDays(-i).ToString("yyyy-MM-dd")}' and Shift='DS' ");
            }
            for (int i = 0; i < 7; i++)
            {
                sb.Append($"union all select avg(CT),Shift from {unitTableName} where  Days='{startTime.AddDays(-i).ToString("yyyy-MM-dd")}' and Shift='NS' ");
            }
            sb.Append(";");
            try
            {
                dt = DBH.SelectValues(sb.ToString()).Tables[0];
            }
            catch(Exception ex)
            {

            Console.WriteLine(ex.Message); 
            }
            return dt;
        }

        public DataTable SelectMultiDayCT(DateTime startTime, DateTime endTime)
        {
            //CT trung bình 
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();
            var Day = (endTime.Date - startTime.Date).Days + 1;
            if(Day >= 30)
            {
                Day = 30;
            }
            sb.Append($"select avg(CT),Shift from {unitTableName} where  Days='{endTime.ToString("yyyy-MM-dd")}' and Shift='DS' ");
            for (int i = 1; i < Day; i++)
            {
                sb.Append($"union all select avg(CT),Shift from {unitTableName} where  Days='{endTime.AddDays(-i).ToString("yyyy-MM-dd")}' and Shift='DS' ");
            }
            for (int i = 0; i < Day; i++)
            {
                sb.Append($"union all select avg(CT),Shift from {unitTableName} where  Days='{endTime.AddDays(-i).ToString("yyyy-MM-dd")}' and Shift='NS' ");
            }
            sb.Append(";");
            try
            {
                dt = DBH.SelectValues(sb.ToString()).Tables[0];
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return dt;
        }

        public DataTable Select24HDavgCT(string Model, DateTime startTime)
        {
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();            
            sb.Append($"select avg(CT),'{startTime.Hour}' as Hours from {unitTableName} " +
                        $"where  End_Time>='{startTime.ToString("yyyy-MM-dd HH:00:00")}' " +
                        $"and End_Time<'{startTime.AddHours(1).ToString("yyyy-MM-dd HH:00:00")}'");

            for (int i = 0; i < 24; i++)
            {               
                sb.Append($"union all select avg(CT),'{startTime.AddHours((i + 1)).Hour}' as Hours from {unitTableName} where  End_Time>='{startTime.AddHours((i + 1)).ToString("yyyy-MM-dd HH:00:00")}' and End_Time<'{startTime.AddHours(i + 2).ToString("yyyy-MM-dd HH:00:00")}' ");
            }
            sb.Append(";");
            string str = sb.ToString();
            dt = DBH.SelectValues(sb.ToString()).Tables[0];
            int x = dt.Rows.Count;
            return dt;
        }



        public DataTable Select24hInDay(DateTime startTime)
        {
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();

            string dateOnly = startTime.ToString("yyyy-MM-dd");

            sb.Append($"select avg(CT), 0 as Hours from {unitTableName} where End_Time >= '{dateOnly} 00:00:00' and End_Time < '{dateOnly} 01:00:00' ");

            // Truy vấn cho các giờ tiếp theo từ 1h đến 23h
            for (int i = 1; i < 24; i++)
            {
                sb.Append($"union all select avg(CT), {i} as Hours from {unitTableName} where End_Time >= '{dateOnly} {i:00}:00:00' and End_Time < '{dateOnly} {i + 1:00}:00:00' ");
            }
            sb.Append(";");
            string str = sb.ToString();
            dt = DBH.SelectValues(sb.ToString()).Tables[0];
            int x = dt.Rows.Count;
            return dt;
        }
        /*Thống kê đầu ra 7D;*/
        public DataTable Count7DayYield(DateTime startTime)
        {
            //DataBaseHelper dataBaseHelper = new DataBaseHelper();
            //   7D  小时平均CT
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"select Count(*),Days,Shift,PASS from {unitTableName} where  Days='{startTime.ToString("yyyy-MM-dd")}' and Shift='DS' and PASS = 'PASS' ");
            sb.Append("union all ");
            sb.Append($"select Count(*),Days,Shift,PASS from {unitTableName} where  Days='{startTime.ToString("yyyy-MM-dd")}' and Shift='DS' and PASS = 'FAIL' ");

            for (int i = 1; i < 7; i++)
            {
                sb.Append($"union all select all Count(*),Days,Shift,PASS from {unitTableName} where  Days='{startTime.AddDays(-i).ToString("yyyy-MM-dd")}' and Shift='DS' and Pass ='PASS' ");
                sb.Append($"union all select all Count(*),Days,Shift,PASS from {unitTableName} where  Days='{startTime.AddDays(-i).ToString("yyyy-MM-dd")}' and Shift='DS' and Pass ='FAIL' ");
            }
            for (int i = 0; i < 7; i++)
            {
                sb.Append($"union all select Count(*),Days,Shift,PASS from {unitTableName} where  Days='{startTime.AddDays(-i).ToString("yyyy-MM-dd")}' and Shift='NS' and Pass ='PASS' ");
                sb.Append($"union all select Count(*),Days,Shift,PASS from {unitTableName} where  Days='{startTime.AddDays(-i).ToString("yyyy-MM-dd")}' and Shift='NS' and Pass ='FAIL' ");
            }
            sb.Append(";");
            string str = sb.ToString();
            dt = DBH.SelectValues(sb.ToString()).Tables[0];
            return dt;
        }
        public DataTable CountDayYield(DateTime startTime)
        {
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"select Count(*),Days,PASS from {unitTableName} where Days='{startTime.ToString("yyyy-MM-dd")}' and PASS = 'PASS' ");

            for (int i = 1; i < 7; i++)
            {
                sb.Append($"union all select all Count(*),Days,PASS from {unitTableName} where Days='{startTime.AddDays(-i).ToString("yyyy-MM-dd")}' and Pass ='PASS' ");
            }
            for (int i = 0; i < 7; i++)
            {
                sb.Append($"union all select Count(*),Days,PASS from {unitTableName} where Days='{startTime.AddDays(-i).ToString("yyyy-MM-dd")}' and Pass ='FAIL' ");
            }
            sb.Append(";");
            string str = sb.ToString();
            dt = DBH.SelectValues(sb.ToString()).Tables[0];
            return dt;
        }

        public DataTable CountMultiDayYield(DateTime startTime,DateTime endTime)
        {
            //   7D  小时平均CT
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();
            var Day = (endTime.Date - startTime.Date).Days + 1;

            if (Day >= 30)
            {
                Day = 30;
            }
            sb.Append($"select Count(*),Days,Pass from {unitTableName} where Days='{endTime.ToString("yyyy-MM-dd")}' and Pass ='PASS' ");

            for (int i = 1; i < Day; i++)
            {
                sb.Append($"union all select all Count(*),Days ,Pass from {unitTableName} where  Days='{endTime.AddDays(-i).ToString("yyyy-MM-dd")}' and Pass ='PASS' ");
            }
            for (int i = 0; i < Day; i++)
            {
                sb.Append($"union all select Count(*),Days, Pass from {unitTableName} where  Days='{endTime.AddDays(-i).ToString("yyyy-MM-dd")}' and Pass ='FAIL' ");
            }
            sb.Append(";");
            string str = sb.ToString();
            dt = DBH.SelectValues(sb.ToString()).Tables[0];
            return dt;
        }


        public DataTable Count24HourYield(DateTime startTime)
        {

            DataTable dt = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"select Count(*),'{startTime.Hour}' as Hours from {unitTableName} where  End_Time>='{startTime.ToString("yyyy-MM-dd HH:00:00")}' and End_Time<'{startTime.AddHours(1).ToString("yyyy-MM-dd HH:00:00")}' ");

            for (int i = 0; i < 23; i++)
            {
                sb.Append($"union all select Count(*),'{startTime.AddHours((i + 1)).Hour}' as Hours from {unitTableName} " +
                          $"where  End_Time>='{startTime.AddHours((i + 1)).ToString("yyyy-MM-dd HH:00:00")}' " +
                          $"and End_Time<'{startTime.AddHours(i + 2).ToString("yyyy-MM-dd HH:00:00")}' ");
            }
            sb.Append(";");
            string str = sb.ToString();
            dt = DBH.SelectValues(sb.ToString()).Tables[0];
            int x = dt.Rows.Count;
            return dt;
        }

        public DataTable Count24HourYield(DateTime startTime, int a)
        {
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();
            sb.Append($"select Count(*), Pass,'{startTime.Hour}' as Hours from {unitTableName} where Pass = 'PASS' " +
                      $"and End_Time >= '{startTime.ToString("yyyy-MM-dd HH:00:00")}' " +
                      $"and End_Time < '{startTime.AddHours(1).ToString("yyyy-MM-dd HH:00:00")}' ");

            for (int i = 0; i < 23; i++)
            {
                sb.Append($"union all select Count(*), Pass,'{startTime.AddHours((i + 1)).Hour}' as Hours from {unitTableName} " +
                          $"where End_Time >='{startTime.AddHours((i + 1)).ToString("yyyy-MM-dd HH:00:00")}' " +
                          $"and End_Time<'{startTime.AddHours(i + 2).ToString("yyyy-MM-dd HH:00:00")}' and Pass = 'PASS' ");
            }
            for (int i = 0; i < 24; i++)
            {
                sb.Append($"union all select Count(*), Pass,'{startTime.AddHours((i)).Hour}' as Hours from {unitTableName}" +
                          $" where  End_Time >= '{startTime.AddHours((i)).ToString("yyyy-MM-dd HH:00:00")}' " +
                          $" and End_Time < '{startTime.AddHours(i + 1).ToString("yyyy-MM-dd HH:00:00")}' and Pass = 'FAIL' ");
            }

            sb.Append(";");
            string str = sb.ToString();
            dt = DBH.SelectValues(sb.ToString()).Tables[0];
            int x = dt.Rows.Count;
            return dt;
        }






        public DataTable Select_7D(DateTime start, DateTime end)
        {
            DataTable dt = null;
            string Command = "";

            Command = $"select Days,Shift,count(*) from {unitTableName} where HappenTime between '{start.ToString("yyyy-MM-dd HH:mm:ss")}' and '{end.ToString("yyyy-MM-dd HH:mm:ss")}' group by Days,Shift order by Days";

            dt = DBH.SelectValues(Command).Tables[0];
            return dt;
        }

        public DataTable SelectDurationUnit(DateTime start, DateTime end)
        {
            DataTable dt = null;
            //string Command = "";
            string[] colName = new string[] { "HappenTime", "Shift", "Unit_SN", "Component_SN", "Start_Time", "End_Time", "Pass", "CT", "Hive_State" };

            //Command = $"select Days,Shift,count(*) form {unitTableName} where HappenTime between {start.ToString("yyyy-MM-dd HH:mm:ss")} and {end.ToString("yyyy-MM-dd HH:mm:ss")} group by Days,Shift order by Days";

            //dt = DBH.SelectValues(Command).Tables[0];

            dt = DBH.SelectValues(unitTableName, colName, "HappenTime", start.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss")).Tables[0];
            return dt;
        }

        public DataTable MonitorMassProduct(DateTime start, DateTime end)
        {
            DataTable dt = null;
            string Command = "";
            Command = $"select HappenTime,Shift,Unit_SN,Component_SN,Start_Time,End_Time,Pass,CT,Hive_State,Model from {unitTableName} where HappenTime between '{start.ToString("yyyy-MM-dd HH:mm:ss")}' and '{end.ToString("yyyy-MM-dd HH:mm:ss")}' order by  HappenTime  desc";

            dt = DBH.SelectValues(Command).Tables[0];
            return dt;
        }

        public DataTable MonitorMassProduct(string Model, DateTime start, DateTime end)
        {
            DataTable dt = null;
            string Command = "";
            Command = $"select HappenTime,Shift,Unit_SN,Component_SN,Start_Time,End_Time,Pass,CT,Hive_State,Model from {unitTableName} where Model = '{Model}' and HappenTime between '{start.ToString("yyyy-MM-dd HH:mm:ss")}' and '{end.ToString("yyyy-MM-dd HH:mm:ss")}' order by  HappenTime  desc";

            dt = DBH.SelectValues(Command).Tables[0];
            return dt;
        }


        public DataTable SelectDurationUnit2(DateTime start, DateTime end)
        {
            DataTable dt = null;

            string[] colName = new string[] { "HappenTime", "Shift", "Unit_SN", "Component_SN", "Start_Time", "End_Time", "Pass", "CT", "Hive_State" };

            dt = DBH.SelectValues(unitTableName, colName, "HappenTime", start.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss")).Tables[0];
            return dt;
        }




        public DataTable SelectUPHData(DateTime start, DateTime end)
        {
            DataTable dt = null;
            string Command = "";

            Command = $"select Hours,count(*),Pass from {unitTableName} where HappenTime between '{start.ToString("yyyy-MM-dd HH:mm:ss")}' and '{end.ToString("yyyy-MM-dd HH:mm:ss")}'  group by Pass  ";

            dt = DBH.SelectValues(Command).Tables[0];

            return dt;

        }

        

        public DataTable SelectLastProduct()
        {
            DataTable dt = null;
            string Command = "";
            Command = $"SELECT * FROM unitall ORDER BY HappenTime DESC LIMIT 1";

            dt = DBH.SelectValues(Command).Tables[0];

            return dt;
        }

        public DataTable SelectUnitAll()
        {
            DataTable dt = null;

            dt = DBH.SelectTables(unitTableName).Tables[0];
            return dt;
        }

        public void DeleteUnitStaleData(double StaleDays = 7.0)
        {
            DBH.DeleteValues(unitTableName, "HappenTime", "<=", DateTime.Now.AddDays(-StaleDays).ToString("yyyy-MM-dd HH:mm:ss"));
        }

        #endregion

        #region   MachineState
        private void IniMachineStateDataBsaseTable()
        {
            string[] colName = new string[] { "HappenTime", "Days", "MachineState", "PreviousState", "TimeSpan" };
            string[] colTypes = new string[] { "DATETIME", "date", "int", "int", "BIGINT " };
            //创建数据表
            //dbh.CreateTable("test001", colName, colTypes);    
            DBH.CreateStandardTable(machineStateTableName, colName, colTypes);
        }



        public void InsertMachineState(HiveMessage HM)
        {
            string[] colName = new string[] { "HappenTime", "Days", "MachineState", "PreviousState", "TimeSpan" };
            string[] colValues = new string[] { HM.HappenTime.ToString("yyyy-MM-dd HH:mm:ss"), HM.HappenTime.ToString("yyyy-MM-dd"), HM.MachineState.ToString(), HM.PreviousState.ToString(), HM.TimeDuration.ToString() };

            DBH.InsertStandardValues(machineStateTableName, colName, colValues);
        }

        public DataTable SelectMachineState(DateTime start, DateTime end)
        {
            DataTable dt = null;
            string Command = "";

            Command = $"select Days,PreviousState,count(*),sum(TimeSpan) from {machineStateTableName} where HappenTime between '{start.ToString("yyyy-MM-dd")}' and '{end.ToString("yyyy-MM-dd HH:mm:ss")}' group by Days,PreviousState order by Days";

            dt = DBH.SelectValues(Command).Tables[0];
            return dt;
        }

        public DataTable SelectMachineStateAll()
        {
            DataTable dt = null;

            dt = DBH.SelectTables(machineStateTableName).Tables[0];
            return dt;
        }
        //增加 读取最后一条 
        public DataTable SelectLastMachineState()
        {
            DataTable dt = null;
            string Command = "";

            Command = $"select * from {machineStateTableName}  order by HappenTime desc limit 1";

            dt = DBH.SelectValues(Command).Tables[0];
            return dt;
        }

        public void DeleteMachineStateStaleData(double StaleDays = 7.0)
        {
            DBH.DeleteValues(machineStateTableName, "HappenTime", "<=", DateTime.Now.AddDays(-StaleDays).ToString("yyyy-MM-dd HH:mm:ss"));
        }


        #endregion

        #region OperationLog
        private void IniOperationLogBsaseTable()
        {
            string[] colName = new string[] { "HappenTime", "User_Name", "User_ID", "Employee_ID", "User_Level", "Company", "JobTitle", "OperationMessage" };
            string[] colTypes = new string[] { "DATETIME", "varchar(50)", "varchar(50)", "varchar(50)", "varchar(50)", "varchar(50)", "varchar(50)", "varchar(200)" };
            //创建数据表
            //dbh.CreateTable("test001", colName, colTypes);    
            DBH.CreateStandardTable(operationLogTableName, colName, colTypes);
        }

        //public void InsertOperationLog(OperationLog OL)
        //{
        //    string[] colName = new string[] { "HappenTime", "User_Name", "User_ID", "Employee_ID", "User_Level", "Company", "JobTitle", "OperationMessage" };
        //    string[] colValues = new string[] { OL.HappenTime.ToString("yyyy-MM-dd HH:mm:ss"), OL.AI.UserName, OL.AI.UserID, OL.AI.EmployeeID, OL.AI.UserLevel.ToString(), OL.AI.Company, OL.AI.JobTitle, OL.OperationMessage };

        //    DBH.InsertStandardValues(operationLogTableName, colName, colValues);
        //}
         
        public void InsertOperationLog(string OperationMessage)
        {
            string[] colName = new string[] { "HappenTime", "User_Name", "JobTitle", "OperationMessage" };
            string[] colValues = new string[] { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), UserAccountControl.currentAccount.Name, UserAccountControl.currentAccount.UserPermission.ToString(), OperationMessage };

            DBH.InsertStandardValues(operationLogTableName, colName, colValues);
        }

        public DataTable SelectOperationLog(DateTime start, DateTime end)
        {
            DataTable dt = null;
            string[] colName = new string[] { };

            dt = DBH.SelectValues(operationLogTableName, colName, "HappenTime", start.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss")).Tables[0];

            return dt;
        }

        public void DeleteOperationLogStaleData(double StaleDays = 7.0)
        {
            DBH.DeleteValues(operationLogTableName, "HappenTime", "<=", DateTime.Now.AddDays(-StaleDays).ToString("yyyy-MM-dd HH:mm:ss"));
        }


        #endregion

        #region users
        private void IniUsersBsaseTable()
        {
            string[] colName = new string[] { "UserID", "UserName", "EmployeeID", "Password", "UserLevel", "Company", "JobTitle", "EstablishmentTime", "LatestModificationTime" };
            string[] colTypes = new string[] { "varchar(50)", "varchar(50)", "varchar(50)", "varchar(50)", "varchar(50)", "varchar(50)", "varchar(50)", "DATETIME", "DATETIME" };
            //创建数据表
            //dbh.CreateTable("test001", colName, colTypes);    
            DBH.CreateStandardTable(usersTableName, colName, colTypes);
        }

        //public void InsertUser(AccountInfo UM)
        //{
        //    string[] colName = new string[] { "UserID", "UserName", "EmployeeID", "Password", "UserLevel", "Company", "JobTitle", "EstablishmentTime", "LatestModificationTime" };
        //    string[] colValues = new string[] { UM.UserID, UM.UserName, UM.EmployeeID, UM.Password, UM.UserLevel.ToString(), UM.Company, UM.JobTitle, UM.EstablishmentTime.ToString("yyyy-MM-dd HH:mm:ss"), UM.LatestModificationTime.ToString("yyyy-MM-dd HH:mm:ss") };

        //    DBH.InsertStandardValues(usersTableName, colName, colValues);
        //}
        //public void UpdateUser(AccountInfo UM)
        //{
        //    string[] colName = new string[] { "UserID", "UserName", "EmployeeID", "Password", "UserLevel", "Company", "JobTitle", "EstablishmentTime", "LatestModificationTime" };
        //    string[] colValues = new string[] { UM.UserID, UM.UserName, UM.EmployeeID, UM.Password, UM.UserLevel.ToString(), UM.Company, UM.JobTitle, UM.EstablishmentTime.ToString("yyyy-MM-dd HH:mm:ss"), UM.LatestModificationTime.ToString("yyyy-MM-dd HH:mm:ss") };

        //    DBH.UpdateValues(usersTableName, colName, colValues, "UserID", UM.UserID);
        //}
        //public void DeleteUserData(AccountInfo UM)
        //{
        //    DBH.DeleteValues(usersTableName, "UserID", "=", UM.UserID);
        //}
        public void DeleteUserDataAll()
        {
            DBH.DeleteValues(usersTableName, "");
        }



        public DataTable SelectUserAll()
        {
            DataTable dt = null;

            dt = DBH.SelectTables(usersTableName).Tables[0];
            return dt;
        }

        #endregion



    }
}
