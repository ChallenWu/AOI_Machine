//using AutoStudio.Core.Views.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoTech;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using XCore;
using Demo.UserControls;
using Demo.Task;
using HB_IWatch;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using OfficeOpenXml;
using System.IO;

namespace Demo.Page
{
    public partial class PageAlarm : UserControlBase
    {
        private static PageAlarm instance;
        public static PageAlarm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new PageAlarm();
                return instance;
            }
        }
        public PageAlarm()
        {
            InitializeComponent();
            //引发VisibleChange 事件
            this.Visible = false;
            this.Visible = true;
            IniAlarmMessage();

            //绑定点击事件 
            //BindColtrolsClick.BindControlClick(this);
            isSelectTime = false;
        }
        private DataTable DT_StatisticsTable;
        private DataTable AlarmDurationTable;
        private DataTable AlarmLogTable;
        public override void StartRefreshPage()
        {
            this.StatusRefreshTimer.Start();
            this.dTP_EndTime.Value = DateTime.Now;
            this.dTP_StartTime.Value = DateTime.Now.AddHours(-48);
        }
        public override void StopRefreshPage()
        {
            this.StatusRefreshTimer.Stop();
        }
        private void StatusRefreshTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.StatusRefreshTimer.Stop();
                this.dTP_EndTime.Value = DateTime.Now;
                //this.dTP_StartTime.Value = DateTime.Now.AddHours(-48);

                btn_Query_Click(sender, e);
                if (isSelectTime == false)
                {
                    this.StatusRefreshTimer.Start();
                }

            }
            catch
            {

            }

        }
        private void IniAlarmMessage()
        {
            cb_Alarm.DataSource = Enum.GetValues(typeof(XAlarmId));

            for (int i = 0; i < cb_Alarm.Items.Count; i++)
            {
                m_asyncHandled.Add(i,true); 
            }
        }


        private DataTable AlarmCountAdd(DataTable dt)
        {
            DataTable dtTemp = new DataTable();
            DataColumn dc_0 = new DataColumn("Error", typeof(System.String));
            DataColumn dc_1 = new DataColumn("TotalTime(Min)", typeof(System.Double));
            DataColumn dc_2 = new DataColumn("CountTotal", typeof(System.Int32));
            dtTemp.Columns.Add(dc_0);
            dtTemp.Columns.Add(dc_1);
            dtTemp.Columns.Add(dc_2);

            try
            {

                List<string> lTemp = new List<string>();
                List<string> lTemp2 = new List<string>();
                List<string> lTemp3 = new List<string>();
                if (dt.Rows.Count < AudioSystem.Errors.Length)
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0] == null)
                        {
                            dt.Rows[i][0] = "";
                        }

                        lTemp.Add(dt.Rows[i][0].ToString());
                    }



                    lTemp2 = AudioSystem.Errors.ToList();

                    for (int i = 0; i < lTemp2.Count; i++)
                    {
                        bool isHave = true;
                        for (int j = 0; j < lTemp.Count; j++)
                        {
                            isHave = true;
                            if (lTemp[j] == lTemp2[i])
                            {
                                continue;
                            }
                            else
                            {
                                isHave = false;
                            }
                        }

                        if (lTemp3.Count == 8 - lTemp.Count)
                        {
                            break;
                        }

                        if (isHave == false || lTemp.Count == 0)
                        {
                            lTemp3.Add(lTemp2[i]);
                        }
                    }


                    for (int i = 0; i < lTemp3.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = lTemp3[i];
                        dr[1] = 0;
                        dr[2] = 0;
                        dt.Rows.Add(dr);

                    }



                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr;
                        dr = dtTemp.NewRow();
                        dr[0] = dt.Rows[i][0];
                        dr[1] = dt.Rows[i][1];
                        dr[2] = dt.Rows[i][2];
                        dtTemp.Rows.Add(dr);
                    }
                    dt = new DataTable();
                    dt = dtTemp;

                }



            }
            catch (Exception ex)
            {
            }


            return dt;


        }

        private void Save_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (DT_StatisticsTable != null)
                ds.Tables.Add(DT_StatisticsTable.Copy());
            if (AlarmDurationTable != null)
                ds.Tables.Add(AlarmDurationTable.Copy());
            if (AlarmLogTable != null)
                ds.Tables.Add(AlarmLogTable.Copy());
            string filePath = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "请选择保存路径";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                filePath = fbd.SelectedPath;
            }
            else
            {
                return;
            }
            filePath += @"\Alarm" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            DataSetToExcel(ds, filePath);
            //DataTable dt = EPH.ExcelToDataTable(filePath);
            //DataSet dss = EPPlusHelper.ExcelToDataSet(filePath);
        }
        private void AlarmPage_VisibleChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            dTP_EndTime.Value = now;
            dTP_StartTime.Value = now.AddHours(-48);
        }

        #region DownTime Statistics

        private void RefreshDowntimeStatistics(DateTime start, DateTime end)
        {
            DataTable dt = DataServerManager.Instance.SelectDT_Statistic(start, end);

            #region 补齐报警条数；
            dt = AlarmCountAdd(dt);

            #endregion


            DT_StatisticsTable = dt;
            DT_StatisticsTable.TableName = "DownTime Statistics";
            this.dT_Statiistic1.Refresh(dt);
        }





        private void button1_Click(object sender, EventArgs e)
        {
            TestDTControl();
        }
        public void TestDTControl()
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Error", typeof(System.String));
            DataColumn dc1 = new DataColumn("Total Time(Min)", typeof(System.Double));
            DataColumn dc2 = new DataColumn("Count Total", typeof(System.Int32));
            dt.Columns.Add(dc);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            Random rd = new Random();
            for (int i = 0; i < 10; i++)
            {
                dt.Rows.Add(dt.NewRow());

                dt.Rows[i][0] = "Error" + i.ToString();
                dt.Rows[i][1] = rd.Next(10, 100) * 1.0;
                dt.Rows[i][2] = rd.Next(10, 50);
            }
            DT_StatisticsTable = dt;
            DT_StatisticsTable.TableName = "DownTime Statistics Test";

            dT_Statiistic1.Refresh(dt);
        }
        #endregion

        #region AlarmDuration
        private void RefreshAlarmDuration(DateTime start, DateTime end)
        {
            DataTable dt = DataServerManager.Instance.SelectAlarmDurationCategory(start, end);
            AlarmDurationTable = dt;
            AlarmDurationTable.TableName = "AlarmDuration";
            this.alarm_Duration1.Refresh(dt);
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region AlarmLog
        private void RefreshAlarmLog(DateTime start, DateTime end)
        {
            DataTable dt = DataServerManager.Instance.SelectAlarmLog(start, end);
            dt.Columns[0].ColumnName = "Data";
            dt.Columns[1].ColumnName = "Start Time";
            dt.Columns[2].ColumnName = "End Time";
            dt.Columns[3].ColumnName = "Time in State (Min)";
            dt.Columns[4].ColumnName = "Error Code";
            dt.Columns[5].ColumnName = "Error Message";
            dt.Columns[6].ColumnName = "Solution";
            DataTable dtt = new DataTable();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn dc = new DataColumn(dt.Columns[i].ColumnName, typeof(System.String));
                dtt.Columns.Add(dc);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtt.Rows.Add(dtt.NewRow());
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    //dtt.Rows[i][j] =  Convert.ToString(dt.Rows[i][j]) ;
                    if (j == 0 && Convert.ToString(dt.Rows[i][j]).Length > 5)
                        dtt.Rows[i][j] = ((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd");
                    else if (j == 1 && Convert.ToString(dt.Rows[i][j]).Length > 5)
                        dtt.Rows[i][j] = ((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm:ss.ff");
                    else if (j == 2 && Convert.ToString(dt.Rows[i][j]).Length > 5)
                        dtt.Rows[i][j] = ((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm:ss.ff");
                    else
                        dtt.Rows[i][j] = dt.Rows[i][j];

                }
            }

            this.alarmLogShow1.Refresh(dtt);
            AlarmLogTable = dtt;
            AlarmLogTable.TableName = "AlarmLog";
        }
        #endregion

        private Dictionary<int, bool> m_asyncHandled = new Dictionary<int, bool>();
        private void TriggerAlarm_Click(object sender, EventArgs e)
        {
           try
            {
                int i = cb_Alarm.SelectedIndex;
                XAlarmId alarmId;                
                if (cb_Alarm.SelectedIndex >= 0)
                    alarmId = (XAlarmId)Enum.Parse(typeof(XAlarmId), cb_Alarm.Text);
                else
                    return;

                //Liệt kê dữ liệu báo lỗi
                string szDescrip;
                string szErrCode;
                string szType;
                string szOk, szCancel, szIgnore;
                string szDetail = "";
                szDescrip = XAlarmReporter.Instance.SystemAlarms[alarmId].Description;
                szType = XAlarmReporter.Instance.SystemAlarms[alarmId].Category.ToString();
                szErrCode = XAlarmReporter.Instance.SystemAlarms[alarmId].Code.ToString();
                //Cách giải quyết error
                string szSolution = XAlarmReporter.Instance.SystemAlarms[alarmId].Solution.ToString();
                string szAlarmLevel = XAlarmReporter.Instance.SystemAlarms[alarmId].AlarmLevel.ToString();
                szDescrip += ":" + szDetail;

                szOk = XAlarmReporter.Instance.SystemAlarms[alarmId].OkOptionText;
                szCancel = XAlarmReporter.Instance.SystemAlarms[alarmId].CancelOptionText;
                szIgnore = XAlarmReporter.Instance.SystemAlarms[alarmId].IgnoreOptionText;


                if (m_asyncHandled[i])
                {
                    m_asyncHandled[i] = false;
                    //ID, Lỗi nào, Phân loại lỗi
                    HBMachine.Instance.ShowErroAsync(szErrCode, szDescrip, szType, szAlarmLevel, szSolution, szOk, szCancel, szIgnore,
                                                    new CallbackAction(() => { m_asyncHandled[i] = true; return true;}), true);
                }
            }
            catch
            {

            }

        }

        private void btnCloseAll_Click(object sender, EventArgs e)
        {
            NewFailTipShow.Instance.CloseAll();
        }


        //Goi du lieu ra
        private void btn_Query_Click(object sender, EventArgs e)
        {
            DateTime before = dTP_StartTime.Value;
            DateTime now = dTP_EndTime.Value;

            if (now > before)
            {
                RefreshDowntimeStatistics(before, now);
                RefreshAlarmDuration(before, now);
                RefreshAlarmLog(before, now);
            }

        }

        bool isSelectTime = false;

        private void dTP_StartTime_MouseDown(object sender, MouseEventArgs e)
        {
            isSelectTime = true;
        }

        private void dT_Statiistic1_DoubleClick(object sender, EventArgs e)
        {
            isSelectTime = false;
            StatusRefreshTimer.Start();

        }

        private void btn_Save_Click_1(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (DT_StatisticsTable != null)
                ds.Tables.Add(DT_StatisticsTable.Copy());
            if (AlarmDurationTable != null)
                ds.Tables.Add(AlarmDurationTable.Copy());
            if (AlarmLogTable != null)
                ds.Tables.Add(AlarmLogTable.Copy());
            string filePath = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "请选择保存路径";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                filePath = fbd.SelectedPath;
            }
            else
            {
                return;
            }
            filePath += @"\Alarm" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            DataSetToExcel(ds, filePath);
        }
        public static bool DataSetToExcel(DataSet sourceSet, string filePath)
        {

            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
                using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(filePath)))
                {
                    for (int k = 0; k < sourceSet.Tables.Count; k++)
                    {
                        string tableName = sourceSet.Tables[k].TableName;
                        if (tableName.Length <= 0)
                            tableName = k.ToString();
                        ExcelWorksheet ws = package.Workbook.Worksheets.Add(tableName);
                        for (int i = 0; i < sourceSet.Tables[k].Columns.Count; i++)
                        {


                            ws.Cells[1, i + 1].Value = sourceSet.Tables[k].Columns[i].ColumnName.ToString();
                            for (int j = 0; j < sourceSet.Tables[k].Rows.Count; j++)
                            {
                                ws.Cells[j + 2, i + 1].Value = sourceSet.Tables[k].Rows[j][i].ToString();
                            }
                        }
                        package.Save();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void btnCloseAlarm_Click(object sender, EventArgs e)
        {
            HBMachine.Instance.CancelAlarmForm();
        }
    }
}
