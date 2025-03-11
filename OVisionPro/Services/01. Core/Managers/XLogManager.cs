using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Shapes;
using static System.Windows.Forms.LinkLabel;


namespace OVisionPro
{
    public class logW
    {
        private string LogPath;
        private string LogConfName;
        private string LogVisionName;
        private static string formattedDateTime;
        public Action<string> onShowLog;

        public logW()
        {
            LogConfName = "VisionApp.txt";
            LogPath = visionGlob.ccdLogPath + LogConfName;
            Directory.CreateDirectory(LogPath);
            // Khởi tạo Serilog Logger với 2 file sink và sử dụng Async sink
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Async(a => a.File(LogPath, rollingInterval: RollingInterval.Day))
                .CreateLogger();
        }
        public void updateCreate()
        {
            Log.CloseAndFlush();
            LogConfName = "VisionApp.txt";
            LogPath = visionGlob.ccdLogPath + LogConfName;
            Directory.CreateDirectory(LogPath);
            // Khởi tạo Serilog Logger với 2 file sink và sử dụng Async sink
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Async(a => a.File(LogPath, rollingInterval: RollingInterval.Day))
                .CreateLogger();
        }
        public void info(string content)
        {
            Log.Information(content);
            onShowLog?.Invoke($"[INF] {content}");
        }

        public void error(string content)
        {
            Log.Error(content);
            onShowLog?.Invoke($"[ERR] {content}");

        }
        public void Except(string content)
        {
            Log.Error(content);
            onShowLog?.Invoke($"[EXC] {content}");
            using (CustomMessageBox cus = new CustomMessageBox(content))
            {
                cus.ShowDialog();
                cus.Dispose();
            }
        }
        public void warning(string content)
        {
            Log.Warning(content);
            onShowLog?.Invoke($"[WAR] {content}");
            using (CustomMessageBox cus = new CustomMessageBox(content))
            {
                cus.ShowDialog();
                cus.Dispose();
            }
        }
        public void debug(string content)
        {
            Log.Debug(content);
            onShowLog?.Invoke($"[DEB] {content}");
        }

        private readonly static logW instance = new logW();
        public static logW Ins { get { return instance; } }
    }
}

