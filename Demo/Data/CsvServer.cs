using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;
using System.Diagnostics;
using NPOI.HSSF.Record.Aggregates;

namespace HB_IWatch
{
    class CsvServer
    {
        private Thread _thread;
        private ConcurrentQueue<CsvInfo> queue = new ConcurrentQueue<CsvInfo>();
        private readonly static CsvServer instance = new CsvServer();
        private object obj = new object();
        CsvServer() { }
        public static CsvServer Instance
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
        /// If the file is opened by Excel, close the corresponding Excel process
        /// Since Excel opens multiple files in the same process, once the file to be used is opened by Excel, all open Excel files can only be closed directly.
        /// </summary>
        /// <param name="filepath">File path (absolute path)</param>
        private void KillProcess(string filepath)
        {
            int n = filepath.LastIndexOf('/') + 1;
            string fileName = filepath.Substring(n, filepath.Length - n);
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                if (p.ProcessName.ToUpper() == "EXCEL")
                {
                    //Confirmation File
                    if (p.MainWindowTitle.Contains(fileName))
                    {
                        p.Kill();//Ending a process
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
                    CsvInfo csvInfo;
                    queue.TryDequeue(out csvInfo);
                    try
                    {
                        //if (!File.Exists(csvInfo.Path))
                        //    continue;
                        KillProcess(csvInfo.Path);//20170327 XSF
                                                  //StreamWriter sw = File.AppendText(csvInfo.Path);
                                                  //sw.WriteLine(csvInfo.Line);
                                                  //sw.Dispose();
                        FileStream fs = new FileStream(csvInfo.Path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                        //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));
                        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("UTF-8"));


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
                CsvInfo csvInfo = new CsvInfo();
                csvInfo.Path = path;
                csvInfo.Line = line;                
                queue.Enqueue(csvInfo);
            }
        }
    }

    class CsvInfo
    {
        public string Path { get; set; }
        public string Line { get; set; }
    }
}
