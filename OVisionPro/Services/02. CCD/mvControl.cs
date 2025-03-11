
//using System;
//using System.Runtime.InteropServices;

//namespace OVisionPro.Services._02._CCD
//{
//    public class MVCamera
//    {
//        public delegate void cbOutputdelegate(IntPtr pData, ref MV_FRAME_OUT_INFO pFrameInfo, IntPtr pUser);

//        public delegate void cbOutputExdelegate(IntPtr pData, ref MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser);

//        public delegate void cbXmlUpdatedelegate(MV_XML_InterfaceType enType, IntPtr pstFeature, ref MV_XML_NODES_LIST pstNodesList, IntPtr pUser);

//        public delegate void cbExceptiondelegate(uint nMsgType, IntPtr pUser);

//        public delegate void cbEventdelegate(uint nUserDefinedId, IntPtr pUser);

//        public delegate void cbEventdelegateEx(ref MV_EVENT_OUT_INFO pEventInfo, IntPtr pUser);

//        public delegate void cbStreamException(MV_CC_STREAM_EXCEPTION_TYPE enExceptionType, IntPtr pUser);

//        public struct MV_INTERFACE_INFO_LIST
//        {
//            public uint nInterfaceNum;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
//            public IntPtr[] pInterfaceInfo;
//        }

//        public struct MV_INTERFACE_INFO
//        {
//            public uint nTLayerType;

//            public uint nPCIEInfo;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chInterfaceID;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chSerialNumber;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chModelName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chManufacturer;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceVersion;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
//            public byte[] chUserDefinedName;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
//            public uint[] nReserved;
//        }

//        public enum MV_SORT_METHOD
//        {
//            SORTMETHOD_SERIALNUMBER,
//            SORTMETHOD_USERID,
//            SORTMETHOD_CURRENTIP_ASC,
//            SORTMETHOD_CURRENTIP_DESC
//        }

//        public struct MV_GIGE_DEVICE_INFO
//        {
//            public uint nIpCfgOption;

//            public uint nIpCfgCurrent;

//            public uint nCurrentIp;

//            public uint nCurrentSubNetMask;

//            public uint nDefultGateWay;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
//            public string chManufacturerName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
//            public string chModelName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
//            public string chDeviceVersion;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
//            public string chManufacturerSpecificInfo;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
//            public string chSerialNumber;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
//            public string chUserDefinedName;

//            public uint nNetExport;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_GIGE_DEVICE_INFO_EX
//        {
//            public uint nIpCfgOption;

//            public uint nIpCfgCurrent;

//            public uint nCurrentIp;

//            public uint nCurrentSubNetMask;

//            public uint nDefultGateWay;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
//            public string chManufacturerName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
//            public string chModelName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
//            public string chDeviceVersion;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
//            public string chManufacturerSpecificInfo;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
//            public string chSerialNumber;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
//            public byte[] chUserDefinedName;

//            public uint nNetExport;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_USB3_DEVICE_INFO
//        {
//            public byte CrtlInEndPoint;

//            public byte CrtlOutEndPoint;

//            public byte StreamEndPoint;

//            public byte EventEndPoint;

//            public ushort idVendor;

//            public ushort idProduct;

//            public uint nDeviceNumber;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceGUID;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chVendorName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chModelName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chFamilyName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceVersion;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chManufacturerName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chSerialNumber;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chUserDefinedName;

//            public uint nbcdUSB;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
//            public uint[] nReserved;
//        }

//        public struct MV_USB3_DEVICE_INFO_EX
//        {
//            public byte CrtlInEndPoint;

//            public byte CrtlOutEndPoint;

//            public byte StreamEndPoint;

//            public byte EventEndPoint;

//            public ushort idVendor;

//            public ushort idProduct;

//            public uint nDeviceNumber;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceGUID;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chVendorName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chModelName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chFamilyName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceVersion;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chManufacturerName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chSerialNumber;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
//            public byte[] chUserDefinedName;

//            public uint nbcdUSB;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
//            public uint[] nReserved;
//        }

//        public struct MV_CamL_DEV_INFO
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chPortID;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chModelName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chFamilyName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceVersion;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chManufacturerName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chSerialNumber;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 38)]
//            public uint[] nReserved;
//        }

//        public struct MV_CML_DEVICE_INFO
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chInterfaceID;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chVendorName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chModelName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chManufacturerInfo;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceVersion;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chSerialNumber;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
//            public byte[] chUserDefinedName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceID;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
//            public uint[] nReserved;
//        }

//        public struct MV_CXP_DEVICE_INFO
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chInterfaceID;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chVendorName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chModelName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chManufacturerInfo;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceVersion;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chSerialNumber;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
//            public byte[] chUserDefinedName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceID;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
//            public uint[] nReserved;
//        }

//        public struct MV_XOF_DEVICE_INFO
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chInterfaceID;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chVendorName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chModelName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chManufacturerInfo;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceVersion;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chSerialNumber;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
//            public byte[] chUserDefinedName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceID;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
//            public uint[] nReserved;
//        }

//        public struct MV_CC_DEVICE_INFO
//        {
//            [StructLayout(LayoutKind.Explicit, Size = 540)]
//            public struct SPECIAL_INFO
//            {
//                [FieldOffset(0)]
//                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 216)]
//                public byte[] stGigEInfo;

//                [FieldOffset(0)]
//                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 536)]
//                public byte[] stCamLInfo;

//                [FieldOffset(0)]
//                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 540)]
//                public byte[] stUsb3VInfo;

//                [FieldOffset(0)]
//                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 540)]
//                public byte[] stCMLInfo;

//                [FieldOffset(0)]
//                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 540)]
//                public byte[] stCXPInfo;

//                [FieldOffset(0)]
//                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 540)]
//                public byte[] stXoFInfo;
//            }

//            public ushort nMajorVer;

//            public ushort nMinorVer;

//            public uint nMacAddrHigh;

//            public uint nMacAddrLow;

//            public uint nTLayerType;

//            public uint nDevTypeInfo;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
//            public uint[] nReserved;

//            public SPECIAL_INFO SpecialInfo;

//            public MV_CC_DEVICE_INFO(uint nAnyNum)
//            {
//                nMajorVer = 0;
//                nMinorVer = 0;
//                nMacAddrHigh = 0u;
//                nMacAddrLow = 0u;
//                nTLayerType = 0u;
//                nDevTypeInfo = 0u;
//                nReserved = new uint[3];
//                SpecialInfo.stGigEInfo = new byte[216];
//                SpecialInfo.stCamLInfo = new byte[536];
//                SpecialInfo.stUsb3VInfo = new byte[540];
//                SpecialInfo.stCMLInfo = new byte[540];
//                SpecialInfo.stCXPInfo = new byte[540];
//                SpecialInfo.stXoFInfo = new byte[540];
//            }
//        }

//        public struct MV_CC_DEVICE_INFO_LIST
//        {
//            public uint nDeviceNum;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
//            public IntPtr[] pDeviceInfo;
//        }

//        public struct MV_GENTL_IF_INFO
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chInterfaceID;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chTLType;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDisplayName;

//            public uint nCtiIndex;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nReserved;
//        }

//        public struct MV_GENTL_IF_INFO_LIST
//        {
//            public uint nInterfaceNum;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
//            public IntPtr[] pIFInfo;
//        }

//        public struct MV_GENTL_DEV_INFO
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chInterfaceID;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceID;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chVendorName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chModelName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chTLType;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chUserDefinedName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chSerialNumber;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceVersion;

//            public uint nCtiIndex;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nReserved;
//        }

//        public struct MV_GENTL_DEV_INFO_EX
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chInterfaceID;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceID;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chVendorName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chModelName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chTLType;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDisplayName;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
//            public byte[] chUserDefinedName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chSerialNumber;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string chDeviceVersion;

//            public uint nCtiIndex;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nReserved;
//        }

//        public struct MV_GENTL_DEV_INFO_LIST
//        {
//            public uint nDeviceNum;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
//            public IntPtr[] pDeviceInfo;
//        }

//        public struct MV_NETTRANS_INFO
//        {
//            public long nReviceDataSize;

//            public int nThrowFrameCount;

//            public uint nNetRecvFrameCount;

//            public long nRequestResendPacketCount;

//            public long nResendPacketCount;
//        }

//        public struct MV_FRAME_OUT_INFO
//        {
//            public ushort nWidth;

//            public ushort nHeight;

//            public MvGvspPixelType enPixelType;

//            public uint nFrameNum;

//            public uint nDevTimeStampHigh;

//            public uint nDevTimeStampLow;

//            public uint nReserved0;

//            public long nHostTimeStamp;

//            public uint nFrameLen;

//            public uint nLostPacket;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
//            public uint[] nReserved;
//        }

//        public struct MV_CHUNK_DATA_CONTENT
//        {
//            public IntPtr pChunkData;

//            public uint nChunkID;

//            public uint nChunkLen;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nReserved;
//        }

//        public struct MV_FRAME_OUT_INFO_EX
//        {
//            [StructLayout(LayoutKind.Explicit)]
//            public struct UNPARSED_CHUNK_LIST
//            {
//                [FieldOffset(0)]
//                public IntPtr pUnparsedChunkContent;

//                [FieldOffset(0)]
//                public long nAligning;
//            }

//            public ushort nWidth;

//            public ushort nHeight;

//            public MvGvspPixelType enPixelType;

//            public uint nFrameNum;

//            public uint nDevTimeStampHigh;

//            public uint nDevTimeStampLow;

//            public uint nReserved0;

//            public long nHostTimeStamp;

//            public uint nFrameLen;

//            public uint nSecondCount;

//            public uint nCycleCount;

//            public uint nCycleOffset;

//            public float fGain;

//            public float fExposureTime;

//            public uint nAverageBrightness;

//            public uint nRed;

//            public uint nGreen;

//            public uint nBlue;

//            public uint nFrameCounter;

//            public uint nTriggerIndex;

//            public uint nInput;

//            public uint nOutput;

//            public ushort nOffsetX;

//            public ushort nOffsetY;

//            public ushort nChunkWidth;

//            public ushort nChunkHeight;

//            public uint nLostPacket;

//            public uint nUnparsedChunkNum;

//            public UNPARSED_CHUNK_LIST UnparsedChunkList;

//            public uint nExtendWidth;

//            public uint nExtendHeight;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 34)]
//            public uint[] nReserved;
//        }

//        public struct MV_FRAME_OUT
//        {
//            public IntPtr pBufAddr;

//            public MV_FRAME_OUT_INFO_EX stFrameInfo;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
//            public uint[] nReserved;
//        }

//        public enum MV_GRAB_STRATEGY
//        {
//            MV_GrabStrategy_OneByOne,
//            MV_GrabStrategy_LatestImagesOnly,
//            MV_GrabStrategy_LatestImages,
//            MV_GrabStrategy_UpcomingImage
//        }

//        public struct MV_DISPLAY_FRAME_INFO
//        {
//            public IntPtr hWnd;

//            public IntPtr pData;

//            public uint nDataLen;

//            public ushort nWidth;

//            public ushort nHeight;

//            public MvGvspPixelType enPixelType;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_DISPLAY_FRAME_INFO_EX
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pData;

//            public uint nDataLen;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public enum MV_SAVE_POINT_CLOUD_FILE_TYPE
//        {
//            MV_PointCloudFile_Undefined,
//            MV_PointCloudFile_PLY,
//            MV_PointCloudFile_CSV,
//            MV_PointCloudFile_OBJ
//        }

//        public struct MV_SAVE_POINT_CLOUD_PARAM
//        {
//            public uint nLinePntNum;

//            public uint nLineNum;

//            public MvGvspPixelType enSrcPixelType;

//            public IntPtr pSrcData;

//            public uint nSrcDataLen;

//            public IntPtr pDstBuf;

//            public uint nDstBufSize;

//            public uint nDstBufLen;

//            public MV_SAVE_POINT_CLOUD_FILE_TYPE enPointCloudFileType;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public enum MV_SAVE_IAMGE_TYPE
//        {
//            MV_Image_Undefined,
//            MV_Image_Bmp,
//            MV_Image_Jpeg,
//            MV_Image_Png,
//            MV_Image_Tif
//        }

//        public struct MV_SAVE_IMAGE_PARAM
//        {
//            public IntPtr pData;

//            public uint nDataLen;

//            public MvGvspPixelType enPixelType;

//            public ushort nWidth;

//            public ushort nHeight;

//            public IntPtr pImageBuffer;

//            public uint nImageLen;

//            public uint nBufferSize;

//            public MV_SAVE_IAMGE_TYPE enImageType;
//        }

//        public struct MV_SAVE_IMAGE_PARAM_EX2
//        {
//            public IntPtr pData;

//            public uint nDataLen;

//            public MvGvspPixelType enPixelType;

//            public ushort nWidth;

//            public ushort nHeight;

//            public IntPtr pImageBuffer;

//            public uint nImageLen;

//            public uint nBufferSize;

//            public MV_SAVE_IAMGE_TYPE enImageType;

//            public uint nJpgQuality;

//            public uint iMethodValue;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
//            public uint[] nReserved;
//        }

//        public struct MV_SAVE_IMAGE_PARAM_EX3
//        {
//            public IntPtr pData;

//            public uint nDataLen;

//            public MvGvspPixelType enPixelType;

//            public uint nWidth;

//            public uint nHeight;

//            public IntPtr pImageBuffer;

//            public uint nImageLen;

//            public uint nBufferSize;

//            public MV_SAVE_IAMGE_TYPE enImageType;

//            public uint nJpgQuality;

//            public uint iMethodValue;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
//            public uint[] nReserved;
//        }

//        public struct MV_SAVE_IMG_TO_FILE_PARAM
//        {
//            public MvGvspPixelType enPixelType;

//            public IntPtr pData;

//            public uint nDataLen;

//            public ushort nWidth;

//            public ushort nHeight;

//            public MV_SAVE_IAMGE_TYPE enImageType;

//            public uint nQuality;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
//            public string pImagePath;

//            public uint iMethodValue;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_SAVE_IMG_TO_FILE_PARAM_EX
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pData;

//            public uint nDataLen;

//            public MV_SAVE_IAMGE_TYPE enImageType;

//            public string pImagePath;

//            public uint nQuality;

//            public uint iMethodValue;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public enum MV_IMG_ROTATION_ANGLE
//        {
//            MV_IMAGE_ROTATE_90 = 1,
//            MV_IMAGE_ROTATE_180,
//            MV_IMAGE_ROTATE_270
//        }

//        public struct MV_CC_ROTATE_IMAGE_PARAM
//        {
//            public MvGvspPixelType enPixelType;

//            public uint nWidth;

//            public uint nHeight;

//            public IntPtr pSrcData;

//            public uint nSrcDataLen;

//            public IntPtr pDstBuf;

//            public uint nDstBufLen;

//            public uint nDstBufSize;

//            public MV_IMG_ROTATION_ANGLE enRotationAngle;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public enum MV_IMG_FLIP_TYPE
//        {
//            MV_FLIP_VERTICAL = 1,
//            MV_FLIP_HORIZONTAL
//        }

//        public struct MV_CC_FLIP_IMAGE_PARAM
//        {
//            public MvGvspPixelType enPixelType;

//            public uint nWidth;

//            public uint nHeight;

//            public IntPtr pSrcData;

//            public uint nSrcDataLen;

//            public IntPtr pDstBuf;

//            public uint nDstBufLen;

//            public uint nDstBufSize;

//            public MV_IMG_FLIP_TYPE enFlipType;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_PIXEL_CONVERT_PARAM
//        {
//            public ushort nWidth;

//            public ushort nHeight;

//            public MvGvspPixelType enSrcPixelType;

//            public IntPtr pSrcData;

//            public uint nSrcDataLen;

//            public MvGvspPixelType enDstPixelType;

//            public IntPtr pDstBuffer;

//            public uint nDstLen;

//            public uint nDstBufferSize;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_PIXEL_CONVERT_PARAM_EX
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public MvGvspPixelType enSrcPixelType;

//            public IntPtr pSrcData;

//            public uint nSrcDataLen;

//            public MvGvspPixelType enDstPixelType;

//            public IntPtr pDstBuffer;

//            public uint nDstLen;

//            public uint nDstBufferSize;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nRes;
//        }

//        public enum MV_CC_GAMMA_TYPE
//        {
//            MV_CC_GAMMA_TYPE_NONE,
//            MV_CC_GAMMA_TYPE_VALUE,
//            MV_CC_GAMMA_TYPE_USER_CURVE,
//            MV_CC_GAMMA_TYPE_LRGB2SRGB,
//            MV_CC_GAMMA_TYPE_SRGB2LRGB
//        }

//        public struct MV_CC_GAMMA_PARAM
//        {
//            public MV_CC_GAMMA_TYPE enGammaType;

//            public float fGammaValue;

//            public IntPtr pGammaCurveBuf;

//            public uint nGammaCurveBufLen;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_CCM_PARAM
//        {
//            public bool bCCMEnable;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
//            public int[] nCCMat;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_CCM_PARAM_EX
//        {
//            public bool bCCMEnable;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
//            public int[] nCCMat;

//            public uint nCCMScale;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_CLUT_PARAM
//        {
//            public bool bCLUTEnable;

//            public uint nCLUTScale;

//            public uint nCLUTSize;

//            public IntPtr pCLUTBuf;

//            public uint nCLUTBufLen;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_CONTRAST_PARAM
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public IntPtr pSrcBuf;

//            public uint nSrcBufLen;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pDstBuf;

//            public uint nDstBufSize;

//            public uint nDstBufLen;

//            public uint nContrastFactor;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_SHARPEN_PARAM
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public IntPtr pSrcBuf;

//            public uint nSrcBufLen;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pDstBuf;

//            public uint nDstBufSize;

//            public uint nDstBufLen;

//            public uint nSharpenAmount;

//            public uint nSharpenRadius;

//            public uint nSharpenThreshold;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_COLOR_CORRECT_PARAM
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public IntPtr pSrcBuf;

//            public uint nSrcBufLen;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pDstBuf;

//            public uint nDstBufSize;

//            public uint nDstBufLen;

//            public uint nImageBit;

//            public MV_CC_GAMMA_PARAM stGammaParam;

//            public MV_CC_CCM_PARAM_EX stCCMParam;

//            public MV_CC_CLUT_PARAM stCLUTParam;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_RECT_I
//        {
//            public uint nX;

//            public uint nY;

//            public uint nWidth;

//            public uint nHeight;
//        }

//        public struct MV_CC_NOISE_ESTIMATE_PARAM
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pSrcBuf;

//            public uint nSrcBufLen;

//            public IntPtr pstROIRect;

//            public uint nROINum;

//            public uint nNoiseThreshold;

//            public IntPtr pNoiseProfile;

//            public uint nNoiseProfileSize;

//            public uint nNoiseProfileLen;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_SPATIAL_DENOISE_PARAM
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pSrcBuf;

//            public uint nSrcBufLen;

//            public IntPtr pDstBuf;

//            public uint nDstBufSize;

//            public uint nDstBufLen;

//            public IntPtr pNoiseProfile;

//            public uint nNoiseProfileLen;

//            public uint nBayerDenoiseStrength;

//            public uint nBayerSharpenStrength;

//            public uint nBayerNoiseCorrect;

//            public uint nNoiseCorrectLum;

//            public uint nNoiseCorrectChrom;

//            public uint nStrengthLum;

//            public uint nStrengthChrom;

//            public uint nStrengthSharpen;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_LSC_CALIB_PARAM
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pSrcBuf;

//            public uint nSrcBufLen;

//            public IntPtr pCalibBuf;

//            public uint nCalibBufSize;

//            public uint nCalibBufLen;

//            public uint nSecNumW;

//            public uint nSecNumH;

//            public uint nPadCoef;

//            public uint nCalibMethod;

//            public uint nTargetGray;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_LSC_CORRECT_PARAM
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pSrcBuf;

//            public uint nSrcBufLen;

//            public IntPtr pDstBuf;

//            public uint nDstBufSize;

//            public uint nDstBufLen;

//            public IntPtr pCalibBuf;

//            public uint nCalibBufLen;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public enum MV_CC_BAYER_NOISE_FEATURE_TYPE
//        {
//            MV_CC_BAYER_NOISE_FEATURE_TYPE_INVALID = 0,
//            MV_CC_BAYER_NOISE_FEATURE_TYPE_PROFILE = 1,
//            MV_CC_BAYER_NOISE_FEATURE_TYPE_LEVEL = 2,
//            MV_CC_BAYER_NOISE_FEATURE_TYPE_DEFAULT = 2
//        }

//        public struct MV_CC_BAYER_NOISE_PROFILE_INFO
//        {
//            public uint nVersion;

//            public MV_CC_BAYER_NOISE_FEATURE_TYPE enNoiseFeatureType;

//            public MvGvspPixelType enPixelType;

//            public int nNoiseLevel;

//            public uint nCurvePointNum;

//            public IntPtr nNoiseCurve;

//            public IntPtr nLumCurve;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_BAYER_NOISE_ESTIMATE_PARAM
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pSrcData;

//            public uint nSrcDataLen;

//            public uint nNoiseThreshold;

//            public IntPtr pCurveBuf;

//            public MV_CC_BAYER_NOISE_PROFILE_INFO stNoiseProfile;

//            public uint nThreadNum;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_BAYER_SPATIAL_DENOISE_PARAM
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pSrcData;

//            public uint nSrcDataLen;

//            public IntPtr pDstBuf;

//            public uint nDstBufSize;

//            public uint nDstBufLen;

//            public MV_CC_BAYER_NOISE_PROFILE_INFO stNoiseProfile;

//            public uint nDenoiseStrength;

//            public uint nSharpenStrength;

//            public uint nNoiseCorrect;

//            public uint nThreadNum;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_FRAME_SPEC_INFO
//        {
//            public uint nSecondCount;

//            public uint nCycleCount;

//            public uint nCycleOffset;

//            public float fGain;

//            public float fExposureTime;

//            public uint nAverageBrightness;

//            public uint nRed;

//            public uint nGreen;

//            public uint nBlue;

//            public uint nFrameCounter;

//            public uint nTriggerIndex;

//            public uint nInput;

//            public uint nOutput;

//            public ushort nOffsetX;

//            public ushort nOffsetY;

//            public ushort nFrameWidth;

//            public ushort nFrameHeight;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_HB_DECODE_PARAM
//        {
//            public IntPtr pSrcBuf;

//            public uint nSrcLen;

//            public uint nWidth;

//            public uint nHeight;

//            public IntPtr pDstBuf;

//            public uint nDstBufSize;

//            public uint nDstBufLen;

//            public MvGvspPixelType enDstPixelType;

//            public MV_CC_FRAME_SPEC_INFO stFrameSpecInfo;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public enum MV_RECORD_FORMAT_TYPE
//        {
//            MV_FormatType_Undefined,
//            MV_FormatType_AVI
//        }

//        public struct MV_CC_RECORD_PARAM
//        {
//            public MvGvspPixelType enPixelType;

//            public ushort nWidth;

//            public ushort nHeight;

//            public float fFrameRate;

//            public uint nBitRate;

//            public MV_RECORD_FORMAT_TYPE enRecordFmtType;

//            public string strFilePath;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public struct MV_CC_INPUT_FRAME_INFO
//        {
//            public IntPtr pData;

//            public uint nDataLen;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nRes;
//        }

//        public enum MV_CAM_ACQUISITION_MODE
//        {
//            MV_ACQ_MODE_SINGLE,
//            MV_ACQ_MODE_MUTLI,
//            MV_ACQ_MODE_CONTINUOUS
//        }

//        public enum MV_CAM_GAIN_MODE
//        {
//            MV_GAIN_MODE_OFF,
//            MV_GAIN_MODE_ONCE,
//            MV_GAIN_MODE_CONTINUOUS
//        }

//        public enum MV_CAM_EXPOSURE_MODE
//        {
//            MV_EXPOSURE_MODE_TIMED,
//            MV_EXPOSURE_MODE_TRIGGER_WIDTH
//        }

//        public enum MV_CAM_EXPOSURE_AUTO_MODE
//        {
//            MV_EXPOSURE_AUTO_MODE_OFF,
//            MV_EXPOSURE_AUTO_MODE_ONCE,
//            MV_EXPOSURE_AUTO_MODE_CONTINUOUS
//        }

//        public enum MV_CAM_TRIGGER_MODE
//        {
//            MV_TRIGGER_MODE_OFF,
//            MV_TRIGGER_MODE_ON
//        }

//        public enum MV_CAM_GAMMA_SELECTOR
//        {
//            MV_GAMMA_SELECTOR_USER = 1,
//            MV_GAMMA_SELECTOR_SRGB
//        }

//        public enum MV_CAM_BALANCEWHITE_AUTO
//        {
//            MV_BALANCEWHITE_AUTO_OFF = 0,
//            MV_BALANCEWHITE_AUTO_ONCE = 2,
//            MV_BALANCEWHITE_AUTO_CONTINUOUS = 1
//        }

//        public enum MV_CAM_TRIGGER_SOURCE
//        {
//            MV_TRIGGER_SOURCE_LINE0 = 0,
//            MV_TRIGGER_SOURCE_LINE1 = 1,
//            MV_TRIGGER_SOURCE_LINE2 = 2,
//            MV_TRIGGER_SOURCE_LINE3 = 3,
//            MV_TRIGGER_SOURCE_COUNTER0 = 4,
//            MV_TRIGGER_SOURCE_SOFTWARE = 7,
//            MV_TRIGGER_SOURCE_FrequencyConverter = 8
//        }

//        public struct MV_ALL_MATCH_INFO
//        {
//            public uint nType;

//            public IntPtr pInfo;

//            public uint nInfoSize;
//        }

//        public struct MV_MATCH_INFO_NET_DETECT
//        {
//            public long nReviceDataSize;

//            public long nLostPacketCount;

//            public uint nLostFrameCount;

//            public uint nNetRecvFrameCount;

//            public long nRequestResendPacketCount;

//            public long nResendPacketCount;
//        }

//        public struct MV_MATCH_INFO_USB_DETECT
//        {
//            public long nReviceDataSize;

//            public uint nRevicedFrameCount;

//            public uint nErrorFrameCount;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
//            public uint[] nReserved;
//        }

//        public struct MV_IMAGE_BASIC_INFO
//        {
//            public ushort nWidthValue;

//            public ushort nWidthMin;

//            public uint nWidthMax;

//            public uint nWidthInc;

//            public uint nHeightValue;

//            public uint nHeightMin;

//            public uint nHeightMax;

//            public uint nHeightInc;

//            public float fFrameRateValue;

//            public float fFrameRateMin;

//            public float fFrameRateMax;

//            public uint enPixelType;

//            public uint nSupportedPixelFmtNum;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
//            public uint[] enPixelList;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nReserved;
//        }

//        public enum MV_XML_Visibility
//        {
//            V_Beginner = 0,
//            V_Expert = 1,
//            V_Guru = 2,
//            V_Invisible = 3,
//            V_Undefined = 99
//        }

//        public struct MV_EVENT_OUT_INFO
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
//            public string EventName;

//            public ushort nEventID;

//            public ushort nStreamChannel;

//            public uint nBlockIdHigh;

//            public uint nBlockIdLow;

//            public uint nTimestampHigh;

//            public uint nTimestampLow;

//            public IntPtr pEventData;

//            public uint nEventDataSize;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
//            public uint[] nReserved;
//        }

//        public struct MV_CC_FILE_ACCESS
//        {
//            public string pUserFileName;

//            public string pDevFileName;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
//            public uint[] nReserved;
//        }

//        public struct MV_CC_FILE_ACCESS_EX
//        {
//            public IntPtr pUserFileBuf;

//            public uint nFileBufSize;

//            public uint nFileBufLen;

//            public string pDevFileName;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
//            public uint[] nReserved;
//        }

//        public struct MV_CC_FILE_ACCESS_PROGRESS
//        {
//            public long nCompleted;

//            public long nTotal;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nReserved;
//        }

//        public enum MV_GIGE_TRANSMISSION_TYPE
//        {
//            MV_GIGE_TRANSTYPE_UNICAST = 0,
//            MV_GIGE_TRANSTYPE_MULTICAST = 1,
//            MV_GIGE_TRANSTYPE_LIMITEDBROADCAST = 2,
//            MV_GIGE_TRANSTYPE_SUBNETBROADCAST = 3,
//            MV_GIGE_TRANSTYPE_CAMERADEFINED = 4,
//            MV_GIGE_TRANSTYPE_UNICAST_DEFINED_PORT = 5,
//            MV_GIGE_TRANSTYPE_UNICAST_WITHOUT_RECV = 65536,
//            MV_GIGE_TRANSTYPE_MULTICAST_WITHOUT_RECV = 65537
//        }

//        public struct MV_CC_TRANSMISSION_TYPE
//        {
//            public MV_GIGE_TRANSMISSION_TYPE enTransmissionType;

//            public uint nDestIp;

//            public ushort nDestPort;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
//            public uint[] nReserved;
//        }

//        public struct MV_ACTION_CMD_INFO
//        {
//            public uint nDeviceKey;

//            public uint nGroupKey;

//            public uint nGroupMask;

//            public uint bActionTimeEnable;

//            public long nActionTime;

//            public string pBroadcastAddress;

//            public uint nTimeOut;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
//            public uint[] nReserved;
//        }

//        public struct MV_ACTION_CMD_RESULT
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
//            public string strDeviceAddress;

//            public int nStatus;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_ACTION_CMD_RESULT_LIST
//        {
//            public uint nNumResults;

//            public IntPtr pResults;
//        }

//        public enum MV_XML_InterfaceType
//        {
//            IFT_IValue,
//            IFT_IBase,
//            IFT_IInteger,
//            IFT_IBoolean,
//            IFT_ICommand,
//            IFT_IFloat,
//            IFT_IString,
//            IFT_IRegister,
//            IFT_ICategory,
//            IFT_IEnumeration,
//            IFT_IEnumEntry,
//            IFT_IPort
//        }

//        public struct MV_XML_NODE_FEATURE
//        {
//            public MV_XML_InterfaceType enType;

//            public MV_XML_Visibility enVisivility;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strDescription;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strToolTip;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_XML_NODES_LIST
//        {
//            public uint nNodeNum;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
//            public MV_XML_NODE_FEATURE[] stNodes;
//        }

//        public struct MVCC_INTVALUE
//        {
//            public uint nCurValue;

//            public uint nMax;

//            public uint nMin;

//            public uint nInc;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MVCC_INTVALUE_EX
//        {
//            public long nCurValue;

//            public long nMax;

//            public long nMin;

//            public long nInc;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
//            public uint[] nReserved;
//        }

//        public struct MVCC_FLOATVALUE
//        {
//            public float fCurValue;

//            public float fMax;

//            public float fMin;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MVCC_ENUMVALUE
//        {
//            public uint nCurValue;

//            public uint nSupportedNum;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
//            public uint[] nSupportValue;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MVCC_STRINGVALUE
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
//            public string chCurValue;

//            public long nMaxLength;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
//            public uint[] nReserved;
//        }

//        public enum MV_XML_AccessMode
//        {
//            AM_NI,
//            AM_NA,
//            AM_WO,
//            AM_RO,
//            AM_RW,
//            AM_Undefined,
//            AM_CycleDetect
//        }

//        public struct MV_XML_FEATURE_Integer
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strDescription;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strToolTip;

//            public MV_XML_Visibility enVisivility;

//            public MV_XML_AccessMode enAccessMode;

//            public int bIsLocked;

//            public long nValue;

//            public long nMinValue;

//            public long nMaxValue;

//            public long nIncrement;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_XML_FEATURE_Boolean
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strDescription;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strToolTip;

//            public MV_XML_Visibility enVisivility;

//            public MV_XML_AccessMode enAccessMode;

//            public int bIsLocked;

//            public bool bValue;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_XML_FEATURE_Command
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strDescription;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strToolTip;

//            public MV_XML_Visibility enVisivility;

//            public MV_XML_AccessMode enAccessMode;

//            public int bIsLocked;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_XML_FEATURE_Float
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strDescription;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strToolTip;

//            public MV_XML_Visibility enVisivility;

//            public MV_XML_AccessMode enAccessMode;

//            public int bIsLocked;

//            public double dfValue;

//            public double dfMinValue;

//            public double dfMaxValue;

//            public double dfIncrement;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_XML_FEATURE_String
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strDescription;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strToolTip;

//            public MV_XML_Visibility enVisivility;

//            public MV_XML_AccessMode enAccessMode;

//            public int bIsLocked;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strValue;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_XML_FEATURE_Register
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strDescription;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strToolTip;

//            public MV_XML_Visibility enVisivility;

//            public MV_XML_AccessMode enAccessMode;

//            public int bIsLocked;

//            public long nAddrValue;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_XML_FEATURE_Category
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strDescription;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strToolTip;

//            public MV_XML_Visibility enVisivility;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_XML_FEATURE_EnumEntry
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strDescription;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strToolTip;

//            public int bIsImplemented;

//            public int nParentsNum;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public MV_XML_NODE_FEATURE[] stParentsList;

//            public MV_XML_Visibility enVisivility;

//            public long nValue;

//            public MV_XML_AccessMode enAccessMode;

//            public int bIsLocked;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nReserved;
//        }

//        public struct StrSymbolic
//        {
//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string str;
//        }

//        public struct MV_XML_FEATURE_Enumeration
//        {
//            public MV_XML_Visibility enVisivility;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strDescription;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strToolTip;

//            public int nSymbolicNum;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strCurrentSymbolic;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
//            public StrSymbolic[] strSymbolic;

//            public MV_XML_AccessMode enAccessMode;

//            public int bIsLocked;

//            public long nValue;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MV_XML_FEATURE_Port
//        {
//            public MV_XML_Visibility enVisivility;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strDescription;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strDisplayName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
//            public string strName;

//            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
//            public string strToolTip;

//            public MV_XML_AccessMode enAccessMode;

//            public int bIsLocked;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MVCC_COLORF
//        {
//            public float fR;

//            public float fG;

//            public float fB;

//            public float fAlpha;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MVCC_POINTF
//        {
//            public float fX;

//            public float fY;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MVCC_RECT_INFO
//        {
//            public float fTop;

//            public float fBottom;

//            public float fLeft;

//            public float fRight;

//            public MVCC_COLORF stColor;

//            public uint nLineWidth;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MVCC_CIRCLE_INFO
//        {
//            public MVCC_POINTF stCenterPoint;

//            public float fR1;

//            public float fR2;

//            public MVCC_COLORF stColor;

//            public uint nLineWidth;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MVCC_LINES_INFO
//        {
//            public MVCC_POINTF stStartPoint;

//            public MVCC_POINTF stEndPoint;

//            public MVCC_COLORF stColor;

//            public uint nLineWidth;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public struct MVCC_ENUMENTRY
//        {
//            public uint nValue;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
//            public byte[] chSymbolic;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public enum MV_CC_STREAM_EXCEPTION_TYPE
//        {
//            MV_CC_STREAM_EXCEPTION_ABNORMAL_IMAGE = 16385,
//            MV_CC_STREAM_EXCEPTION_LIST_OVERFLOW,
//            MV_CC_STREAM_EXCEPTION_LIST_EMPTY,
//            MV_CC_STREAM_EXCEPTION_RECONNECTION,
//            MV_CC_STREAM_EXCEPTION_DISCONNECTED,
//            MV_CC_STREAM_EXCEPTION_DEVICE
//        }

//        public struct MV_OUTPUT_IMAGE_INFO
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pBuf;

//            public uint nBufLen;

//            public uint nBufSize;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public uint[] nReserved;
//        }

//        public enum MV_IMAGE_RECONSTRUCTION_METHOD
//        {
//            MV_SPLIT_BY_LINE = 1
//        }

//        public struct MV_RECONSTRUCT_IMAGE_PARAM
//        {
//            public uint nWidth;

//            public uint nHeight;

//            public MvGvspPixelType enPixelType;

//            public IntPtr pSrcData;

//            public uint nSrcDataLen;

//            public uint nExposureNum;

//            public MV_IMAGE_RECONSTRUCTION_METHOD enReconstructMethod;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//            public MV_OUTPUT_IMAGE_INFO[] stDstBufList;

//            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
//            public uint[] nReserved;
//        }

//        public enum MvGvspPixelType
//        {
//            PixelType_Gvsp_Undefined = -1,
//            PixelType_Gvsp_Mono1p = 16842807,
//            PixelType_Gvsp_Mono2p = 16908344,
//            PixelType_Gvsp_Mono4p = 17039417,
//            PixelType_Gvsp_Mono8 = 17301505,
//            PixelType_Gvsp_Mono8_Signed = 17301506,
//            PixelType_Gvsp_Mono10 = 17825795,
//            PixelType_Gvsp_Mono10_Packed = 17563652,
//            PixelType_Gvsp_Mono12 = 17825797,
//            PixelType_Gvsp_Mono12_Packed = 17563654,
//            PixelType_Gvsp_Mono14 = 17825829,
//            PixelType_Gvsp_Mono16 = 17825799,
//            PixelType_Gvsp_BayerGR8 = 17301512,
//            PixelType_Gvsp_BayerRG8 = 17301513,
//            PixelType_Gvsp_BayerGB8 = 17301514,
//            PixelType_Gvsp_BayerBG8 = 17301515,
//            PixelType_Gvsp_BayerRBGG8 = 17301574,
//            PixelType_Gvsp_BayerGR10 = 17825804,
//            PixelType_Gvsp_BayerRG10 = 17825805,
//            PixelType_Gvsp_BayerGB10 = 17825806,
//            PixelType_Gvsp_BayerBG10 = 17825807,
//            PixelType_Gvsp_BayerGR12 = 17825808,
//            PixelType_Gvsp_BayerRG12 = 17825809,
//            PixelType_Gvsp_BayerGB12 = 17825810,
//            PixelType_Gvsp_BayerBG12 = 17825811,
//            PixelType_Gvsp_BayerGR10_Packed = 17563686,
//            PixelType_Gvsp_BayerRG10_Packed = 17563687,
//            PixelType_Gvsp_BayerGB10_Packed = 17563688,
//            PixelType_Gvsp_BayerBG10_Packed = 17563689,
//            PixelType_Gvsp_BayerGR12_Packed = 17563690,
//            PixelType_Gvsp_BayerRG12_Packed = 17563691,
//            PixelType_Gvsp_BayerGB12_Packed = 17563692,
//            PixelType_Gvsp_BayerBG12_Packed = 17563693,
//            PixelType_Gvsp_BayerGR16 = 17825838,
//            PixelType_Gvsp_BayerRG16 = 17825839,
//            PixelType_Gvsp_BayerGB16 = 17825840,
//            PixelType_Gvsp_BayerBG16 = 17825841,
//            PixelType_Gvsp_RGB8_Packed = 35127316,
//            PixelType_Gvsp_BGR8_Packed = 35127317,
//            PixelType_Gvsp_RGBA8_Packed = 35651606,
//            PixelType_Gvsp_BGRA8_Packed = 35651607,
//            PixelType_Gvsp_RGB10_Packed = 36700184,
//            PixelType_Gvsp_BGR10_Packed = 36700185,
//            PixelType_Gvsp_RGB12_Packed = 36700186,
//            PixelType_Gvsp_BGR12_Packed = 36700187,
//            PixelType_Gvsp_RGB16_Packed = 36700211,
//            PixelType_Gvsp_BGR16_Packed = 36700235,
//            PixelType_Gvsp_RGBA16_Packed = 37748800,
//            PixelType_Gvsp_BGRA16_Packed = 37748817,
//            PixelType_Gvsp_RGB10V1_Packed = 35651612,
//            PixelType_Gvsp_RGB10V2_Packed = 35651613,
//            PixelType_Gvsp_RGB12V1_Packed = 35913780,
//            PixelType_Gvsp_RGB565_Packed = 34603061,
//            PixelType_Gvsp_BGR565_Packed = 34603062,
//            PixelType_Gvsp_YUV411_Packed = 34340894,
//            PixelType_Gvsp_YUV422_Packed = 34603039,
//            PixelType_Gvsp_YUV422_YUYV_Packed = 34603058,
//            PixelType_Gvsp_YUV444_Packed = 35127328,
//            PixelType_Gvsp_YCBCR8_CBYCR = 35127354,
//            PixelType_Gvsp_YCBCR422_8 = 34603067,
//            PixelType_Gvsp_YCBCR422_8_CBYCRY = 34603075,
//            PixelType_Gvsp_YCBCR411_8_CBYYCRYY = 34340924,
//            PixelType_Gvsp_YCBCR601_8_CBYCR = 35127357,
//            PixelType_Gvsp_YCBCR601_422_8 = 34603070,
//            PixelType_Gvsp_YCBCR601_422_8_CBYCRY = 34603076,
//            PixelType_Gvsp_YCBCR601_411_8_CBYYCRYY = 34340927,
//            PixelType_Gvsp_YCBCR709_8_CBYCR = 35127360,
//            PixelType_Gvsp_YCBCR709_422_8 = 34603073,
//            PixelType_Gvsp_YCBCR709_422_8_CBYCRY = 34603077,
//            PixelType_Gvsp_YCBCR709_411_8_CBYYCRYY = 34340930,
//            PixelType_Gvsp_YUV420SP_NV12 = 34373633,
//            PixelType_Gvsp_YUV420SP_NV21 = 34373634,
//            PixelType_Gvsp_RGB8_Planar = 35127329,
//            PixelType_Gvsp_RGB10_Planar = 36700194,
//            PixelType_Gvsp_RGB12_Planar = 36700195,
//            PixelType_Gvsp_RGB16_Planar = 36700196,
//            PixelType_Gvsp_Jpeg = -2145910783,
//            PixelType_Gvsp_Coord3D_ABC32f = 39846080,
//            PixelType_Gvsp_Coord3D_ABC32f_Planar = 39846081,
//            PixelType_Gvsp_Coord3D_AC32f = 37748930,
//            PixelType_Gvsp_COORD3D_DEPTH_PLUS_MASK = -2112094207,
//            PixelType_Gvsp_Coord3D_ABC32 = -2107625471,
//            PixelType_Gvsp_Coord3D_AB32f = -2109722622,
//            PixelType_Gvsp_Coord3D_AB32 = -2109722621,
//            PixelType_Gvsp_Coord3D_AC32f_64 = 37748930,
//            PixelType_Gvsp_Coord3D_AC32f_Planar = 37748931,
//            PixelType_Gvsp_Coord3D_AC32 = -2109722620,
//            PixelType_Gvsp_Coord3D_A32f = 18874557,
//            PixelType_Gvsp_Coord3D_A32 = -2128596987,
//            PixelType_Gvsp_Coord3D_C32f = 18874559,
//            PixelType_Gvsp_Coord3D_C32 = -2128596986,
//            PixelType_Gvsp_Coord3D_ABC16 = 36700345,
//            PixelType_Gvsp_Coord3D_C16 = 17825976,
//            PixelType_Gvsp_Float32 = -2128609279,
//            PixelType_Gvsp_HB_Mono8 = -2130182143,
//            PixelType_Gvsp_HB_Mono10 = -2129657853,
//            PixelType_Gvsp_HB_Mono10_Packed = -2129919996,
//            PixelType_Gvsp_HB_Mono12 = -2129657851,
//            PixelType_Gvsp_HB_Mono12_Packed = -2129919994,
//            PixelType_Gvsp_HB_Mono16 = -2129657849,
//            PixelType_Gvsp_HB_BayerGR8 = -2130182136,
//            PixelType_Gvsp_HB_BayerRG8 = -2130182135,
//            PixelType_Gvsp_HB_BayerGB8 = -2130182134,
//            PixelType_Gvsp_HB_BayerBG8 = -2130182133,
//            PixelType_Gvsp_HB_BayerRBGG8 = -2130182074,
//            PixelType_Gvsp_HB_BayerGR10 = -2129657844,
//            PixelType_Gvsp_HB_BayerRG10 = -2129657843,
//            PixelType_Gvsp_HB_BayerGB10 = -2129657842,
//            PixelType_Gvsp_HB_BayerBG10 = -2129657841,
//            PixelType_Gvsp_HB_BayerGR12 = -2129657840,
//            PixelType_Gvsp_HB_BayerRG12 = -2129657839,
//            PixelType_Gvsp_HB_BayerGB12 = -2129657838,
//            PixelType_Gvsp_HB_BayerBG12 = -2129657837,
//            PixelType_Gvsp_HB_BayerGR10_Packed = -2129919962,
//            PixelType_Gvsp_HB_BayerRG10_Packed = -2129919961,
//            PixelType_Gvsp_HB_BayerGB10_Packed = -2129919960,
//            PixelType_Gvsp_HB_BayerBG10_Packed = -2129919959,
//            PixelType_Gvsp_HB_BayerGR12_Packed = -2129919958,
//            PixelType_Gvsp_HB_BayerRG12_Packed = -2129919957,
//            PixelType_Gvsp_HB_BayerGB12_Packed = -2129919956,
//            PixelType_Gvsp_HB_BayerBG12_Packed = -2129919955,
//            PixelType_Gvsp_HB_YUV422_Packed = -2112880609,
//            PixelType_Gvsp_HB_YUV422_YUYV_Packed = -2112880590,
//            PixelType_Gvsp_HB_RGB8_Packed = -2112356332,
//            PixelType_Gvsp_HB_BGR8_Packed = -2112356331,
//            PixelType_Gvsp_HB_RGBA8_Packed = -2111832042,
//            PixelType_Gvsp_HB_BGRA8_Packed = -2111832041,
//            PixelType_Gvsp_HB_RGB16_Packed = -2110783437,
//            PixelType_Gvsp_HB_BGR16_Packed = -2110783413,
//            PixelType_Gvsp_HB_RGBA16_Packed = -2109734812,
//            PixelType_Gvsp_HB_BGRA16_Packed = -2109734831
//        }

//        public const int MV_GIGE_INTERFACE = 1;

//        public const int MV_CAMERALINK_INTERFACE = 4;

//        public const int MV_CXP_INTERFACE = 8;

//        public const int MV_XOF_INTERFACE = 16;

//        public const int MV_UNKNOW_DEVICE = 0;

//        public const int MV_GIGE_DEVICE = 1;

//        public const int MV_1394_DEVICE = 2;

//        public const int MV_USB_DEVICE = 4;

//        public const int MV_CAMERALINK_DEVICE = 8;

//        public const int MV_VIR_GIGE_DEVICE = 16;

//        public const int MV_VIR_USB_DEVICE = 32;

//        public const int MV_GENTL_GIGE_DEVICE = 64;

//        public const int MV_GENTL_CAMERALINK_DEVICE = 128;

//        public const int MV_GENTL_CXP_DEVICE = 256;

//        public const int MV_GENTL_XOF_DEVICE = 512;

//        public const int INFO_MAX_BUFFER_SIZE = 64;

//        public const int MV_MAX_DEVICE_NUM = 256;

//        public const int MV_MAX_GENTL_IF_NUM = 256;

//        public const int MV_MAX_GENTL_DEV_NUM = 256;

//        public const int MV_MAX_XML_DISC_STRLEN_C = 512;

//        public const int MV_MAX_XML_NODE_STRLEN_C = 64;

//        public const int MV_MAX_XML_NODE_NUM_C = 128;

//        public const int MV_MAX_XML_SYMBOLIC_NUM = 64;

//        public const int MV_MAX_XML_STRVALUE_STRLEN_C = 64;

//        public const int MV_MAX_XML_PARENTS_NUM = 8;

//        public const int MV_MAX_XML_SYMBOLIC_STRLEN_C = 64;

//        public const int MV_EXCEPTION_DEV_DISCONNECT = 32769;

//        public const int MV_EXCEPTION_VERSION_CHECK = 32770;

//        public const int MAX_EVENT_NAME_SIZE = 128;

//        public const int MV_MAX_SYMBOLIC_LEN = 64;

//        public const int MV_MAX_SPLIT_NUM = 8;

//        public const int MV_MAX_INTERFACE_NUM = 64;

//        public const int MV_IP_CFG_STATIC = 83886080;

//        public const int MV_IP_CFG_DHCP = 100663296;

//        public const int MV_IP_CFG_LLA = 67108864;

//        public const int MV_CAML_BAUDRATE_9600 = 1;

//        public const int MV_CAML_BAUDRATE_19200 = 2;

//        public const int MV_CAML_BAUDRATE_38400 = 4;

//        public const int MV_CAML_BAUDRATE_57600 = 8;

//        public const int MV_CAML_BAUDRATE_115200 = 16;

//        public const int MV_CAML_BAUDRATE_230400 = 32;

//        public const int MV_CAML_BAUDRATE_460800 = 64;

//        public const int MV_CAML_BAUDRATE_921600 = 128;

//        public const int MV_CAML_BAUDRATE_AUTOMAX = 1073741824;

//        public const int MV_MATCH_TYPE_NET_DETECT = 1;

//        public const int MV_MATCH_TYPE_USB_DETECT = 2;

//        public const int MV_ACCESS_Exclusive = 1;

//        public const int MV_ACCESS_ExclusiveWithSwitch = 2;

//        public const int MV_ACCESS_Control = 3;

//        public const int MV_ACCESS_ControlWithSwitch = 4;

//        public const int MV_ACCESS_ControlSwitchEnable = 5;

//        public const int MV_ACCESS_ControlSwitchEnableWithKey = 6;

//        public const int MV_ACCESS_Monitor = 7;

//        public const int MV_OK = 0;

//        public const int MV_E_HANDLE = int.MinValue;

//        public const int MV_E_SUPPORT = -2147483647;

//        public const int MV_E_BUFOVER = -2147483646;

//        public const int MV_E_CALLORDER = -2147483645;

//        public const int MV_E_PARAMETER = -2147483644;

//        public const int MV_E_RESOURCE = -2147483642;

//        public const int MV_E_NODATA = -2147483641;

//        public const int MV_E_PRECONDITION = -2147483640;

//        public const int MV_E_VERSION = -2147483639;

//        public const int MV_E_NOENOUGH_BUF = -2147483638;

//        public const int MV_E_ABNORMAL_IMAGE = -2147483637;

//        public const int MV_E_LOAD_LIBRARY = -2147483636;

//        public const int MV_E_NOOUTBUF = -2147483635;

//        public const int MV_E_ENCRYPT = -2147483634;

//        public const int MV_E_OPENFILE = -2147483633;

//        public const int MV_E_UNKNOW = -2147483393;

//        public const int MV_E_GC_GENERIC = -2147483392;

//        public const int MV_E_GC_ARGUMENT = -2147483391;

//        public const int MV_E_GC_RANGE = -2147483390;

//        public const int MV_E_GC_PROPERTY = -2147483389;

//        public const int MV_E_GC_RUNTIME = -2147483388;

//        public const int MV_E_GC_LOGICAL = -2147483387;

//        public const int MV_E_GC_ACCESS = -2147483386;

//        public const int MV_E_GC_TIMEOUT = -2147483385;

//        public const int MV_E_GC_DYNAMICCAST = -2147483384;

//        public const int MV_E_GC_UNKNOW = -2147483137;

//        public const int MV_E_NOT_IMPLEMENTED = -2147483136;

//        public const int MV_E_INVALID_ADDRESS = -2147483135;

//        public const int MV_E_WRITE_PROTECT = -2147483134;

//        public const int MV_E_ACCESS_DENIED = -2147483133;

//        public const int MV_E_BUSY = -2147483132;

//        public const int MV_E_PACKET = -2147483131;

//        public const int MV_E_NETER = -2147483130;

//        public const int MV_E_IP_CONFLICT = -2147483103;

//        public const int MV_E_USB_READ = -2147482880;

//        public const int MV_E_USB_WRITE = -2147482879;

//        public const int MV_E_USB_DEVICE = -2147482878;

//        public const int MV_E_USB_GENICAM = -2147482877;

//        public const int MV_E_USB_BANDWIDTH = -2147482876;

//        public const int MV_E_USB_DRIVER = -2147482875;

//        public const int MV_E_USB_UNKNOW = -2147482625;

//        public const int MV_E_UPG_FILE_MISMATCH = -2147482624;

//        public const int MV_E_UPG_LANGUSGE_MISMATCH = -2147482623;

//        public const int MV_E_UPG_CONFLICT = -2147482622;

//        public const int MV_E_UPG_INNER_ERR = -2147482621;

//        public const int MV_E_UPG_UNKNOW = -2147482369;

//        public const int MV_ALG_OK = 0;

//        public const int MV_ALG_ERR = 268435456;

//        public const int MV_ALG_E_ABILITY_ARG = 268435457;

//        public const int MV_ALG_E_MEM_NULL = 268435458;

//        public const int MV_ALG_E_MEM_ALIGN = 268435459;

//        public const int MV_ALG_E_MEM_LACK = 268435460;

//        public const int MV_ALG_E_MEM_SIZE_ALIGN = 268435461;

//        public const int MV_ALG_E_MEM_ADDR_ALIGN = 268435462;

//        public const int MV_ALG_E_IMG_FORMAT = 268435463;

//        public const int MV_ALG_E_IMG_SIZE = 268435464;

//        public const int MV_ALG_E_IMG_STEP = 268435465;

//        public const int MV_ALG_E_IMG_DATA_NULL = 268435466;

//        public const int MV_ALG_E_CFG_TYPE = 268435467;

//        public const int MV_ALG_E_CFG_SIZE = 268435468;

//        public const int MV_ALG_E_PRC_TYPE = 268435469;

//        public const int MV_ALG_E_PRC_SIZE = 268435470;

//        public const int MV_ALG_E_FUNC_TYPE = 268435471;

//        public const int MV_ALG_E_FUNC_SIZE = 268435472;

//        public const int MV_ALG_E_PARAM_INDEX = 268435473;

//        public const int MV_ALG_E_PARAM_VALUE = 268435474;

//        public const int MV_ALG_E_PARAM_NUM = 268435475;

//        public const int MV_ALG_E_NULL_PTR = 268435476;

//        public const int MV_ALG_E_OVER_MAX_MEM = 268435477;

//        public const int MV_ALG_E_CALL_BACK = 268435478;

//        public const int MV_ALG_E_ENCRYPT = 268435479;

//        public const int MV_ALG_E_EXPIRE = 268435480;

//        public const int MV_ALG_E_BAD_ARG = 268435481;

//        public const int MV_ALG_E_DATA_SIZE = 268435482;

//        public const int MV_ALG_E_STEP = 268435483;

//        public const int MV_ALG_E_CPUID = 268435484;

//        public const int MV_ALG_WARNING = 268435485;

//        public const int MV_ALG_E_TIME_OUT = 268435486;

//        public const int MV_ALG_E_LIB_VERSION = 268435487;

//        public const int MV_ALG_E_MODEL_VERSION = 268435488;

//        public const int MV_ALG_E_GPU_MEM_ALLOC = 268435489;

//        public const int MV_ALG_E_FILE_NON_EXIST = 268435490;

//        public const int MV_ALG_E_NONE_STRING = 268435491;

//        public const int MV_ALG_E_IMAGE_CODEC = 268435492;

//        public const int MV_ALG_E_FILE_OPEN = 268435493;

//        public const int MV_ALG_E_FILE_READ = 268435494;

//        public const int MV_ALG_E_FILE_WRITE = 268435495;

//        public const int MV_ALG_E_FILE_READ_SIZE = 268435496;

//        public const int MV_ALG_E_FILE_TYPE = 268435497;

//        public const int MV_ALG_E_MODEL_TYPE = 268435498;

//        public const int MV_ALG_E_MALLOC_MEM = 268435499;

//        public const int MV_ALG_E_BIND_CORE_FAILED = 268435500;

//        public const int MV_ALG_E_DENOISE_NE_IMG_FORMAT = 272637953;

//        public const int MV_ALG_E_DENOISE_NE_FEATURE_TYPE = 272637954;

//        public const int MV_ALG_E_DENOISE_NE_PROFILE_NUM = 272637955;

//        public const int MV_ALG_E_DENOISE_NE_GAIN_NUM = 272637956;

//        public const int MV_ALG_E_DENOISE_NE_GAIN_VAL = 272637957;

//        public const int MV_ALG_E_DENOISE_NE_BIN_NUM = 272637958;

//        public const int MV_ALG_E_DENOISE_NE_INIT_GAIN = 272637959;

//        public const int MV_ALG_E_DENOISE_NE_NOT_INIT = 272637960;

//        public const int MV_ALG_E_DENOISE_COLOR_MODE = 272637961;

//        public const int MV_ALG_E_DENOISE_ROI_NUM = 272637962;

//        public const int MV_ALG_E_DENOISE_ROI_ORI_PT = 272637963;

//        public const int MV_ALG_E_DENOISE_ROI_SIZE = 272637964;

//        public const int MV_ALG_E_DENOISE_GAIN_NOT_EXIST = 272637965;

//        public const int MV_ALG_E_DENOISE_GAIN_BEYOND_RANGE = 272637966;

//        public const int MV_ALG_E_DENOISE_NP_BUF_SIZE = 272637967;

//        private IntPtr handle;

//        public MyCamera()
//        {
//            handle = IntPtr.Zero;
//        }

//        ~MyCamera()
//        {
//            handle = IntPtr.Zero;
//        }

//        public static int MV_CC_Initialize_NET()
//        {
//            return MV_CC_Initialize();
//        }

//        public static int MV_CC_Finalize_NET()
//        {
//            return MV_CC_Finalize();
//        }

//        public static int MV_CC_EnumInterfaces_NET(uint nTLayerType, ref MV_INTERFACE_INFO_LIST pInterfaceInfoList)
//        {
//            return MV_CC_EnumInterfaces(nTLayerType, ref pInterfaceInfoList);
//        }

//        public int MV_CC_CreateInterface_NET(ref MV_INTERFACE_INFO pInterfaceInfo)
//        {
//            if (IntPtr.Zero != handle)
//            {
//                MV_CC_DestroyInterface(handle);
//                handle = IntPtr.Zero;
//            }

//            return MV_CC_CreateInterface(ref handle, ref pInterfaceInfo);
//        }

//        public int MV_CC_CreateInterfaceByID_NET(string pInterfaceID)
//        {
//            if (IntPtr.Zero != handle)
//            {
//                MV_CC_DestroyInterface(handle);
//                handle = IntPtr.Zero;
//            }

//            return MV_CC_CreateInterfaceByID(ref handle, pInterfaceID);
//        }

//        public int MV_CC_OpenInterface_NET(string strConfigFile)
//        {
//            return MV_CC_OpenInterface(handle, strConfigFile);
//        }

//        public int MV_CC_CloseInterface_NET()
//        {
//            return MV_CC_CloseInterface(handle);
//        }

//        public int MV_CC_DestroyInterface_NET()
//        {
//            int result = MV_CC_DestroyInterface(handle);
//            handle = IntPtr.Zero;
//            return result;
//        }

//        public IntPtr GetCameraHandle()
//        {
//            return handle;
//        }

//        public static uint MV_CC_GetSDKVersion_NET()
//        {
//            return MV_CC_GetSDKVersion();
//        }

//        public static int MV_CC_EnumerateTls_NET()
//        {
//            return MV_CC_EnumerateTls();
//        }

//        public static int MV_CC_EnumDevices_NET(uint nTLayerType, ref MV_CC_DEVICE_INFO_LIST stDevList)
//        {
//            return MV_CC_EnumDevices(nTLayerType, ref stDevList);
//        }

//        public static int MV_CC_EnumDevicesEx_NET(uint nTLayerType, ref MV_CC_DEVICE_INFO_LIST stDevList, string pManufacturerName)
//        {
//            return MV_CC_EnumDevicesEx(nTLayerType, ref stDevList, pManufacturerName);
//        }

//        public static int MV_CC_EnumDevicesEx2_NET(uint nTLayerType, ref MV_CC_DEVICE_INFO_LIST stDevList, string pManufacturerName, MV_SORT_METHOD enSortMethod)
//        {
//            return MV_CC_EnumDevicesEx2(nTLayerType, ref stDevList, pManufacturerName, enSortMethod);
//        }

//        public static bool MV_CC_IsDeviceAccessible_NET(ref MV_CC_DEVICE_INFO stDevInfo, uint nAccessMode)
//        {
//            if (MV_CC_IsDeviceAccessible(ref stDevInfo, nAccessMode) != 0)
//            {
//                return true;
//            }

//            return false;
//        }

//        public static int MV_CC_SetSDKLogPath_NET(string pSDKLogPath)
//        {
//            return MV_CC_SetSDKLogPath(pSDKLogPath);
//        }

//        public int MV_CC_CreateDevice_NET(ref MV_CC_DEVICE_INFO stDevInfo)
//        {
//            if (IntPtr.Zero != handle)
//            {
//                MV_CC_DestroyHandle(handle);
//                handle = IntPtr.Zero;
//            }

//            return MV_CC_CreateHandle(ref handle, ref stDevInfo);
//        }

//        public int MV_CC_CreateDeviceWithoutLog_NET(ref MV_CC_DEVICE_INFO stDevInfo)
//        {
//            if (IntPtr.Zero != handle)
//            {
//                MV_CC_DestroyHandle(handle);
//                handle = IntPtr.Zero;
//            }

//            return MV_CC_CreateHandleWithoutLog(ref handle, ref stDevInfo);
//        }

//        public int MV_CC_DestroyDevice_NET()
//        {
//            int result = MV_CC_DestroyHandle(handle);
//            handle = IntPtr.Zero;
//            return result;
//        }

//        public int MV_CC_OpenDevice_NET()
//        {
//            return MV_CC_OpenDevice(handle, 1u, 0);
//        }

//        public int MV_CC_OpenDevice_NET(uint nAccessMode, ushort nSwitchoverKey)
//        {
//            return MV_CC_OpenDevice(handle, nAccessMode, nSwitchoverKey);
//        }

//        public int MV_CC_CloseDevice_NET()
//        {
//            return MV_CC_CloseDevice(handle);
//        }

//        public bool MV_CC_IsDeviceConnected_NET()
//        {
//            if (handle == IntPtr.Zero)
//            {
//                return false;
//            }

//            if (MV_CC_IsDeviceConnected(handle) != 0)
//            {
//                return true;
//            }

//            return false;
//        }

//        public int MV_CC_RegisterImageCallBackEx_NET(cbOutputExdelegate cbOutput, IntPtr pUser)
//        {
//            return MV_CC_RegisterImageCallBackEx(handle, cbOutput, pUser);
//        }

//        public int MV_CC_RegisterImageCallBackForRGB_NET(cbOutputExdelegate cbOutput, IntPtr pUser)
//        {
//            return MV_CC_RegisterImageCallBackForRGB(handle, cbOutput, pUser);
//        }

//        public int MV_CC_RegisterImageCallBackForBGR_NET(cbOutputExdelegate cbOutput, IntPtr pUser)
//        {
//            return MV_CC_RegisterImageCallBackForBGR(handle, cbOutput, pUser);
//        }

//        public int MV_CC_StartGrabbing_NET()
//        {
//            return MV_CC_StartGrabbing(handle);
//        }

//        public int MV_CC_StopGrabbing_NET()
//        {
//            return MV_CC_StopGrabbing(handle);
//        }

//        public int MV_CC_GetImageForRGB_NET(IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO_EX pFrameInfo, int nMsec)
//        {
//            return MV_CC_GetImageForRGB(handle, pData, nDataSize, ref pFrameInfo, nMsec);
//        }

//        public int MV_CC_GetImageForBGR_NET(IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO_EX pFrameInfo, int nMsec)
//        {
//            return MV_CC_GetImageForBGR(handle, pData, nDataSize, ref pFrameInfo, nMsec);
//        }

//        public int MV_CC_GetImageBuffer_NET(ref MV_FRAME_OUT pFrame, int nMsec)
//        {
//            return MV_CC_GetImageBuffer(handle, ref pFrame, nMsec);
//        }

//        public int MV_CC_FreeImageBuffer_NET(ref MV_FRAME_OUT pFrame)
//        {
//            return MV_CC_FreeImageBuffer(handle, ref pFrame);
//        }

//        public int MV_CC_GetOneFrameTimeout_NET(IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO_EX pFrameInfo, int nMsec)
//        {
//            return MV_CC_GetOneFrameTimeout(handle, pData, nDataSize, ref pFrameInfo, nMsec);
//        }

//        public int MV_CC_ClearImageBuffer_NET()
//        {
//            return MV_CC_ClearImageBuffer(handle);
//        }

//        public int MV_CC_GetValidImageNum_NET(ref uint pnValidImageNum)
//        {
//            return MV_CC_GetValidImageNum(handle, ref pnValidImageNum);
//        }

//        public int MV_CC_DisplayOneFrame_NET(ref MV_DISPLAY_FRAME_INFO pDisplayInfo)
//        {
//            return MV_CC_DisplayOneFrame(handle, ref pDisplayInfo);
//        }

//        public int MV_CC_DisplayOneFrameEx_NET(IntPtr pDisplayHandle, ref MV_DISPLAY_FRAME_INFO_EX pDisplayInfoEx)
//        {
//            if (IntPtr.Zero == pDisplayHandle)
//            {
//                return -2147483644;
//            }

//            return MV_CC_DisplayOneFrameEx(handle, pDisplayHandle, ref pDisplayInfoEx);
//        }

//        public int MV_CC_SetImageNodeNum_NET(uint nNum)
//        {
//            return MV_CC_SetImageNodeNum(handle, nNum);
//        }

//        public int MV_CC_SetGrabStrategy_NET(MV_GRAB_STRATEGY enGrabStrategy)
//        {
//            return MV_CC_SetGrabStrategy(handle, enGrabStrategy);
//        }

//        public int MV_CC_SetOutputQueueSize_NET(uint nOutputQueueSize)
//        {
//            return MV_CC_SetOutputQueueSize(handle, nOutputQueueSize);
//        }

//        public int MV_CC_GetDeviceInfo_NET(ref MV_CC_DEVICE_INFO pstDevInfo)
//        {
//            return MV_CC_GetDeviceInfo(handle, ref pstDevInfo);
//        }

//        public int MV_CC_GetAllMatchInfo_NET(ref MV_ALL_MATCH_INFO pstInfo)
//        {
//            return MV_CC_GetAllMatchInfo(handle, ref pstInfo);
//        }

//        public int MV_CC_GetIntValueEx_NET(string strKey, ref MVCC_INTVALUE_EX pstValue)
//        {
//            return MV_CC_GetIntValueEx(handle, strKey, ref pstValue);
//        }

//        public int MV_CC_SetIntValueEx_NET(string strKey, long nValue)
//        {
//            return MV_CC_SetIntValueEx(handle, strKey, nValue);
//        }

//        public int MV_CC_GetEnumValue_NET(string strKey, ref MVCC_ENUMVALUE pstValue)
//        {
//            return MV_CC_GetEnumValue(handle, strKey, ref pstValue);
//        }

//        public int MV_CC_SetEnumValue_NET(string strKey, uint nValue)
//        {
//            return MV_CC_SetEnumValue(handle, strKey, nValue);
//        }

//        public int MV_CC_GetEnumEntrySymbolic_NET(string strKey, ref MVCC_ENUMENTRY pstEnumEntry)
//        {
//            return MV_CC_GetEnumEntrySymbolic(handle, strKey, ref pstEnumEntry);
//        }

//        public int MV_CC_SetEnumValueByString_NET(string strKey, string sValue)
//        {
//            return MV_CC_SetEnumValueByString(handle, strKey, sValue);
//        }

//        public int MV_CC_GetFloatValue_NET(string strKey, ref MVCC_FLOATVALUE pstValue)
//        {
//            return MV_CC_GetFloatValue(handle, strKey, ref pstValue);
//        }

//        public int MV_CC_SetFloatValue_NET(string strKey, float fValue)
//        {
//            return MV_CC_SetFloatValue(handle, strKey, fValue);
//        }

//        public int MV_CC_GetBoolValue_NET(string strKey, ref bool pbValue)
//        {
//            return MV_CC_GetBoolValue(handle, strKey, ref pbValue);
//        }

//        public int MV_CC_SetBoolValue_NET(string strKey, bool bValue)
//        {
//            return MV_CC_SetBoolValue(handle, strKey, bValue);
//        }

//        public int MV_CC_GetStringValue_NET(string strKey, ref MVCC_STRINGVALUE pstValue)
//        {
//            return MV_CC_GetStringValue(handle, strKey, ref pstValue);
//        }

//        public int MV_CC_SetStringValue_NET(string strKey, string strValue)
//        {
//            return MV_CC_SetStringValue(handle, strKey, strValue);
//        }

//        public int MV_CC_SetCommandValue_NET(string strKey)
//        {
//            return MV_CC_SetCommandValue(handle, strKey);
//        }

//        public int MV_CC_ReadMemory_NET(IntPtr pBuffer, long nAddress, long nLength)
//        {
//            return MV_CC_ReadMemory(handle, pBuffer, nAddress, nLength);
//        }

//        public int MV_CC_WriteMemory_NET(IntPtr pBuffer, long nAddress, long nLength)
//        {
//            return MV_CC_WriteMemory(handle, pBuffer, nAddress, nLength);
//        }

//        public int MV_CC_InvalidateNodes_NET()
//        {
//            return MV_CC_InvalidateNodes(handle);
//        }

//        public int MV_XML_GetGenICamXML_NET(IntPtr pData, uint nDataSize, ref uint pnDataLen)
//        {
//            return MV_XML_GetGenICamXML(handle, pData, nDataSize, ref pnDataLen);
//        }

//        public int MV_XML_GetNodeAccessMode_NET(string pstrName, ref MV_XML_AccessMode pAccessMode)
//        {
//            return MV_XML_GetNodeAccessMode(handle, pstrName, ref pAccessMode);
//        }

//        public int MV_XML_GetNodeInterfaceType_NET(string pstrName, ref MV_XML_InterfaceType pInterfaceType)
//        {
//            return MV_XML_GetNodeInterfaceType(handle, pstrName, ref pInterfaceType);
//        }

//        public int MV_CC_FeatureSave_NET(string pFileName)
//        {
//            return MV_CC_FeatureSave(handle, pFileName);
//        }

//        public int MV_CC_FeatureLoad_NET(string pFileName)
//        {
//            return MV_CC_FeatureLoad(handle, pFileName);
//        }

//        public int MV_CC_FileAccessRead_NET(ref MV_CC_FILE_ACCESS pstFileAccess)
//        {
//            return MV_CC_FileAccessRead(handle, ref pstFileAccess);
//        }

//        public int MV_CC_FileAccessReadEx_NET(ref MV_CC_FILE_ACCESS_EX pstFileAccessEx)
//        {
//            return MV_CC_FileAccessReadEx(handle, ref pstFileAccessEx);
//        }

//        public int MV_CC_FileAccessWrite_NET(ref MV_CC_FILE_ACCESS pstFileAccess)
//        {
//            return MV_CC_FileAccessWrite(handle, ref pstFileAccess);
//        }

//        public int MV_CC_FileAccessWriteEx_NET(ref MV_CC_FILE_ACCESS_EX pstFileAccessEx)
//        {
//            return MV_CC_FileAccessWriteEx(handle, ref pstFileAccessEx);
//        }

//        public int MV_CC_GetFileAccessProgress_NET(ref MV_CC_FILE_ACCESS_PROGRESS pstFileAccessProgress)
//        {
//            return MV_CC_GetFileAccessProgress(handle, ref pstFileAccessProgress);
//        }

//        public int MV_CC_LocalUpgrade_NET(string pFilePathName)
//        {
//            return MV_CC_LocalUpgrade(handle, pFilePathName);
//        }

//        public int MV_CC_GetUpgradeProcess_NET(ref uint pnProcess)
//        {
//            return MV_CC_GetUpgradeProcess(handle, ref pnProcess);
//        }

//        public int MV_CC_RegisterExceptionCallBack_NET(cbExceptiondelegate cbException, IntPtr pUser)
//        {
//            return MV_CC_RegisterExceptionCallBack(handle, cbException, pUser);
//        }

//        public int MV_CC_RegisterAllEventCallBack_NET(cbEventdelegateEx cbEvent, IntPtr pUser)
//        {
//            return MV_CC_RegisterAllEventCallBack(handle, cbEvent, pUser);
//        }

//        public int MV_CC_RegisterEventCallBackEx_NET(string pEventName, cbEventdelegateEx cbEvent, IntPtr pUser)
//        {
//            return MV_CC_RegisterEventCallBackEx(handle, pEventName, cbEvent, pUser);
//        }

//        public static int MV_GIGE_SetEnumDevTimeout_NET(uint nMilTimeout)
//        {
//            return MV_GIGE_SetEnumDevTimeout(nMilTimeout);
//        }

//        public int MV_GIGE_ForceIpEx_NET(uint nIP, uint nSubNetMask, uint nDefaultGateWay)
//        {
//            return MV_GIGE_ForceIpEx(handle, nIP, nSubNetMask, nDefaultGateWay);
//        }

//        public int MV_GIGE_SetIpConfig_NET(uint nType)
//        {
//            return MV_GIGE_SetIpConfig(handle, nType);
//        }

//        public int MV_GIGE_SetNetTransMode_NET(uint nType)
//        {
//            return MV_GIGE_SetNetTransMode(handle, nType);
//        }

//        public int MV_GIGE_GetNetTransInfo_NET(ref MV_NETTRANS_INFO pstInfo)
//        {
//            return MV_GIGE_GetNetTransInfo(handle, ref pstInfo);
//        }

//        public static int MV_GIGE_SetDiscoveryMode_NET(uint nMode)
//        {
//            return MV_GIGE_SetDiscoveryMode(nMode);
//        }

//        public int MV_GIGE_SetGvspTimeout_NET(uint nMillisec)
//        {
//            return MV_GIGE_SetGvspTimeout(handle, nMillisec);
//        }

//        public int MV_GIGE_GetGvspTimeout_NET(ref uint pMillisec)
//        {
//            return MV_GIGE_GetGvspTimeout(handle, ref pMillisec);
//        }

//        public int MV_GIGE_SetGvcpTimeout_NET(uint nMillisec)
//        {
//            return MV_GIGE_SetGvcpTimeout(handle, nMillisec);
//        }

//        public int MV_GIGE_GetGvcpTimeout_NET(ref uint pMillisec)
//        {
//            return MV_GIGE_GetGvcpTimeout(handle, ref pMillisec);
//        }

//        public int MV_GIGE_SetRetryGvcpTimes_NET(uint nRetryGvcpTimes)
//        {
//            return MV_GIGE_SetRetryGvcpTimes(handle, nRetryGvcpTimes);
//        }

//        public int MV_GIGE_GetRetryGvcpTimes_NET(ref uint pRetryGvcpTimes)
//        {
//            return MV_GIGE_GetRetryGvcpTimes(handle, ref pRetryGvcpTimes);
//        }

//        public int MV_CC_GetOptimalPacketSize_NET()
//        {
//            return MV_CC_GetOptimalPacketSize(handle);
//        }

//        public int MV_GIGE_SetResend_NET(uint bEnable, uint nMaxResendPercent, uint nResendTimeout)
//        {
//            return MV_GIGE_SetResend(handle, bEnable, nMaxResendPercent, nResendTimeout);
//        }

//        public int MV_GIGE_SetResendMaxRetryTimes_NET(uint nRetryTimes)
//        {
//            return MV_GIGE_SetResendMaxRetryTimes(handle, nRetryTimes);
//        }

//        public int MV_GIGE_GetResendMaxRetryTimes_NET(ref uint pnRetryTimes)
//        {
//            return MV_GIGE_GetResendMaxRetryTimes(handle, ref pnRetryTimes);
//        }

//        public int MV_GIGE_SetResendTimeInterval_NET(uint nMillisec)
//        {
//            return MV_GIGE_SetResendTimeInterval(handle, nMillisec);
//        }

//        public int MV_GIGE_GetResendTimeInterval_NET(ref uint pnMillisec)
//        {
//            return MV_GIGE_GetResendTimeInterval(handle, ref pnMillisec);
//        }

//        public int MV_GIGE_SetTransmissionType_NET(ref MV_CC_TRANSMISSION_TYPE pstTransmissionType)
//        {
//            return MV_GIGE_SetTransmissionType(handle, ref pstTransmissionType);
//        }

//        public int MV_GIGE_IssueActionCommand_NET(ref MV_ACTION_CMD_INFO pstActionCmdInfo, ref MV_ACTION_CMD_RESULT_LIST pstActionCmdResults)
//        {
//            return MV_GIGE_IssueActionCommand(ref pstActionCmdInfo, ref pstActionCmdResults);
//        }

//        public static int MV_GIGE_GetMulticastStatus_NET(ref MV_CC_DEVICE_INFO pstDevInfo, ref bool pStatus)
//        {
//            return MV_GIGE_GetMulticastStatus(ref pstDevInfo, ref pStatus);
//        }

//        public int MV_CAML_SetDeviceBauderate_NET(uint nBaudrate)
//        {
//            return MV_CAML_SetDeviceBaudrate(handle, nBaudrate);
//        }

//        public int MV_CAML_GetDeviceBauderate_NET(ref uint pnCurrentBaudrate)
//        {
//            return MV_CAML_GetDeviceBaudrate(handle, ref pnCurrentBaudrate);
//        }

//        public int MV_CAML_GetSupportBauderates_NET(ref uint pnBaudrateAblity)
//        {
//            return MV_CAML_GetSupportBaudrates(handle, ref pnBaudrateAblity);
//        }

//        public int MV_CAML_SetGenCPTimeOut_NET(uint nMillisec)
//        {
//            return MV_CAML_SetGenCPTimeOut(handle, nMillisec);
//        }

//        public int MV_USB_SetTransferSize_NET(uint nTransferSize)
//        {
//            return MV_USB_SetTransferSize(handle, nTransferSize);
//        }

//        public int MV_USB_GetTransferSize_NET(ref uint pTransferSize)
//        {
//            return MV_USB_GetTransferSize(handle, ref pTransferSize);
//        }

//        public int MV_USB_SetTransferWays_NET(uint nTransferWays)
//        {
//            return MV_USB_SetTransferWays(handle, nTransferWays);
//        }

//        public int MV_USB_GetTransferWays_NET(ref uint pTransferWays)
//        {
//            return MV_USB_GetTransferWays(handle, ref pTransferWays);
//        }

//        public int MV_USB_RegisterStreamExceptionCallBack_NET(cbStreamException cbException, IntPtr pUser)
//        {
//            return MV_USB_RegisterStreamExceptionCallBack(handle, cbException, pUser);
//        }

//        public int MV_USB_SetEventNodeNum_NET(uint nEventNodeNum)
//        {
//            return MV_USB_SetEventNodeNum(handle, nEventNodeNum);
//        }

//        public int MV_USB_SetSyncTimeOut_NET(uint nMills)
//        {
//            return MV_USB_SetSyncTimeOut(handle, nMills);
//        }

//        public int MV_USB_GetSyncTimeOut_NET(ref uint pnMills)
//        {
//            return MV_USB_GetSyncTimeOut(handle, ref pnMills);
//        }

//        public static int MV_CC_EnumInterfacesByGenTL_NET(ref MV_GENTL_IF_INFO_LIST stIFInfoList, string pGenTLPath)
//        {
//            return MV_CC_EnumInterfacesByGenTL(ref stIFInfoList, pGenTLPath);
//        }

//        public static int MV_CC_UnloadGenTLLibrary_NET(string strGenTLPath)
//        {
//            return MV_CC_UnloadGenTLLibrary(strGenTLPath);
//        }

//        public static int MV_CC_EnumDevicesByGenTL_NET(ref MV_GENTL_IF_INFO stIFInfo, ref MV_GENTL_DEV_INFO_LIST stDevList)
//        {
//            return MV_CC_EnumDevicesByGenTL(ref stIFInfo, ref stDevList);
//        }

//        public int MV_CC_CreateDeviceByGenTL_NET(ref MV_GENTL_DEV_INFO stDevInfo)
//        {
//            if (IntPtr.Zero != handle)
//            {
//                MV_CC_DestroyHandle(handle);
//                handle = IntPtr.Zero;
//            }

//            return MV_CC_CreateHandleByGenTL(ref handle, ref stDevInfo);
//        }

//        public int MV_CC_SaveImageEx3_NET(ref MV_SAVE_IMAGE_PARAM_EX3 stSaveParam)
//        {
//            return MV_CC_SaveImageEx3(handle, ref stSaveParam);
//        }

//        public int MV_CC_SaveImageToFileEx_NET(ref MV_SAVE_IMG_TO_FILE_PARAM_EX pstSaveFileParam)
//        {
//            return MV_CC_SaveImageToFileEx(handle, ref pstSaveFileParam);
//        }

//        public int MV_CC_SavePointCloudData_NET(ref MV_SAVE_POINT_CLOUD_PARAM pstPointDataParam)
//        {
//            return MV_CC_SavePointCloudData(handle, ref pstPointDataParam);
//        }

//        public int MV_CC_RotateImage_NET(ref MV_CC_ROTATE_IMAGE_PARAM pstRotateParam)
//        {
//            return MV_CC_RotateImage(handle, ref pstRotateParam);
//        }

//        public int MV_CC_FlipImage_NET(ref MV_CC_FLIP_IMAGE_PARAM pstFlipParam)
//        {
//            return MV_CC_FlipImage(handle, ref pstFlipParam);
//        }

//        public int MV_CC_ConvertPixelTypeEx_NET(ref MV_CC_PIXEL_CONVERT_PARAM_EX pstCvtParam)
//        {
//            return MV_CC_ConvertPixelTypeEx(handle, ref pstCvtParam);
//        }

//        public int MV_CC_SetBayerCvtQuality_NET(uint BayerCvtQuality)
//        {
//            return MV_CC_SetBayerCvtQuality(handle, BayerCvtQuality);
//        }

//        public int MV_CC_SetBayerFilterEnable_NET(bool bFilterEnable)
//        {
//            return MV_CC_SetBayerFilterEnable(handle, bFilterEnable);
//        }

//        public int MV_CC_SetBayerGammaValue_NET(float fBayerGammaValue)
//        {
//            return MV_CC_SetBayerGammaValue(handle, fBayerGammaValue);
//        }

//        public int MV_CC_SetGammaValue_NET(MvGvspPixelType enPixelType, float fGammaValue)
//        {
//            return MV_CC_SetGammaValue(handle, enPixelType, fGammaValue);
//        }

//        public int MV_CC_SetBayerGammaParam_NET(ref MV_CC_GAMMA_PARAM pstGammaParam)
//        {
//            return MV_CC_SetBayerGammaParam(handle, ref pstGammaParam);
//        }

//        public int MV_CC_SetBayerCCMParam_NET(ref MV_CC_CCM_PARAM pstCCMParam)
//        {
//            return MV_CC_SetBayerCCMParam(handle, ref pstCCMParam);
//        }

//        public int MV_CC_SetBayerCCMParamEx_NET(ref MV_CC_CCM_PARAM_EX pstCCMParam)
//        {
//            return MV_CC_SetBayerCCMParamEx(handle, ref pstCCMParam);
//        }

//        public int MV_CC_ImageContrast_NET(ref MV_CC_CONTRAST_PARAM pstContrastParam)
//        {
//            return MV_CC_ImageContrast(handle, ref pstContrastParam);
//        }

//        public int MV_CC_HB_Decode_NET(ref MV_CC_HB_DECODE_PARAM pstDecodeParam)
//        {
//            return MV_CC_HB_Decode(handle, ref pstDecodeParam);
//        }

//        public int MV_CC_DrawRect_NET(ref MVCC_RECT_INFO pstRectInfo)
//        {
//            return MV_CC_DrawRect(handle, ref pstRectInfo);
//        }

//        public int MV_CC_DrawCircle_NET(ref MVCC_CIRCLE_INFO pstCircleInfo)
//        {
//            return MV_CC_DrawCircle(handle, ref pstCircleInfo);
//        }

//        public int MV_CC_DrawLines_NET(ref MVCC_LINES_INFO pstLinesInfo)
//        {
//            return MV_CC_DrawLines(handle, ref pstLinesInfo);
//        }

//        public int MV_CC_StartRecord_NET(ref MV_CC_RECORD_PARAM pstRecordParam)
//        {
//            return MV_CC_StartRecord(handle, ref pstRecordParam);
//        }

//        public int MV_CC_InputOneFrame_NET(ref MV_CC_INPUT_FRAME_INFO pstInputFrameInfo)
//        {
//            return MV_CC_InputOneFrame(handle, ref pstInputFrameInfo);
//        }

//        public int MV_CC_StopRecord_NET()
//        {
//            return MV_CC_StopRecord(handle);
//        }

//        public int MV_CC_OpenParamsGUI_NET()
//        {
//            return MV_CC_OpenParamsGUI(handle);
//        }

//        public int MV_CC_ReconstructImage_NET(ref MV_RECONSTRUCT_IMAGE_PARAM pstReconstructParam)
//        {
//            return MV_CC_ReconstructImage(handle, ref pstReconstructParam);
//        }

//        public static object ByteToStruct(byte[] bytes, Type type)
//        {
//            int num = Marshal.SizeOf(type);
//            if (num > bytes.Length)
//            {
//                return null;
//            }

//            IntPtr intPtr = Marshal.AllocHGlobal(num);
//            Marshal.Copy(bytes, 0, intPtr, num);
//            object result = Marshal.PtrToStructure(intPtr, type);
//            Marshal.FreeHGlobal(intPtr);
//            return result;
//        }

//        public static bool IsTextUTF8(byte[] inputStream)
//        {
//            int num = 0;
//            bool flag = true;
//            for (int i = 0; i < inputStream.Length; i++)
//            {
//                byte b = inputStream[i];
//                if ((b & 0x80) == 128)
//                {
//                    flag = false;
//                }

//                if (num == 0)
//                {
//                    if ((b & 0x80u) != 0)
//                    {
//                        if ((b & 0xC0) != 192)
//                        {
//                            return false;
//                        }

//                        num = 1;
//                        b <<= 2;
//                        while ((b & 0x80) == 128)
//                        {
//                            b <<= 1;
//                            num++;
//                        }
//                    }
//                }
//                else
//                {
//                    if ((b & 0xC0) != 128)
//                    {
//                        return false;
//                    }

//                    num--;
//                }
//            }

//            if (num != 0)
//            {
//                return false;
//            }

//            return !flag;
//        }

//        public static void WriteErrorMsg(string csMessage, int nErrorNum)
//        {
//            if (nErrorNum != 0)
//            {
//                _ = csMessage + ": Error =" + $"{nErrorNum:X}";
//            }
//        }

//        public int MV_CC_SaveImageEx2_NET(ref MV_SAVE_IMAGE_PARAM_EX2 stSaveParam)
//        {
//            return MV_CC_SaveImageEx2(handle, ref stSaveParam);
//        }

//        public int MV_CC_SaveImageToFile_NET(ref MV_SAVE_IMG_TO_FILE_PARAM pstSaveFileParam)
//        {
//            return MV_CC_SaveImageToFile(handle, ref pstSaveFileParam);
//        }

//        public int MV_CC_ConvertPixelType_NET(ref MV_PIXEL_CONVERT_PARAM pstCvtParam)
//        {
//            return MV_CC_ConvertPixelType(handle, ref pstCvtParam);
//        }

//        public int MV_CC_GetImageInfo_NET(ref MV_IMAGE_BASIC_INFO pstInfo)
//        {
//            return MV_CC_GetImageInfo(handle, ref pstInfo);
//        }

//        public IntPtr MV_CC_GetTlProxy_NET()
//        {
//            return MV_CC_GetTlProxy(handle);
//        }

//        public int MV_XML_GetRootNode_NET(ref MV_XML_NODE_FEATURE pstNode)
//        {
//            return MV_XML_GetRootNode(handle, ref pstNode);
//        }

//        public int MV_XML_GetChildren_NET(ref MV_XML_NODE_FEATURE pstNode, IntPtr pstNodesList)
//        {
//            return MV_XML_GetChildren(handle, ref pstNode, pstNodesList);
//        }

//        public int MV_XML_GetChildren_NET(ref MV_XML_NODE_FEATURE pstNode, ref MV_XML_NODES_LIST pstNodesList)
//        {
//            return MV_XML_GetChildren(handle, ref pstNode, ref pstNodesList);
//        }

//        public int MV_XML_GetNodeFeature_NET(ref MV_XML_NODE_FEATURE pstNode, IntPtr pstFeature)
//        {
//            return MV_XML_GetNodeFeature(handle, ref pstNode, pstFeature);
//        }

//        public int MV_XML_UpdateNodeFeature_NET(MV_XML_InterfaceType enType, IntPtr pstFeature)
//        {
//            return MV_XML_UpdateNodeFeature(handle, enType, pstFeature);
//        }

//        public int MV_XML_RegisterUpdateCallBack_NET(cbXmlUpdatedelegate cbXmlUpdate, IntPtr pUser)
//        {
//            return MV_XML_RegisterUpdateCallBack(handle, cbXmlUpdate, pUser);
//        }

//        public int MV_CC_BayerNoiseEstimate_NET(ref MV_CC_BAYER_NOISE_ESTIMATE_PARAM pstNoiseEstimateParam)
//        {
//            return MV_CC_BayerNoiseEstimate(handle, ref pstNoiseEstimateParam);
//        }

//        public int MV_CC_BayerSpatialDenoise_NET(ref MV_CC_BAYER_SPATIAL_DENOISE_PARAM pstSpatialDenoiseParam)
//        {
//            return MV_CC_BayerSpatialDenoise(handle, ref pstSpatialDenoiseParam);
//        }

//        public int MV_CC_Display_NET(IntPtr hWnd)
//        {
//            return MV_CC_Display(handle, hWnd);
//        }

//        public int MV_CC_GetOneFrame_NET(IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO pFrameInfo)
//        {
//            return MV_CC_GetOneFrame(handle, pData, nDataSize, ref pFrameInfo);
//        }

//        public int MV_CC_GetOneFrameEx_NET(IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO_EX pFrameInfo)
//        {
//            return MV_CC_GetOneFrameEx(handle, pData, nDataSize, ref pFrameInfo);
//        }

//        public int MV_CC_SaveImage_NET(ref MV_SAVE_IMAGE_PARAM stSaveParam)
//        {
//            return MV_CC_SaveImage(ref stSaveParam);
//        }

//        public int MV_GIGE_ForceIp_NET(uint nIP)
//        {
//            return MV_GIGE_ForceIp(handle, nIP);
//        }

//        public int MV_CC_RegisterEventCallBack_NET(cbEventdelegate cbEvent, IntPtr pUser)
//        {
//            return MV_CC_RegisterEventCallBack(handle, cbEvent, pUser);
//        }

//        public int MV_CC_GetIntValue_NET(string strKey, ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetIntValue(handle, strKey, ref pstValue);
//        }

//        public int MV_CC_SetIntValue_NET(string strKey, uint nValue)
//        {
//            return MV_CC_SetIntValue(handle, strKey, nValue);
//        }

//        public int MV_CC_SetBayerCLUTParam_NET(ref MV_CC_CLUT_PARAM pstCLUTParam)
//        {
//            return MV_CC_SetBayerCLUTParam(handle, ref pstCLUTParam);
//        }

//        public int MV_CC_ImageSharpen_NET(ref MV_CC_SHARPEN_PARAM pstSharpenParam)
//        {
//            return MV_CC_ImageSharpen(handle, ref pstSharpenParam);
//        }

//        public int MV_CC_ColorCorrect_NET(ref MV_CC_COLOR_CORRECT_PARAM pstColorCorrectParam)
//        {
//            return MV_CC_ColorCorrect(handle, ref pstColorCorrectParam);
//        }

//        public int MV_CC_NoiseEstimate_NET(ref MV_CC_NOISE_ESTIMATE_PARAM pstNoiseEstimateParam)
//        {
//            return MV_CC_NoiseEstimate(handle, ref pstNoiseEstimateParam);
//        }

//        public int MV_CC_SpatialDenoise_NET(ref MV_CC_SPATIAL_DENOISE_PARAM pstSpatialDenoiseParam)
//        {
//            return MV_CC_SpatialDenoise(handle, ref pstSpatialDenoiseParam);
//        }

//        public int MV_CC_LSCCalib_NET(ref MV_CC_LSC_CALIB_PARAM pstLSCCalibParam)
//        {
//            return MV_CC_LSCCalib(handle, ref pstLSCCalibParam);
//        }

//        public int MV_CC_LSCCorrect_NET(ref MV_CC_LSC_CORRECT_PARAM pstLSCCorrectParam)
//        {
//            return MV_CC_LSCCorrect(handle, ref pstLSCCorrectParam);
//        }

//        public int MV_CC_GetWidth_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetWidth(handle, ref pstValue);
//        }

//        public int MV_CC_SetWidth_NET(uint nValue)
//        {
//            return MV_CC_SetWidth(handle, nValue);
//        }

//        public int MV_CC_GetHeight_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetHeight(handle, ref pstValue);
//        }

//        public int MV_CC_SetHeight_NET(uint nValue)
//        {
//            return MV_CC_SetHeight(handle, nValue);
//        }

//        public int MV_CC_GetAOIoffsetX_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetAOIoffsetX(handle, ref pstValue);
//        }

//        public int MV_CC_SetAOIoffsetX_NET(uint nValue)
//        {
//            return MV_CC_SetAOIoffsetX(handle, nValue);
//        }

//        public int MV_CC_GetAOIoffsetY_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetAOIoffsetY(handle, ref pstValue);
//        }

//        public int MV_CC_SetAOIoffsetY_NET(uint nValue)
//        {
//            return MV_CC_SetAOIoffsetY(handle, nValue);
//        }

//        public int MV_CC_GetAutoExposureTimeLower_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetAutoExposureTimeLower(handle, ref pstValue);
//        }

//        public int MV_CC_SetAutoExposureTimeLower_NET(uint nValue)
//        {
//            return MV_CC_SetAutoExposureTimeLower(handle, nValue);
//        }

//        public int MV_CC_GetAutoExposureTimeUpper_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetAutoExposureTimeUpper(handle, ref pstValue);
//        }

//        public int MV_CC_SetAutoExposureTimeUpper_NET(uint nValue)
//        {
//            return MV_CC_SetAutoExposureTimeUpper(handle, nValue);
//        }

//        public int MV_CC_GetBrightness_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetBrightness(handle, ref pstValue);
//        }

//        public int MV_CC_SetBrightness_NET(uint nValue)
//        {
//            return MV_CC_SetBrightness(handle, nValue);
//        }

//        public int MV_CC_GetFrameRate_NET(ref MVCC_FLOATVALUE pstValue)
//        {
//            return MV_CC_GetFrameRate(handle, ref pstValue);
//        }

//        public int MV_CC_SetFrameRate_NET(float fValue)
//        {
//            return MV_CC_SetFrameRate(handle, fValue);
//        }

//        public int MV_CC_GetGain_NET(ref MVCC_FLOATVALUE pstValue)
//        {
//            return MV_CC_GetGain(handle, ref pstValue);
//        }

//        public int MV_CC_SetGain_NET(float fValue)
//        {
//            return MV_CC_SetGain(handle, fValue);
//        }

//        public int MV_CC_GetExposureTime_NET(ref MVCC_FLOATVALUE pstValue)
//        {
//            return MV_CC_GetExposureTime(handle, ref pstValue);
//        }

//        public int MV_CC_SetExposureTime_NET(float fValue)
//        {
//            return MV_CC_SetExposureTime(handle, fValue);
//        }

//        public int MV_CC_GetPixelFormat_NET(ref MVCC_ENUMVALUE pstValue)
//        {
//            return MV_CC_GetPixelFormat(handle, ref pstValue);
//        }

//        public int MV_CC_SetPixelFormat_NET(uint nValue)
//        {
//            return MV_CC_SetPixelFormat(handle, nValue);
//        }

//        public int MV_CC_GetAcquisitionMode_NET(ref MVCC_ENUMVALUE pstValue)
//        {
//            return MV_CC_GetAcquisitionMode(handle, ref pstValue);
//        }

//        public int MV_CC_SetAcquisitionMode_NET(uint nValue)
//        {
//            return MV_CC_SetAcquisitionMode(handle, nValue);
//        }

//        public int MV_CC_GetGainMode_NET(ref MVCC_ENUMVALUE pstValue)
//        {
//            return MV_CC_GetGainMode(handle, ref pstValue);
//        }

//        public int MV_CC_SetGainMode_NET(uint nValue)
//        {
//            return MV_CC_SetGainMode(handle, nValue);
//        }

//        public int MV_CC_GetExposureAutoMode_NET(ref MVCC_ENUMVALUE pstValue)
//        {
//            return MV_CC_GetExposureAutoMode(handle, ref pstValue);
//        }

//        public int MV_CC_SetExposureAutoMode_NET(uint nValue)
//        {
//            return MV_CC_SetExposureAutoMode(handle, nValue);
//        }

//        public int MV_CC_GetTriggerMode_NET(ref MVCC_ENUMVALUE pstValue)
//        {
//            return MV_CC_GetTriggerMode(handle, ref pstValue);
//        }

//        public int MV_CC_SetTriggerMode_NET(uint nValue)
//        {
//            return MV_CC_SetTriggerMode(handle, nValue);
//        }

//        public int MV_CC_GetTriggerDelay_NET(ref MVCC_FLOATVALUE pstValue)
//        {
//            return MV_CC_GetTriggerDelay(handle, ref pstValue);
//        }

//        public int MV_CC_SetTriggerDelay_NET(float fValue)
//        {
//            return MV_CC_SetTriggerDelay(handle, fValue);
//        }

//        public int MV_CC_GetTriggerSource_NET(ref MVCC_ENUMVALUE pstValue)
//        {
//            return MV_CC_GetTriggerSource(handle, ref pstValue);
//        }

//        public int MV_CC_SetTriggerSource_NET(uint nValue)
//        {
//            return MV_CC_SetTriggerSource(handle, nValue);
//        }

//        public int MV_CC_TriggerSoftwareExecute_NET()
//        {
//            return MV_CC_TriggerSoftwareExecute(handle);
//        }

//        public int MV_CC_GetGammaSelector_NET(ref MVCC_ENUMVALUE pstValue)
//        {
//            return MV_CC_GetGammaSelector(handle, ref pstValue);
//        }

//        public int MV_CC_SetGammaSelector_NET(uint nValue)
//        {
//            return MV_CC_SetGammaSelector(handle, nValue);
//        }

//        public int MV_CC_GetGamma_NET(ref MVCC_FLOATVALUE pstValue)
//        {
//            return MV_CC_GetGamma(handle, ref pstValue);
//        }

//        public int MV_CC_SetGamma_NET(float fValue)
//        {
//            return MV_CC_SetGamma(handle, fValue);
//        }

//        public int MV_CC_GetSharpness_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetSharpness(handle, ref pstValue);
//        }

//        public int MV_CC_SetSharpness_NET(uint nValue)
//        {
//            return MV_CC_SetSharpness(handle, nValue);
//        }

//        public int MV_CC_GetHue_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetHue(handle, ref pstValue);
//        }

//        public int MV_CC_SetHue_NET(uint nValue)
//        {
//            return MV_CC_SetHue(handle, nValue);
//        }

//        public int MV_CC_GetSaturation_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetSaturation(handle, ref pstValue);
//        }

//        public int MV_CC_SetSaturation_NET(uint nValue)
//        {
//            return MV_CC_SetSaturation(handle, nValue);
//        }

//        public int MV_CC_GetBalanceWhiteAuto_NET(ref MVCC_ENUMVALUE pstValue)
//        {
//            return MV_CC_GetBalanceWhiteAuto(handle, ref pstValue);
//        }

//        public int MV_CC_SetBalanceWhiteAuto_NET(uint nValue)
//        {
//            return MV_CC_SetBalanceWhiteAuto(handle, nValue);
//        }

//        public int MV_CC_GetBalanceRatioRed_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetBalanceRatioRed(handle, ref pstValue);
//        }

//        public int MV_CC_SetBalanceRatioRed_NET(uint nValue)
//        {
//            return MV_CC_SetBalanceRatioRed(handle, nValue);
//        }

//        public int MV_CC_GetBalanceRatioGreen_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetBalanceRatioGreen(handle, ref pstValue);
//        }

//        public int MV_CC_SetBalanceRatioGreen_NET(uint nValue)
//        {
//            return MV_CC_SetBalanceRatioGreen(handle, nValue);
//        }

//        public int MV_CC_GetBalanceRatioBlue_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetBalanceRatioBlue(handle, ref pstValue);
//        }

//        public int MV_CC_SetBalanceRatioBlue_NET(uint nValue)
//        {
//            return MV_CC_SetBalanceRatioBlue(handle, nValue);
//        }

//        public int MV_CC_GetDeviceUserID_NET(ref MVCC_STRINGVALUE pstValue)
//        {
//            return MV_CC_GetDeviceUserID(handle, ref pstValue);
//        }

//        public int MV_CC_SetDeviceUserID_NET(string chValue)
//        {
//            return MV_CC_SetDeviceUserID(handle, chValue);
//        }

//        public int MV_CC_GetBurstFrameCount_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetBurstFrameCount(handle, ref pstValue);
//        }

//        public int MV_CC_SetBurstFrameCount_NET(uint nValue)
//        {
//            return MV_CC_SetBurstFrameCount(handle, nValue);
//        }

//        public int MV_CC_GetAcquisitionLineRate_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetAcquisitionLineRate(handle, ref pstValue);
//        }

//        public int MV_CC_SetAcquisitionLineRate_NET(uint nValue)
//        {
//            return MV_CC_SetAcquisitionLineRate(handle, nValue);
//        }

//        public int MV_CC_GetHeartBeatTimeout_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_CC_GetHeartBeatTimeout(handle, ref pstValue);
//        }

//        public int MV_CC_SetHeartBeatTimeout_NET(uint nValue)
//        {
//            return MV_CC_SetHeartBeatTimeout(handle, nValue);
//        }

//        public int MV_GIGE_GetGevSCPSPacketSize_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_GIGE_GetGevSCPSPacketSize(handle, ref pstValue);
//        }

//        public int MV_GIGE_SetGevSCPSPacketSize_NET(uint nValue)
//        {
//            return MV_GIGE_SetGevSCPSPacketSize(handle, nValue);
//        }

//        public int MV_GIGE_GetGevSCPD_NET(ref MVCC_INTVALUE pstValue)
//        {
//            return MV_GIGE_GetGevSCPD(handle, ref pstValue);
//        }

//        public int MV_GIGE_SetGevSCPD_NET(uint nValue)
//        {
//            return MV_GIGE_SetGevSCPD(handle, nValue);
//        }

//        public int MV_GIGE_GetGevSCDA_NET(ref uint pnIP)
//        {
//            return MV_GIGE_GetGevSCDA(handle, ref pnIP);
//        }

//        public int MV_GIGE_SetGevSCDA_NET(uint nIP)
//        {
//            return MV_GIGE_SetGevSCDA(handle, nIP);
//        }

//        public int MV_GIGE_GetGevSCSP_NET(ref uint pnPort)
//        {
//            return MV_GIGE_GetGevSCSP(handle, ref pnPort);
//        }

//        public int MV_GIGE_SetGevSCSP_NET(uint nPort)
//        {
//            return MV_GIGE_SetGevSCSP(handle, nPort);
//        }

//        public int MV_CC_RegisterImageCallBack_NET(cbOutputdelegate cbOutput, IntPtr pUser)
//        {
//            return MV_CC_RegisterImageCallBack(handle, cbOutput, pUser);
//        }

//        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
//        private static extern void OutputDebugString(string message);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_EnumInterfaces(uint nTLayerType, ref MV_INTERFACE_INFO_LIST pInterfaceInfoList);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_CreateInterface(ref IntPtr handle, ref MV_INTERFACE_INFO pInterfaceInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_CreateInterfaceByID(ref IntPtr handle, string pInterfaceID);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_OpenInterface(IntPtr handle, string pConfigFile);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_CloseInterface(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_DestroyInterface(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_Initialize();

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_Finalize();

//        [DllImport("MvCameraControl.dll")]
//        private static extern uint MV_CC_GetSDKVersion();

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_EnumerateTls();

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_EnumDevices(uint nTLayerType, ref MV_CC_DEVICE_INFO_LIST stDevList);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_EnumDevicesEx(uint nTLayerType, ref MV_CC_DEVICE_INFO_LIST stDevList, string pManufacturerName);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_EnumDevicesEx2(uint nTLayerType, ref MV_CC_DEVICE_INFO_LIST stDevList, string pManufacturerName, MV_SORT_METHOD enSortMethod);

//        [DllImport("MvCameraControl.dll")]
//        private static extern byte MV_CC_IsDeviceAccessible(ref MV_CC_DEVICE_INFO stDevInfo, uint nAccessMode);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetSDKLogPath(string pSDKLogPath);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_CreateHandle(ref IntPtr handle, ref MV_CC_DEVICE_INFO stDevInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_CreateHandleWithoutLog(ref IntPtr handle, ref MV_CC_DEVICE_INFO stDevInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_DestroyHandle(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_OpenDevice(IntPtr handle, uint nAccessMode, ushort nSwitchoverKey);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_CloseDevice(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern byte MV_CC_IsDeviceConnected(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_RegisterImageCallBackEx(IntPtr handle, cbOutputExdelegate cbOutput, IntPtr pUser);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_RegisterImageCallBackForRGB(IntPtr handle, cbOutputExdelegate cbOutput, IntPtr pUser);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_RegisterImageCallBackForBGR(IntPtr handle, cbOutputExdelegate cbOutput, IntPtr pUser);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_StartGrabbing(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_StopGrabbing(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetImageForRGB(IntPtr handle, IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO_EX pFrameInfo, int nMsec);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetImageForBGR(IntPtr handle, IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO_EX pFrameInfo, int nMsec);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetImageBuffer(IntPtr handle, ref MV_FRAME_OUT pFrame, int nMsec);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_FreeImageBuffer(IntPtr handle, ref MV_FRAME_OUT pFrame);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetOneFrameTimeout(IntPtr handle, IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO_EX pFrameInfo, int nMsec);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_ClearImageBuffer(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetValidImageNum(IntPtr handle, ref uint pnValidImageNum);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_DisplayOneFrame(IntPtr handle, ref MV_DISPLAY_FRAME_INFO pDisplayInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_DisplayOneFrameEx(IntPtr handle, IntPtr hWnd, ref MV_DISPLAY_FRAME_INFO_EX pDisplayInfoEx);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetImageNodeNum(IntPtr handle, uint nNum);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetGrabStrategy(IntPtr handle, MV_GRAB_STRATEGY enGrabStrategy);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetOutputQueueSize(IntPtr handle, uint nOutputQueueSize);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetDeviceInfo(IntPtr handle, ref MV_CC_DEVICE_INFO pstDevInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetAllMatchInfo(IntPtr handle, ref MV_ALL_MATCH_INFO pstInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetIntValueEx(IntPtr handle, string strValue, ref MVCC_INTVALUE_EX pIntValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetIntValueEx(IntPtr handle, string strValue, long nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetEnumValue(IntPtr handle, string strValue, ref MVCC_ENUMVALUE pEnumValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetEnumValue(IntPtr handle, string strValue, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetEnumEntrySymbolic(IntPtr handle, string strKey, ref MVCC_ENUMENTRY pstEnumEntry);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetEnumValueByString(IntPtr handle, string strValue, string sValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetFloatValue(IntPtr handle, string strValue, ref MVCC_FLOATVALUE pFloatValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetFloatValue(IntPtr handle, string strValue, float fValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetBoolValue(IntPtr handle, string strValue, ref bool pBoolValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBoolValue(IntPtr handle, string strValue, bool bValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetStringValue(IntPtr handle, string strKey, ref MVCC_STRINGVALUE pStringValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetStringValue(IntPtr handle, string strKey, string sValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetCommandValue(IntPtr handle, string strValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_InvalidateNodes(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_LocalUpgrade(IntPtr handle, string pFilePathName);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetUpgradeProcess(IntPtr handle, ref uint pnProcess);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_ReadMemory(IntPtr handle, IntPtr pBuffer, long nAddress, long nLength);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_WriteMemory(IntPtr handle, IntPtr pBuffer, long nAddress, long nLength);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_RegisterExceptionCallBack(IntPtr handle, cbExceptiondelegate cbException, IntPtr pUser);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_RegisterEventCallBack(IntPtr handle, cbEventdelegate cbEvent, IntPtr pUser);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_RegisterAllEventCallBack(IntPtr handle, cbEventdelegateEx cbEvent, IntPtr pUser);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_RegisterEventCallBackEx(IntPtr handle, string pEventName, cbEventdelegateEx cbEvent, IntPtr pUser);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetEnumDevTimeout(uint nMilTimeout);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_ForceIpEx(IntPtr handle, uint nIP, uint nSubNetMask, uint nDefaultGateWay);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetIpConfig(IntPtr handle, uint nType);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetNetTransMode(IntPtr handle, uint nType);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_GetNetTransInfo(IntPtr handle, ref MV_NETTRANS_INFO pstInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetDiscoveryMode(uint nMode);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetGvspTimeout(IntPtr handle, uint nMillisec);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_GetGvspTimeout(IntPtr handle, ref uint pMillisec);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetGvcpTimeout(IntPtr handle, uint nMillisec);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_GetGvcpTimeout(IntPtr handle, ref uint pMillisec);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetRetryGvcpTimes(IntPtr handle, uint nRetryGvcpTimes);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_GetRetryGvcpTimes(IntPtr handle, ref uint pRetryGvcpTimes);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetOptimalPacketSize(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetResend(IntPtr handle, uint bEnable, uint nMaxResendPercent, uint nResendTimeout);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetResendMaxRetryTimes(IntPtr handle, uint nRetryTimes);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_GetResendMaxRetryTimes(IntPtr handle, ref uint pnRetryTimes);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetResendTimeInterval(IntPtr handle, uint nMillisec);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_GetResendTimeInterval(IntPtr handle, ref uint pnMillisec);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetTransmissionType(IntPtr handle, ref MV_CC_TRANSMISSION_TYPE pstTransmissionType);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_IssueActionCommand(ref MV_ACTION_CMD_INFO pstActionCmdInfo, ref MV_ACTION_CMD_RESULT_LIST pstActionCmdResults);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_GetMulticastStatus(ref MV_CC_DEVICE_INFO pstDevInfo, ref bool pStatus);

//        [DllImport("MvCameraControl.dll", EntryPoint = "MV_CAML_SetDeviceBauderate")]
//        private static extern int MV_CAML_SetDeviceBaudrate(IntPtr handle, uint nBaudrate);

//        [DllImport("MvCameraControl.dll", EntryPoint = "MV_CAML_GetDeviceBauderate")]
//        private static extern int MV_CAML_GetDeviceBaudrate(IntPtr handle, ref uint pnCurrentBaudrate);

//        [DllImport("MvCameraControl.dll", EntryPoint = "MV_CAML_GetSupportBauderates")]
//        private static extern int MV_CAML_GetSupportBaudrates(IntPtr handle, ref uint pnBaudrateAblity);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CAML_SetGenCPTimeOut(IntPtr handle, uint nMillisec);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_USB_SetTransferSize(IntPtr handle, uint nTransferSize);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_USB_GetTransferSize(IntPtr handle, ref uint pTransferSize);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_USB_SetTransferWays(IntPtr handle, uint nTransferWays);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_USB_GetTransferWays(IntPtr handle, ref uint pTransferWays);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_USB_RegisterStreamExceptionCallBack(IntPtr handle, cbStreamException cbException, IntPtr pUser);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_USB_SetEventNodeNum(IntPtr handle, uint nEventNodeNum);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_USB_SetSyncTimeOut(IntPtr handle, uint nMills);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_USB_GetSyncTimeOut(IntPtr handle, ref uint pnMills);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_EnumInterfacesByGenTL(ref MV_GENTL_IF_INFO_LIST pstIFInfoList, string sGenTLPath);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_EnumDevicesByGenTL(ref MV_GENTL_IF_INFO stIFInfo, ref MV_GENTL_DEV_INFO_LIST pstDevList);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_UnloadGenTLLibrary(string strGenTLPath);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_CreateHandleByGenTL(ref IntPtr handle, ref MV_GENTL_DEV_INFO stDevInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_XML_GetGenICamXML(IntPtr handle, IntPtr pData, uint nDataSize, ref uint pnDataLen);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_XML_GetNodeAccessMode(IntPtr handle, string pstrName, ref MV_XML_AccessMode pAccessMode);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_XML_GetNodeInterfaceType(IntPtr handle, string pstrName, ref MV_XML_InterfaceType pInterfaceType);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_XML_GetRootNode(IntPtr handle, ref MV_XML_NODE_FEATURE pstNode);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_XML_GetChildren(IntPtr handle, ref MV_XML_NODE_FEATURE pstNode, IntPtr pstNodesList);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_XML_GetChildren(IntPtr handle, ref MV_XML_NODE_FEATURE pstNode, ref MV_XML_NODES_LIST pstNodesList);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_XML_GetNodeFeature(IntPtr handle, ref MV_XML_NODE_FEATURE pstNode, IntPtr pstFeature);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_XML_UpdateNodeFeature(IntPtr handle, MV_XML_InterfaceType enType, IntPtr pstFeature);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_XML_RegisterUpdateCallBack(IntPtr handle, cbXmlUpdatedelegate cbXmlUpdate, IntPtr pUser);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SaveImageEx3(IntPtr handle, ref MV_SAVE_IMAGE_PARAM_EX3 stSaveParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SaveImageToFileEx(IntPtr handle, ref MV_SAVE_IMG_TO_FILE_PARAM_EX pstSaveFileParamEx);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SavePointCloudData(IntPtr handle, ref MV_SAVE_POINT_CLOUD_PARAM pstPointDataParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_RotateImage(IntPtr handle, ref MV_CC_ROTATE_IMAGE_PARAM pstRotateParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_FlipImage(IntPtr handle, ref MV_CC_FLIP_IMAGE_PARAM pstFlipParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_ConvertPixelTypeEx(IntPtr handle, ref MV_CC_PIXEL_CONVERT_PARAM_EX pstCvtParamEx);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetGammaValue(IntPtr handle, MvGvspPixelType enSrcPixelType, float fGammaValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBayerCvtQuality(IntPtr handle, uint BayerCvtQuality);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBayerFilterEnable(IntPtr handle, bool bFilterEnable);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBayerGammaParam(IntPtr handle, ref MV_CC_GAMMA_PARAM pstGammaParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBayerCCMParam(IntPtr handle, ref MV_CC_CCM_PARAM pstCCMParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBayerCCMParamEx(IntPtr handle, ref MV_CC_CCM_PARAM_EX pstCCMParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_ImageContrast(IntPtr handle, ref MV_CC_CONTRAST_PARAM pstContrastParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_HB_Decode(IntPtr handle, ref MV_CC_HB_DECODE_PARAM pstDecodeParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_DrawRect(IntPtr handle, ref MVCC_RECT_INFO pRectInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_DrawCircle(IntPtr handle, ref MVCC_CIRCLE_INFO pCircleInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_DrawLines(IntPtr handle, ref MVCC_LINES_INFO pLinesInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_FeatureSave(IntPtr handle, string pFileName);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_FeatureLoad(IntPtr handle, string pFileName);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_FileAccessRead(IntPtr handle, ref MV_CC_FILE_ACCESS pstFileAccess);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_FileAccessReadEx(IntPtr handle, ref MV_CC_FILE_ACCESS_EX pstFileAccessEx);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_FileAccessWrite(IntPtr handle, ref MV_CC_FILE_ACCESS pstFileAccess);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_FileAccessWriteEx(IntPtr handle, ref MV_CC_FILE_ACCESS_EX pstFileAccessEx);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetFileAccessProgress(IntPtr handle, ref MV_CC_FILE_ACCESS_PROGRESS pstFileAccessProgress);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_StartRecord(IntPtr handle, ref MV_CC_RECORD_PARAM pstRecordParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_InputOneFrame(IntPtr handle, ref MV_CC_INPUT_FRAME_INFO pstInputFrameInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_StopRecord(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_OpenParamsGUI(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_ReconstructImage(IntPtr handle, ref MV_RECONSTRUCT_IMAGE_PARAM pstReconstructParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SaveImageEx2(IntPtr handle, ref MV_SAVE_IMAGE_PARAM_EX2 stSaveParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SaveImageToFile(IntPtr handle, ref MV_SAVE_IMG_TO_FILE_PARAM pstSaveFileParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_ConvertPixelType(IntPtr handle, ref MV_PIXEL_CONVERT_PARAM pstCvtParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetOneFrame(IntPtr handle, IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO pFrameInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetOneFrameEx(IntPtr handle, IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO_EX pFrameInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_RegisterImageCallBack(IntPtr handle, cbOutputdelegate cbOutput, IntPtr pUser);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SaveImage(ref MV_SAVE_IMAGE_PARAM stSaveParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_ForceIp(IntPtr handle, uint nIP);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_BayerNoiseEstimate(IntPtr handle, ref MV_CC_BAYER_NOISE_ESTIMATE_PARAM pstNoiseEstimateParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_BayerSpatialDenoise(IntPtr handle, ref MV_CC_BAYER_SPATIAL_DENOISE_PARAM pstSpatialDenoiseParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_Display(IntPtr handle, IntPtr hWnd);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetImageInfo(IntPtr handle, ref MV_IMAGE_BASIC_INFO pstInfo);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_GetGevSCPSPacketSize(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetGevSCPSPacketSize(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_GetGevSCPD(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetGevSCPD(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_GetGevSCDA(IntPtr handle, ref uint pnIP);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetGevSCDA(IntPtr handle, uint nIP);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_GetGevSCSP(IntPtr handle, ref uint pnPort);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_GIGE_SetGevSCSP(IntPtr handle, uint nPort);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBayerCLUTParam(IntPtr handle, ref MV_CC_CLUT_PARAM pstCLUTParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_ImageSharpen(IntPtr handle, ref MV_CC_SHARPEN_PARAM pstSharpenParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_ColorCorrect(IntPtr handle, ref MV_CC_COLOR_CORRECT_PARAM pstColorCorrectParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_NoiseEstimate(IntPtr handle, ref MV_CC_NOISE_ESTIMATE_PARAM pstNoiseEstimateParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SpatialDenoise(IntPtr handle, ref MV_CC_SPATIAL_DENOISE_PARAM pstSpatialDenoiseParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_LSCCalib(IntPtr handle, ref MV_CC_LSC_CALIB_PARAM pstLSCCalibParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_LSCCorrect(IntPtr handle, ref MV_CC_LSC_CORRECT_PARAM pstLSCCorrectParam);

//        [DllImport("MvCameraControl.dll")]
//        private static extern IntPtr MV_CC_GetTlProxy(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_WriteLog(string strLog);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetIntValue(IntPtr handle, string strValue, ref MVCC_INTVALUE pIntValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetIntValue(IntPtr handle, string strValue, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetWidth(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetWidth(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetHeight(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetHeight(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetAOIoffsetX(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetAOIoffsetX(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetAOIoffsetY(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetAOIoffsetY(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetAutoExposureTimeLower(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetAutoExposureTimeLower(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetAutoExposureTimeUpper(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetAutoExposureTimeUpper(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetBrightness(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBrightness(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetFrameRate(IntPtr handle, ref MVCC_FLOATVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetFrameRate(IntPtr handle, float fValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetGain(IntPtr handle, ref MVCC_FLOATVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetGain(IntPtr handle, float fValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetExposureTime(IntPtr handle, ref MVCC_FLOATVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetExposureTime(IntPtr handle, float fValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetPixelFormat(IntPtr handle, ref MVCC_ENUMVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetPixelFormat(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetAcquisitionMode(IntPtr handle, ref MVCC_ENUMVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetAcquisitionMode(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetGainMode(IntPtr handle, ref MVCC_ENUMVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetGainMode(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetExposureAutoMode(IntPtr handle, ref MVCC_ENUMVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetExposureAutoMode(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetTriggerMode(IntPtr handle, ref MVCC_ENUMVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetTriggerMode(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetTriggerDelay(IntPtr handle, ref MVCC_FLOATVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetTriggerDelay(IntPtr handle, float fValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetTriggerSource(IntPtr handle, ref MVCC_ENUMVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetTriggerSource(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_TriggerSoftwareExecute(IntPtr handle);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetGammaSelector(IntPtr handle, ref MVCC_ENUMVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetGammaSelector(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetGamma(IntPtr handle, ref MVCC_FLOATVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetGamma(IntPtr handle, float fValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetSharpness(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetSharpness(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetHue(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetHue(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetSaturation(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetSaturation(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetBalanceWhiteAuto(IntPtr handle, ref MVCC_ENUMVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBalanceWhiteAuto(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetBalanceRatioRed(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBalanceRatioRed(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetBalanceRatioGreen(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBalanceRatioGreen(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetBalanceRatioBlue(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBalanceRatioBlue(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetDeviceUserID(IntPtr handle, ref MVCC_STRINGVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetDeviceUserID(IntPtr handle, string chValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetBurstFrameCount(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBurstFrameCount(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetAcquisitionLineRate(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetAcquisitionLineRate(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_GetHeartBeatTimeout(IntPtr handle, ref MVCC_INTVALUE pstValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetHeartBeatTimeout(IntPtr handle, uint nValue);

//        [DllImport("MvCameraControl.dll")]
//        private static extern int MV_CC_SetBayerGammaValue(IntPtr handle, float fBayerGammaValue);
//    }
//}
