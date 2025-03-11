using AutoStudio.Core.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Các thông số dành cho model 
    /// </summary>
    class ModelSettings
    {
        public const String Default_Model = "model1";
        public String modelName { get; set; }
        public List<PLC_Point> points { get; set; }
        public DateTime updateTime { get; set; }
        public List<VisionModel> visionModels { get; set; }

        public ModelSettings(bool initializeDefaults = true)
        {
            if (initializeDefaults)
            {
                this.updateTime = DateTime.Now;
                this.modelName = Default_Model;
                points = new List<PLC_Point>
            {
                new PLC_Point { Name = "Point0" }
            };
            //    visionModels = new List<VisionModel>
            //{
            //    new VisionModel { VisionModelName = "Capture1" },
            //    new VisionModel { VisionModelName = "Capture2" }
            //};
            }
            else
            {
                points = new List<PLC_Point>();
                //visionModels = new List<VisionModel>();
            }
        }

        public ModelSettings Clone()
        {
            return new ModelSettings(false)
            {
                modelName = this.modelName,
                updateTime = this.updateTime,
                points = this.points.Select(point => new PLC_Point
                {
                    Name = point.Name,
                }).ToList(),
                //visionModels = this.visionModels.Select(visionModel => visionModel.Clone()).ToList()
            };
        }

        public String ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static ModelSettings FromJson(String js)
        {
            return !string.IsNullOrEmpty(js)
                ? JsonConvert.DeserializeObject<ModelSettings>(js)
                : null;
        }

        public Boolean HasSameModel(ModelSettings x)
        {
            return x != null && !string.IsNullOrEmpty(this.modelName) && this.modelName.Equals(x.modelName);
        }
    }
    class PLC_Point
    {
        //Name point
        public string Name { get; set; } = "Point_1";
        //Tọa độ
        public int x { get; set; } = 0;
        public int y { get; set; } = 0;
        public int z { get; set; } = 0;
        public int r { get; set; } = 0;
        //Thanh ghi PLC
        public int X_Reg { get; set; }
        public int Y_Reg { get; set; }
        public int Z_Reg { get; set; }
        public int R_Reg { get; set; }  
               
    }

    class VisionModel
    {
        public string VisionModelName { get; set; } = "Vision Default";
        public List<VisionTool> tools { get; set; }

        // Constructor duy nhất
        public VisionModel(List<VisionTool> tools = null)
        {
            // Nếu tools là null, khởi tạo danh sách mặc định
            this.tools = tools ?? new List<VisionTool>
        {
            new VisionTool { VisionToolName = "CogToolBlob_0" }
        };
        }

        // Clone sâu (Deep Clone)
        public VisionModel Clone()
        {
            return new VisionModel(this.tools?.Select(tool => new VisionTool
            {
                VisionToolName = tool.VisionToolName}).ToList())
            {
                VisionModelName = this.VisionModelName
            };
        }
    }


    class VisionTool
    {
        public string VisionToolName { get; set; }

        public VisionTool Clone()
        {
            return new VisionTool
            {
                VisionToolName = this.VisionToolName
            };
        }
    }

    class AppSettings
    {
        public String currentModel { get; set; }
        public AppSettings()
        {
            currentModel = ModelSettings.Default_Model;
        }

        public static AppSettings FromJSON(String js)
        {
            var j = JsonConvert.DeserializeObject<AppSettings>(js);
            return j;
        }
        public String ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
 }
