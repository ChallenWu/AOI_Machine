using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.InteropServices;
namespace Demo
{
    public enum SoftElemType
    {
        ////AM600
        //ELEM_QX = 0,     //QX元件
        //ELEM_MW = 1,     //MW元件
        //ELEM_X = 2,		 //X元件(对应QX200~QX300)
        //ELEM_Y = 3,		 //Y元件(对应QX300~QX400)

        ////H3U
        //REGI_H3U_Y = 0x20,       //Y元件的定义	
        //REGI_H3U_X = 0x21,		//X元件的定义							
        //REGI_H3U_S = 0x22,		//S元件的定义				
        //REGI_H3U_M = 0x23,		//M元件的定义							
        //REGI_H3U_TB = 0x24,		//T位元件的定义				
        //REGI_H3U_TW = 0x25,		//T字元件的定义				
        //REGI_H3U_CB = 0x26,		//C位元件的定义				
        //REGI_H3U_CW = 0x27,		//C字元件的定义				
        //REGI_H3U_DW = 0x28,		//D字元件的定义
        //REGI_H3U_CW2 = 0x29,	    //C双字元件的定义
        //REGI_H3U_SM = 0x2a,		//SM
        //REGI_H3U_SD = 0x2b,		//
        //REGI_H3U_R = 0x2c,		//

        //H5u
        REGI_H5U_Y = 0x30,       //Y元件的定义	
        REGI_H5U_X = 0x31,		//X元件的定义							
        REGI_H5U_S = 0x32,		//S元件的定义				
        REGI_H5U_M = 0x33,		//M元件的定义	
        REGI_H5U_B = 0x34,       //B元件的定义
        REGI_H5U_D = 0x35,       //D字元件的定义
        REGI_H5U_R = 0x36,       //R字元件的定义

    }

    public class ModbusApiH5U
    {
        #region 标准库
        [DllImport("StandardModbusApi.dll", EntryPoint = "Init_ETH_String", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Init_ETH_String(string sIpAddr, int nNetId = 0, int IpPort = 502);

        [DllImport("StandardModbusApi.dll", EntryPoint = "Exit_ETH", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Exit_ETH(int nNetId = 0);

        #region H3U
        //[DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int H3u_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        //[DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int H3u_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        //[DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Read_Soft_Elem_Float", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int H3u_Read_Soft_Elem_Float(SoftElemType eType, int nStartAddr, int nCount, float[] pValue, int nNetId = 0); 

        //[DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int Am600_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        //[DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int Am600_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);
        #endregion

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);
        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);


        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Read_Soft_Elem_Int16", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Read_Soft_Elem_Int16(SoftElemType eType, int nStartAddr, int nCount, short[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Write_Soft_Elem_Int16", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Write_Soft_Elem_Int16(SoftElemType eType, int nStartAddr, int nCount, short[] pValue, int nNetId = 0);


        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Read_Soft_Elem_Int32", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Read_Soft_Elem_Int32(SoftElemType eType, int nStartAddr, int nCount, int[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Write_Soft_Elem_Int32", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Write_Soft_Elem_Int32(SoftElemType eType, int nStartAddr, int nCount, int[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Read_Soft_Elem_Float", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Read_Soft_Elem_Float(SoftElemType eType, int nStartAddr, int nCount, float[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Write_Soft_Elem_Float", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Write_Soft_Elem_Float(SoftElemType eType, int nStartAddr, int nCount, float[] pValue, int nNetId = 0);

        #endregion

        private static bool _isConnected = false;
        private static int netId = 0;
        private int port = 502;
        private static object readLock = new object();
        private static object writeLock = new object();
        public static bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
            }
        }


        /// <summary>
        /// PLC建立连接
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public bool PLCConnect(string IP, int nNetId)
        {
            port = 502;
            netId = nNetId;
            bool result = Init_ETH_String(IP, netId, port);

            if (result == true)
            {
                _isConnected = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// PLC断开连接
        /// </summary>
        /// <param name="netId">PLC编号</param>
        /// <returns></returns>
        public bool DisConnect(int netId)
        {
            bool rtn = false;
            rtn = Exit_ETH(netId);

            if (rtn == true)
            {
                _isConnected = false;
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 读寄存器
        /// </summary>
        /// <param name="softElemType"></param>
        /// <param name="startAddr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AutoStudio.Core.OperateResult<bool[]> ReadValueBit(SoftElemType softElemType, int startAddr, int count)
        {
            lock (readLock)
            {
                try
                {
                    byte[] pValue = new byte[count]; //缓冲区
                    int nRet = H5u_Read_Soft_Elem(softElemType, startAddr, count, pValue, netId);
                    bool[] value = new bool[count];
                    for (int i = 0; i < pValue.Length; i++)
                    {
                        value[i] = pValue[i] == 1;
                    }
                    return new AutoStudio.Core.OperateResult<bool[]> { IsSuccess = nRet == 1, Result = value };
                }
                catch (Exception ex)
                {
                    return new AutoStudio.Core.OperateResult<bool[]> { IsSuccess = false, Message = $"PLC寄存器{softElemType}读取异常！{ex.Message}" };
                }
            }
        }
        /// <summary>
        /// 读寄存器
        /// </summary>
        /// <param name="softElemType"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public AutoStudio.Core.OperateResult<bool> ReadValueBit(SoftElemType softElemType, int address)
        {
            lock (readLock)
            {
                try
                {
                    byte[] pValue = new byte[1]; //缓冲区
                    int nRet = H5u_Read_Soft_Elem(softElemType, address, 1, pValue, netId);
                    return new AutoStudio.Core.OperateResult<bool> { IsSuccess = nRet == 1, Result = pValue[0] == 1 };
                }
                catch (Exception ex)
                {
                    return new AutoStudio.Core.OperateResult<bool> { IsSuccess = false, Message = $"PLC寄存器{softElemType}读取异常！{ex.Message}" };
                }
            }
        }

        /// <summary>
        /// 写寄存器
        /// </summary>
        /// <param name="softElemType">寄存器类型</param>
        /// <param name="startAddr">寄存器地址</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public AutoStudio.Core.OperateResult WriteValueBit(SoftElemType softElemType, int startAddr, params bool[] value)
        {
            lock (writeLock)
            {
                try
                {
                    byte[] pValue = new byte[value.Length];  //缓冲区
                    for (int i = 0; i < value.Length; i++)
                    {
                        pValue[i] = (byte)(value[i] ? 1 : 0);
                    }
                    //调用api写数据
                    int nRet = H5u_Write_Soft_Elem(softElemType, startAddr, value.Length, pValue, netId);

                    return new AutoStudio.Core.OperateResult { IsSuccess = nRet == 1 };
                }
                catch (Exception ex)
                {
                    return new AutoStudio.Core.OperateResult { IsSuccess = false, Message = $"PLC寄存器{softElemType}写入异常！{ex.Message}" };
                }
            }
        }

        /// <summary>
        /// 读取Int16
        /// </summary>
        /// <param name="softElemType"></param>
        /// <param name="startAddr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AutoStudio.Core.OperateResult<short[]> ReadValueInt16(SoftElemType softElemType, int startAddr, int count)
        {
            lock (readLock)
            {
                try
                {
                    short[] pValue = new short[count]; //缓冲区
                    int nRet = H5u_Read_Soft_Elem_Int16(softElemType, startAddr, count, pValue, netId);
                    return new AutoStudio.Core.OperateResult<short[]> { IsSuccess = nRet == 1, Result = pValue };
                }
                catch (Exception ex)
                {
                    return new AutoStudio.Core.OperateResult<short[]> { IsSuccess = false, Message = $"PLC寄存器{softElemType}读取异常！{ex.Message}" };
                }
            }
        }

        /// <summary>
        /// 读取Int16
        /// </summary>
        /// <param name="softElemType"></param>
        /// <param name="startAddr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AutoStudio.Core.OperateResult<short> ReadValueInt16(SoftElemType softElemType, int startAddr)
        {
            lock (readLock)
            {
                try
                {
                    short[] pValue = new short[1]; //缓冲区
                    int nRet = H5u_Read_Soft_Elem_Int16(softElemType, startAddr, 1, pValue, netId);
                    return new AutoStudio.Core.OperateResult<short> { IsSuccess = nRet == 1, Result = pValue[0] };
                }
                catch (Exception ex)
                {
                    return new AutoStudio.Core.OperateResult<short> { IsSuccess = false, Message = $"PLC寄存器{softElemType}读取异常！{ex.Message}" };
                }
            }
        }
        /// <summary>
        /// 写入Int16
        /// </summary>
        /// <param name="softElemType">寄存器类型</param>
        /// <param name="startAddr">寄存器地址</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AutoStudio.Core.OperateResult WriteValueInt16(SoftElemType softElemType, int startAddr, params short[] value)
        {
            lock (writeLock)
            {
                try
                {
                    //调用api写数据
                    int nRet = H5u_Write_Soft_Elem_Int16(softElemType, startAddr, value.Length, value, netId);
                    return new AutoStudio.Core.OperateResult { IsSuccess = nRet == 1 };
                }
                catch (Exception ex)
                {
                    return new AutoStudio.Core.OperateResult { IsSuccess = false, Message = $"PLC寄存器{softElemType}写入异常！{ex.Message}" };
                }
            }
        }

        /// <summary>
        /// 读取Int32
        /// </summary>
        /// <param name="softElemType"></param>
        /// <param name="startAddr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AutoStudio.Core.OperateResult<int[]> ReadValueInt32(SoftElemType softElemType, int startAddr, int count)
        {
            lock (readLock)
            {
                try
                {
                    int[] pValue = new int[count]; //缓冲区
                    int nRet = H5u_Read_Soft_Elem_Int32(softElemType, startAddr, count, pValue, netId);
                    return new AutoStudio.Core.OperateResult<int[]> { IsSuccess = nRet == 1, Result = pValue };
                }
                catch (Exception ex)
                {
                    return new AutoStudio.Core.OperateResult<int[]> { IsSuccess = false, Message = $"PLC寄存器{softElemType}读取异常！{ex.Message}" };
                }
            }
        }

        /// <summary>
        /// 写入Int32
        /// </summary>
        /// <param name="softElemType">寄存器类型</param>
        /// <param name="startAddr">寄存器地址</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public AutoStudio.Core.OperateResult WriteValueInt32(SoftElemType softElemType, int startAddr, params int[] value)
        {
            lock (writeLock)
            {
                try
                {
                    //byte[] pValue = new byte[value.Length * 4];
                    //for (int i = 0; i < value.Length; i++)
                    //{
                    //    pValue
                    //}
                    ////把要写的数据存入缓冲区，备写
                    //pValue[0] = (byte)(nValue % 256);
                    //pValue[1] = (byte)(nValue / 256);


                    //H5u_Write_Soft_Elem(softElemType, startAddr, value.Length, pValue, netId);
                    int nRet = H5u_Write_Soft_Elem_Int32(softElemType, startAddr, value.Length, value, netId);
                    return new AutoStudio.Core.OperateResult { IsSuccess = nRet == 1 };
                }
                catch (Exception ex)
                {
                    return new AutoStudio.Core.OperateResult { IsSuccess = false, Message = $"PLC寄存器{softElemType}写入异常！{ex.Message}" };
                }
            }
        }

        /// <summary>
        /// 读取float
        /// </summary>
        /// <param name="softElemType"></param>
        /// <param name="startAddr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AutoStudio.Core.OperateResult<float[]> ReadValuefloat(SoftElemType softElemType, int startAddr, int count)
        {
            lock (readLock)
            {
                try
                {
                    float[] pValue = new float[count]; //缓冲区
                    int nRet = H5u_Read_Soft_Elem_Float(softElemType, startAddr, count, pValue, netId);
                    return new AutoStudio.Core.OperateResult<float[]> { IsSuccess = nRet == 1, Result = pValue };
                }
                catch (Exception ex)
                {
                    return new AutoStudio.Core.OperateResult<float[]> { IsSuccess = false, Message = $"PLC寄存器{softElemType}读取异常！{ex.Message}" };
                }
            }
        }

        /// <summary>
        /// 写入Int32
        /// </summary>
        /// <param name="softElemType">寄存器类型</param>
        /// <param name="startAddr">寄存器地址</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public AutoStudio.Core.OperateResult WriteValuefloat(SoftElemType softElemType, int startAddr, params float[] value)
        {
            lock (writeLock)
            {
                try
                {
                    int nRet = H5u_Write_Soft_Elem_Float(softElemType, startAddr, value.Length, value, netId);
                    return new AutoStudio.Core.OperateResult { IsSuccess = nRet == 1 };
                }
                catch (Exception ex)
                {
                    return new AutoStudio.Core.OperateResult { IsSuccess = false, Message = $"PLC寄存器{softElemType}写入异常！{ex.Message}" };
                }
            }
        }
    }
}
