using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCore;

namespace Demo.UserControls
{
    public partial class AxisControl : UserControl
    {

        private int m_AxisId;
        private int m_JogStart;
        private XAxis m_Axis;
        private double m_Distance;
        private double m_Vel;
        private double m_Acc = 1000;
        private Timer m_Timer;
        private int taskId = 1;
        private Dictionary<int, PictureBox> pictureBoxMap = new Dictionary<int, PictureBox>();

        Dictionary<string, PLC> axisDictionary;
        public event EventHandler RestEventHandler;
        int Manual_JogFwd, Manual_JogBack,  Manual_InchFwd, Manual_InchBack, Manual_Speed, Manual_InchDistance;
        int Manual_Home, Manual_Stop, Manual_Emg, Manual_ClearError, Manual_ServoOn;
        int Signal_ZeroLimit, Signal_NegativeLimit, Signal_PositiveLimit, Signal_Error, Signal_Moving;
        public AxisControl()
        {
            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            PLC XAxis = new PLC() {
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
                ServoOn1 = 5008
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
                ServoOn1 = 5108
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
                ServoOn1 = 5208
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
                ServoOn1 = 5308
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
            
        }

        class PLC
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
            private int ZeroLimit;
            private int NegativeLimit;
            private int PositiveLimit;
            private int Errors;
            private int Moving;
            private int InitalFinish;
            
            //Register
            private int SpeedJog;
            private int InchDis;
            private double CurrentPosition;


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
        }
    }
}
