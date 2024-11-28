using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro
{
    public partial class XVisionManager
    {
        public ImageToolBase FilterToolIns = new ImageToolBase();
        public XCCDHIK CCDInstance = new XCCDHIK();
        public Globals.RunMode VisionRunMode = Globals.RunMode.production;
        private List<string> modelNames = new List<string>();
        private string SelectedModel = "";
        private string SelectFuncID = "";
        private readonly static XVisionManager instance = new XVisionManager();
        public Dictionary<string, Dictionary<int, Action<OpenCvSharp.Mat>>> TaskRuns = new Dictionary<string, Dictionary<int, Action<OpenCvSharp.Mat>>>();
        public Dictionary<string, bool> taskRunStatus = new Dictionary<string, bool>();
        public TimeSpan CCTime;
        public Action OnUpdateCCTimeUI;

        public void AddTaskRun(string modelName, int modelTaskId, Action<OpenCvSharp.Mat> taskrun)
        {
            if (!TaskRuns.ContainsKey(modelName))
            {
                TaskRuns.Add(modelName, new Dictionary<int, Action<OpenCvSharp.Mat>>() { });
            }
            TaskRuns[modelName][modelTaskId] = taskrun;
            taskRunStatus[$"{modelName}_0_{modelTaskId}"] = false;
        }
        public void SetSelectModelRun(string modelRun)
        {
            if (!TaskRuns.Keys.Contains(modelRun))
            {
                MessageBox.Show($"SET MODEL NAME RUN FAIL. {modelRun} IS NOT ADDED");
            }
            else
            {
                SelectedModel = modelRun;
            }
        }

        public string GetSelectModelRun()
        {
            return SelectedModel;
        }


        public void SetSelectFuncID(string SelectFunc)
        {
            SelectFuncID = SelectFunc;
        }

        public string GetSelectFuncID()
        {
            return SelectFuncID;
        }

        public string StartWorkInTask(string modelName, int modelTaskId)
        {
            if (VisionRunMode == Globals.RunMode.debug)
            {
                return $"{modelName}_0_{modelTaskId} IS DEBUGGING";
            }
            if (taskRunStatus.ContainsKey($"{modelName}_0_{modelTaskId}") && taskRunStatus[$"{modelName}_0_{modelTaskId}"])
            {
                return $"{modelName}_0_{modelTaskId} IS RUNNING";
            }
            taskRunStatus[$"{modelName}_0_{modelTaskId}"] = true;
            //OpenCvSharp.OpenCvSharp.Mat frame = CCDInstance.frame;
            Task.Run(() =>
            {
                try
                {
                    DateTime startTime = DateTime.Now;
                    OpenCvSharp.Mat frame = CCDInstance.frame;
                    Action<OpenCvSharp.Mat> func1 = TaskRuns[modelName][modelTaskId];
                    func1(frame);

                    DateTime endTime = DateTime.Now;
                    // Tính khoảng thời gian giữa startTime và endTime
                    CCTime = endTime - startTime;
                    OnUpdateCCTimeUI?.Invoke();
                }
                catch (Exception x) { }
                taskRunStatus[$"{modelName}_0_{modelTaskId}"] = false;
            });
            return "";
        }
        
        public string StartWorkInTaskDebug(string modelName, int modelTaskId, string ImgUrl)
        {
            if (taskRunStatus.ContainsKey($"DB_{modelName}_0_{modelTaskId}") && taskRunStatus[$"{modelName}_0_{modelTaskId}"])
            {
                return $"DB_{modelName}_0_{modelTaskId} IS RUNNING";
            }
            taskRunStatus[$"DB_{modelName}_0_{modelTaskId}"] = true;
            Task.Run(() =>
            {
                try
                {
                    OpenCvSharp.Mat OpenImage(string filePath)
                    {
                        try
                        {
                            return OpenCvSharp.Cv2.ImRead(filePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error loading image: {ex.Message}");
                        }
                        return null;
                    }
                    DateTime startTime = DateTime.Now;

                    OpenCvSharp.Mat frame = OpenImage(ImgUrl);
                    Action<OpenCvSharp.Mat> func1 = TaskRuns[modelName][modelTaskId];
                    func1(frame);
                    DateTime endTime = DateTime.Now;
                    // Tính khoảng thời gian giữa startTime và endTime
                    CCTime = endTime - startTime;
                    OnUpdateCCTimeUI?.Invoke();
                    frame.Dispose();
                }
                catch (Exception ex) { }
                taskRunStatus[$"{modelName}_0_{modelTaskId}"] = false;
            });
            return "";
        }

        public void Initialize()
        {
            InitializeCCD();
            InitializeParameter();
        }
        public void InitializeParameter()
        {
            XParameterManager.Instance.InitialParameterCV();
            XParameterManager.Instance.LoadConfig();
        }
        public void InitializeCCD()
        {
            CCDInstance = new XCCDHIK();
            CCDInstance.Start(VisionRunMode);
        }

        XVisionManager()
        {

        }

        public static XVisionManager Instance
        {
            get { return instance; }
        }
    }
}
