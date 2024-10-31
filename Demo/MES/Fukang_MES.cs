using BoTech;
using HB_IWatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static BoTech.AudioSystem;
using static BoTech.GTKMES;

namespace Demo.MES
{
    public class Fukang_MES_Model
    {
        public string MachinePosition { get; set; }
        public string errException;
        public string Url { get; set; }  // "http://172.28.1.191:8080/Api/";
        public string OperatorUsername { get; set; }        //0949435
        public string OperatorPassword { get; set; }          //0949435
        public string LineEncoding { get; set; }             //E07-4FT-01
        public string WorkstationCode { get; set; }             //T04-STATION61
        public int WorkstationCoding { get; set; }               //2969
        public string[] ProductSN { get; set; }
        public string CarrierSN { get; set; }
        public string HWD { get; set; }                 //令牌号
        public string ServerVersion { get; set; }
        public string FIXTURE { get; set; }             //MacMnini过站使用
        public string MachineID { get; set; }
        public string StationID { get; set; }
        public string Opid { get; set; }
        public string FixtureType { get; set; }
        public string Cavity_No { get; set; }
        public string DIRName { get; set; }
        public string DIRDepartment { get; set; }

        public string[] listCode { get; set; }
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        //>>>>>>>>>>>>>>>>>>>软件ASH1值>>>>>>>>>>>>>>>>>>>>>>>>>
        public string BZSWHashPath { get; set; }
        public string BZSWHashValue { get; set; }
        public string BZConfigHashPath { get; set; }
        public string BZConfigHashValue { get; set; }
        public string CCDParHashPath { get; set; }
        public string CCDParHashValue { get; set; }
    }
    public class Fukang_MES
    {
        public static Fukang_MES tempMes = new Fukang_MES();

        public static string Mes_response;
        public static string Mes_request;

        #region HttpPost
        //Đây là phương thức RESTful dùng Post HttpLink
        public static string HttpPost(string Url, string SendData)
        {
            string strReturnValue = "";
            string tUrl = Url;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(tUrl);
                request.Method = "POST";

                byte[] bytes = Encoding.UTF8.GetBytes(SendData);
                request.ContentType = "application/json; charset=UTF-8";//"application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;
                Stream myResponseStream = request.GetRequestStream();
                myResponseStream.Write(bytes, 0, bytes.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                strReturnValue = retString;

                Mes_request = SendData;
                //WiteLog(Mes_request);
                myStreamReader.Close();
                myResponseStream.Close();
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            catch (Exception ex)
            {
                strReturnValue = "Funtion(HttpPost) catch aError:" + ex.Message.ToString();
            }
            Mes_response = strReturnValue;
            return strReturnValue;
        }

        private static void WriteLog(string log)
        {
            string MES_log = @"D:\MES_log\";
            string filePath = MES_log + DateTime.Now.ToString("yyyyMMdd");
            string fileName = filePath + "\\MES_Hour" + DateTime.Now.ToString("HH") + ".txt";
            string str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " => " + log;
            CsvServer.Instance.WriteLine(fileName, str);

        }
        #endregion


        #region Mes Login to System
        /// <summary>
        /// MES Login function, check user
        /// </summary>
        /// <param name="uiData"></param>
        /// <returns></returns>
        public static bool Mes_Login(ref Fukang_MES_Model uiData)
        {
            try
            {
                MesLogin Lg1 = new MesLogin();
                Lg1.cmd = 2;
                Lg1.Hwd = "";
                Login_Send_SerializeData Lg_Send_SerializeData = new Login_Send_SerializeData();
                Lg_Send_SerializeData.user = uiData.OperatorUsername;
                Lg_Send_SerializeData.pwd = uiData.OperatorPassword;
                Lg1.SerializeData = JsonConvert.SerializeObject(Lg_Send_SerializeData, Formatting.None);
                string strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
                string ReturnValue = HttpPost(uiData.Url + "/gate", strSendValue);
                //AddList("登录", strSendValue, ReturnValue, uiData);
                MesLogin Lg2 = new MesLogin();
                Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                uiData.HWD = Lg2.Hwd;
                bool result = Lg2.cmd == 4 ? true : false;
                return result;
            }
            catch (Exception ex)
            {
                uiData.errException = ex.Message.ToString();
                return false;
            }
        }

        #endregion
        private class tmp
        {
            public Dictionary<object, object> OPRequestInfo = new Dictionary<object, object>();
            public Dictionary<object, object> OPResponseInfo = new Dictionary<object, object>();
        }

        #region Mes Check Ver project
        /// <summary>
        /// Check version cua chuong trinh
        /// </summary>
        /// <param name="uiData"></param>
        /// <returns></returns>
        public static bool MesCheckVer(ref Fukang_MES_Model uiData)
        {
            try
            {
                UpdataSWVersion2 Updata1 = new UpdataSWVersion2();
                tmp t = new tmp();
                UpdataSWVersion Updata2 = new UpdataSWVersion();
                Updata1.LineCode = uiData.LineEncoding;
                Updata1.SectionCode = uiData.WorkstationCode;
                Updata1.StationCode = uiData.WorkstationCoding;
                Updata1.OPCategory = "CHECK_SECTION_SOFT_VERSION";
                t.OPRequestInfo.Add("SOFT_VERSION", AudioSystem.AudioMachineMessage.SW_version.Trim());
                t.OPRequestInfo.Add("VENDOR_CODE", "BZ");
                t.OPResponseInfo = new Dictionary<object, object>();


                MesLogin Lg1 = new MesLogin();
                Lg1.cmd = 16;
                Lg1.Hwd = uiData.HWD;
                Lg1.Indicator = "QUERY_RECORD";
                string a = JsonConvert.SerializeObject(t, Formatting.None);
                string b = JsonConvert.SerializeObject(Updata1, Formatting.None);
                string c = b + a;
                c = c.Replace("}{", ",");
                c = c.Replace("\r\n", "");
                c = c.Trim();
                Lg1.SerializeData = c;
                //Lg1.SerializeData = JsonConvert.SerializeObject(Updata1, Formatting.None);


                string strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
                string ReturnValue = HttpPost(uiData.Url + "/autogate", strSendValue);
                Lg1 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg1.SerializeData);
                //AddList("CHECK_SECTION_SOFT_VERSION", strSendValue, ReturnValue, uiData);
                if (Updata2.OPResponseInfo["Result"].ToString().Contains("OK"))
                {
                    return true;
                }
                else
                {
                    uiData.errException = "检查软件版本 NG:" + Updata2.OPResponseInfo["Result"].ToString();
                    return false;
                }
            }
            catch (Exception ex)
            {
                uiData.errException = ex.Message.ToString();
                return false;
            }
        }

        #endregion

        #region Mes get SN
        public static bool MesGetSNByProductSN(ref Fukang_MES_Model uiData)
        {
            try
            {
                UpdataSWVersion2 Updata1 = new UpdataSWVersion2();
                tmp t = new tmp();
                Updata1.LineCode = uiData.LineEncoding;
                Updata1.SectionCode = uiData.WorkstationCode;
                Updata1.StationCode = uiData.WorkstationCoding;
                Updata1.OPCategory = "GET_SN_BY_SN_FIXTURE";
                t.OPRequestInfo.Add("REF_VALUE", uiData.CarrierSN);
                t.OPRequestInfo.Add("REF_TYPE", "SN_FIXTURE");
                t.OPResponseInfo = new Dictionary<object, object>();
                MesLogin Lg1 = new MesLogin();
                Lg1.cmd = 16;
                Lg1.Hwd = uiData.HWD;
                Lg1.Indicator = "QUERY_RECORD";
                string a = JsonConvert.SerializeObject(t, Formatting.None);
                string b = JsonConvert.SerializeObject(Updata1, Formatting.None);
                string c = b + a;
                c = c.Replace("}{", ",");
                c = c.Replace("\r\n", "");
                c = c.Trim();
                Lg1.SerializeData = c;
                //Lg1.SerializeData = JsonConvert.SerializeObject(Updata1, Formatting.None);
                string strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);

                string ReturnValue = HttpPost(uiData.Url + "/autogate", strSendValue);
                //AddList("GET_SN_BY_SN_FIXTURE", strSendValue, ReturnValue, uiData);
                MesLogin Lg2 = new MesLogin();
                Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                UpdataSWVersion Updata2 = new UpdataSWVersion();
                Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg2.SerializeData);
                string ReturnSN = JsonConvert.DeserializeObject<string>(Updata2.OPResponseInfo["SN"].ToString()).ToString();
                if (ReturnSN.Length > 3)
                {
                    uiData.ProductSN = ReturnSN.Split(';');
                    return true;
                }
                else
                {
                    uiData.errException = "";
                    uiData.ProductSN = null;
                    uiData.errException = Lg2.SerializeData;
                    return false;
                }

            }
            catch (Exception ex)
            {
                uiData.errException = ex.Message.ToString();
                return false;
            }
        }
        #endregion


        public static bool MesCheckSN(ref Fukang_MES_Model uiData, string sn)
        {
            try
            {
                uiData.errException = "";
                UpdataSWVersion2 Updata1 = new UpdataSWVersion2();
                tmp t = new tmp();
                MesLogin Lg1 = new MesLogin();
                UpdataSWVersion Updata2 = new UpdataSWVersion();
                MesLogin Lg2 = new MesLogin();

                Updata1.LineCode = uiData.LineEncoding;
                Updata1.SectionCode = uiData.WorkstationCode;
                Updata1.StationCode = uiData.WorkstationCoding;
                Updata1.OPCategory = "UNIT_PROCESS_CHECK";
                string strSendValue;
                string ReturnValue;

                //foreach (string sn in uiData.产品SN)
                //{
                t.OPRequestInfo.Clear();
                Updata2.OPResponseInfo.Clear();

                t.OPRequestInfo.Add("SN", sn);
                t.OPResponseInfo = new Dictionary<object, object>();
                Lg1.cmd = 16;
                Lg1.Hwd = uiData.HWD;
                Lg1.Indicator = "QUERY_RECORD";
                string a = JsonConvert.SerializeObject(t, Formatting.None);
                string b = JsonConvert.SerializeObject(Updata1, Formatting.None);
                string c = b + a;
                c = c.Replace("}{", ",");
                c = c.Replace("\r\n", "");
                c = c.Trim();
                Lg1.SerializeData = c;
                //Lg1.SerializeData = JsonConvert.SerializeObject(Updata1, Formatting.None);

                strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
                ReturnValue = HttpPost(uiData.Url + "/autogate", strSendValue);
                //AddList("UNIT_PROCESS_CHECK", strSendValue, ReturnValue, uiData);

                Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg2.SerializeData);
                if (!Updata2.OPResponseInfo["Result"].ToString().Contains("OK"))
                {
                    uiData.errException += sn + ":" + "Check NG:" + Updata2.OPResponseInfo["Result"].ToString() + "\n";
                }
                //}
                bool result = uiData.errException == "" ? true : false;
                return result;
            }
            catch (Exception ex)
            {
                uiData.errException = ex.Message.ToString();
                return false;
            }
        }

        public static bool MesGetStationMsgByStationCode(ref Fukang_MES_Model uiData)
        {
            try
            {
                uiData.errException = "";
                string strSendValue;
                string ReturnValue;

                UpdataSWVersion2 Updata1 = new UpdataSWVersion2();
                tmp t = new tmp();
                MesLogin Lg1 = new MesLogin();
                UpdataSWVersion Updata2 = new UpdataSWVersion();
                MesLogin Lg2 = new MesLogin();
                DataCheckResult UpSOpR = new DataCheckResult();

                Updata1.LineCode = uiData.LineEncoding;
                Updata1.SectionCode = uiData.WorkstationCode;
                Updata1.StationCode = uiData.WorkstationCoding;
                Updata1.OPCategory = "GET_STATIONNAME_BY_STATIONCODE";


                t.OPRequestInfo.Clear();
                Updata2.OPResponseInfo.Clear();
                t.OPRequestInfo.Add("STATIONCODE", uiData.WorkstationCoding);
                t.OPResponseInfo = new Dictionary<object, object>();

                Lg1.cmd = 16;
                Lg1.Hwd = uiData.HWD;
                Lg1.Indicator = "QUERY_RECORD";
                string a = JsonConvert.SerializeObject(t, Formatting.None);
                string b = JsonConvert.SerializeObject(Updata1, Formatting.None);
                string c = b + a;
                c = c.Replace("}{", ",");
                c = c.Replace("\r\n", "");
                c = c.Trim();
                Lg1.SerializeData = c;
                //Lg1.SerializeData = JsonConvert.SerializeObject(Updata1, Formatting.None);

                strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
                ReturnValue = HttpPost(uiData.Url + "/autogate", strSendValue);
                //AddList("GET_STATIONNAME_BY_STATIONCODE", strSendValue, ReturnValue, uiData);
                Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg2.SerializeData);

                bool result = uiData.errException == "" ? true : false;
                return result;
            }
            catch (Exception ex)
            {
                uiData.errException = ex.Message.ToString();
                return false;
            }
        }
        public static bool MesGetStationMsgBySN(ref Fukang_MES_Model uiData, string sn)    //依据SN获得当前工站工装/设备参数信息
        {
            try
            {
                uiData.errException = "";
                string strSendValue;
                string ReturnValue;

                UpdataSWVersion2 Updata1 = new UpdataSWVersion2();
                tmp t = new tmp();
                MesLogin Lg1 = new MesLogin();
                UpdataSWVersion Updata2 = new UpdataSWVersion();
                MesLogin Lg2 = new MesLogin();
                DataCheckResult UpSOpR = new DataCheckResult();

                Updata1.LineCode = uiData.LineEncoding;
                Updata1.SectionCode = uiData.WorkstationCode;
                Updata1.StationCode = uiData.WorkstationCoding;
                Updata1.OPCategory = "GET_INPUT_DATA";

                t.OPRequestInfo.Clear();
                Updata2.OPResponseInfo.Clear();
                t.OPRequestInfo.Add("SN", sn);
                t.OPResponseInfo = new Dictionary<object, object>();

                Lg1.cmd = 16;
                Lg1.Hwd = uiData.HWD;
                Lg1.Indicator = "QUERY_RECORD";
                string a = JsonConvert.SerializeObject(t, Formatting.None);
                string b = JsonConvert.SerializeObject(Updata1, Formatting.None);
                string c = b + a;
                c = c.Replace("}{", ",");
                c = c.Replace("\r\n", "");
                c = c.Trim();
                Lg1.SerializeData = c;
                //Lg1.SerializeData = JsonConvert.SerializeObject(Updata1, Formatting.None);

                strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
                ReturnValue = HttpPost(uiData.Url + "/autogate", strSendValue);
                //AddList("GET_INPUT_DATA", strSendValue, ReturnValue, uiData);
                Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg2.SerializeData);

                //Dictionary<string, string> ReturnData = JsonConvert.DeserializeObject<Dictionary<string, string>>(Updata2.OPResponseInfo["Data"].ToString());
                //if (ReturnData != null)
                //{
                //    //uiData.胶水码物料料号 = ReturnData[uiData.胶水物料号];
                //    //uiData.FIXTURE = ReturnData[uiData.FixtureType];
                //}
                //else
                //{
                //    uiData.errException += sn + ":" + Lg2.SerializeData + "\n";
                //}

                bool result = uiData.errException == "" ? true : false;
                return result;
            }
            catch (Exception ex)
            {
                uiData.errException = ex.Message.ToString();
                return false;
            }
        }

        #region Port 6：Submit the station test according to SN
        public static bool MesSNPassStation(ref Fukang_MES_Model uiData, string sn)   //依据SN，提交过站数据
        {
            try
            {
                uiData.errException = "";
                string strSendValue;
                string ReturnValue;
                UpdataSWVersion2 Updata1 = new UpdataSWVersion2();
                tmp t = new tmp();
                MesLogin Lg1 = new MesLogin();
                UpdataSWVersion Updata2 = new UpdataSWVersion();
                MesLogin Lg2 = new MesLogin();
                Updata1.LineCode = uiData.LineEncoding;
                Updata1.SectionCode = uiData.WorkstationCode;
                Updata1.StationCode = uiData.WorkstationCoding;
                Updata1.OPCategory = "UNIT_PROCESS_COMMIT";

                t.OPRequestInfo.Clear();
                Updata2.OPResponseInfo.Clear();

                //上传项
                t.OPRequestInfo.Add("SN", sn);

                t.OPResponseInfo = new Dictionary<object, object>();

                Lg1.cmd = 16;
                Lg1.Hwd = uiData.HWD;
                Lg1.Indicator = "ADD_RECORD";
                string a = JsonConvert.SerializeObject(t, Formatting.None);
                string b = JsonConvert.SerializeObject(Updata1, Formatting.None);
                string c = b + a;
                c = c.Replace("}{", ",");
                c = c.Replace("\r\n", "");
                c = c.Trim();
                Lg1.SerializeData = c;
                //Lg1.SerializeData = JsonConvert.SerializeObject(Updata1, Formatting.None);

                strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
                ReturnValue = HttpPost(uiData.Url + "/autogate", strSendValue);
                //AddList("UNIT_PROCESS_COMMIT", strSendValue, ReturnValue, uiData);

                Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg2.SerializeData);
                if (!Updata2.OPResponseInfo["Result"].ToString().Contains("OK"))
                {
                    uiData.errException += sn + ":" + "PassStation NG:" + Updata2.OPResponseInfo["Result"].ToString() + "\n";
                }

                bool result = uiData.errException == "" ? true : false;
                return result;
            }
            catch (Exception ex)
            {
                uiData.errException = ex.Message.ToString();
                return false;
            }
        }
        #endregion
    }
    public class AssyCheckData
    {
        /// <summary>
        /// 不可空  必传项 作业员工号
        /// </summary>
        public string empNo { get; set; }
        /// <summary>
        /// 不可空 不可空  不可空 
        /// </summary>
        public string terminalName { get; set; }
        /// <summary>
        /// 不可空  必传项  产品序号
        /// </summary>
        public string serial_Number { get; set; }
        /// <summary>
        /// 可空 根据当站需求而定 机台号
        /// </summary>
        public string machine { get; set; }
        /// <summary>
        /// 可空 根据当站需求而定 治具号
        /// </summary>
        public string toolingNo { get; set; }
        /// <summary>
        /// 可空 根据当站需求而定 批次物料
        /// </summary>
        public string lotNo { get; set; }
        /// <summary>
        /// 可空  根据当站需求而定 材料序号
        /// </summary>
        public string kpsn { get; set; }
        /// <summary>
        /// 可空 根据当站需求而定 卷料号
        /// </summary>
        public string reelNo { get; set; }
        /// <summary>
        /// 可空  首站、投入站必传工单号；无特殊情况下，其他站点可传空 工单号
        /// </summary>
        public string workOrder { get; set; }

    }
}
