using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization.Charting.Utilities;
using XCore;
using Demo;

namespace HB_IWatch
{
    public partial class AlarmChart : System.Windows.Forms.UserControl
    {
        private DataPoint[] dataPoints;

        private int[] Pts;


        /// <summary>
        /// 
        /// </summary>
        public int[] DataShowCode
        {
            set
            {
                Pts = value;

                UpdateChartDatas();
            }
        }
        public int[] DataShowCategory
        {
            set
            {
                Pts = value;

                UpdateChartDatasCate();
            }
        }

        public void UpdatAlarmChart(object sender, EventArgs e)
        {
            RbtnCode_Click(sender, e);
        }

        private void UpdateChartDatas()
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    var i = 0;
                    dataPoints = new DataPoint[Pts.Length];
                    chart1.Series[0].Points.Clear();
                    foreach (var item in Pts)
                    {

                        if (item > 0)
                        {
                            dataPoints[i] = new DataPoint(0, (double)item);
                            if (i < 100)
                            {
                                //dataPoints[i].LegendText = MultiLanguage.GetMessage(((XAlarmId)i).ToString());
                                dataPoints[i].LegendText = ((XAlarmId)i).ToString();
                            }
                            else
                            {
                                //dataPoints[i].LegendText = MultiLanguage.GetMessage(((AlarmCategory)i).ToString());
                                dataPoints[i].LegendText = ((AlarmCategory)i).ToString();
                            }


                            dataPoints[i].MarkerStyle = MarkerStyle.Circle;
                            chart1.Series[0].Points.Add(dataPoints[i]);
                        }

                        i++;
                    }
                }
                catch (Exception)
                {
                }

            }));
        }

        private void UpdateChartDatasCate()
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    var i = 0;
                    dataPoints = new DataPoint[Pts.Length];
                    chart1.Series[0].Points.Clear();
                    foreach (var item in Pts)
                    {
                        if (item > 0)
                        {
                            dataPoints[i] = new DataPoint(0, (double)item);

                            dataPoints[i].LegendText = ((AlarmCategory)i).ToString();


                            dataPoints[i].MarkerStyle = MarkerStyle.Circle;

                            chart1.Series[0].Points.Add(dataPoints[i]);
                        }

                        i++;
                    }
                }
                catch (Exception)
                {
                }

            }));
        }

        public AlarmChart()
        {
            InitializeComponent();

            button1.Text = "重置";
        }

        private void PieChartType_Load(object sender, System.EventArgs e)
        {
            RbtnCode_Click(sender, e);
        }

        private void RbtnCode_Click(object sender, EventArgs e)
        {
            RbtnCode.BackColor = MyColor.LightGreen;
            RBtnCateg.BackColor = MyColor.White;
            DataShowCode = DataManager.Instance.alarmRecord.AlarmCodeArr;
        }

        private void RBtnCateg_Click(object sender, EventArgs e)
        {
            RbtnCode.BackColor = MyColor.White;
            RBtnCateg.BackColor = MyColor.LightGreen;
            DataShowCategory = DataManager.Instance.alarmRecord.AlarmCategArr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FailTip ft2 = new FailTip("请输入Administrator权限密码！");
                ft2.SelectAll();
                ft2.SetSubmitText("确认");
                ft2.SetCancelText("取消");

                ft2.ShowDialog();
                ft2.WaitOne();
                if (ft2.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (ft2.GetText() == UserAccountControl.GetAdministratorPassWord())
                    {
                        DataManager.Instance.alarmRecord.Reset();
                        DataManager.Instance.alarmRecord = DataManager.Instance.alarmRecord.Load2();
                        DataManager.Instance.alarmRecord.UpdateDataHandle += UpdatAlarmChart;
                        RbtnCode_Click(sender, e);
                    }
                    else
                    {
                        BzMessagebox.Show(MultiLanguage.GetMessage("密码错误"), MultiLanguage.GetMessage("错误"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;

                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ep)
            {
                BzMessagebox.Show(ep.ToString(), MultiLanguage.GetMessage("错误"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
