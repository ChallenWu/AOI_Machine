using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using Serilog;

namespace OVisionPro
{
    public class XImgPatternManager
    {
        private Dictionary<string, Mat> _ImgMatPatternData = new Dictionary<string, Mat>();
        private Dictionary<string, byte[]> _ImgBytePatternData = new Dictionary<string, byte[]>();

        public Dictionary<string, Mat> ImgMatPatternData
        {
            get { return _ImgMatPatternData; }
            set { _ImgMatPatternData = value; }
        }
        public Dictionary<string, byte[]> ImgBytePatternData
        {
            get { return _ImgBytePatternData; }
            set { _ImgBytePatternData = value; }
        }

        public void CheckImgPatternModel(string modelName)
        {
            if (XParameterManager.Instance.modelNameExec != "")
            {
                if (XVisionManager.Instance.VisionRunMode == visionGlob.RunMode.production)
                {
                    string filePath = visionGlob.ccdTemplatePatternsPath + XParameterManager.Instance.modelNameExec + FileTagPattern;
                    if (!File.Exists(filePath))
                    {
                        using (var cmsg = new CustomMessageBox())
                        {
                            cmsg.Settext($"KHONG TON TAI FILE TRAIN...\n>>> MODEL: \"{XParameterManager.Instance.modelNameExec}\" CHUA TRAIN/ MAT FILE.");
                            cmsg.ShowDialog();
                        }
                    }
                    else 
                    {
                        ImgPatternDerialization();
                    }
                }
            }
        }


        private readonly static XImgPatternManager instance = new XImgPatternManager();
        public static XImgPatternManager Instance { get { return instance; } }

        public XImgPatternManager() 
        {
            ImgPatternDerialization();
        }
        public string FileTagPattern = "IMGPattern.bin";        // modelName + FileTagPattern

        // Chuyển đối tượng Mat thành mảng byte[]
        public byte[] ConvertMatToBytes(Mat mat)
        {
            // Sử dụng OpenCvSharp để mã hóa ảnh Mat thành byte[] (ở đây sử dụng .jpg, bạn có thể thay đổi định dạng)
            Cv2.ImEncode(".jpg", mat, out var bytes);
            return bytes;
        }

        // Chuyển đối tượng  mảng byte[] thành Mat
        public Mat ConvertBytesToMat(Byte[] bMat)
        {
            return Cv2.ImDecode(bMat, ImreadModes.Color);
        }

        public byte[] MatToByteArray(Mat mat)
        {// Convert Mat to byte array
            if (mat == null || mat.Empty())
                throw new ArgumentException("Invalid Mat object");

            int size = mat.Rows * mat.Cols * mat.ElemSize(); // Total number of bytes
            byte[] byteArray = new byte[size];

            // Copy data from Mat to byte array
            System.Runtime.InteropServices.Marshal.Copy(mat.Data, byteArray, 0, size);
            return byteArray;
        }
        public Mat ByteArrayToMat(byte[] byteArray, int rows, int cols, MatType matType)
        { // Convert byte array back to Mat
            if (byteArray == null || byteArray.Length == 0)
                throw new ArgumentException("Invalid byte array");
            // Create a new Mat from the byte array
            Mat mat = new Mat(rows, cols, matType);
            // Copy data from byte array to Mat
            System.Runtime.InteropServices.Marshal.Copy(byteArray, 0, mat.Data, byteArray.Length);
            return mat;
        }
        public string ImgPatternSerialization()
        { // save
            string filePath = visionGlob.ccdTemplatePatternsPath + XParameterManager.Instance.modelNameExec + FileTagPattern;
            logW.Ins.info($"PatternSerialization  >> SAVE IMG PATTERN. - {filePath}");
            BinaryFormatter formatter = new BinaryFormatter();
            ImgBytePatternData.Clear();
            string errorSer = "";
            foreach (string keyPattern in ImgMatPatternData.Keys)
            {
                if (ImgMatPatternData[keyPattern] != null)
                {
                    Byte[] mobj = ConvertMatToBytes(ImgMatPatternData[keyPattern]);
                    ImgBytePatternData[keyPattern] = mobj;
                }
                else
                {
                    errorSer += $" __ {keyPattern}: {ImgMatPatternData[keyPattern].ToString()}";
                }
            }
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, ImgBytePatternData);
            }
            return errorSer;
        }
        public string ImgPatternDerialization()
        { // load
            string filePath = visionGlob.ccdTemplatePatternsPath + XParameterManager.Instance.modelNameExec + FileTagPattern;
            if (XParameterManager.Instance.modelNameExec == "") return "CHUA LOAD MODEL.";
            if (!File.Exists(filePath))
            {
                using (var cmsg = new CustomMessageBox())
                {
                    cmsg.Settext($"KHONG TON TAI FILE TRAIN...\n>>> MODEL: \"{XParameterManager.Instance.modelNameExec}\" CHUA TRAIN/ MAT FILE.");
                    cmsg.ShowDialog();

                }
                return $"ImgPatternDerialization  >>  {filePath} KHONG TON TAI.";
            }
            logW.Ins.info($"PatternDerialization  >> LOAD IMG PATTERN. - {filePath}");
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                ImgBytePatternData = (Dictionary<string, byte[]>)formatter.Deserialize(stream);
            }
            string errorDes = "";
            foreach (string keyPattern in ImgBytePatternData.Keys)
            {
                if (ImgBytePatternData[keyPattern] != null)
                {
                    Mat mobj = ConvertBytesToMat(ImgBytePatternData[keyPattern]);
                    if (ImgMatPatternData.ContainsKey(keyPattern)) ImgMatPatternData[keyPattern]?.Dispose();
                    ImgMatPatternData[keyPattern] = mobj;
                }
                else
                {
                    errorDes += $" __ {keyPattern}: {ImgBytePatternData[keyPattern].ToString()}";
                }
            }
            return errorDes;
        }
        public void updateImgMatPattern(string keyPattern, Mat mat)
        {// clear
            logW.Ins.info($"updateImgMatPattern  >> CAP NHAT IMG PATTERN. - ALL");
            //if (ImgMatPatternData.ContainsKey(keyPattern))
            //{
            //    ImgMatPatternData[keyPattern].imgMat?.Dispose();
            //}
            //else
            //{
            //    ImgMatPatternData[keyPattern] = new MatPattern();
            //}

            //ImgMatPatternData[keyPattern].cols = mat.Cols;
            //ImgMatPatternData[keyPattern].rows = mat.Rows;
            //ImgMatPatternData[keyPattern].imgMat = mat;
        }
        public void CleanMatPatternData()
        { // clear
            logW.Ins.info($"CleanMatPatternData  >> DON DEP IMG PATTERN. - ALL");
            //foreach (string keyPattern in ImgMatPatternData.Keys)
            //{
            //    ImgMatPatternData[keyPattern].imgMat?.Dispose();
            //}
            //ImgMatPatternData.Clear();
        }
        public void RemoveMatPatternData(string keyPattern)
        { // clear
            logW.Ins.info($"PatternRemove  >> DON DEP IMG PATTERN. - {keyPattern}");
        }
        
    }
}
