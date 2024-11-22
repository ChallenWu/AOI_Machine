using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.UserControls
{
    public partial class STN : UserControl
    {
        public STN()
        {
            InitializeComponent();
            //this.AutoScaleMode = AutoScaleMode.Font;
        }

        public event Action<object, EventArgs> SensorPageCLick;
        public event Action<object, EventArgs> HiveLogCLick;
        public event Action<object, EventArgs> MachineLogCLick;
        public event Action<object, EventArgs> MainSWPathCLick;
        private string _sTN;
        private string _site;
        private string _vender;
        private string _sW_Version;
        private string _main_SW_Path;
        private string _mS_Hash;
        private bool _sensorPageButtonVisible;
        private bool _hiveConnected;
        private bool _mESConnected;
        private bool _pDCAConnected;
        private bool _isRemote;
        private Color _backColor = Color.LightGray;
        //[Category("GUI属性")]
        //[Browsable(true)]
        //[DefaultValue("")]
        [Category("GUI属性"), Description("STNName ")]
        public string STNName 
        {
            get { 
                return _sTN;
            } 
            set { 
                _sTN = value; 
                this.lbl_STN.Text = _sTN; 
            } 
        }
        [Category("GUI属性"), Description("SiteName")]
        public string SiteName { 
            get { 
                return _site;
            } 
            set
            { 
                _site = value;
                this.lbl_Site.Text = _site;
            }
        }
        [Category("GUI属性"), Description("Vender")]
        public string Vender { get { return _vender; } set { _vender = value; this.lbl_Vender.Text = _vender; } }
        [Category("GUI属性"), Description("SW_Version")]
        public string SW_Version { get { return _sW_Version; } set { _sW_Version = value; this.lbl_SW.Text = _sW_Version; } }
        [Category("GUI属性"), Description("Main_SW_Path")]
        public string Main_SW_Path { get { return _main_SW_Path; } set { _main_SW_Path = value; this.lbl_MSP.Text = _main_SW_Path; } }
        [Category("GUI属性"), Description("MS_Hash")]
        public string MS_Hash { get { return _mS_Hash; } set { _mS_Hash = value; this.lbl_MH.Text = _mS_Hash; } }
        [Category("GUI属性"), Description("HIVEConnected")]
        public bool HIVEConnected
        {
            get { return _hiveConnected; }
            set
            {
                _hiveConnected = value;
                if (_hiveConnected)
                {
                    //lbl_Hive.Text = "HIVE Connected";
                    //lbl_Hive.ForeColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                   // lbl_Hive.Text = "HIVE Disconnected";
                   // lbl_Hive.ForeColor = Color.Red;
                }

            }
        }
        [Category("GUI属性"), Description("MESConnected")]
        public bool MESConnected
        {
            get { return _mESConnected; }
            set
            {
                _mESConnected = value;
                if (_mESConnected)
                {
                  //  lbl_MES.Text = "MES Connected";
                  //  lbl_MES.ForeColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                  //  lbl_MES.Text = "MES Disconnected";
                  //  lbl_MES.ForeColor = Color.Red;
                }

            }
        }
        [Category("GUI属性"), Description("PDCAConnected")]
        public bool PDCAConnected
        {
            get { return _pDCAConnected; }
            set
            {
                _pDCAConnected = value;
                if (_pDCAConnected)
                {
                 //   lbl_PDCA.Text = "PDCA Connected";
                 //   lbl_PDCA.ForeColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                   // lbl_PDCA.Text = "PDCA Disconnected";
                   // lbl_PDCA.ForeColor = Color.Red;
                }

            }
        }

        [Category("GUI属性"), Description("SensorPageButtonVisible")]
        public Color NewBackColor
        {
            get { return _backColor; }
            set { _backColor = value; ChangeBackColor(); }
        }
        private void ChangeBackColor()
        {
            this.label10.BackColor = _backColor;
            this.label2.BackColor = _backColor;
            this.label4.BackColor = _backColor;
            this.label6.BackColor = _backColor;
            this.label8.BackColor = _backColor;
          //  this.lbl_Hive.BackColor = _backColor;
          //  this.lbl_MES.BackColor = _backColor;
            this.lbl_MH.BackColor = _backColor;
            this.lbl_MSP.BackColor = _backColor;
          //  this.lbl_PDCA.BackColor = _backColor;
            this.lbl_Site.BackColor = _backColor;
            this.lbl_STN.BackColor = _backColor;
            this.lbl_SW.BackColor = _backColor;
            this.lbl_Vender.BackColor = _backColor;

            //Control.ControlCollection sonControls = this.Controls;
            //foreach (Control control in sonControls)
            //{
            //    if (control is Label)
            //        control.BackColor = this._backColor;
            //}
            //this.OnPaint(pea);
        }
        PaintEventArgs pea;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            pea = e;
            Rectangle rect = new Rectangle(0, 0, this.panel1.Width, this.panel1.Height);
            //Draw(e.ClipRectangle, e.Graphics, 50, ColorTranslator.FromHtml("#d3d3d3"), ColorTranslator.FromHtml("#d3d3d3"));
            //Draw(rect, e.Graphics, 50, ColorTranslator.FromHtml("#d3d3d3"), ColorTranslator.FromHtml("#d3d3d3"));
            Draw(rect, e.Graphics, 50, _backColor, _backColor);
            base.OnPaint(e);

        }
        private void Draw(Rectangle rectangle, Graphics g, int _radius, Color begin_color, Color end_color)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var path = DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - 2, rectangle.Height - 2, _radius);
            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(rectangle, begin_color, end_color, LinearGradientMode.Vertical);
            g.FillPath(myLinearGradientBrush, path);
            g.DrawPath(new Pen(Color.Black, 2), path);
        }
        private static GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            gp.AddArc(x, height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();
            return gp;
        }

        #region 事件 event
        private void SensorPage_Click(object sender, EventArgs e)
        {
            if (SensorPageCLick != null)
                SensorPageCLick(sender, e);
        }

        private void nB_MachineLog_Click(object sender, EventArgs e)
        {
            if (MachineLogCLick != null)
                MachineLogCLick(sender, e);
        }

        private void nB_HiveLog_Click(object sender, EventArgs e)
        {
            if (HiveLogCLick != null)
                HiveLogCLick(sender, e);
        }
        #endregion

        private void panel1_VisibleChanged(object sender, EventArgs e)
        {
            //Rectangle rect = new Rectangle(0, 0, this.panel1.Width, this.panel1.Height);
            //Graphics g = this.CreateGraphics();
            //Draw(rect, g, 50, ColorTranslator.FromHtml("#d3d3d3"), ColorTranslator.FromHtml("#d3d3d3"));
            ////PaintEventArgs ee=this.create
            ////base.OnPaint(e);
            ////base.Paint(e)
        }

        private void lbl_MSP_Click(object sender, EventArgs e)
        {
            if (MainSWPathCLick != null)
                MainSWPathCLick(sender, e);
        }
    }
}
