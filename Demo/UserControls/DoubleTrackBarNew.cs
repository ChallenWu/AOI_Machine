using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoTech
{
    public partial class DoubleTrackBarNew : Control
    {
        private const int DaysNumSet = 7;
        private const int Minimum = 0;
        private const int Maximum = 100;
        private Size _sliderSize = new Size(12, 14);

        private const string DateTimeFormate = "MM/dd/yyyy HH:mm:ss";//定义时间格式；
        private DateTime _sliderFrontDT;
        private DateTime _sliderBackDT;
        private DateTime _realDT;//实时日期；
        private double m_dbValue1 = 0;  //滑块1初始值；
        private double m_dbValue2 = 100;//滑块2初始值；
        private Point m_lastMouseLocation;   //鼠标坐标最后位置
        private double m_dbRate = 0;  //比例
        private string _dtNewestTime = DateTime.Now.ToString(DateTimeFormate);

        public event Action<DateTime, DateTime> UpdateDT;

        /// <summary>
        /// 设备状态数据表；
        /// </summary>
        [Description("数据源最新时间")]
        public string DataTableNewestTime
        {
            get
            {
                if (_dtNewestTime == null || _dtNewestTime == string.Empty)
                {
                    _dtNewestTime = DateTime.Now.ToString(DateTimeFormate);
                }
                return _dtNewestTime;
            }
            set
            {
                _dtNewestTime = value;
                base.Refresh();
            }
        }


        public DateTime SliderFrontDT
        {
            get
            {
                if (_sliderFrontDT.ToString("yyyy").Contains("0001"))
                {
                    _sliderFrontDT = RealDT.AddDays(-6);
                }
                return _sliderFrontDT;
            }
            set
            {
                if (_sliderFrontDT <= _sliderBackDT)
                {
                    _sliderFrontDT = value;
                    //调用重绘；

                    Value1 = DateTimeToSlidePos(_sliderFrontDT);

                    this.Invalidate();
                }

            }
        }

        public DateTime SliderBackDT
        {
            get
            {
                if (_sliderBackDT.ToString("yyyy").Contains("0001"))
                {
                    _sliderBackDT = RealDT;
                }
                return _sliderBackDT;
            }
            set
            {

                if (_sliderBackDT >= _sliderFrontDT)
                {
                    _sliderBackDT = value;
                    //调用重绘；
                    Value2 = DateTimeToSlidePos(_sliderBackDT);
                    this.Invalidate();
                }


            }
        }

        //实时时间；
        public DateTime RealDT
        {
            get
            {
                if (_realDT.ToString("yyyy").Contains("0001"))
                {
                    _realDT = DateTime.Now;
                }
                return _realDT;
            }
            set
            {
                _realDT = value;

            }
        }


        public Size TrackButtonSize
        {
            get { return _sliderSize; }
            set
            {
                _sliderSize = value;
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
                        UpdateDT(_sliderFrontDT, _sliderBackDT);
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
                        UpdateDT(_sliderFrontDT, _sliderBackDT);
                    }
                    this.Invalidate();
                }
            }
        }



        /*滑块与时间的关系*/
        private double DateTimeToSlidePos(DateTime dtValue)
        {



            DateTime dtTemp = dtValue;
            DateTime dtTempStart = _realDT.AddDays(-7);
            double rate = 0.0;
            int weekSec = 7 * 24 * 3600;
            rate = 100.0 / (weekSec);

            double iSec = dtTemp.Subtract(dtTempStart).TotalSeconds;
            double dLength = 0.0;
            dLength = iSec * rate;
            if (dLength > 100)
            { dLength = 100; }
            if (dLength < 0)
            {
                dLength = 0;
            }
            return dLength;

        }





        private enum emBtnStatus
        {
            None,
            Btn1MouseIn,
            Btn2MouseIn,
            Btn1MouseDown,
            Btn2MouseDown,
        }
        private emBtnStatus m_btnStatus = emBtnStatus.None; //按钮状态；按下、松开、无、移动中；

        /// <summary>
        /// 键1
        /// </summary>
        private Rectangle m_rectBtn1;


        /// <summary>
        /// 键2
        /// </summary>
        private Rectangle m_rectBtn2;
        public DoubleTrackBarNew()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            CreateControl();
        }



        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (m_rectBtn1.Contains(e.X, e.Y))
            {
                m_btnStatus = emBtnStatus.Btn1MouseDown;
                m_lastMouseLocation = e.Location;

            }
            else if (m_rectBtn2.Contains(e.X, e.Y))
            {
                m_btnStatus = emBtnStatus.Btn2MouseDown;
                m_lastMouseLocation = e.Location;

            }
            else if (m_rectBtn1.Contains(e.X, e.Y) && m_rectBtn2.Contains(e.X, e.Y))
            {

                if (Value2 <= Maximum)
                {
                    m_btnStatus = emBtnStatus.Btn1MouseDown;
                    m_lastMouseLocation = e.Location;

                }

                if (Value1 >= Minimum)
                {
                    m_btnStatus = emBtnStatus.Btn2MouseDown;
                    m_lastMouseLocation = e.Location;

                }
            }

            this.Invalidate();


        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_btnStatus = emBtnStatus.None;
            this.Refresh();
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
            else if (m_btnStatus == emBtnStatus.Btn2MouseDown)
            {
                if (m_dbRate != 0)
                {
                    this.Value2 += GetMouseChangedValue(e.Location, m_lastMouseLocation);
                }
                m_lastMouseLocation = e.Location;
            }
            else
            {
                if (m_btnStatus != emBtnStatus.None)
                {
                    m_btnStatus = emBtnStatus.None;

                }
            }



        }

        private double GetMouseChangedValue(Point nowLocation, Point oldLocation)
        {
            double rlt = 0;
            rlt = nowLocation.X - oldLocation.X;

            return rlt / m_dbRate;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(this.BackColor);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            OnPaintHorizontal(e);

        }


        protected void OnPaintHorizontal(PaintEventArgs e)
        {
            double left = 0;
            double right = this.Width;



            int m_TrackSize = 2;

            Color UnSelectColor = Color.White;
            Color SelectColor = Color.DarkGreen;//深灰色；

            try
            {

                Pen pen_track = new Pen(new SolidBrush(UnSelectColor), 2);
                int trackTop = Height - TrackButtonSize.Height;
                int trackHeight = m_TrackSize;

                int trackBtnTop = trackTop - (TrackButtonSize.Height - trackHeight) / 2;
                int trackBtnBottom = trackBtnTop + trackHeight + (TrackButtonSize.Height - trackHeight) / 2; //滑块高度；
                int trackBtnHeight = trackBtnBottom - trackBtnTop;



                e.Graphics.DrawRectangle(pen_track, new Rectangle((int)left, trackTop, (int)(right - left), trackHeight));
                e.Graphics.FillRectangle(new SolidBrush(UnSelectColor), new Rectangle((int)left, trackTop, (int)(right - left), trackHeight));



                double rate = (right - left) / (Maximum - Minimum);
                m_dbRate = rate;

                int value1 = (int)((m_dbValue1 - Minimum) * rate);
                int value2 = (int)((m_dbValue2 - Minimum) * rate);

                int valueSec1 = Convert.ToInt32((double)m_dbValue1 / 100 * 7 * 24 * 3600);
                int valueSec2 = Convert.ToInt32((double)m_dbValue2 / 100 * 7 * 24 * 3600);

                _sliderFrontDT = _realDT.AddDays(-7).AddSeconds(valueSec1);
                _sliderBackDT = _realDT.AddDays(-7).AddSeconds(valueSec2);


                int value1_x = (int)(left + value1);
                int value2_x = (int)(left + value2);

                m_rectBtn1 = new Rectangle(value1_x - TrackButtonSize.Width / 2, trackBtnTop, TrackButtonSize.Width, TrackButtonSize.Height);
                m_rectBtn2 = new Rectangle(value2_x - TrackButtonSize.Width / 2, trackBtnTop, TrackButtonSize.Width, TrackButtonSize.Height);

                e.Graphics.FillRectangle(new SolidBrush(SelectColor), new Rectangle((int)(value1_x), trackTop, (int)(value2 - value1), trackHeight));


                e.Graphics.FillRectangle(new SolidBrush(m_btnStatus == emBtnStatus.Btn1MouseDown || m_btnStatus == emBtnStatus.Btn1MouseIn ? SelectColor : Color.DarkGray), m_rectBtn1);
                e.Graphics.FillRectangle(new SolidBrush(m_btnStatus == emBtnStatus.Btn2MouseDown || m_btnStatus == emBtnStatus.Btn2MouseIn ? SelectColor : Color.DarkGray), m_rectBtn2);





                #region font
                //double dValue1Rate = Math.Round(Value1 / (Maximum - Minimum), 1);
                //double dValue2Rate = Math.Round(Value2 / (Maximum - Minimum), 1);

                //IFormatProvider culture = new CultureInfo("en-US", true);



                //DateTime dtCheckEnd = DateTime.ParseExact(_dtNewestTime, DateTimeFormate, culture);

                //DateTime dtCheckStart = dtCheckEnd.AddDays(-7);
                //double iSecondValue1 = dValue1Rate * 7 * 24 * 3600;
                //double iSecondValue2 = dValue2Rate * 7 * 24 * 3600;
                //DateTime dTimeValue1 = dtCheckStart.AddSeconds(iSecondValue1);
                //DateTime dTimeValue2 = dtCheckStart.AddSeconds(iSecondValue2);


                //string strDispDateStart;
                //strDispDateStart = dTimeValue1.ToString(DateTimeFormate);
                //string strDispDateEnd;
                //strDispDateEnd = dTimeValue2.ToString(DateTimeFormate);

                //RectangleF startRect = new RectangleF((int)left, trackTop - 20, 200, 300);
                //RectangleF endRect = new RectangleF((int)right - 120, trackTop - 20, 200, 300);
                //Font m_TrackBarFont = new Font("Arial", 8, FontStyle.Regular);//滑块日期字体；


                //e.Graphics.DrawString(strDispDateStart, m_TrackBarFont, Brushes.Black, startRect);
                //e.Graphics.DrawString(strDispDateEnd, m_TrackBarFont, Brushes.Black, endRect);

                //m_TrackBarFont.Dispose();
                #endregion


            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private void BarSizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }



    }
}
