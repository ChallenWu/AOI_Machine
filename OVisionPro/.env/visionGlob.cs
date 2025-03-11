using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVisionPro
{
    public class visionGlob
    {
        //private readonly static Globals _instance = new Globals();
        //public static Globals Instance
        //{
        //    get { return _instance; }
        //}
        public visionGlob() { }
        public enum Permission
        {
            user,
            admin
        }

        public enum RunState
        {
            stopping,
            running,
            idling,
            pausing,
        }

        public enum ToolTypleData
        {
            Img,
            Pos,
            Doublue,
            Str,
            Float,
            Line,
        }

        public Dictionary<ToolTypleData, Type> ToolTypleDataVals = new Dictionary<ToolTypleData, Type>()
        {
            { ToolTypleData.Img, typeof(Mat) },
            { ToolTypleData.Pos, typeof(Tuple<int, int>) },
            { ToolTypleData.Str, typeof(string) },
            { ToolTypleData.Doublue, typeof(double) },
            { ToolTypleData.Float, typeof(float) },
            { ToolTypleData.Line, typeof(Tuple<int, int, int, int>) },
        };

        public enum RunMode
        {
            production,
            debug,
        }

        public enum TaskID
        {
            BlobTool,
            FilterTool,
            EdgeTool,
            ThresholdTool
        }

        public static string ccdConfigFolderName = "CCDConf";    // cungf duong dan file folder bin cua ctrinh
        public static string ccdResoucesPath = "D:\\DataCCDs\\Resource\\";
        public static string ccdLogPath = "D:\\DataCCDs\\Logs\\";
        public static string ccdTemplatePatternsPath = "";
        public static string ccdTemplatePatternsBakPath = "";

        public void CreateModelTemplateFolder(string modelName)
        {
            Directory.CreateDirectory($"{ccdResoucesPath}\\{modelName}");
        }

        private void CreateCCDResourcesFolder()
        {
            DateTime date = DateTime.Now; // Hoặc một giá trị DateTime cụ thể
            string formattedDateTime = date.ToString("yyyy-MM-dd");
            // Kiểm tra nếu thư mục chưa tồn tại
            Directory.CreateDirectory(ccdResoucesPath);
            //Directory.CreateDirectory(ccdTemplateImgs); không cần vì bak sẽ tự tạo ở code dưới
            Directory.CreateDirectory(ccdTemplatePatternsBakPath);
        }

        public void InitializeCCDRootSourceFolder(string ccdRootR, string ccdResoucesR, string ccdTemplateImgsR)
        {
            ccdResoucesPath = ccdResoucesR;
            ccdTemplatePatternsPath = ccdTemplateImgsR;
            CreateCCDResourcesFolder();
        }

        public void InitializeCCDRootSource()
        {
            CreateCCDResourcesFolder();
        }

        public static Action<string> PostError;

        public struct AOIErrorCodes
        {
            public const string CCD_NG = "CCD KET NOI THAT BAI";
            public const string CCD_OK = "CCD KET NOI THANH CONG";
        }
    }
}
