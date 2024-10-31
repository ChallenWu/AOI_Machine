using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCore;
using System.ComponentModel;
using System.Windows.Forms;
using XCore.Framework.MultiLang;
using System.Reflection;

namespace HB_IWatch
{
    class SettingCalibration : XSetting
    {

        #region 运动参数

        [MyProperty("支架供料_速度百分比", "3运动参数")]
        public double 支架供料_速度百分比
        {
            get
            {
                return GetNodeValue("Support_VelRatio", (double)20);
            }
            set
            {
                if (value < 1 || value > 40)
                {
                    ShowParamOverRangeError("支架供料_速度百分比", 1, 40);
                }
                else
                    SetNodeValue("Support_VelRatio", value);
            }
        }

        [MyProperty("组装_Z轴贴合下压速度", "3运动参数")]
        public double 组装_Z轴贴合下压速度
        {
            get
            {
                return GetNodeValue("LAssemble_ZVelPress", (double)40);
            }
            set
            {
                if (value < 1 || value > 40)
                    ShowParamOverRangeError("组装_Z轴贴合下压速度", 1, 40);
                else
                    SetNodeValue("LAssemble_ZVelPress", value);
            }
        }
        [MyProperty("组装_Z轴取料下压速度", "3运动参数")]
        public double 组装_Z轴取料下压速度
        {
            get
            {
                return GetNodeValue("LAssemble_ZVelPick", (double)50);
            }
            set
            {
                if (value < 1 || value > 50)
                    ShowParamOverRangeError("组装_Z轴取料下压速度", 1, 50);
                else
                    SetNodeValue("LAssemble_ZVelPick", value);
            }
        }
        [MyProperty("组装_速度百分比", "3运动参数")]
        public double 组装_速度百分比
        {
            get
            {
                return GetNodeValue("LAssemble_VelRatio", (double)20);
            }
            set
            {
                if (value < 1 || value > 65)
                    ShowParamOverRangeError("组装_速度百分比", 1, 65);
                else
                    SetNodeValue("LAssemble_VelRatio", value);
            }
        }

        #endregion

        #region 左机标定参数

        [MyProperty("左组装吸头1压力传感器k", "1左组装标定参数")]
        public double 左组装吸头1压力传感器k
        {
            get
            {
                return GetNodeValue("LPicker1ForceFactorK", 0.0167353212419305);
            }
            set
            {
                SetNodeValue("LPicker1ForceFactorK", value);
            }
        }
        [MyProperty("左组装吸头1压力传感器b", "1左组装标定参数")]
        public double 左组装吸头1压力传感器b
        {
            get
            {
                return GetNodeValue("LPicker1ForceFactorB", 0.266422724144304);
            }
            set
            {
                SetNodeValue("LPicker1ForceFactorB", value);
            }
        }
        [MyProperty("左组装吸头2压力传感器k", "1左组装标定参数")]
        public double 左组装吸头2压力传感器k
        {
            get
            {
                return GetNodeValue("LPicker2ForceFactorK", 0.02001749511166);
            }
            set
            {
                SetNodeValue("LPicker2ForceFactorK", value);
            }
        }
        [MyProperty("左组装吸头2压力传感器b", "1左组装标定参数")]
        public double 左组装吸头2压力传感器b
        {
            get
            {
                return GetNodeValue("LPicker2ForceFactorB", 0.491930221255672);
            }
            set
            {
                SetNodeValue("LPicker2ForceFactorB", value);
            }
        }
        [MyProperty("左组装吸头3压力传感器k", "1左组装标定参数")]
        public double 左组装吸头3压力传感器k
        {
            get
            {
                return GetNodeValue("LPicker3ForceFactorK", 0.017346499556906);
            }
            set
            {
                SetNodeValue("LPicker3ForceFactorK", value);
            }
        }
        [MyProperty("左组装吸头3压力传感器b", "1左组装标定参数")]
        public double 左组装吸头3压力传感器b
        {
            get
            {
                return GetNodeValue("LPicker3ForceFactorB", 0.508764651032424);
            }
            set
            {
                SetNodeValue("LPicker3ForceFactorB", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nozzleId">1-6左机123右机123</param>
        /// <returns></returns>
        public double GetNozzleSettingAttachForce(int nozzleId)
        {
            switch (nozzleId)
            {
                case 1:
                    return this.左组装吸头1压力阈值Kg;
                    break;
                case 2:
                    return this.左组装吸头2压力阈值Kg;
                    break;
                case 3:
                    return this.左组装吸头3压力阈值Kg;
                    break;
                case 4:
                    return this.右组装吸头1压力阈值Kg;
                    break;
                case 5:
                    return this.右组装吸头2压力阈值Kg;
                    break;
                case 6:
                    return this.右组装吸头3压力阈值Kg;
                    break;
                default:
                    return 1;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nozzleId">1-6左机123右机123</param>
        /// <param name="value">设定的值</param>
        public void SetNozzleSettingAttachForce(int nozzleId, double value)
        {
            switch (nozzleId)
            {
                case 1:
                    this.左组装吸头1压力阈值Kg = value;
                    break;
                case 2:
                    this.左组装吸头2压力阈值Kg = value;
                    break;
                case 3:
                    this.左组装吸头3压力阈值Kg = value;
                    break;
                case 4:
                    this.右组装吸头1压力阈值Kg = value;
                    break;
                case 5:
                    this.右组装吸头2压力阈值Kg = value;
                    break;
                case 6:
                    this.右组装吸头3压力阈值Kg = value;
                    break;
                default:
                    break;
            }
        }
        [MyProperty("左组装吸头1压力阈值Kg", "1左组装标定参数")]
        private double 左组装吸头1压力阈值Kg
        {
            get
            {
                return GetNodeValue("Picker1ForceThresL", 1.5);
            }
            set
            {
                SetNodeValue("Picker1ForceThresL", value);
            }
        }
        [MyProperty("左组装吸头2压力阈值Kg", "1左组装标定参数")]
        private double 左组装吸头2压力阈值Kg
        {
            get
            {
                return GetNodeValue("Picker2ForceThresL", 1.5);
            }
            set
            {
                SetNodeValue("Picker2ForceThresL", value);
            }
        }

        [MyProperty("左组装吸头3压力阈值Kg", "1左组装标定参数")]
        private double 左组装吸头3压力阈值Kg
        {
            get
            {
                return GetNodeValue("Picker3ForceThresL", 1.5);
            }
            set
            {
                SetNodeValue("Picker3ForceThresL", value);
            }
        }
        #endregion

        #region 右机标定参数
        [MyProperty("右组装吸头1压力传感器k", "2右组装标定参数")]
        public double 右组装吸头1压力传感器k
        {
            get
            {
                return GetNodeValue("RPicker1ForceFactorK", 0.0167353212419305);
            }
            set
            {
                SetNodeValue("RPicker1ForceFactorK", value);
            }
        }
        [MyProperty("右组装吸头1压力传感器b", "2右组装标定参数")]
        public double 右组装吸头1压力传感器b
        {
            get
            {
                return GetNodeValue("RPicker1ForceFactorB", 0.266422724144304);
            }
            set
            {
                SetNodeValue("RPicker1ForceFactorB", value);
            }
        }
        [MyProperty("右组装吸头2压力传感器k", "2右组装标定参数")]
        public double 右组装吸头2压力传感器k
        {
            get
            {
                return GetNodeValue("RPicker2ForceFactorK", 0.02001749511166);
            }
            set
            {
                SetNodeValue("RPicker2ForceFactorK", value);
            }
        }
        [MyProperty("右组装吸头2压力传感器b", "2右组装标定参数")]
        public double 右组装吸头2压力传感器b
        {
            get
            {
                return GetNodeValue("RPicker2ForceFactorB", 0.491930221255672);
            }
            set
            {
                SetNodeValue("RPicker2ForceFactorB", value);
            }
        }
        [MyProperty("右组装吸头3压力传感器k", "2右组装标定参数")]
        public double 右组装吸头3压力传感器k
        {
            get
            {
                return GetNodeValue("RPicker3ForceFactorK", 0.017346499556906);
            }
            set
            {
                SetNodeValue("RPicker3ForceFactorK", value);
            }
        }
        [MyProperty("右组装吸头3压力传感器b", "2右组装标定参数")]
        public double 右组装吸头3压力传感器b
        {
            get
            {
                return GetNodeValue("RPicker3ForceFactorB", 0.508764651032424);
            }
            set
            {
                SetNodeValue("RPicker3ForceFactorB", value);
            }
        }


        [MyProperty("右组装吸头1压力阈值Kg", "2右组装标定参数")]
        private double 右组装吸头1压力阈值Kg
        {
            get
            {
                return GetNodeValue("Picker1ForceThresR", 1.5);
            }
            set
            {
                SetNodeValue("Picker1ForceThresR", value);
            }
        }

        [MyProperty("右组装吸头2压力阈值Kg", "2右组装标定参数")]
        private double 右组装吸头2压力阈值Kg
        {
            get
            {
                return GetNodeValue("Picker2ForceThresR", 1.5);
            }
            set
            {
                SetNodeValue("Picker2ForceThresR", value);
            }
        }
        [MyProperty("右组装吸头3压力阈值Kg", "2右组装标定参数")]
        private double 右组装吸头3压力阈值Kg
        {
            get
            {
                return GetNodeValue("Picker3ForceThresR", 1.5);
            }
            set
            {
                SetNodeValue("Picker3ForceThresR", value);
            }
        }
        #endregion


        public double GetPickerForceOffset(int pickerId, bool isLeftStation)
        {
            if (pickerId < 0 || pickerId > 2)
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("吸嘴索引号超出范围!"));
                return 0;
            }

            if (isLeftStation)
            {
                if (pickerId == 0)
                    return GetNodeValue("Picker1ForceOffsetL", 0.2);
                else if (pickerId == 1)
                    return GetNodeValue("Picker2ForceOffsetL", 0.15);
                else
                    return GetNodeValue("Picker3ForceOffsetL", 0.125);
            }
            else
            {
                if (pickerId == 0)
                    return GetNodeValue("Picker1ForceOffsetR", 0.2);
                else if (pickerId == 1)
                    return GetNodeValue("Picker2ForceOffsetR", 0.15);
                else
                    return GetNodeValue("Picker3ForceOffsetR", 0.125);
            }

        }

        public void SetPickerForceOffset(int pickerId, double offset, bool isLeftStation)
        {
            if (pickerId < 0 || pickerId > 2)
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("吸嘴索引号超出范围!"));
                return;
            }
            if (isLeftStation)
            {
                if (pickerId == 0)
                    SetNodeValue("Picker1ForceOffsetL", offset);
                else if (pickerId == 1)
                    SetNodeValue("Picker2ForceOffsetL", offset);
                else
                    SetNodeValue("Picker3ForceOffsetL", offset);
            }
            else
            {
                if (pickerId == 0)
                    SetNodeValue("Picker1ForceOffsetR", offset);
                else if (pickerId == 1)
                    SetNodeValue("Picker2ForceOffsetR", offset);
                else
                    SetNodeValue("Picker3ForceOffsetR", offset);
            }
        }

        public void SetNozzleCalibPara(int pickerId, double kk, double bb, bool isLeftStation)
        {
            if (isLeftStation)
            {
                if (pickerId == 0)
                {
                    左组装吸头1压力传感器k = kk;
                    左组装吸头1压力传感器b = bb;
                }
                else if (pickerId == 1)
                {
                    左组装吸头2压力传感器k = kk;
                    左组装吸头2压力传感器b = bb;
                }
                else if (pickerId == 2)
                {
                    左组装吸头3压力传感器k = kk;
                    左组装吸头3压力传感器b = bb;
                }
            }
            else
            {
                if (pickerId == 0)
                {
                    右组装吸头1压力传感器k = kk;
                    右组装吸头1压力传感器b = bb;
                }
                else if (pickerId == 1)
                {
                    右组装吸头2压力传感器k = kk;
                    右组装吸头2压力传感器b = bb;
                }
                else if (pickerId == 2)
                {
                    右组装吸头3压力传感器k = kk;
                    右组装吸头3压力传感器b = bb;
                }
            }
        }

    }
}
