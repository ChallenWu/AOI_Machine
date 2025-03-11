using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVisionPro
{
    public partial class XModels
    {
        public Dictionary<string, Dictionary<int, Func<OpenCvSharp.Mat, int, bool>>> TaskRuns = new Dictionary<string, Dictionary<int, Func<OpenCvSharp.Mat, int, bool>>>();
        public Dictionary<string, bool> taskRunStatus = new Dictionary<string, bool>();
        
        public Action<string> OnDelModelAcion;
        public Action<string> OnUpdateModelRunningAcion;
        public Action OnAddModelAcion;
        private string _modelRunning;
        public string modelDebugging { get; set; }
        public string modelRunning 
        {
            get { return _modelRunning; }
            set {
                _modelRunning = value;
                XParameterManager.Instance.modelNameExec = value;
                XParameterManager.Instance.InitialParameterCV();
                XParameterManager.Instance.LoadConfig();
                OnUpdateModelRunningAcion?.Invoke(value);
            }
        }

        
        private readonly static XModels instance = new XModels();
        public static XModels Instance { get { return instance; } }

        public XModels() { }

        public void AddTaskRun(string modelName, int modelTaskId, Func<OpenCvSharp.Mat, int, bool> taskrun)
        {
            if (!TaskRuns.ContainsKey(modelName))
            {
                TaskRuns.Add(modelName, new Dictionary<int, Func<OpenCvSharp.Mat, int, bool>>());
                OnAddModelAcion?.Invoke();
            }
            TaskRuns[modelName][modelTaskId] = taskrun;
            taskRunStatus[$"{modelName}_0_{modelTaskId}"] = false;
            logW.Ins.info($"AddTaskRun >> THEM MODEL {modelName} - modelTaskId: {modelTaskId}");
        }
        public bool RemoveModel(string modelName)
        {
            if (modelName == modelRunning)
            {
                logW.Ins.info($"RemoveModel >> XOA MODEL {modelName} NG - MODEL LA MODEL DANG CHON.");
                return false;
            }
            if (TaskRuns.ContainsKey(modelName))
            {
                TaskRuns.Remove(modelName);
                OnDelModelAcion?.Invoke(modelName);
                logW.Ins.info($"RemoveModel >> XOA MODEL {modelName} OK.");
                return true;
            }
            logW.Ins.info($"RemoveModel >> XOA MODEL {modelName} NG - MODEL CHUA TON TAI.");
            return false;
        }
    }
}
