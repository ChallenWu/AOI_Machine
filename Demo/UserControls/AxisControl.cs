using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using XCore;

namespace Demo.UserControls
{
    public partial class AxisControl : UserControl
    {
        private CancellationTokenSource _cancellationTokenSource;

        private int m_AxisId;
        private int m_JogStart;
        private XAxis m_Axis;
        private double m_Distance;
        private double m_Vel;
        private double m_Acc = 1000;
        private System.Threading.Timer m_Timer;
        private int taskId = 1;
        private Dictionary<int, PictureBox> pictureBoxMap = new Dictionary<int, PictureBox>();

        public Thread thread;

        public Dictionary<string, PLC> axisDictionary;
        //public event EventHandler RestEventHandler;
        int Manual_JogFwd, Manual_JogBack,  Manual_InchFwd, Manual_InchBack, Manual_Speed, Manual_InchDistance;
        int Manual_Home, Manual_Stop, Manual_Emg, Manual_ClearError, Manual_ServoOn;
        int signal_isServoOn, signal_isHomeOK, signal_ZeroLimit, signal_isNegLimit, signal_isPosLimit, signal_Err, signal_Moving;
        bool m_IsServoOn, m_IsHomeOK,m_ZeroLimit, m_IsNegLimit, m_IsPosLimit, m_Err, m_Moving;
        private bool stopPlcThread;


        public AxisControl()
        {
            InitializeComponent();
            stopPlcThread = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            PLC XAxis = new PLC() {
                //PC > PLC
                Home1 = 5004,
                Stop1 = 5005,
                EMG1 = 5006,
                ClearErrors1 = 5007,
                InchDis1 = 5000,
                SpeedJog1 = 5002,
                JogForwrd1 = 5000,
                JogBack1 = 5001,
                InchBack1 = 5002,
                InchFwd1 = 5003,
                ServoOn1 = 5008,
                //PLC > PC
                IsServoOn = 5000,
                IsHomeOK = 5001,
                IsZeroLimit1 = 5002,
                IsNegativeLimit1 = 5003,
                IsPositiveLimit1 = 5004,
                IsErrors1 = 5005,
                IsMoving = 5006,
                // Register
                CurrentPosition = 5000


            };
            PLC YAxis = new PLC() {
                Home1 = 5104,
                Stop1 = 5105,
                EMG1 = 5106,
                ClearErrors1 = 5107,
                InchDis1 = 5100,
                SpeedJog1 = 5102,
                JogForwrd1 = 5100,
                JogBack1 = 5101,
                InchBack1 = 5102,
                InchFwd1 = 5103,
                ServoOn1 = 5108,
                //Signal 
                IsServoOn = 5100,
                IsHomeOK = 5101,
                IsZeroLimit1 =5102,
                IsNegativeLimit1 = 5103,
                IsPositiveLimit1 = 5104,
                IsErrors1 = 5105,
                IsMoving = 5106,
                // Register
                CurrentPosition = 5100

            };
            PLC ZAxis = new PLC() {
                Home1 = 5204,
                Stop1 = 5205,
                EMG1 = 5206,
                ClearErrors1 = 5207,
                InchDis1 = 5200,
                SpeedJog1 = 5202,
                JogForwrd1 = 5200,
                JogBack1 = 5201,
                InchBack1 = 5202,
                InchFwd1 = 5203,
                ServoOn1 = 5208,
                //Signal 
                IsServoOn = 5200,
                IsHomeOK = 5201,
                IsZeroLimit1 = 5202,
                IsNegativeLimit1 = 5203,
                IsPositiveLimit1 = 5204,
                IsErrors1 = 5205,
                IsMoving = 5206,
                // Register
                CurrentPosition = 5200

            };
            PLC RAxis = new PLC() {
                Home1 = 5304,
                Stop1 = 5305,
                EMG1 = 5306,
                ClearErrors1 = 5307,
                InchDis1 = 5300,
                SpeedJog1 = 5302,
                JogForwrd1 = 5300,
                JogBack1 = 5301,
                InchBack1 = 5302,
                InchFwd1 = 5303,
                ServoOn1 = 5308,
                //Signal 
                IsServoOn = 5300,
                IsHomeOK = 5301,
                IsZeroLimit1 = 5302,
                IsNegativeLimit1 = 5303,
                IsPositiveLimit1 = 5304,
                IsErrors1 = 5305,
                IsMoving = 5306,
                // Register
                CurrentPosition = 5300

            };

            axisDictionary = new Dictionary<string, PLC>
                {
                    { "XAxis", XAxis },
                    { "YAxis", YAxis },
                    { "ZAxis", ZAxis },
                    { "RAxis", RAxis }
                };

            this.Comb_AxisNo.SelectedIndex = 0;

            pictureBoxMap.Add(0, PB_SVON);
            pictureBoxMap.Add(1, PB_MEL);
            pictureBoxMap.Add(2, PB_ORG);
            pictureBoxMap.Add(3, PB_PEL);
            pictureBoxMap.Add(4, PB_ALM);
            pictureBoxMap.Add(5, PB_ASTP);

            foreach (PictureBox pb in pictureBoxMap.Values)
            {
                pb.BackgroundImageLayout = ImageLayout.Center;
                pb.BackgroundImage = Properties.Resources._lampGray20;
            }
            InitialDistance();
            InitialVel();
            ChangeButton_Image(Comb_AxisNo.SelectedItem.ToString());
            InitialDatagridView();
        }


        public class PLC
        {
            
            //ActiveButton
            private int InchFwd;
            private int InchBack;
            private int JogForwrd;
            private int JogBack;
            private int Home;
            private int Stop;
            private int EMG;
            private int ClearErrors;
            private int ServoOn;
            //Signal
            private int IsZeroLimit;
            private int IsNegativeLimit;
            private int IsPositiveLimit;
            private int IsErrors;
            private int isMoving;
            private int isHomeOK;
            private int isServoOn;
            
            //Register
            private int SpeedJog;
            private int InchDis;
            private int currentPosition;


            public int JogForwrd1 { get => JogForwrd; set => JogForwrd = value; }
            public int JogBack1 { get => JogBack; set => JogBack = value; }
            public int Home1 { get => Home; set => Home = value; }
            public int Stop1 { get => Stop; set => Stop = value; }
            public int EMG1 { get => EMG; set => EMG = value; }
            public int SpeedJog1 { get => SpeedJog; set => SpeedJog = value; }
            public int InchDis1 { get => InchDis; set => InchDis = value; }
            public int InchFwd1 { get => InchFwd; set => InchFwd = value; }
            public int InchBack1 { get => InchBack; set => InchBack = value; }
            public int ClearErrors1 { get => ClearErrors; set => ClearErrors = value; }
            public int ServoOn1 { get => ServoOn; set => ServoOn = value; }
            public int IsZeroLimit1 { get => IsZeroLimit; set => IsZeroLimit = value; }
            public int IsNegativeLimit1 { get => IsNegativeLimit; set => IsNegativeLimit = value; }
            public int IsPositiveLimit1 { get => IsPositiveLimit; set => IsPositiveLimit = value; }
            public int IsErrors1 { get => IsErrors; set => IsErrors = value; }
            public int IsMoving { get => isMoving; set => isMoving = value; }
            public int IsHomeOK { get => isHomeOK; set => isHomeOK = value; }
            public int IsServoOn { get => isServoOn; set => isServoOn = value; }
            public int CurrentPosition { get => currentPosition; set => currentPosition = value; }
        }

        private void bt_JOG_N_MouseDown(object sender, MouseEventArgs e)
        {
            if (Comb_AxisNo.Items.Count > 0)
            { 
                SLMP.Instance.WriteDoubleWord(DevideCode.D, Manual_Speed, Bar_Vel.Value);
                SLMP.Instance.WriteBit(DevideCode.M, Manual_JogBack, true);
            }
        }

        private void Bar_Vel_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = Bar_Vel.Value.ToString() + "mm/s";
        }

        private void bt_JOG_N_MouseUp(object sender, MouseEventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.M, Manual_JogBack, false);
        }

        private void bt_JOG_P_MouseDown(object sender, MouseEventArgs e)
        {
            if (Comb_AxisNo.Items.Count > 0)
            {
                SLMP.Instance.WriteDoubleWord(DevideCode.D, Manual_Speed, Bar_Vel.Value);
                SLMP.Instance.WriteBit(DevideCode.M, Manual_JogFwd, true);
            }
        }

        private void bt_JOG_P_MouseUp(object sender, MouseEventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.M, Manual_JogFwd, false);
        }

        private void btn_ClearError_Click(object sender, EventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.M, Manual_Stop, true);
        }

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.M, Manual_Stop, true);
        }

        private void PB_SVON_Click(object sender, EventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.M, Manual_Stop, true);
        }

        private void Btn_Forward_Click(object sender, EventArgs e)
        {
            if (Comb_AxisNo.Items.Count > 0)
            {
                SLMP.Instance.WriteWord(DevideCode.D, Manual_InchDistance, Convert.ToInt16(Comb_Distance.SelectedItem));
                SLMP.Instance.WriteBit(DevideCode.M, Manual_InchFwd, true);
            }
            else
                return;
        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            if (Comb_AxisNo.Items.Count > 0)
            {
                SLMP.Instance.WriteWord(DevideCode.D, Manual_InchDistance, Convert.ToInt16(Comb_Distance.SelectedItem));
                SLMP.Instance.WriteBit(DevideCode.M, Manual_InchFwd, false);
            }
        }

        private void Btn_Home_Click(object sender, EventArgs e)
        {
            if (Comb_AxisNo.Items.Count > 0)
            {
                SLMP.Instance.WriteBit(DevideCode.M, Manual_Home, true);
            }
        }

        private void InitialDistance()
        {
            Comb_Distance.Items.Add(1);
            Comb_Distance.Items.Add(10);
            Comb_Distance.Items.Add(200);
            Comb_Distance.SelectedIndex = 1;
        }

        private void InitialVel()
        {
            this.Bar_Vel.SetRange(0, 200);
            this.Bar_Vel.Value = 10;
            this.label1.Text = "10mm/s";
            m_Vel = 10;
            textBox_Acc.Text = "1000";
        }

        private void ChangeButton_Image(string axisNow)
        {
            switch (axisNow)
            {
                case "XAxis":
                    this.Btn_Forward.Image = Properties.Resources._right;
                    this.Btn_Back.Image = Properties.Resources._left;
                    break;
                case "YAxis":
                    this.Btn_Forward.Image = Properties.Resources._right;
                    this.Btn_Back.Image = Properties.Resources._left;
                    break;
                case "RAxis":
                    this.Btn_Forward.Image = Properties.Resources._rotate_clock;
                    this.Btn_Back.Image = Properties.Resources._rotate_antiClock;
                    break;
                case "ZAxis":
                    this.Btn_Forward.Image = Properties.Resources._up;
                    this.Btn_Back.Image = Properties.Resources._down;
                    break;
                default:
                    this.Btn_Forward.Image = Properties.Resources._left;
                    this.Btn_Back.Image = Properties.Resources._right;
                    break;
            }
        }

        private void Comb_AxisNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAxis = Comb_AxisNo.SelectedItem.ToString();
            if (axisDictionary.ContainsKey(selectedAxis))
            {
                PLC selectedPLC = axisDictionary[selectedAxis];
                UpdateLabels(selectedPLC);
                ChangeButton_Image(Comb_AxisNo.SelectedItem.ToString());
            }
        }

        private void UpdateLabels(PLC plc)
        {
            this.Manual_JogFwd = plc.JogForwrd1;
            this.Manual_JogBack = plc.JogBack1;
            this.Manual_InchBack = plc.InchBack1;
            this.Manual_InchFwd = plc.InchFwd1;
            this.Manual_Home = plc.Home1;
            this.Manual_Stop = plc.Stop1;
            this.Manual_Emg = plc.EMG1;
            this.Manual_Speed = plc.SpeedJog1;
            this.Manual_InchDistance = plc.InchDis1;
            this.Manual_ClearError = plc.ClearErrors1;
            this.Manual_ServoOn = plc.ServoOn1;

            //Signal
            this.signal_isServoOn = plc.ServoOn1;
            this.signal_ZeroLimit = plc.IsZeroLimit1;
            this.signal_Moving = plc.IsMoving;
            this.signal_isNegLimit = plc.IsNegativeLimit1;
            this.signal_isPosLimit = plc.IsPositiveLimit1;
            this.signal_Err = plc.IsErrors1;
            this.signal_isHomeOK = plc.Home1;
        }

        // ServoOn, HomeOK, Limit +, ,Origin , Limit - , Alarm, Moving
        private string[] ColumnsHeaderText = new string[10] { "Id", "Name", "POS", "SVON", "HMOK", "MEL", "ORG", "PEL", "ALM", "ASTP" };
        private int[] ColumnsWidth = new int[8] { 22, 22, 22, 22, 22, 22, 22, 30 };
        private void InitialDatagridView()
        {
            try
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToOrderColumns = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.RowHeadersVisible = false;

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                //Tạo số cột
                for (int i = 3; i < ColumnsHeaderText.Length; i++)
                {
                    dataGridView1.Columns.Add(new DataGridViewImageColumn());
                }
                //Tạo chiều dài và header từng cột
                dataGridView1.Columns[0].Width = 15;
                for (int i = 0; i < ColumnsHeaderText.Length; i++)
                {
                    dataGridView1.Columns[i].HeaderText = ColumnsHeaderText[i];
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    if (i > 2)
                    {
                        dataGridView1.Columns[i].Width = ColumnsWidth[i - 3];
                    }
                }
                //Thêm dữ liệu vào từng hàng và cột trong header
                InitDgv();
               
            }
            catch (Exception ex)
            {

            }            
        }

        public void DateUpdate()
        {
            try
            {
                while (stopPlcThread)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    if (Comb_AxisNo.Items.Count > 0)
                    {
                        bool[] axisSts = new bool[] { m_IsServoOn, m_IsNegLimit, m_IsHomeOK, m_IsPosLimit, m_Err, m_Moving };

                        SLMP.Instance.ReadBit(DevideCode.M, signal_isServoOn, out m_IsServoOn);
                        SLMP.Instance.ReadBit(DevideCode.M, signal_isHomeOK, out m_IsHomeOK);
                        SLMP.Instance.ReadBit(DevideCode.M, signal_isPosLimit, out m_IsPosLimit);
                        SLMP.Instance.ReadBit(DevideCode.M, signal_isNegLimit, out m_IsNegLimit);
                        SLMP.Instance.ReadBit(DevideCode.M, signal_ZeroLimit, out m_ZeroLimit);
                        SLMP.Instance.ReadBit(DevideCode.M, signal_Moving, out m_Moving);
                        SLMP.Instance.ReadBit(DevideCode.M, signal_Err, out m_Err);

                        LB_AxisNo.Text = m_AxisId.ToString() + ":";



                        for (int i = 0; i < 6; i++)
                        {
                            if (i < 5)
                            {
                                if (axisSts[i] == true)
                                {
                                    if (i == 0)
                                    {
                                        pictureBoxMap[i].BackgroundImage = Properties.Resources._lampGreen20;
                                    }
                                    else
                                    {
                                        pictureBoxMap[i].BackgroundImage = Properties.Resources._lampRed20;
                                    }
                                }
                                else
                                {
                                    pictureBoxMap[i].BackgroundImage = Properties.Resources._lampGray20;
                                }
                            }
                            else
                            {
                                if (axisSts[i] == true)
                                {
                                    LB_Home.Text = MultiLanguage.GetMessage("Initial OK");
                                    LB_Home.ForeColor = Color.Black;
                                }
                                else
                                {
                                    LB_Home.Text = MultiLanguage.GetMessage("Not Initial");
                                    LB_Home.ForeColor = Color.Red;
                                }
                            }
                        }
                    }

                    foreach (var item in axisDictionary)
                    {
                        string axisId = item.Key;
                        //bool IsSVON;
                        if (axisDictionary.TryGetValue(axisId, out PLC plc))
                        {
                            bool IsSVON, IsHomeOK, IsPositiveLimit1, IsNegativeLimit1, IsZeroLimit1, IsMoving, IsErrors1;
                            int curPos;
                            // Read each bit status from the device
                            SLMP.Instance.ReadBit(DevideCode.M, plc.IsServoOn, out IsSVON);
                            SLMP.Instance.ReadBit(DevideCode.M, plc.IsHomeOK, out IsHomeOK);
                            SLMP.Instance.ReadBit(DevideCode.M, plc.IsPositiveLimit1, out IsPositiveLimit1);
                            SLMP.Instance.ReadBit(DevideCode.M, plc.IsNegativeLimit1, out IsNegativeLimit1);
                            SLMP.Instance.ReadBit(DevideCode.M, plc.IsZeroLimit1, out IsZeroLimit1);
                            SLMP.Instance.ReadBit(DevideCode.M, plc.IsMoving, out IsMoving);
                            SLMP.Instance.ReadBit(DevideCode.M, plc.IsErrors1, out IsErrors1);
                            SLMP.Instance.ReadDoubleWord(DevideCode.D, plc.CurrentPosition, out curPos);



                            stsMap[axisId] = new bool[7]
                            {
                        IsSVON, IsHomeOK, IsPositiveLimit1, IsNegativeLimit1, IsZeroLimit1, IsMoving, IsErrors1
                            };

                            //stsMap[axisId][0] = IsSVON;

                            foreach (DataGridViewRow dr in dataGridView1.Rows)
                            {
                                if (dr.Cells[1].Value?.ToString() == axisId) // Ensure axisId matches the row
                                {
                                    dr.Cells[2].Value = curPos;
                                    for (int i = 0; i < 7; i++)
                                    {
                                        // Update cell visuals based on the status transitions
                                        if (stsMap[axisId][i] && !lastStsMap[axisId][i])
                                        {
                                            //dr.Cells[i + 3].Value = (i < 3) ? Properties.Resources._lampGreen20 : Properties.Resources._lampRed20;
                                            dr.Cells[i + 3].Value = Properties.Resources._lampGreen20;
                                        }
                                        else if (!stsMap[axisId][i] && lastStsMap[axisId][i])
                                        {
                                            dr.Cells[i + 3].Value = Properties.Resources._lampGray20;
                                        }
                                    }
                                    lastStsMap[axisId] = new bool[7];
                                    Array.Copy(stsMap[axisId], lastStsMap[axisId], 7);
                                    break;
                                }
                            }
                        }
                        sw.Stop();
                        Console.WriteLine("CT read data from Plc {0}", sw.ElapsedMilliseconds);
                        Thread.Sleep(500);
                    }
                    Thread.Sleep(500);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private Dictionary<string, bool[]> stsMap = new Dictionary<string, bool[]>();
        private Dictionary<string, bool[]> lastStsMap = new Dictionary<string, bool[]>();

        public bool StopPlcThread { get => stopPlcThread; set => stopPlcThread = value; }

        private void InitDgv()
        {
            try
            {
                foreach (var item in axisDictionary)
                {
                    dataGridView1.Rows.Clear();
                    stsMap.Clear(); 
                    lastStsMap.Clear();
                    foreach (var kvp in axisDictionary)
                    {
                        DataGridViewRow dr = new DataGridViewRow();
                        dr.Cells.Add(new DataGridViewTextBoxCell());
                        dr.Cells.Add(new DataGridViewTextBoxCell());
                        dr.Cells.Add(new DataGridViewTextBoxCell());
                        dr.Cells[1].Value = kvp.Key;
                        dr.Cells[2].Value = 0;
                        for (int i = 3; i < 10; i++)
                        {
                            dr.Cells.Add(new DataGridViewImageCell());
                            dr.Cells[i].Value = Properties.Resources._lampGray20;
                        }
                        dataGridView1.Rows.Add(dr);
                        stsMap.Add(kvp.Key, new bool[7] { false, false, false, false, false, false, false });
                        lastStsMap.Add(kvp.Key, new bool[7] { false, false, false, false, false, false, false });
                    }
                }
            }
            catch
            {

            }
        }
    }
}
