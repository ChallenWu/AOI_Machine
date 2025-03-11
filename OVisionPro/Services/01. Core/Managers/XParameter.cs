using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVisionPro
{
    public sealed class XParameter
    {
        private static Dictionary<string, Dictionary<string, object>> _parameters = new Dictionary<string, Dictionary<string, object>>();
        private static Dictionary<string, Dictionary<string, object[]>> _limitCV = new Dictionary<string, Dictionary<string, object[]>>();

        // ViDu _parameters
        // Threshold
        // {
        //     thresh: 127,
        //     maxval: 255
        // }

        // ViDu _limitCV
        // Threshold
        // {
        //     thresh: { 1.0, 255.0, 0.1 },
        //     maxval: { 0, 255, 1 }
        //     adaptiveMethod: new List<object>() { "MeanC", "GaussianC" }
        // }

        // Lấy giá trị từ cấu hình nếu có, nếu không thì trả về giá trị mặc định
        private readonly static XParameter instance = new XParameter();

        public static XParameter Instance
        {
            get { return instance; }
        }

        public object[] GetLimitParam(string cvname, string cvparam)
        {
            if (_limitCV.ContainsKey(cvname) && _limitCV[cvname].ContainsKey(cvparam))
            {
                return _limitCV[cvname][cvparam];
            }
            return new object[]{ };
        }
        public Dictionary<string, object> GetParameter(string cvname)
        {
            if (_parameters.ContainsKey(cvname))
            {
                return _parameters[cvname];
            }
            return new Dictionary<string, object>() { };
        }

        // Thiết lập giá trị tham số
        public void SetParameter(string cvname, string parameterName, object value)
        {
            try
            {
                if (!_parameters.ContainsKey(cvname))
                {
                    _parameters.Add(cvname, new Dictionary<string, object>() { });
                }

                parameterName = $"{cvname}_1_{parameterName}";
                _parameters[cvname][parameterName] = value;
            }
            catch (Exception ex)
            {
                logW.Ins.info($"[setparam] ex: {ex}");
            }
        }

        public void SetParameter(string cvname, string parameterName, object value, object[] limitValue)
        {
            try
            {
                //cvname = $"{parameterName}_0_{cvname}";
                if (!_parameters.ContainsKey(cvname))
                {
                    _parameters.Add(cvname, new Dictionary<string, object>() { });
                }
                parameterName = $"{cvname}_1_{parameterName}";
                _parameters[cvname][parameterName] = value;
                if (!_limitCV.ContainsKey(cvname))
                {
                    _limitCV.Add(cvname, new Dictionary<string, object[]>() { });
                }
                _limitCV[cvname][parameterName] = limitValue;

                ///
                //if (limitValue[0] is string)
                //{
                //    // make combobox in 
                //}
                //else if (limitValue[0] is double)
                //{
                //    // make spinbox
                //}
                //else
                //{
                //    // make spinbox
                //}
            }
            catch (Exception ex)
            {
                logW.Ins.info($"[setparameter] ex: {ex}");
            }
        }
    }
}
