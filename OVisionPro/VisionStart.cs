using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace OVisionPro
{
    class VisionStart
    {
        public Dictionary<string, OutVisionObject> VisionResult = new Dictionary<string, OutVisionObject>();
        private static List<Task> taskVision = new List<Task>();

        public Globals.TaskID toolRunstep1 = new Globals.TaskID();
        public Globals.TaskID toolRunstep2 = new Globals.TaskID();

        private static void clearTaskVision()
        {
            taskVision.Clear();
        }

        public static void Start_step1(Mat frameImg)
        {
            // Tạo Task cho case 2
            taskVision.Add(Task.Run(() =>
            {
                Console.WriteLine("Running Capture 1.");
                DoAction1();
            }));

        }
        public static void Start_step2(Mat frameImg)
        {
            // Tạo Task cho case 2
            taskVision.Add(Task.Run(() =>
            {
                Console.WriteLine("Running Capture 1.");
                DoAction1();
            }));
            // Chờ tất cả các Task hoàn thành
            Task.WaitAll(taskVision.ToArray());
            Console.WriteLine("All tasks completed.");
        }

        private static void DoAction1()
        {
            // Logic của case 2
            Console.WriteLine("Executing action for case 2...");
            // Giả lập thời gian thực hiện
            Task.Delay(3000).Wait();
            Console.WriteLine("Case 2 action completed.");
        }

        private static void DoAction2()
        {
            // Logic của case 2
            Console.WriteLine("Executing action for case 2...");
            // Giả lập thời gian thực hiện
            Task.Delay(2000).Wait();
            Console.WriteLine("Case 2 action completed.");
        }
    }

}
