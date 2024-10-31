using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;
using System.Diagnostics;

namespace HB_IWatch
{
    class CsvServer2
    {
        private Thread _thread;
        private ConcurrentQueue<CsvInfo2> queue = new ConcurrentQueue<CsvInfo2>();
        private readonly static CsvServer2 instance = new CsvServer2();
        private object obj = new object();
        CsvServer2() { }
        public static CsvServer2 Instance
        {
            get { return instance; }
        }
        public Thread Server
        {
            get
            {
                return _thread;
            }
        }

        public void Start()
        {
            Stop();
            _thread = new Thread(new ThreadStart(ProcessEventQueue));
            _thread.IsBackground = true;
            _thread.Start();
        }

        public void Stop()
        {
            if (this._thread != null)
            {
                this._thread.Abort();
            }
        }

        //private void Kill()//20170327 XSF
        //{
        //    Process[] process = Process.GetProcesses();
        //    foreach (Process p in process)
        //    {
        //        if (p.ProcessName.ToUpper() == "EXCEL")
        //        {
        //            p.Kill();
        //            p.WaitForExit();
        //        }
        //    }
        //}

        /// <summary>
        /// 如果文件被Excel打开，则关闭对应的Excel进程 
        /// 由于Excel打开多个文件都是在同一进程中，一旦需要被使用的文件被Excel打开，只能直接关闭所有打开的Excel文件。
        /// </summary>
        /// <param name="filepath">文件路径（绝对路径）</param>
        private void KillProcess(string filepath)
        {
            int n = filepath.LastIndexOf('/') + 1;
            string fileName = filepath.Substring(n, filepath.Length - n);
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                if (p.ProcessName.ToUpper() == "EXCEL")
                {
                    //确认文件
                    if (p.MainWindowTitle.Contains(fileName))
                    {
                        p.Kill();//结束进程
                    }
                }
            }
        }

        private void ProcessEventQueue()
        {
            while (true)
            {
                if (queue.Count > 0)
                {
                    CsvInfo2 csvInfo;
                    queue.TryDequeue(out csvInfo);
                    try
                    {
                        //if (!File.Exists(csvInfo.Path))
                        //    continue;
                        KillProcess(csvInfo.Path);//20170327 XSF
                                                  //StreamWriter sw = File.AppendText(csvInfo.Path);
                                                  //sw.WriteLine(csvInfo.Line);
                                                  //sw.Dispose();
                        StreamWriter sw = new StreamWriter(csvInfo.Path, true, System.Text.Encoding.GetEncoding("GB2312"));
                        sw.WriteLine(csvInfo.Line);
                        sw.Dispose();
                    }
                    catch
                    {

                    }
                }
                Thread.Sleep(20);
            }
        }

        public void WriteLine(string path, string line)
        {
            lock (obj)
            {
                CsvInfo2 csvInfo = new CsvInfo2();
                csvInfo.Path = path;
                csvInfo.Line = line;
                queue.Enqueue(csvInfo);
            }
        }
    }

    class CsvInfo2
    {
        public string Path { get; set; }
        public string Line { get; set; }
    }
}
