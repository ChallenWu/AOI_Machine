using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCTest
{
    public class DoubleTrackBar : Control
    {

        #region 变量
        private const string DateTimeFormate = "MM/dd/yyyy HH:mm:ss";//定义时间格式；
        private int _checkDays = 7;//数据回溯日期；
        private DateTime _dateStartValue1;//日期回溯起始值；
        private DateTime _dateEndValue2;//日期回溯终值；
        private DateTime _temStartMouseDown;
        private DateTime _temStartMouseUp;
        private DateTime _temEndMouseDown;
        private DateTime _temEndMouseUp;

        private string _dtNewestTime = DateTime.Now.ToString(DateTimeFormate);//获取数据库dataTable里的当前时间，此时间是滑动条最新值；
        private int _TrackBarFontSize = 8;//滑动条日期字体大小设置，默认8号字体大小；

        //private const int m_MinSize = 10;//最小控件尺寸； 
        //private const int m_TrackSize = 3;//轨道宽度；

        private const int m_MinSize = 1;//最小控件尺寸； 
        private const int m_TrackSize = 1;//轨道宽度；


        private int _controlHeight = 10;//默认控件高度为10；
        private string _errorMessage; //报错信息，可读可写属性；
        

        private emBtnStatus m_btnStatus = emBtnStatus.None; //按钮状态；按下、松开、无、移动中；
        private double m_dbRate = 0;  //比例
        private Point m_lastMouseLocation;   //鼠标坐标最后位置
        private double m_dbMinimum = 0;  //滑动条最小值；不可修改；
        private double m_dbMaximum = 10000;  //滑动条最大值；不可修改；
        private double m_dbValue1 = 0;  //滑块1初始值；
        private double m_dbValue2 = 10000;//滑块2初始值；
        #endregion




        #region 事件属性；
        [Description("滑块滑动事件")]
        public Action<DateTime , DateTime> ValueChanged_Event;


        public event Action Value1Changed;
        public event Action Value2Changed;
        public event Action ValueChanged;
        public event Action<DateTime, DateTime> UpdateDT;

        /// <summary>
        /// 数据查询起始时间；
        /// </summary>
        [Description("Data backtracking time 1")]
        public DateTime DateStartValue1
        {
            get { return _dateStartValue1; }
            set
            {
                _dateStartValue1 = value;
                Value1 = DateTimeToSlidePos(_dateStartValue1);
            }

        }



        /// <summary>
        /// 数据查询第二段时间；
        /// </summary>
        [Description("Data backtracking time 2")]
        public DateTime DateEndValue2
        {
            get { return _dateEndValue2; }
            set
            {
                _dateEndValue2 = value;
                Value2 = DateTimeToSlidePos(_dateEndValue2);
            }

        }


        public double Value1
        {
            get { return m_dbValue1; }
            set
            {
                if (value <= Value2 && value >= Minimum)
                {
                    m_dbValue1 = value;
                    if (UpdateDT != null)
                    {
                        UpdateDT(_dateStartValue1, _dateEndValue2);
                    }
                    this.Invalidate();
                }
            }
        }


        public double Value2
        {
            get { return m_dbValue2; }
            set
            {
                if (value >= Value1 && value <= Maximum)
                {
                    m_dbValue2 = value;
                    if (UpdateDT != null)
                    {
                        UpdateDT(_dateStartValue1, _dateEndValue2);
                    }
                    this.Invalidate();
                }
            }
        }



        /// <summary>
        /// 数据回溯天数；
        /// </summary>
        [Description("Data back-dating days")]
        public int CheckDays
        {
            get { return _checkDays; }
            set
            {
                if (value > 0)
                {
                    _checkDays = value;
                }
            }
        }


 


        /// <summary>
        /// 设备状态数据表；
        /// </summary>
        [Description("Latest time of data source")]
        public string  DataTableNewestTime
        {
            get
            {
                if (_dtNewestTime == null || _dtNewestTime == string.Empty|| _dtNewestTime.Contains("0001"))
                {
                    _dtNewestTime = DateTime.Now.ToString(DateTimeFormate);
                }
                return _dtNewestTime;
            }
            set
            {
       
                _dtNewestTime = value;
                if (_dtNewestTime.Contains("0001"))
                {
                    _dtNewestTime = DateTime.Now.ToString(DateTimeFormate);
                }
                base.Refresh();
            }
        }




        [Description("Rails date font size")]
        public int TrackBarFontSize
        {
            get { return _TrackBarFontSize; }
            set
            {
                _TrackBarFontSize = value;

            }
        }



        public override Size MinimumSize
        {
            get
            {
                Size rlt = base.MinimumSize;
                if (this.Orientation == Orientation.Horizontal)
                {
                    rlt = new Size(m_MinSize, ControlHeight);
                }
                else
                {
                    rlt = new Size(ControlHeight, m_MinSize);
                }
                return rlt;
            }
            set
            {
                base.MinimumSize = value;
            }
        }



        [Description("Is slider 2 available?")]
        public bool IsSlider2Enable
        {
            get;
            set;
        } = true;

        private enum emBtnStatus
        {
            None,
            Btn1MouseIn,
            Btn2MouseIn,
            Btn1MouseDown,
            Btn2MouseDown,
        }



        /// <summary>
        /// Width (vertical)/height (horizontal) of the control
        /// </summary>
        [Description(" Kiểm soát chiều cao (ngang)/chiều rộng (dọc)")]
        public int ControlHeight
        {
            get { return _controlHeight; }
            set
            {
                if (_controlHeight < 1)
                { _controlHeight = 10; }
                _controlHeight = value;
                if (this.Orientation == Orientation.Horizontal)
                {
                    Size = new Size(Width, _controlHeight);
                }
            }
        }




        public double Minimum
        {
            get { return m_dbMinimum; }
            //set
            //{
            //    if (value <= Maximum)
            //    {
            //        m_dbMinimum = value;
            //        this.Invalidate();
            //    }
            //}
        }


        public double Maximum
        {
            get { return m_dbMaximum; }
            //set
            //{
            //    if (value >= Minimum)
            //    {
            //        m_dbMaximum = value;
            //        this.Invalidate();
            //    }
            //}
        }


       



        private Orientation m_Orientation = Orientation.Horizontal;

        public Orientation Orientation
        {
            get { return m_Orientation; }
            set
            {
                if (m_Orientation != value)
                {
                    m_Orientation = value;
                    this.Invalidate();
                }
            }
        }


        public bool TickLabelVisible { get; set; } = false;


        public uint LabelPlaces { get; set; } = 1;

        /// <summary>
        /// 键1
        /// </summary>
        private Rectangle m_rectBtn1;


        /// <summary>
        /// 键2
        /// </summary>
        private Rectangle m_rectBtn2;


        public enum emTrackBarSelectedMode
        {
            //选中两个滑块内部的值；
            Inner,
            //选中两个滑块外部的值；
            Outer,
        }

        /// <summary>
        /// 刻度线个数
        /// </summary>
        public int TickCount { get; set; } = 5;

        public Color TickColor { get; set; } = Color.Black;

        /// <summary>
        /// 读两个滑块之间的值；
        /// </summary>
        public emTrackBarSelectedMode TrackSelectedMode
        {
            get;
            set;
        } = emTrackBarSelectedMode.Inner;


        public Color SelectTrackColor { get; set; } = Color.DarkGray;
        public Color TrackColor { get; set; } = Color.White;
        public Color TrackButtonColor1 { get; set; } = Color.LightGray;
        public Color TrackButtonColor2 { get; set; } = Color.LightGray;
        public Color TrackButtonClickColor { get; set; } = Color.Green;


        [Browsable(true)]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set
            {
                base.AutoSize = value;
            }
        }


        /// <summary>
        /// 滑块的大小；
        /// </summary>
        public Size TrackButtonSize
        {
            get { return new Size(12, 12); }
            set
            {
                this.Refresh();
            }
             
        }

        #endregion




        public DoubleTrackBar()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            CreateControl();            
        }


        #region 方法；
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (m_rectBtn1.Contains(e.X, e.Y))
            {
                m_btnStatus = emBtnStatus.Btn1MouseDown;
                m_lastMouseLocation = e.Location;
                this.Invalidate();
            }
            else if (m_rectBtn2.Contains(e.X, e.Y))
            {
                m_btnStatus = emBtnStatus.Btn2MouseDown;
                m_lastMouseLocation = e.Location;
                this.Invalidate();
            }
            else if (m_rectBtn1.Contains(e.X, e.Y) && m_rectBtn2.Contains(e.X, e.Y))
            {

                if (Value2 <= Maximum)
                {
                    m_btnStatus = emBtnStatus.Btn1MouseDown;
                    m_lastMouseLocation = e.Location;
                    this.Invalidate();
                }

                if (Value1 >= Minimum)
                {
                    m_btnStatus = emBtnStatus.Btn2MouseDown;
                    m_lastMouseLocation = e.Location;
                    this.Invalidate();
                }
            }
            this._temEndMouseDown = this._dateEndValue2;
            this._temStartMouseDown = this._dateStartValue1;
        }



        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_btnStatus = emBtnStatus.None;



         
            if (ValueChanged_Event != null)
            { ValueChanged_Event(DateStartValue1, DateEndValue2); }

            this._temEndMouseUp = this._dateEndValue2;
            this._temStartMouseUp = this._dateStartValue1;

            if((_temEndMouseUp!=_temEndMouseDown)&& Value2Changed != null)
                Value2Changed();

            if ((_temStartMouseUp != _temStartMouseDown) && Value1Changed != null)
                Value1Changed();

            if(((_temStartMouseUp != _temStartMouseDown)|| (_temEndMouseUp != _temEndMouseDown))&& ValueChanged != null)
                ValueChanged();
              

        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_btnStatus == emBtnStatus.Btn1MouseDown)
            {
                if (m_dbRate != 0)
                {
                    this.Value1 += GetMouseChangedValue(e.Location, m_lastMouseLocation);
                }
                m_lastMouseLocation = e.Location;
            }


            else if (m_btnStatus == emBtnStatus.Btn2MouseDown  && IsSlider2Enable)
            {
                if (m_dbRate != 0)
                {
                    this.Value2 += GetMouseChangedValue(e.Location, m_lastMouseLocation);
                }
                m_lastMouseLocation = e.Location;
            }

            else if (m_rectBtn1.Contains(e.X, e.Y))
            {
                if (m_btnStatus != emBtnStatus.Btn1MouseIn)
                {
                    m_btnStatus = emBtnStatus.Btn1MouseIn;
                    this.Refresh();
                }
            }
            else if (m_rectBtn2.Contains(e.X, e.Y))
            {
                if (m_btnStatus != emBtnStatus.Btn2MouseIn)
                {
                    m_btnStatus = emBtnStatus.Btn2MouseIn;
                    this.Refresh();
                }
            }

            else
            {
                if (m_btnStatus != emBtnStatus.None)
                {
                    m_btnStatus = emBtnStatus.None;
                    this.Refresh();
                }
            }
        }


        /*滑块与时间的关系*/
        private double DateTimeToSlidePos(DateTime dtValue)
        {
            DateTime dtTemp = dtValue;
            IFormatProvider culture = new CultureInfo("en-US", true);
            DateTime dtNew = DateTime.ParseExact(_dtNewestTime, DateTimeFormate, culture);
            DateTime dtTempStart = dtNew.AddDays(-7);
            double rate = 0.0;
            int weekSec = 7 * 24 * 3600;
            rate = 10000.0 / (weekSec);
            double iSec = dtTemp.Subtract(dtTempStart).TotalSeconds;
            double dLength = 0.0;
            dLength = iSec * rate;
            if (dLength > 10000)
            { dLength = 10000; }
            if (dLength < 0)
            {
                dLength = 0;
            }
            return Math.Round(dLength, 1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            this.SizeChanged -= OnLhxTrackBar2SizeChanged;
            base.OnPaint(e);
            e.Graphics.Clear(this.BackColor);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            if (this.Orientation == Orientation.Horizontal)
            {
                OnPaintHorizontal(e);
            }
            else
            {
                OnPaintVertical(e);
            }

            this.SizeChanged += OnLhxTrackBar2SizeChanged;

        }


        private void CheckValue()
        {
            if (this.m_dbMaximum > Maximum)
            {
                m_dbMaximum = Maximum;
            }

            if (m_dbMinimum < Minimum)
            {
                m_dbMinimum = Minimum;
            }
        }


        private double GetMouseChangedValue(Point nowLocation, Point oldLocation)
        {
            double rlt = 0;
            if (this.Orientation == Orientation.Horizontal)
            {
                rlt = nowLocation.X - oldLocation.X;
            }
            else
            {
                rlt = nowLocation.Y - oldLocation.Y;
            }
            return rlt / m_dbRate;
        }



        private void GetMaximumPaintInfo(Graphics g, out string text, out SizeF size)
        {
            GetValuePaintInfo(g, Maximum, out text, out size);
        }


        private void GetMinimumPaintInfo(Graphics g, out string text, out SizeF size)
        {
            GetValuePaintInfo(g, Minimum, out text, out size);
        }


        private void GetValuePaintInfo(Graphics g, double value, out string text, out SizeF size)
        {
            text = value.ToString($"f{LabelPlaces}");
            size = g.MeasureString(text, this.Font);
        }


        private void OnLhxTrackBar2SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }


        /// <summary>
        /// 水平
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPaintHorizontal(PaintEventArgs e)
        {
            double left = 0;
            double right = this.Width;
             
           
            int btnSize = TrackButtonSize.Width;
            try
            {
                //if (this.AutoSize)
                //{
                //    this.Height = ControlHeight;
                //}

                #region 画滑块
                Pen pen_track = new Pen(new SolidBrush(TrackColor), 1);
                int trackTop = Height- TrackButtonSize.Height; //滑轨顶部坐标；
                int trackHeight = m_TrackSize;//滑轨高度；

                int trackBtnTop = trackTop - (TrackButtonSize.Height - trackHeight) / 2;
                int trackBtnBottom = trackBtnTop + trackHeight + (TrackButtonSize.Height - trackHeight) / 2; //滑块高度；
                int trackBtnHeight = trackBtnBottom - trackBtnTop;


                //画空滑轨
                e.Graphics.DrawRectangle(pen_track, new Rectangle((int)left, trackTop, (int)(right - left), trackHeight));
                e.Graphics.FillRectangle(new SolidBrush(TrackColor), new Rectangle((int)left, trackTop, (int)(right - left), trackHeight));
                //画选中的滑块部分
                double rate = (right - left) / (Maximum - Minimum);
                m_dbRate = rate;

                int value1 = (int)((Value1 - Minimum) * rate);
                int value2 = (int)((Value2 - Minimum) * rate);
                int value1_x = (int)(left + value1);
                int value2_x = (int)(left + value2);

                //画选中部分
                if (TrackSelectedMode == emTrackBarSelectedMode.Inner)
                {
                    e.Graphics.FillRectangle(new SolidBrush(SelectTrackColor), new Rectangle((int)(value1_x), trackTop, (int)(value2 - value1), trackHeight + 2));
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(SelectTrackColor), new Rectangle((int)(left), trackTop, (int)(value1), m_TrackSize));
                    e.Graphics.FillRectangle(new SolidBrush(SelectTrackColor), new Rectangle((int)(value2_x), trackTop, (int)(right - value2 - left), trackHeight));
                }


                //画左侧滑块
                e.Graphics.FillRectangle(new SolidBrush(m_btnStatus == emBtnStatus.Btn1MouseDown || m_btnStatus == emBtnStatus.Btn1MouseIn ? TrackButtonClickColor : Color.DarkGray),
                   new Rectangle((int)(value1_x - TrackButtonSize.Width / 2), trackBtnTop, (int)(TrackButtonSize.Width), TrackButtonSize.Height));
                if(IsSlider2Enable)
                {
                    e.Graphics.FillRectangle(new SolidBrush(m_btnStatus == emBtnStatus.Btn2MouseDown || m_btnStatus == emBtnStatus.Btn2MouseIn ? TrackButtonClickColor : Color.DarkGray),
                   new Rectangle((int)(value2_x - TrackButtonSize.Width / 2), trackBtnTop, (int)(TrackButtonSize.Width), TrackButtonSize.Height));

                }

                m_rectBtn1 = new Rectangle(value1_x - TrackButtonSize.Width / 2, trackBtnTop, TrackButtonSize.Width, TrackButtonSize.Height);
                m_rectBtn2 = new Rectangle(value2_x - TrackButtonSize.Width / 2, trackBtnTop, TrackButtonSize.Width, TrackButtonSize.Height);

                #endregion

                #region 
                double dValue1Rate = Math.Round(Value1 / (Maximum - Minimum), 10);
                double dValue2Rate = Math.Round(Value2 / (Maximum - Minimum), 10);


                IFormatProvider culture = new CultureInfo("en-US", true);
                DateTime dtCheckEnd = DateTime.ParseExact(_dtNewestTime, DateTimeFormate, culture);
                DateTime dtCheckStart = dtCheckEnd.AddDays(-_checkDays);
                double iSecondValue1 = dValue1Rate * _checkDays * 24 * 3600;
                double iSecondValue2 = dValue2Rate * _checkDays * 24 * 3600;
                DateTime dTimeValue1 = dtCheckStart.AddSeconds(iSecondValue1);
                _dateStartValue1 = dTimeValue1;
                DateTime dTimeValue2 = dtCheckStart.AddSeconds(iSecondValue2);
                _dateEndValue2 = dTimeValue2;


                //IFormatProvider culture = new CultureInfo("en-US", true);
                //DateTime dtCheckEnd = DateTime.ParseExact(_dtNewestTime, DateTimeFormate, culture);
                //DateTime dtCheckStart = dtCheckEnd.AddDays(-_checkDays);
                //double iSecondValue1 = dValue1Rate * _checkDays * 24;
                //double iSecondValue2 = dValue2Rate * _checkDays * 24;
                //DateTime dTimeValue1 = dtCheckStart.AddHours(iSecondValue1);
                //_dateStartValue1 = dTimeValue1;
                //DateTime dTimeValue2 = dtCheckStart.AddHours(iSecondValue2);
                //_dateEndValue2 = dTimeValue2;


                _errorMessage = "";
                #endregion


            }
            catch (Exception ex)
            {
                _errorMessage = ex.ToString();

            }

        }


        protected virtual void OnPaintVertical(PaintEventArgs e)
        {
            ///如果是垂直放置滑动条需要修改；
        }


        public void SetMinMax(double minimum, double maximum)
        {
            if (minimum < maximum)
            {
                this.m_dbMinimum = minimum;
                this.m_dbMaximum = maximum;
                CheckValue();
                this.Invalidate();
            }
        }


        public void SetValue(double value1, double value2)
        {
            if (value1 < value2)
            {
                this.m_dbValue1 = value1;
                this.m_dbValue2 = value2;
                CheckValue();
                this.Invalidate();
            }
        }



        public void SetValue(double minimum, double maximum, double value1, double value2)
        {
            if (minimum < maximum)
            {
                this.m_dbMinimum = minimum;
                this.m_dbMaximum = maximum;
            }

            if (value1 < value2)
            {
                this.m_dbValue1 = value1;
                this.m_dbValue2 = value2;
            }

            this.CheckValue();
            this.Invalidate();
        }
        #endregion



    }
}

