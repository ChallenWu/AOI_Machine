using OpenCvSharp.ML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace OVisionPro
{
    class XParameterManager
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, object>>> ucBlockBasicControllersCached = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
        public Dictionary<string, Dictionary<string, Dictionary<string, object>>> ucBlockBasicControllers = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>() { };
        public List<string> listCVFunc = new List<string>() { };
        public XObjectCVParameters propertyGridData = new XObjectCVParameters();
        public dynamic cvPropertyGridControl = new XObjectCVParameters();
        public Action<string> onUpdateModelNameToUIAction;
        public Action<string> onUpdateFuncIDToUIAction;

        private readonly static XParameterManager instance = new XParameterManager();
        public static XParameterManager Instance
        {
            get { return instance; }
        }

        public T GetUcBlockBasicData<T>(string funcID, string cvID, string paramName, T defaultValue)
        {
            if (ucBlockBasicControllers.ContainsKey(funcID))
            {
                if (ucBlockBasicControllers[funcID].ContainsKey(cvID))
                {
                    string[] res_cv_func = cvID.Split(new string[] { "_0_" }, StringSplitOptions.None);
                    string paramcvID = $"{res_cv_func[0]}_1_{paramName}";
                    var s = ucBlockBasicControllers[funcID][cvID];
                    if (ucBlockBasicControllers[funcID][cvID].TryGetValue(paramcvID, out object value))
                    {
                        return (T)value;
                    }
                }
            }
            return defaultValue;
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
            catch { };
            return false;
        }

        public void LoadConfig(string filePath = "ParameterAOI.yaml")
        {
            try
            {
                // Load dữ liệu từ tệp YAML vào Dictionary
                ucBlockBasicControllersCached = LoadFromYaml<Dictionary<string, Dictionary<string, Dictionary<string, object>>>>(filePath);
                updateConfig();
            }
            catch (Exception ex)
            {
                // Ghi log lỗi với thông tin chi tiết
                Console.WriteLine($"Error loading config: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void updateConfig()
        {
            foreach (string key1 in ucBlockBasicControllersCached.Keys)
            {
                // Kiểm tra nếu ucBlockBasicControllers đã chứa key1
                if (ucBlockBasicControllers.ContainsKey(key1))
                {
                    foreach (string key2 in ucBlockBasicControllersCached[key1].Keys)
                    {
                        // Kiểm tra nếu ucBlockBasicControllers[key1] chứa key2
                        if (ucBlockBasicControllers[key1].ContainsKey(key2))
                        {
                            foreach (string key3 in ucBlockBasicControllersCached[key1][key2].Keys)
                            {
                                // Kiểm tra nếu ucBlockBasicControllers[key1][key2] chứa key3
                                if (ucBlockBasicControllers[key1][key2].ContainsKey(key3))
                                {
                                    var newValue = ucBlockBasicControllersCached[key1][key2][key3];

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
                                            Console.WriteLine($"Error converting value for {key1}.{key2}.{key3}: {ex.Message}");
                                            convertedValue = newValue; // Nếu không ép được kiểu, giữ nguyên giá trị gốc
                                        }

                                        // Kiểm tra nếu giá trị mới khác với giá trị hiện tại, nếu có thay đổi thì cập nhật
                                        if (!Object.Equals(ucBlockBasicControllers[key1][key2][key3], convertedValue))
                                        {
                                            // Cập nhật giá trị mới
                                            ucBlockBasicControllers[key1][key2][key3] = convertedValue;

                                            // Log kết quả cập nhật
                                            Console.WriteLine($"Updated {key1}.{key2}.{key3}: {convertedValue} (previous: {ucBlockBasicControllers[key1][key2][key3]})");
                                        }
                                        else
                                        {
                                            // Log trường hợp không có thay đổi
                                            Console.WriteLine($"No update needed for {key1}.{key2}.{key3}, value is unchanged.");
                                        }
                                    }
                                }
                                else
                                {
                                    // Log trường hợp không tìm thấy key3 trong ucBlockBasicControllers
                                    Console.WriteLine($"Key3 {key3} not found in ucBlockBasicControllers[{key1}][{key2}]");
                                }
                            }
                        }
                        else
                        {
                            // Log trường hợp không tìm thấy key2 trong ucBlockBasicControllers
                            Console.WriteLine($"Key2 {key2} not found in ucBlockBasicControllers[{key1}]");
                        }
                    }
                }
                else
                {
                    // Log trường hợp không tìm thấy key1 trong ucBlockBasicControllers
                    Console.WriteLine($"Key1 {key1} not found in ucBlockBasicControllers.");
                }
            }
        }

        public void SaveConfig(string filePath = "ParameterAOI.yaml")
        {
            SaveToYaml(ucBlockBasicControllers, filePath);
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
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance) // Dùng camelCase
                .Build();

            var yaml = serializer.Serialize(data);

            File.WriteAllText(filePath, yaml); // Ghi YAML ra tệp
        }

        public void SetDataToPropertyGrid()
        {
            //VisionUI.
        }

        public void LoadUCBasicDataFromConfig(string funcID)
        {
            // Load data to property Grid
            Dictionary<string, Dictionary<string, object>> cvIDs = ucBlockBasicControllers[funcID];
            foreach (var cvID in cvIDs.Keys)
            {
                cvPropertyGridControl.name = 1;
                // Cập nhật thuộc tính 'Name' với mô tả mới
                System.ComponentModel.PropertyDescriptor nameProperty = TypeDescriptor.GetProperties(cvPropertyGridControl)["Name"];
                var nameCategoryAttribute = new CategoryAttribute("Personal Information");
                var nameDescriptionAttribute = new DescriptionAttribute("The person's legal full name.");

            }
            // Cập nhật lại PropertyGrid để phản ánh thay đổi
            // propertyGrid1.Refresh();
        }

        // Lấy giá trị từ cấu hình nếu có, nếu không thì trả về giá trị mặc định
        public bool CreateUserControlBlockBasic(string funcID, string cvID)
        {
            try
            {

            } 
            catch (Exception ce)
            {

            }
            return false;
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

        public bool AddParamManager(string funcID, string cvID)
        {
            try
            {
                if (listCVFunc.Contains($"{funcID}_{cvID}"))
                {
                    return false;
                }
                listCVFunc.Add($"{funcID}_{cvID}");
                onUpdateFuncIDToUIAction?.Invoke(funcID);
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
                    propertyGridData.posX = 0;
                    propertyGridData.posY = 0;
                    propertyGridData.dictTool = funcID;
                    propertyGridData.Parameters = wrapper;
                    propertyGridData.DictNestAll.Add(funcID, wrapper);
                }
                updateConfig();
                return true;
            }
            catch (Exception ae)
            {

            }
            updateConfig();
            return false;
        }

        public bool ChangeFunctionDebug(string funcID)
        {
            try
            {
                Dictionary<string, object> keyValuePairs = ConvertDictToNestDict(ucBlockBasicControllers[funcID]);
                NestedDictionaryWrapper wrapper = new NestedDictionaryWrapper(keyValuePairs);
                propertyGridData.Parameters = wrapper;
                propertyGridData.dictTool = funcID;
                return true;
            }
            catch (Exception ae) { }
            return false;
        }
        protected object LockObj = new object();
        public bool AddParamManagerROI(string funcID, string cvID)
        {
            lock (LockObj)
            {
                try
                {
                    if (listCVFunc.Contains($"{funcID}_0_{cvID}"))
                    {
                        return false;
                    }
                    listCVFunc.Add($"{funcID}_0_{cvID}");
                    onUpdateFuncIDToUIAction?.Invoke(funcID);
                    string[] res_cv_func = cvID.Split(new string[] { "_0_" }, StringSplitOptions.None);
                    Dictionary<string, object> defaultParam = new Dictionary<string, object>();
                    foreach(string key in XParameter.Instance.GetParameter(res_cv_func[0]).Keys){
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
                        propertyGridData.posX = 0;
                        propertyGridData.posY = 0;
                        propertyGridData.dictTool = funcID;
                        propertyGridData.Parameters = wrapper;
                        propertyGridData.DictNestAll.Add($"{funcID}_{cvID}", wrapper);
                    }
                    ShapeEditor.Instance.AddNewShape(funcID, cvID);

                    LoadConfig();
                    return true;
                }
                catch (Exception ae)
                {

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



        public void InitialParameterCV()
        {
            propertyGridData.dictTool = "Store";
            //string colorName = "Green";
            //Colors color = (Colors)Enum.Parse(typeof(Colors), colorName);
            // ROI
            XParameter.Instance.SetParameter("SelectROI", "x1", 0, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("SelectROI", "y1", 0, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("SelectROI", "x2", 0, new object[] { -999999, 999999, 1 });
            XParameter.Instance.SetParameter("SelectROI", "y2", 0, new object[] { -999999, 999999, 1 });

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

            // Add
            XParameter.Instance.SetParameter("Add", "dtype", -1, new object[] {  -200, 200, 1 });
        }
    }
}
