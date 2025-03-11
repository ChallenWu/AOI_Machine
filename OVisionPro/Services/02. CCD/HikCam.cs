using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using MvCameraControl;
using System.Globalization;
using Serilog;

namespace OVisionPro
{
    public class HikCam
    {
        public enum AquisMode
        {
            AcquisitionMode,
            TriggerMode
        }
        private MyCamera m_pCSI;
        public MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList;
        public bool isConnected { get; set; }
        bool m_bGrabbing;
        UInt32 m_nBufSizeForDriver = 3072 * 2048 * 3;
        byte[] m_pBufForDriver = new byte[3072 * 2048 * 3];            // Buffer for getting image from driver
        UInt32 m_nBufSizeForSaveImage = 3072 * 2048 * 3 * 3 + 2048;
        byte[] m_pBufForSaveImage = new byte[3072 * 2048 * 3 * 3 + 2048];         // Buffer for saving image
        public List<string> devicesList = new List<string>();
        public string CameraName { get; set; }
        public HikCam(string CamName)
        {
            InitializeCamera();
            this.CameraName = CamName;
        }

        // Hàm chuyển đổi byte[] thành Mat
        public static OpenCvSharp.Mat ByteArrayToMat(byte[] byteArray)
        {
            // Giải mã byte[] thành Mat (ảnh)
            return OpenCvSharp.Cv2.ImDecode(byteArray, OpenCvSharp.ImreadModes.Color);
        }

        bool InitializeCamera()
        {
            m_pCSI = new MyCamera();
            m_bGrabbing = false;
            DeviceListAcq();
            return true;
        }
        public void DeviceListAcq()
        {
            int nRet;
            System.GC.Collect();
            nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
            if (0 != nRet)
            {
                return;
            }
            for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    if (gigeInfo.chUserDefinedName != "")
                    {
                        devicesList.Add(gigeInfo.chUserDefinedName);
                    }
                    else
                    {
                        devicesList.Add("GigE: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    if (usbInfo.chUserDefinedName != "")
                    {
                        devicesList.Add(usbInfo.chUserDefinedName);
                    }
                    else
                    {
                        devicesList.Add("USB: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                    }
                }
            }

        }
        public int Open()
        {
            int ret = 0;
            try
            {
                ret = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
                int HIK_Index = devicesList.IndexOf(CameraName);
                if (HIK_Index == -1)
                {
                    return -1;
                }
                MyCamera.MV_CC_DEVICE_INFO device =
                    (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[HIK_Index],
                                                                    typeof(MyCamera.MV_CC_DEVICE_INFO));
                ret = m_pCSI.MV_CC_CreateDevice_NET(ref device);
                if (MyCamera.MV_OK != ret)
                {
                    return -1;
                }
                ret = m_pCSI.MV_CC_OpenDevice_NET();
                if (MyCamera.MV_OK != ret)
                {
                    return -1;
                }
                ret = m_pCSI.MV_CC_SetEnumValue_NET("AcquisitionMode", 2);
                ret = m_pCSI.MV_CC_SetEnumValue_NET("TriggerMode", 0);
                ret = StartGrab(device, AquisMode.AcquisitionMode);
                isConnected = true;
                return ret;
            }
            catch (Exception ex)
            {
                Log.Information($"[onmouseDown] ex: {ex}");
            }
            return -1;

        }
        public int GetEnumValue(string strKey, ref UInt32 pnValue)
        {
            MyCamera.MVCC_ENUMVALUE stParam = new MyCamera.MVCC_ENUMVALUE();
            int nRet = m_pCSI.MV_CC_GetEnumValue_NET(strKey, ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }
            pnValue = stParam.nCurValue;
            return -1;
        }
        public int Close()
        {

            int ret = m_pCSI.MV_CC_CloseDevice_NET();
            ret = m_pCSI.MV_CC_DestroyDevice_NET();
            isConnected = false;
            return ret;
        }

        public int DisPose()
        {
            isConnected = false;
            return m_pCSI.MV_CC_DestroyDevice_NET();
        }

        public int StartGrab(MyCamera.MV_CC_DEVICE_INFO device, AquisMode mode)
        {
            return m_pCSI.MV_CC_StartGrabbing_NET();
        }
        public int GetIntValue(string strKey, ref UInt32 pnValue)
        {

            MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
            int nRet = m_pCSI.MV_CC_GetIntValue_NET(strKey, ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            pnValue = stParam.nCurValue;

            return 0;
        }

        public OpenCvSharp.Mat CaptureImageMat()
        {
            int nRet;
            UInt32 nPayloadSize = 0;
            nRet = GetIntValue("PayloadSize", ref nPayloadSize);
            if (MyCamera.MV_OK != nRet)
            {
                //MessageBox.Show("Get PayloadSize failed");
                return null;
            }
            if (nPayloadSize + 2048 > m_nBufSizeForDriver)
            {
                m_nBufSizeForDriver = nPayloadSize + 2048;
                m_pBufForDriver = new byte[m_nBufSizeForDriver];

                // Determine the buffer size to save image
                // BMP image size: width * height * 3 + 2048 (Reserved for BMP header)
                m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 2048;
                m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
            }

            IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0);
            UInt32 nDataLen = 0;
            MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

            //Get one frame timeout, timeout is 1 sec
            nRet = GetOneFrameTimeout(pData, ref nDataLen, m_nBufSizeForDriver, ref stFrameInfo, 1000);
            if (MyCamera.MV_OK != nRet)
            {
                //MessageBox.Show("No Data!");
                return null;
            }

            IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
            MyCamera.MV_SAVE_IMAGE_PARAM_EX2 stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX2();
            stSaveParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Bmp;
            stSaveParam.enPixelType = stFrameInfo.enPixelType;
            stSaveParam.pData = pData;
            stSaveParam.nDataLen = stFrameInfo.nFrameLen;
            stSaveParam.nHeight = stFrameInfo.nHeight;
            stSaveParam.nWidth = stFrameInfo.nWidth;
            stSaveParam.pImageBuffer = pImage;
            stSaveParam.nBufferSize = m_nBufSizeForSaveImage;
            stSaveParam.nJpgQuality = 80;
            nRet = SaveImage(ref stSaveParam);
            if (MyCamera.MV_OK != nRet)
            {
                //MessageBox.Show("Save Fail!");
                return null;
            }
            return ByteArrayToMat(m_pBufForSaveImage);
        }

        public Bitmap CaptureImage()
        {
            int nRet;
            UInt32 nPayloadSize = 0;
            nRet = GetIntValue("PayloadSize", ref nPayloadSize);
            if (MyCamera.MV_OK != nRet)
            {
                //MessageBox.Show("Get PayloadSize failed");
                return null;
            }
            if (nPayloadSize + 2048 > m_nBufSizeForDriver)
            {
                m_nBufSizeForDriver = nPayloadSize + 2048;
                m_pBufForDriver = new byte[m_nBufSizeForDriver];

                // Determine the buffer size to save image
                // BMP image size: width * height * 3 + 2048 (Reserved for BMP header)
                m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 2048;
                m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
            }

            IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0);
            UInt32 nDataLen = 0;
            MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

            //Get one frame timeout, timeout is 1 sec
            nRet = GetOneFrameTimeout(pData, ref nDataLen, m_nBufSizeForDriver, ref stFrameInfo, 1000);
            if (MyCamera.MV_OK != nRet)
            {
                //MessageBox.Show("No Data!");
                return null;
            }

            IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
            MyCamera.MV_SAVE_IMAGE_PARAM_EX2 stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX2();
            stSaveParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Bmp;
            stSaveParam.enPixelType = stFrameInfo.enPixelType;
            stSaveParam.pData = pData;
            stSaveParam.nDataLen = stFrameInfo.nFrameLen;
            stSaveParam.nHeight = stFrameInfo.nHeight;
            stSaveParam.nWidth = stFrameInfo.nWidth;
            stSaveParam.pImageBuffer = pImage;
            stSaveParam.nBufferSize = m_nBufSizeForSaveImage;
            stSaveParam.nJpgQuality = 80;
            nRet = SaveImage(ref stSaveParam);
            if (MyCamera.MV_OK != nRet)
            {
                //MessageBox.Show("Save Fail!");
                return null;
            }
            FileStream file = new FileStream("image.bmp", FileMode.Create, FileAccess.Write);
            file.Write(m_pBufForSaveImage, 0, (int)stSaveParam.nImageLen);
            file.Close();
            byte[] imageData = m_pBufForSaveImage;
            Bitmap bmp;
            using (var ms = new MemoryStream(imageData))
            {
                bmp = new Bitmap(ms);
            }
            return bmp;
        }
        public int SaveImage(ref MyCamera.MV_SAVE_IMAGE_PARAM_EX2 pSaveParam)
        {
            int nRet;
            nRet = m_pCSI.MV_CC_SaveImageEx2_NET(ref pSaveParam);
            return nRet;
        }
        public int GetOneFrameTimeout(IntPtr pData, ref UInt32 pnDataLen, UInt32 nDataSize, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, Int32 nMsec)
        {
            pnDataLen = 0;
            int nRet = m_pCSI.MV_CC_GetOneFrameTimeout_NET(pData, nDataSize, ref pFrameInfo, nMsec);
            pnDataLen = pFrameInfo.nFrameLen;
            if (MyCamera.MV_OK != nRet)
            {
                return nRet;
            }

            return nRet;
        }

        public bool SetExposeTime(int Exp)
        {
            int nRet = SetFloatValue("ExposureTime", (float)Exp);
            if (nRet != 0)
            {
                return false;
            }
            return true;

        }
        public bool GetExposeTime(ref float Exp)
        {
            int nRet = GetFloatValue("ExposureTime", ref Exp);
            if (nRet != 0)
            {
                return false;
            }
            return true;

        }
        public int GetFloatValue(string strKey, ref float pfValue)
        {
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = m_pCSI.MV_CC_GetFloatValue_NET(strKey, ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            pfValue = stParam.fCurValue;

            return 0;
        }
        public int SetFloatValue(string strKey, float fValue)
        {
            m_pCSI.MV_CC_SetEnumValue_NET(strKey, 0);
            int nRet = m_pCSI.MV_CC_SetFloatValue_NET(strKey, fValue);
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }
            return 0;
        }
    }

}