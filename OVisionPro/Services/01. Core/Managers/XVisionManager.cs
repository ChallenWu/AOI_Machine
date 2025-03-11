using OVisionPro.Services.ImageProcessing;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro
{
    public partial class XVisionManager
    {
        public visionGlob.RunMode VisionRunMode = visionGlob.RunMode.production;
        public Dictionary<int, bool> FinalResult = new Dictionary<int, bool>();
        public Dictionary<int, bool> OnCaptureDone = new Dictionary<int, bool>();
        public Action<string> OnUpdateResultUI;
        public Action XcountBlocksAction;
        public string mainSN = "DEFAULT";
        public static int XcountBlocks = 0;
        public string ImgUrlTest = "";
        public int taskDegging = 0;

        public XVisionManager() { }
        private readonly static XVisionManager instance = new XVisionManager();
        public static XVisionManager Instance { get { return instance; } }
        public Task StartWorkInTask(int modelTaskId)
        {
            OnCaptureDone[modelTaskId] = false;
            string modelexec = XParameterManager.Instance.modelNameExec;
            string taskstatusID = $"{modelexec}_0_{modelTaskId}";
            // Ignore if last exec is not DONE.
            if (FinalResult.ContainsKey(modelTaskId)) { logW.Ins.info($"CCD {modelTaskId} DA HOAN THANH. \n>>>DANG CHO CCD TIEP THEO"); Task.FromResult(false); }
            // Ignore process while deugging.
            if (VisionRunMode == visionGlob.RunMode.debug) return Task.FromResult(false);
            if (XModels.Instance.taskRunStatus.ContainsKey(taskstatusID) && XModels.Instance.taskRunStatus[taskstatusID]) return Task.FromResult(false);
            // update to load config while running on production.
            if (XModels.Instance.modelRunning != modelexec) { XModels.Instance.modelRunning = XModels.Instance.modelRunning; }
            XModels.Instance.taskRunStatus[taskstatusID] = true;
            return Task.Run(() =>
            {
                try
                {
                    // Thực thi hành động xử lý ảnh
                    if (XModels.Instance.TaskRuns.ContainsKey(modelexec) && XModels.Instance.TaskRuns[modelexec].ContainsKey(modelTaskId))
                    {
                        OpenCvSharp.Mat frame = XCCD.Instance.HIK1.frame;
                        OnCaptureDone[modelTaskId] = true;
                        if (frame != null)
                        {
                            Func<OpenCvSharp.Mat, int, bool> func1 = XModels.Instance.TaskRuns[modelexec][modelTaskId];
                            bool res = func1(frame, modelTaskId);
                            FinalResult.Add(modelTaskId, res);
                            if (FinalResult.Count == 2)
                            {
                                string finalRes = (!FinalResult.Values.Contains(false)) ? "OK" : "NG";
                                OnUpdateResultUI?.Invoke(finalRes);
                            }
                            if (XcountBlocks != XParameterManager.Instance.ucBlockBasicControllers.Keys.Count)
                            {
                                XcountBlocks = XParameterManager.Instance.ucBlockBasicControllers.Count;
                                XcountBlocksAction?.Invoke();
                            }
                        }
                        // Giải phóng tài nguyên ảnh
                        frame?.Dispose();
                    }
                }
                catch (Exception ex) { logW.Ins.info($"[StartWorkintask] ex: {ex}"); }
                finally
                {
                    // Đặt lại trạng thái taskRunStatus khi hoàn thành
                    XModels.Instance.taskRunStatus[taskstatusID] = false;
                }
            });
        }

        public async Task StartWorkInTaskDebug(int modelTaskId)
        {
            modelTaskId = taskDegging;
            string modelName = XParameterManager.Instance.modelNameExec;
            // Kiểm tra nếu task đang chạy
            string taskKey = $"DB_{modelName}_0_{modelTaskId}";
            // Nếu task đang chạy, trả về một Task.CompletedTask (để đảm bảo trả về Task hợp lệ)
            if (XModels.Instance.taskRunStatus.ContainsKey(taskKey) && XModels.Instance.taskRunStatus[taskKey]) { return; }
            XModels.Instance.taskRunStatus[taskKey] = true;
            OnCaptureDone[modelTaskId] = false;
            // Sử dụng Task.Run để chạy công việc trong một luồng nền
            await Task.Run(() =>
            {
                try
                {
                    // Xử lý ảnh
                    if (XModels.Instance.TaskRuns.ContainsKey(modelName) && XModels.Instance.TaskRuns[modelName].ContainsKey(modelTaskId))
                    {
                        OpenCvSharp.Mat frame = OpenImage(ImgUrlTest);
                        OnCaptureDone[modelTaskId] = true;
                        if (OBasicAlgorithm.IsMat(frame))
                        {
                            Func<OpenCvSharp.Mat, int, bool> func1 = XModels.Instance.TaskRuns[modelName][modelTaskId];
                            bool res = func1(frame, modelTaskId);
                            // show messkhi lỗi
                            if (FinalResult.Count == 2) {
                                string finalRes = (FinalResult.Values.Contains(false)) ? "NG" : "OK";
                                OnUpdateResultUI?.Invoke(finalRes);
                            }
                            if (XcountBlocks != XParameterManager.Instance.ucBlockBasicControllers.Keys.Count)
                            {
                                XcountBlocks = XParameterManager.Instance.ucBlockBasicControllers.Count;
                                XcountBlocksAction?.Invoke();
                            }
                            // Giải phóng tài nguyên ảnh
                            frame?.Dispose();
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    logW.Ins.warning($"[SHAPE][2] StartWorkInTaskDebug: \n {ex.ToString()}");
                }

                // Đặt lại trạng thái taskRunStatus khi hoàn thành
                XModels.Instance.taskRunStatus[taskKey] = false;
            });
        }

        OpenCvSharp.Mat OpenImage(string filePath)
        {
            if (filePath == null) { return null; }
            try { return OpenCvSharp.Cv2.ImRead(filePath); }
            catch (Exception ex)
            {
                logW.Ins.Except($"[VISIONMAN] LOI LOAD ANH");
            }
            return null;
        }
    }
}
