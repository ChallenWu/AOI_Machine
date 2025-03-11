using OpenCvSharp.ML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;
using YamlDotNet.Core.Tokens;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using OVisionPro.Services._01._Core.ShapesPics;
using Serilog;

namespace OVisionPro
{
    public class ColorData
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int A { get; set; }
        public bool IsKnownColor { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsNamedColor { get; set; }
        public bool IsSystemColor { get; set; }
        public string Name { get; set; }

        public ColorData() { }

        public ColorData(int r, int g, int b, int a, bool isknowcolor, bool isempty, bool isnamedcolor, bool issystemcolor, string name)
        {
            R = r;
            G = g; B = b; A = a;
            IsKnownColor = isknowcolor;
            IsEmpty = isempty;
            IsNamedColor = isnamedcolor;
            IsSystemColor = issystemcolor;
            Name = name;
        }

        public ColorData(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
            IsKnownColor = color.IsKnownColor;
            IsEmpty = color.IsEmpty;
            IsNamedColor = color.IsNamedColor;
            IsSystemColor = color.IsSystemColor;
            Name = color.Name;
        }

        // Phương thức chuyển từ ColorData thành đối tượng Color
        public Color ToColor()
        {
            // Khôi phục Color từ các thành phần
            return Color.FromArgb(A, R, G, B);
        }
    }
    public class XParameterManager
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, object>>>> ucBlockBasicControllersCached = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, object>>>>() { };
        public Dictionary<string, Dictionary<string, Dictionary<string, object>>> ucBlockBasicControllers = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>() { };
        public Dictionary<string, Dictionary<string, Dictionary<string, Shape>>> ucBlockROIShapes = new Dictionary<string, Dictionary<string, Dictionary<string, Shape>>>() { };
        public List<string> listCVFunc = new List<string>() { };
        public XObjectCVParameters propertyGridData = new XObjectCVParameters();
        public Action<string> onUpdateModelNameToUIAction;
        public Action onUpdateFuncIDToUIAction;
        public Action <string, string> onAddNewShapeROI;
        public string modelNameExec = "";    // model dung de reload config. = running/debug.
        public string SelectedBlockID = "";

        private readonly static XParameterManager instance = new XParameterManager();
        public static XParameterManager Instance
        {
            get { return instance; }
        }

        public XParameterManager()
        {
            XModels.Instance.OnUpdateModelRunningAcion += OnUpdateModelRunningAcion;
            XModels.Instance.OnDelModelAcion += RemoveModelConf;
            InitialParameterCV();
            LoadConfig();
        }


        public T GetUcBlockBasicData<T>(string funcID, string cvID, string paramName, T defaultValue)
        {
            try
            {
                if (ucBlockBasicControllers.ContainsKey(funcID))
                {
                    if (ucBlockBasicControllers[funcID].ContainsKey(cvID))
                    {
                        string[] res_cv_func = cvID.Split(new string[] { "_0_" }, StringSplitOptions.None);
                        string paramcvID = $"{res_cv_func[0]}_1_{paramName}";
                        if (ucBlockBasicControllers[funcID][cvID].TryGetValue(paramcvID, out object value))
                        {
                            return (defaultValue is string) ? (T)(object)value.ToString() : (T)value;
                        }
                    }
                }

            }
            catch (Exception e)
            { }
            return defaultValue;
        }

        public int GetUcBlockBasicDataInt(string funcID, string cvID, string paramName, int defaultValue)
        {
            try
            {
                if (ucBlockBasicControllers.ContainsKey(funcID))
                {
                    if (ucBlockBasicControllers[funcID].ContainsKey(cvID))
                    {
                        string[] res_cv_func = cvID.Split(new string[] { "_0_" }, StringSplitOptions.None);
                        string paramcvID = $"{res_cv_func[0]}_1_{paramName}";
                        if (ucBlockBasicControllers[funcID][cvID].TryGetValue(paramcvID, out object value))
                        {
                            return int.Parse(value.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            { }
            return defaultValue;
        }

        public double GetUcBlockBasicDataDouble(string funcID, string cvID, string paramName, double defaultValue)
        {
            try
            {
                if (ucBlockBasicControllers.ContainsKey(funcID))
                {
                    if (ucBlockBasicControllers[funcID].ContainsKey(cvID))
                    {
                        string[] res_cv_func = cvID.Split(new string[] { "_0_" }, StringSplitOptions.None);
                        string paramcvID = $"{res_cv_func[0]}_1_{paramName}";
                        if (ucBlockBasicControllers[funcID][cvID].TryGetValue(paramcvID, out object value))
                        {
                            return double.Parse(value.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            { }
            return defaultValue;
        }
        public string GetUcBlockBasicData1(string funcID, string cvID, string paramName, string defaultValue)
        {
            try
            {
                if (ucBlockBasicControllers.ContainsKey(funcID))
                {
                    if (ucBlockBasicControllers[funcID].ContainsKey(cvID))
                    {
                        string[] res_cv_func = cvID.Split(new string[] { "_0_" }, StringSplitOptions.None);
                        string paramcvID = $"{res_cv_func[0]}_1_{paramName}";
                        if (ucBlockBasicControllers[funcID][cvID].TryGetValue(paramcvID, out object value))
                        {
                            return value.ToString();
                        }
                    }
                }

            }
            catch (Exception e)
            { }
            return defaultValue;
        }
        public Color GetUcBlockBasicDataColor(string funcID, string cvID, string paramName, Color defaultValue)
        {
            try
            {
                if (ucBlockBasicControllers.ContainsKey(funcID))
                {
                    if (ucBlockBasicControllers[funcID].ContainsKey(cvID))
                    {
                        string[] res_cv_func = cvID.Split(new string[] { "_0_" }, StringSplitOptions.None);
                        string paramcvID = $"{res_cv_func[0]}_1_{paramName}";
                        if (ucBlockBasicControllers[funcID][cvID].TryGetValue(paramcvID, out object value))
                        {
                            return defaultValue;
                        }
                    }
                }

            }
            catch (Exception e)
            { }
            return defaultValue;
        }


        public void DelUcBlockBasicData(string funcID)
        {
            if (ucBlockBasicControllers.ContainsKey(funcID))
            {
                foreach (string key1 in ucBlockBasicControllers[funcID].Keys)
                {
                    if (key1.Contains("ROI1"))
                    {
                        ucBlockBasicControllers[funcID].Remove(key1);
                    }
                }
            }
        }

        public void OnUpdateModelRunningAcion(string modelName)
        {
            modelNameExec = modelName;
            XImgPatternManager.Instance.CheckImgPatternModel(modelName);
            logW.Ins.info($"[PARAM][2] OnUpdateModelRunningAcion");
        }

        public void RemoveModelConf(string modelName)
        {
            if (modelName == XModels.Instance.modelRunning)
            {
                logW.Ins.info($"RemoveModelConf >> XOA MODEL NG - MODEL DANG CHAY KHONG THE XOA.");
            } 
            else if (ucBlockBasicControllersCached.ContainsKey(modelName))
            {
                ucBlockBasicControllersCached.Remove(modelName);
                SaveToYaml(ucBlockBasicControllersCached, "ParameterAOI.yaml");
                LoadConfig();
                logW.Ins.info($"RemoveModelConf >> XOA MODEL OK.");
            }
            else
            {
                logW.Ins.info($"RemoveModelConf >> XOA MODEL NG - MODEL KHONG TON TAI.");
            }
        }
        public void AddMoreModelConf(string modelName)
        {
            if (!ucBlockBasicControllersCached.ContainsKey(modelName))
            {
                ucBlockBasicControllersCached.Add(modelName, new Dictionary<string, Dictionary<string, Dictionary<string, object>>>());
                SaveToYaml(ucBlockBasicControllersCached, "ParameterAOI.yaml");
                logW.Ins.info($"AddMoreModelConf >> THEM MODEL OK");
            }
            else
            {
                logW.Ins.info($"AddMoreModelConf >> THEM MODEL NG - MODEL DA TON TAI.");
            }
        }

        public bool SetUcBlockBasicData<T>(string funcID, string cvID, string paramName, T value)
        {
            try
            {
                string[] res_cv_func = cvID.Split(new string[] { "_0_" }, StringSplitOptions.None);
                string paramcvID = $"{res_cv_func[0]}_1_{paramName}";
                ucBlockBasicControllers[funcID][cvID][paramcvID] = value;
                return true;
            }
            catch (Exception ex)
            {
                logW.Ins.info($"[setucblockbasicData] ex: {ex}");
            }
            return false;
        }
        public bool SetUcBlockBasicDataColor<T>(string funcID, string cvID, string paramName, T value)
        {
            try
            {
                string[] res_cv_func = cvID.Split(new string[] { "_0_" }, StringSplitOptions.None);
                string paramcvID = $"{res_cv_func[0]}_1_{paramName}";
                ucBlockBasicControllers[funcID][cvID][paramcvID] = value;
                return true;
            }
            catch (Exception ex)
            {
                logW.Ins.info($"[setucblockbasicData] ex: {ex}");
            }
            return false;
        }

        public void LoadConfig(string filePath = "ParameterAOI.yaml")
        {
            string msg = "LOAD THANH CONG";
            try
            {
                // Load dữ liệu từ tệp YAML vào Dictionary
                if (File.Exists(filePath))
                {

                    var tmp_ucBlockBasicControllersCached = LoadFromYaml<Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, object>>>>>(filePath);
                    if (tmp_ucBlockBasicControllersCached != null)
                    {
                        ucBlockBasicControllersCached = tmp_ucBlockBasicControllersCached;
                    }
                    updateConfig();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                // Ghi log lỗi với thông tin chi tiết
            }
            logW.Ins.warning(msg);
        }

        public void updateConfig()
        {
            string key = modelNameExec;
            if (ucBlockBasicControllersCached.ContainsKey(key))
            {
                foreach (string key1 in ucBlockBasicControllersCached[key].Keys)
                {
                    if (!ucBlockROIShapes.ContainsKey(key1))
                    {
                        // Log trường hợp không tìm thấy key1 trong ucBlockROIShapes
                        ucBlockROIShapes.Add(key1, new Dictionary<string, Dictionary<string, Shape>>());
                        logW.Ins.info($"Key1 {key1} not found in ucBlockROIShapes.");
                    }
                    // Kiểm tra nếu ucBlockBasicControllers đã chứa key1
                    if (!ucBlockBasicControllers.ContainsKey(key1))
                    {
                        // Log trường hợp không tìm thấy key1 trong ucBlockBasicControllers
                        ucBlockBasicControllers.Add(key1, new Dictionary<string, Dictionary<string, object>>());
                        logW.Ins.info($"Key1 {key1} not found in ucBlockBasicControllers.");
                    }
                    foreach (string key2 in ucBlockBasicControllersCached[key][key1].Keys)
                    {
                        // Kiểm tra nếu ucBlockBasicControllers[key1] chứa key2
                        if (!ucBlockBasicControllers[key1].ContainsKey(key2))
                        {
                            // Log trường hợp không tìm thấy key2 trong ucBlockBasicControllers
                            ucBlockBasicControllers[key1].Add(key2, new Dictionary<string, object>());
                            logW.Ins.info($"Key2 {key2} not found in ucBlockBasicControllers[{key1}]");
                        }
                        foreach (string key3 in ucBlockBasicControllersCached[key][key1][key2].Keys)
                        {
                            // Kiểm tra nếu ucBlockBasicControllers[key1][key2] chứa key3
                            if (!ucBlockBasicControllers[key1][key2].ContainsKey(key3))
                            {
                                // Log trường hợp không tìm thấy key3 trong ucBlockBasicControllers
                                ucBlockBasicControllers[key1][key2].Add(key3, new Dictionary<string, object>());
                                logW.Ins.info($"Key3 {key3} not found in ucBlockBasicControllers[{key1}][{key2}]");
                                
                            }
                            var newValue = ucBlockBasicControllersCached[key][key1][key2][key3];

                            if (newValue != null)
                            {
                                object convertedValue = null;

                                // Cố gắng ép kiểu từ string sang các kiểu dữ liệu khác
                                try
                                {
                                    // Cách này sử dụng Convert.ChangeType để tự động ép kiểu
                                    if (newValue is string strValue)
                                    {
                                        // Thử ép kiểu về int, double, bool nếu là string
                                        if (int.TryParse(strValue, out int intValue))
                                        {
                                            convertedValue = intValue;
                                        }
                                        else if (double.TryParse(strValue, out double doubleValue))
                                        {
                                            convertedValue = doubleValue;
                                        }
                                        else if (bool.TryParse(strValue, out bool boolValue))
                                        {
                                            convertedValue = boolValue;
                                        }
                                        else if (decimal.TryParse(strValue, out decimal decimalValue))
                                        {
                                            convertedValue = decimalValue;
                                        }
                                        else
                                        {
                                            // Nếu không thể ép kiểu, để nguyên giá trị là string
                                            convertedValue = strValue;
                                        }
                                    }
                                    else
                                    {
                                        // Nếu giá trị không phải là string, ép kiểu trực tiếp
                                        convertedValue = Convert.ChangeType(newValue, newValue.GetType());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    logW.Ins.info($"Error converting value for {key1}.{key2}.{key3}: {ex.Message}");
                                    convertedValue = newValue; // Nếu không ép được kiểu, giữ nguyên giá trị gốc
                                }

                                // Kiểm tra nếu giá trị mới khác với giá trị hiện tại, nếu có thay đổi thì cập nhật
                                if (!Object.Equals(ucBlockBasicControllers[key1][key2][key3], convertedValue))
                                {
                                    // Cập nhật giá trị mới
                                    ucBlockBasicControllers[key1][key2][key3] = convertedValue;

                                    // Log kết quả cập nhật
                                    logW.Ins.info($"Updated {key} {key1}.{key2}.{key3}: {convertedValue} (previous: {ucBlockBasicControllers[key1][key2][key3]})");
                                }
                                else
                                {
                                    // Log trường hợp không có thay đổi
                                    // logW.Ins.info($"No update needed for {key1}.{key2}.{key3} = {convertedValue}, value is unchanged.");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ucBlockBasicControllersCached.Add(key, new Dictionary<string, Dictionary<string, Dictionary<string, object>>>());
            }
            
        }

        public void updateCachedConfig()
        {
            string key = modelNameExec;
            ucBlockBasicControllersCached[key] = ucBlockBasicControllers;
        }

        public void SaveConfig(string filePath = "ParameterAOI.yaml")
        {
            updateCachedConfig();
            SaveToYaml(ucBlockBasicControllersCached, filePath);
        }


        private T LoadFromYaml<T>(string filePath)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance) // Dùng camelCase
                .Build();

            var yaml = File.ReadAllText(filePath); // Đọc tệp YAML
            var yaml1 = deserializer.Deserialize<T>(yaml);
            return yaml1; // Giải tuần tự hóa
        }

        private void SaveToYaml(object data, string filePath)
        {
            string msg = "LUU PARAMETERS THANH CONG.";
            try
            {
                var serializer = new SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance) // Dùng camelCase
                    .Build();

                var yaml = serializer.Serialize(data);

                File.WriteAllText(filePath, yaml); // Ghi YAML ra tệp
            }
            catch (Exception e) { msg = "LUU PARAMETERS THAT BAI."; }
            logW.Ins.warning(msg);
            //using (var dia = new CustomMessageBox(msg)) {  dia.ShowDialog(); }

        }

        public void SetDataToPropertyGrid()
        {
            //VisionUI.
        }

        public void GetNestedDictionaryWrapper(Dictionary<string, object> NestDict)
        {
            Dictionary<string, object> tmpDict = new Dictionary<string, object>();
            foreach (var key in NestDict.Keys)
            {
                // Kiểm tra nếu giá trị là một Dictionary cấp 2
                if (NestDict[key] is Dictionary<string, object> nestedDict)
                {
                    foreach (var key1 in nestedDict.Keys)
                    {
                        tmpDict.Add(key1, nestedDict[key1]);
                    }
                }
                else
                {
                    //
                }
            }
        }

        private static readonly object lockAddParamObject = new object();
        public bool AddParamManager(string funcID, string cvID)
        {
            lock (lockAddParamObject)
            {
                try
                {
                    if (listCVFunc.Contains($"{funcID}_0_{cvID}"))
                    {
                        return false;
                    }
                    string[] res_cv_func = cvID.Split(new string[] { "_0_" }, StringSplitOptions.None);
                    Dictionary<string, object> defaultParam = new Dictionary<string, object>();
                    foreach (string key in XParameter.Instance.GetParameter(res_cv_func[0]).Keys)
                    {
                        defaultParam[key] = XParameter.Instance.GetParameter(res_cv_func[0])[key];
                    }
                    if (!ucBlockBasicControllers.ContainsKey(funcID))
                    {
                        ucBlockBasicControllers.Add(funcID, new Dictionary<string, Dictionary<string, object>>());
                    }

                    if (!ucBlockBasicControllers[funcID].ContainsKey(cvID))
                    {
                        ucBlockBasicControllers[funcID].Add(cvID, defaultParam);
                        Dictionary<string, object> keyValuePairs = ConvertDictToNestDict(ucBlockBasicControllers[funcID]);
                        NestedDictionaryWrapper wrapper = new NestedDictionaryWrapper(keyValuePairs);
                        propertyGridData.Parameters = wrapper;
                        propertyGridData.posX = 0;
                        propertyGridData.posY = 0;
                        propertyGridData.toolID = funcID;
                    }
                    listCVFunc.Add($"{funcID}_0_{cvID}");
                    updateConfig();
                    return true;
                }
                catch (Exception ae)
                {
                    logW.Ins.info($"[AddParamManager] ex: {ae}");
                }
                updateConfig();
            }
            return false;
        }

        public bool ChangeFunctionDebug(string funcID)
        {
            try
            {
                if (!ucBlockBasicControllers.ContainsKey(funcID)) return false;
                Dictionary<string, object> keyValuePairs = ConvertDictToNestDict(ucBlockBasicControllers[funcID]);
                NestedDictionaryWrapper wrapper = new NestedDictionaryWrapper(keyValuePairs);
                propertyGridData.Parameters = wrapper;
                propertyGridData.toolID = funcID;
                return true;
            }
            catch (Exception ex)
            {
                logW.Ins.info($"[ChangeFunctionDebug] ex: {ex}");
            }
            return false;
        }

        protected object LockObj1 = new object();
        public bool AddParamManagerROI1(string funcID, string cvID)
        {
            lock (LockObj1)
            {
                try
                {
                    // Kiểm tra và thêm vào listCVFunc nếu chưa tồn tại
                    if (listCVFunc.Contains($"{funcID}_0_{cvID}"))
                    {
                        return false;
                    }
                    else
                    {
                        if (ucBlockBasicControllers.ContainsKey(funcID) && ucBlockBasicControllers[funcID].ContainsKey(cvID))
                        {

                            listCVFunc.Add($"{funcID}_0_{cvID}");
                            return true;
                        }
                    }
                    // Lấy tham số từ XParameter và kiểm tra null
                    string[] res_cv_func = cvID.Split(new string[] { "_0_" }, StringSplitOptions.None);
                    var param = XParameter.Instance.GetParameter(res_cv_func[0]);
                    if (param == null)
                    {
                        logW.Ins.info("No parameters found for: " + res_cv_func[0]);
                        return false;
                    }

                    // Sao chép tham số vào defaultParam
                    Dictionary<string, object> defaultParam = new Dictionary<string, object>();
                    foreach (string key in param.Keys)
                    {
                        defaultParam[key] = (key.Contains("BlockParentKey")) ? funcID : param[key];
                    }
                    // Kiểm tra và thêm vào ucBlockBasicControllers
                    if (!ucBlockBasicControllers.ContainsKey(funcID))
                    {
                        ucBlockBasicControllers.Add(funcID, new Dictionary<string, Dictionary<string, object>>());
                    }
                    // Kiểm tra cvID và thêm vào ucBlockBasicControllers
                    if (!ucBlockBasicControllers.ContainsKey(cvID))
                    {
                        ucBlockBasicControllers.Add(cvID, new Dictionary<string, Dictionary<string, object>>());
                    }

                    // Thêm tham số vào funcID và cvID
                    if (!ucBlockBasicControllers[funcID].ContainsKey(cvID))
                    {
                        ucBlockBasicControllers[funcID].Add(cvID, defaultParam);
                        Dictionary<string, object> keyValuePairs = ConvertDictToNestDict(ucBlockBasicControllers[funcID]);
                        NestedDictionaryWrapper wrapper = new NestedDictionaryWrapper(keyValuePairs);
                        propertyGridData.Parameters = wrapper;
                        propertyGridData.posX = 0;
                        propertyGridData.posY = 0;
                        propertyGridData.toolID = funcID;
                    }

                    // Thêm hình mới vào ShapeEditor
                    onAddNewShapeROI?.Invoke(funcID, cvID);
                    listCVFunc.Add($"{funcID}_0_{cvID}");
                    return true;
                }
                catch (Exception ex)
                {
                    logW.Ins.info($"[AddParamManagerROI1] ex: {ex}");
                    return false; // Xử lý lỗi
                }
            }
            return false;
        }

        protected object LockObj2 = new object();
        public bool AddParamManagerROI2(string funcID, string cvID)
        {
            lock (LockObj2)
            {
                try
                {
                    // Kiểm tra và thêm vào listCVFunc nếu chưa tồn tại
                    if (listCVFunc.Contains($"{funcID}_0_{cvID}"))
                    {
                        return false;
                    }
                    else
                    {
                        if (ucBlockBasicControllers.ContainsKey(funcID) && ucBlockBasicControllers[funcID].ContainsKey(cvID))
                        {

                            listCVFunc.Add($"{funcID}_0_{cvID}");
                            return true;
                        }
                    }
                    // Lấy tham số từ XParameter và kiểm tra null
                    string[] res_cv_func = cvID.Split(new string[] { "_0_" }, StringSplitOptions.None);
                    var param = XParameter.Instance.GetParameter(res_cv_func[0]);
                    if (param == null)
                    {
                        logW.Ins.info("No parameters found for: " + res_cv_func[0]);
                        return false;
                    }

                    // Sao chép tham số vào defaultParam
                    Dictionary<string, object> defaultParam = new Dictionary<string, object>();
                    foreach (string key in param.Keys)
                    {
                        defaultParam[key] = (key.Contains("BlockParentKey")) ? funcID : (key.Contains("BlockID")) ? $"{funcID}_0_{cvID}" : param[key];
                    }
                    // Kiểm tra và thêm vào ucBlockBasicControllers
                    if (!ucBlockBasicControllers.ContainsKey(funcID))
                    {
                        ucBlockBasicControllers.Add(funcID, new Dictionary<string, Dictionary<string, object>>());
                    }

                    // Thêm tham số vào funcID và cvID
                    if (!ucBlockBasicControllers[funcID].ContainsKey(cvID))
                    {
                        ucBlockBasicControllers[funcID][cvID] = defaultParam;
                        Dictionary<string, object> keyValuePairs = ConvertDictToNestDict(ucBlockBasicControllers[funcID]);
                        NestedDictionaryWrapper wrapper = new NestedDictionaryWrapper(keyValuePairs);
                        propertyGridData.Parameters = wrapper;
                        propertyGridData.posX = 0;
                        propertyGridData.posY = 0;
                        propertyGridData.toolID = funcID;
                    }
                    listCVFunc.Add($"{funcID}_0_{cvID}");
                    ShapesBase.Instance.OnActions?.Invoke(funcID, cvID);
                    return true;
                }
                catch (Exception ex)
                {
                    logW.Ins.info($"[AddParamManagerROI2] ex: {ex}");
                    return false; // Xử lý lỗi
                }
            }
            updateConfig();
            return false;
        }
        public Dictionary<string, object> ConvertDictToNestDict(Dictionary<string, Dictionary<string, object>> dictParams)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>() { };
            foreach (var keyVa in dictParams.Keys)
            {
                keyValuePairs.Add(keyVa, dictParams[keyVa]);
            }
            return keyValuePairs;
        }

        public void AddInitialROI1ParameterCV(object value, object[] limitValue)
        {
            XParameter.Instance.SetParameter("ROI1", "BlockID", value, limitValue);
        }

        public void InitialParameterCV()
        {
            propertyGridData.toolID = "TOOL_C9200";
            //string colorName = "Green";
            //Colors color = (Colors)Enum.Parse(typeof(Colors), colorName);
            // ROI1 to show cac dialog type1: chi co true/fale o ket qua blockID va BlockName de mac dinh
            XParameter.Instance.SetParameter("ROI1", "x1", 0, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI1", "y1", 0, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI1", "x2", 10, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI1", "y2", 10, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI1", "BlockID", "");       // autogen
            XParameter.Instance.SetParameter("ROI1", "BlockName", "NamE");     // nhap tay
            XParameter.Instance.SetParameter("ROI1", "BlockParentKey", "");     // nhap tay

            // ROI1 to show cac dialog type1: chi co true/fale o ket qua blockID va BlockName de mac dinh
            XParameter.Instance.SetParameter("ROITEMPMATCH", "x1", 0, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROITEMPMATCH", "y1", 0, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROITEMPMATCH", "x2", 10, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROITEMPMATCH", "y2", 10, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROITEMPMATCH", "BlockID", "");       // autogen
            XParameter.Instance.SetParameter("ROITEMPMATCH", "BlockName", "NamE");     // nhap tay
            XParameter.Instance.SetParameter("ROITEMPMATCH", "BlockParentKey", "");     // nhap tay

            // ROI1 to show cac dialog type1: set blocID va BlocName
            XParameter.Instance.SetParameter("ROI2", "x1", 0, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI2", "y1", 0, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI2", "x2", 50, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI2", "y2", 50, new object[] { -999999, 999999, 1 });


            XParameter.Instance.SetParameter("ROI2", "xT", 0, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI2", "yT", 0, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI2", "w1", 5, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI2", "h1", 5, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI2", "w2", 5, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI2", "h2", 5, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("ROI2", "BlockID", "");       // autogen
            XParameter.Instance.SetParameter("ROI2", "BlockName", "NamE");     // nhap tay
            XParameter.Instance.SetParameter("ROI2", "BlockParentKey", "");     // nhap tay

            // ussing to set min max
            XParameter.Instance.SetParameter("CVLimits", "min", 0, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("CVLimits", "max", 1, new object[] { -999999, 999999, 1 });

            // return a number (can use to count, lenght or number of anything)
            XParameter.Instance.SetParameter("CVNumberF", "number", 1, new object[] { -999999, 999999, 0.1 });
            XParameter.Instance.SetParameter("CVNumberD", "number", 1, new object[] { -999999, 999999, 0.01 });
            XParameter.Instance.SetParameter("CVNumber", "number", 1, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("CVReadOnly", "ReadOnly", 1, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("CVBool", "boolean", 1, new object[] { 0, 1, 1 });
            // 
            XParameter.Instance.SetParameter("ImShow", "boolean", 0, new object[] { 0, 1, 1 });

            // CharArray
            XParameter.Instance.SetParameter("CharArray", "Characters", "Char1,char2");            // CharArray
                                                                                                   // Lấy tất cả tên của enum ColorEnum
            string[] enumColor = Enum.GetNames(typeof(KnownColor));
            string[] enumHerSheyFont = Enum.GetNames(typeof(OpenCvSharp.HersheyFonts));

            // Chuyển đổi mảng string[] sang object[]
            object[] enumColorObjectArray = enumColor as object[];
            XParameter.Instance.SetParameter("Scalar", "color", "ControlDarkDark", enumColorObjectArray);
            object[] enumHerSheyFontArray = enumHerSheyFont as object[];
            XParameter.Instance.SetParameter("OFont", "HerSheyType", "HersheySimplex", enumHerSheyFontArray);
            XParameter.Instance.SetParameter("OFont", "thickness", 3, new object[] { 0, 255, 1 });
            XParameter.Instance.SetParameter("OFont", "fontScale", 3, new object[] { 0, 255, 1 });
            // MatchTemplate
            XParameter.Instance.SetParameter("MatchTemplate", "TemplateMatchModes", "CCoeffNormed", new object[] { "CCoeffNormed", "SqDiff", "SqDiffNormed", "CCorr", "CCorrNormed", "CCoeff" });
            XParameter.Instance.SetParameter("PerformPatternMatching", "MatchAlgorithm", "MaxAccuracy", new object[] { "QuickFinding", "MaxAccuracy", "EdgeMatching" });
            // Threshold
            XParameter.Instance.SetParameter("Threshold", "thresh", 127, new object[] {  0, 255, 1 });
            XParameter.Instance.SetParameter("Threshold", "maxVal", 255, new object[] {  0, 255, 1 });
            XParameter.Instance.SetParameter("Threshold", "type", "Binary", new object[] {  "Binary", "BinaryInv", "Trunc", "Tozero", "TozeroInv", "Mask", "Otsu", "Triangle" });

            // AdaptiveThreshold
            XParameter.Instance.SetParameter("AdaptiveThreshold", "maxValue", 255, new object[] {  0, 255, 1});
            XParameter.Instance.SetParameter("AdaptiveThreshold", "adaptiveMethod", "GaussianC", new object[] {  "MeanC", "GaussianC" });
            XParameter.Instance.SetParameter("AdaptiveThreshold", "thresholdType", "Binary", new object[] {  "Binary", "BinaryInv", "Trunc", "Tozero", "TozeroInv", "Mask", "Otsu", "Triangle" });
            XParameter.Instance.SetParameter("AdaptiveThreshold", "blockSize", 3, new object[] {  0, 255, 1 });
            XParameter.Instance.SetParameter("AdaptiveThreshold", "c", 3.0, new object[] {  0, 255, 1 });

            // HoughCircles
            XParameter.Instance.SetParameter("HoughCircles", "HoughModes", "Gradient", new object[] { "Standard", "Probabilistic", "MultiScale", "GradientAlt", "Gradient" });
            XParameter.Instance.SetParameter("HoughCircles", "dp", 255, new object[] { 1, 255, 1 });
            XParameter.Instance.SetParameter("HoughCircles", "minDist", 3, new object[] { 1, 255, 1 });
            XParameter.Instance.SetParameter("HoughCircles", "param1", 30, new object[] { 1, 255, 1 });
            XParameter.Instance.SetParameter("HoughCircles", "param2", 60, new object[] { 1, 255, 1 });
            XParameter.Instance.SetParameter("HoughCircles", "minRadius", 3, new object[] { 1, 255, 1 });
            XParameter.Instance.SetParameter("HoughCircles", "maxRadius", 3, new object[] { 1, 255, 1 });

            // Add
            XParameter.Instance.SetParameter("Add", "dtype", -1, new object[] {  -200, 200, 1 });
        }
    }
}
