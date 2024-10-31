using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore;
using System.Windows.Forms;
using Demo;

namespace HB_IWatch
{
    class MotorSpeed
    {
        class SpeedParameter
        {
            public double vel = 10;
            public double acc = 1000;
            public double dec = 1000;
            public double jerk = -1;
            public double kdec = -1;
        }
        Dictionary<AxisId, SpeedParameter> maxSpeedMap = new Dictionary<AxisId, SpeedParameter>();

        public MotorSpeed()
        {
            RegisterAllSpeed();
        }

        // 轴种类：            直线              旋转
        // 位置单位：           mm               度
        // 速度单位：           mm/s             度/s
        // 加速度单位：         mm/s^2           度/s^2
        void RegisterAllSpeed()
        {
            RegisterSpeed(AxisId.左贴装X轴, 2200, 1000 * 50, 1000 * 50, 1000 * 1500, 1000 * 1500);
            RegisterSpeed(AxisId.右贴装X轴, 2200, 1000 * 50, 1000 * 50, 1000 * 1500, 1000 * 1500);
            RegisterSpeed(AxisId.左贴装Y轴, 1200, 1200 * 50, 1200 * 50, 1200 * 800, 1200 * 800);
            RegisterSpeed(AxisId.右贴装Y轴, 1200, 1200 * 50, 1200 * 50, 1200 * 800, 1200 * 800);
            RegisterSpeed(AxisId.左贴装Z1轴, 1500, 1500 * 50, 1500 * 50, 1500 * 500, 1500 * 500);
            RegisterSpeed(AxisId.左贴装Z2轴, 1500, 1500 * 50, 1500 * 50, 1500 * 500, 1500 * 500);
            RegisterSpeed(AxisId.左贴装Z3轴, 1500, 1500 * 50, 1500 * 50, 1500 * 500, 1500 * 500);
            RegisterSpeed(AxisId.右贴装Z1轴, 1500, 1500 * 50, 1500 * 50, 1500 * 500, 1500 * 500);
            RegisterSpeed(AxisId.右贴装Z2轴, 1500, 1500 * 50, 1500 * 50, 1500 * 500, 1500 * 500);
            RegisterSpeed(AxisId.右贴装Z3轴, 1500, 1500 * 50, 1500 * 50, 1500 * 500, 1500 * 500);
            RegisterSpeed(AxisId.左贴装R1轴, 1000, 1000 * 50, 1000 * 50, 1000 * 500, 1000 * 500);
            RegisterSpeed(AxisId.左贴装R2轴, 1000, 1000 * 50, 1000 * 50, 1000 * 500, 1000 * 500);
            RegisterSpeed(AxisId.左贴装R3轴, 1000, 1000 * 50, 1000 * 50, 1000 * 500, 1000 * 500);
            RegisterSpeed(AxisId.右贴装R1轴, 1000, 1000 * 50, 1000 * 50, 1000 * 500, 1000 * 500);
            RegisterSpeed(AxisId.右贴装R2轴, 1000, 1000 * 50, 1000 * 50, 1000 * 500, 1000 * 500);
            RegisterSpeed(AxisId.右贴装R3轴, 1000, 1000 * 50, 1000 * 50, 1000 * 500, 1000 * 500);
            RegisterSpeed(AxisId.左供料左上Z轴, 100, 100 * 50, 100 * 50, -1, -1);
            RegisterSpeed(AxisId.左供料左下Z轴, 100, 100 * 50, 100 * 50, -1, -1);
            RegisterSpeed(AxisId.左供料右上Z轴, 100, 100 * 50, 100 * 50, -1, -1);
            RegisterSpeed(AxisId.左供料右下Z轴, 100, 100 * 50, 100 * 50, -1, -1);
            RegisterSpeed(AxisId.右供料左上Z轴, 100, 100 * 50, 100 * 50, -1, -1);
            RegisterSpeed(AxisId.右供料左下Z轴, 100, 100 * 50, 100 * 50, -1, -1);
            RegisterSpeed(AxisId.右供料右上Z轴, 100, 100 * 50, 100 * 50, -1, -1);
            RegisterSpeed(AxisId.右供料右下Z轴, 100, 100 * 50, 100 * 50, -1, -1);
            RegisterSpeed(AxisId.左预取料1Y轴, 800, 10 * 50, 10 * 50, -1, -1);
            RegisterSpeed(AxisId.左预取料2Y轴, 800, 10 * 50, 10 * 50, -1, -1);
            RegisterSpeed(AxisId.右预取料1Y轴, 800, 10 * 50, 10 * 50, -1, -1);
            RegisterSpeed(AxisId.右预取料2Y轴, 800, 10 * 50, 10 * 50, -1, -1);
        }

        void RegisterSpeed(AxisId axisId, double vel, double acc, double dec, double jerk, double kdec)
        {
            if (maxSpeedMap.ContainsKey(axisId) == false)
            {
                SpeedParameter par = new SpeedParameter();
                par.vel = vel; par.acc = acc; par.dec = dec; par.jerk = jerk; par.kdec = kdec;
                maxSpeedMap.Add(axisId, par);
            }
        }

        SpeedParameter FindSpeedById(AxisId axisId)
        {
            if (maxSpeedMap.ContainsKey(axisId) == false)
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("没有找到电机速度值!"));
                return null;
            }
            return maxSpeedMap[axisId];
        }

        public double GetSpeedRatio(AxisId axisId)
        {
            double ratio = 1.0;
            switch (axisId)
            {
                case AxisId.左贴装X轴:
                case AxisId.右贴装X轴:
                case AxisId.左贴装Y轴:
                case AxisId.右贴装Y轴:
                case AxisId.左贴装Z1轴:
                case AxisId.左贴装Z2轴:
                case AxisId.左贴装Z3轴:
                case AxisId.右贴装Z1轴:
                case AxisId.右贴装Z2轴:
                case AxisId.右贴装Z3轴:
                case AxisId.左贴装R1轴:
                case AxisId.左贴装R2轴:
                case AxisId.左贴装R3轴:
                case AxisId.右贴装R1轴:
                case AxisId.右贴装R2轴:
                case AxisId.右贴装R3轴:
                    //ratio = Globals.SettingCalibration.组装_速度百分比 * 0.01;
                    break;
                case AxisId.左供料左上Z轴:
                case AxisId.左供料左下Z轴:
                case AxisId.左供料右上Z轴:
                case AxisId.左供料右下Z轴:
                case AxisId.右供料左上Z轴:
                case AxisId.右供料左下Z轴:
                case AxisId.右供料右上Z轴:
                case AxisId.右供料右下Z轴:
                case AxisId.左预取料1Y轴:
                case AxisId.左预取料2Y轴:
                case AxisId.右预取料1Y轴:
                case AxisId.右预取料2Y轴:
                    //ratio = Globals.SettingCalibration.支架供料_速度百分比 * 0.01;
                    break;
            }
            return ratio;
        }

        // 得到轴的最大速度
        public double GetMaxVel(AxisId axisId)
        {
            SpeedParameter par = FindSpeedById(axisId);
            if (par != null)
                return par.vel;
            return 10;
        }

        // 得到轴的调机速度
        public double GetManualVel(AxisId axisId)
        {
            return 30;
        }

        // 设置轴的加速度，减速度
        public void UpdateSpeedParameter(int axis)
        {
            AxisId axisId = (AxisId)axis;
            SpeedParameter par = maxSpeedMap[axisId];
            double ratio = GetSpeedRatio(axisId);
            XDevice.Instance.FindAxisById(axis).SetAxisAccAndDec(par.acc * ratio, par.dec * ratio);
            if (par.jerk > 0 && par.kdec > 0)
                XDevice.Instance.FindAxisById(axis).SetAxisJerkAndKDec(par.jerk * ratio, par.kdec * ratio);
        }
    }
}
