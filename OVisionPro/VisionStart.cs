using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using Serilog;

namespace OVisionPro
{
    public class VisionStart
    {
        private static List<Task> taskVision = new List<Task>();

        private static void clearTaskVision()
        {
            taskVision.Clear();
        }

        public static void Start_step1(Mat frameImg)
        {
            // Tạo Task cho case 2
            taskVision.Add(Task.Run(() =>
            {
                Log.Information("Running Capture 1.");
                DoAction1();
            }));

        }
        public static void Start_step2(Mat frameImg)
        {
            // Tạo Task cho case 2
            taskVision.Add(Task.Run(() =>
            {
                Log.Information("Running Capture 1.");
                //ImageToolC9200.Instance.ExecToolModel1();
            }));
            // Chờ tất cả các Task hoàn thành
            Task.WaitAll(taskVision.ToArray());
            Log.Information("All tasks completed.");
        }

        private static void DoAction1()
        {
            // Logic của case 2
            Log.Information("Executing action for case 2...");
            // Giả lập thời gian thực hiện
            Task.Delay(3000).Wait();
            Log.Information("Case 2 action completed.");
        }

        private static void DoAction2()
        {
            // Logic của case 2
            Log.Information("Executing action for case 2...");
            // Giả lập thời gian thực hiện
            Task.Delay(2000).Wait();
            Log.Information("Case 2 action completed.");
        }
    }

}
