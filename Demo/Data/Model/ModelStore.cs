using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore;

namespace Models
{
    class ModelStore
    {
        private static String MODEL_SETTINGS_FILE_NAME = "model_settings.json";

        private static Object lockObj = new object();

        public static ModelSettings GetModelSettings(String modelName)
        {
            ModelSettings ret = null;

            lock (lockObj)
            {
                try
                {
                    // Load settings from file:
                    String filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), MODEL_SETTINGS_FILE_NAME);
                    if (File.Exists(filePath))
                    {
                        using (StreamReader file = File.OpenText(filePath))
                        {
                            var js = file.ReadToEnd();
                            var specList = JsonConvert.DeserializeObject<ModelSettings[]>(js);
                            foreach (var x in specList)
                            {
                                if (x.modelName.Equals(modelName))
                                {
                                    //ret = x.Clone();
                                    ret = x;
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
            return ret;
        }

        public static void UpdateModelSettings(ModelSettings newSettings)
        {
            lock (lockObj)
            {
                try
                {
                    List<ModelSettings> modelList = new List<ModelSettings>(0);

                    String filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), MODEL_SETTINGS_FILE_NAME);
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            using (StreamReader file = File.OpenText(filePath))
                            {
                                var js = file.ReadToEnd();
                                
                                modelList.AddRange(JsonConvert.DeserializeObject<ModelSettings[]>(js));
                            }
                        }
                        catch (Exception ex1)
                        {
                           
                        }
                    }

                    // Update existing:
                    var hasUpdated = false;
                    for (int i = 0; i < modelList.Count; i++)
                    {
                        if (modelList[i].HasSameModel(newSettings))
                        {
                            modelList[i] = newSettings;
                            hasUpdated = true;
                            break;
                        }
                    }

                    // Add new:
                    if (!hasUpdated)
                    {
                        modelList.Add(newSettings);
                    }

                    // Store:
                    var jsNew = JsonConvert.SerializeObject(modelList);
                    File.WriteAllText(filePath, jsNew);
                }
                catch (Exception ex)
                {
                   
                }
            }
        }

        public static List<ModelInfo> GetModelInfoList()
        {
            List<ModelInfo> ret = new List<ModelInfo>();

            lock (lockObj)
            {
                try
                {
                    ModelInfo.ResetIndex();

                    String filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), MODEL_SETTINGS_FILE_NAME);
                    if (File.Exists(filePath))
                    {
                        using (StreamReader file = File.OpenText(filePath))
                        {
                            var js = file.ReadToEnd();
                            var models = JsonConvert.DeserializeObject<ModelSettings[]>(js);
                            foreach (var x in models)
                            {
                                var modelInfo = new ModelInfo(x.modelName, x.updateTime);
                                ret.Add(modelInfo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
            return ret;
        }

        public static void DeleteModel(String model)
        {
            lock (lockObj)
            {
                try
                {
                    List<ModelSettings> modelList = new List<ModelSettings>(0);

                    String filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), MODEL_SETTINGS_FILE_NAME);
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            using (StreamReader file = File.OpenText(filePath))
                            {
                                var js = file.ReadToEnd();
                                modelList.AddRange(JsonConvert.DeserializeObject<ModelSettings[]>(js));
                            }
                        }
                        catch (Exception ex1)
                        {
                          
                        }
                    }

                    // Find & delete model:
                    var newList = new List<ModelSettings>();
                    var hasDelete = false;
                    for (int i = 0; i < modelList.Count; i++)
                    {
                        if (!modelList[i].modelName.Equals(model))
                        {
                            newList.Add(modelList[i]);
                        }
                        else
                        {
                            hasDelete = true;
                        }
                    }

                    // Store:
                    if (hasDelete)
                    {
                        var jsNew = JsonConvert.SerializeObject(newList);
                        File.WriteAllText(filePath, jsNew);
                    }
                }
                catch (Exception ex)
                {
                   
                }
            }
        }

        public static void DeleteAll()
        {
            lock (lockObj)
            {
                try
                {
                    List<ModelSettings> modelList = new List<ModelSettings>(0);
                    String filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), MODEL_SETTINGS_FILE_NAME);

                    // Store:
                    var jsNew = JsonConvert.SerializeObject(modelList);
                    File.WriteAllText(filePath, jsNew);

                }
                catch (Exception ex)
                {
                    
                }
            }
        }
    }

    class ModelInfo
    {
        private static int _index = 0;
        public int Index { get; set; }
        public String Name { get; set; }
        public String Time { get; set; }

        public static void ResetIndex()
        {
            _index = 0;
        }

        public ModelInfo(String name, DateTime updatedTime)
        {
            _index++;
            this.Index = _index;
            this.Name = name;
            this.Time = updatedTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
