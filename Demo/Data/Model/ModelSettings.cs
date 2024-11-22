using AutoStudio.Core.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Các thông số dành cho model 
    /// </summary>
    class ModelSettings
    {
        public const String Default_Model = "model_default";
        public String modelName { get; set; }
        public List<PLC_Point> points { get; set; }
        public DateTime updateTime { get; set; }

        public ModelSettings()
        {
            this.updateTime = DateTime.Now;
            this.modelName = Default_Model;
            points = new List<PLC_Point>();
        }
        public ModelSettings Clone()
        {
            return new ModelSettings
            {
                modelName = this.modelName,
                updateTime = this.updateTime,
                points = this.points
            };
        }
        public String ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static ModelSettings FromJson(String js)
        {
            var j = JsonConvert.DeserializeObject<ModelSettings>(js);
            return j;
        }

        public Boolean HasSameModel(ModelSettings x)
        {
            if (this.modelName != null && x != null && this.modelName.Equals(x.modelName))
            {
                return true;
            }
            return false;
        }
    }
     class PLC_Point
    {
        public string Name { get; set; } = "Point_1";
        public double x { get; set; } = 0;
        public double y { get; set; } = 0;
        public double z { get; set; } = 0;
        public double r { get; set; } = 0;
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
