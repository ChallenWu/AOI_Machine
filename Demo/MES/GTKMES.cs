using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using XStation;
//using CoreFunction;
//using static CoreFunction.mFunction;
//using static FilePath.mFilePath;
using System;
using System.IO;
namespace BoTech
{
    public class GTK_MESData_Model
    {
        
        public string 机械手工位 { get; set; }
        public string errException;
        public string Url { get; set; }  // "http://172.28.1.191:8080/Api/";
        public string 作业员用户名 { get; set; }        //0949435
        public string 作业员密码 { get; set; }          //0949435
        public string 线体编码 { get; set; }             //E07-4FT-01
        public string 工站编码 { get; set; }             //T04-STATION61
        public int 工位编码 { get; set; }               //2969
        public string[] 产品SN { get; set; }
        public string 载具码 { get; set; }
        public string HWD { get; set; }                 //令牌号
        public string 服务端版本 { get; set; }
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
    class GTKMES
    {
        #region 变量
        public static GTK_MESData_Model TempGtkMesData = new GTK_MESData_Model() { 机械手工位 = "扫码站临时存储数据。" };//仅用于扫码站临时存储数据用

        #endregion

        public static string Mes_response;
        public static string Mes_request;

        public static bool ReadUiXmlData()
        {
            try
            {
                //Read file xml
                //string filePath = FilePath.mFilePath.BZ_NewPar + "New_Config\\MesPar_GTK.xml";
                //mFunction.ReadXml(filePath, ref GTKMES.TempGtkMesData);
                //GTKMES.TempGtkMesData.机械手工位 = "扫码站临时存储数据。";
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //Ghi lai LOG MES
        #region 记录数据
        public static bool AddList(string funcName, string strSendValue, string ReturnValue, GTK_MESData_Model objMesData)
        {

            //窗体加载.WriteLog("MesComunication", funcName + "发:" + strSendValue);

            //窗体加载.WriteLog("MesComunication", funcName + "收:" + ReturnValue);
             
            return true;
        }
        #endregion    
        #region HttpPost
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
        #endregion
        #region Messenger login to get token number
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiData">从登陆框中获取的数据</param>
        /// <param name="listBox1">传入listbox控件显示相关数据（可不填）</param>
        /// <param name="tbox_HWD">传入textbox控件显示相关数据（可不填）</param>
        public static bool Mes_Login(ref GTK_MESData_Model uiData)
        {
            try
            {
                MesLogin Lg1 = new MesLogin();
                Lg1.cmd = 2;
                Lg1.Hwd = "";
                Login_Send_SerializeData Lg_Send_SerializeData = new Login_Send_SerializeData();
                Lg_Send_SerializeData.user = uiData.作业员用户名;
                Lg_Send_SerializeData.pwd = uiData.作业员密码;
                Lg1.SerializeData = JsonConvert.SerializeObject(Lg_Send_SerializeData, Formatting.None);
                string strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
                string ReturnValue = HttpPost(uiData.Url + "/gate", strSendValue);
                AddList("登录", strSendValue, ReturnValue, uiData);
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
        #region  Bind workstation_determine whether the account has workstation permissions  3
        public static bool MesBandStaion(ref GTK_MESData_Model uiData)
        {
            try
            {
                UpdataSWVersion10 Updata1 = new UpdataSWVersion10();
                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
                Updata1.ServerVersion = uiData.服务端版本;


                MesLogin Lg1 = new MesLogin();
                Lg1.cmd = 10;
                Lg1.Hwd = uiData.HWD;
                Lg1.Indicator = null;
                Lg1.SerializeData = JsonConvert.SerializeObject(Updata1, Formatting.None);

                string strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
                //"http://172.28.1.191:8080/Api/";
                //Send phương thức POST thông qua Http/AI
                string ReturnValue = HttpPost(uiData.Url + "gate", strSendValue);

                AddList("3绑定工位", strSendValue, ReturnValue, uiData);
                MesLogin Lg2 = new MesLogin();
                Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                bool result = Lg2.cmd == 11 ? true : false;
                return result;
            }
            catch (Exception ex)
            {
                uiData.errException = ex.Message.ToString();
                return false;
            }
        }
        #endregion

        #region 接口16：Mes检查版本
        public static bool MesCheckVer(ref GTK_MESData_Model uiData)
        {
            try
            {
                UpdataSWVersion2 Updata1 = new UpdataSWVersion2();
                tmp t = new tmp();
                UpdataSWVersion Updata2 = new UpdataSWVersion();
                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
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
                AddList("CHECK_SECTION_SOFT_VERSION", strSendValue, ReturnValue, uiData);
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

        #region 接口61：根据工单生成SN
        //public static bool MesGetSNFromWO(ref GTK_MESData_Model uiData)
        //{
        //    try
        //    {
        //        UpdataSWVersion2 Updata1 = new UpdataSWVersion2();
        //        tmp t = new tmp();
        //        UpdataSWVersion Updata2 = new UpdataSWVersion();
        //        Updata1.LineCode = uiData.线体编码;
        //        Updata1.SectionCode = uiData.工站编码;
        //        Updata1.StationCode = uiData.工位编码;
        //        Updata1.OPCategory = "GENERATE_SN_BY_WO";
        //        t.OPRequestInfo.Add("WO_NO", AudioSystem.AudioMachineMessage.MES.workOrder);
        //        t.OPResponseInfo = new Dictionary<object, object>();


        //        MesLogin Lg1 = new MesLogin();
        //        Lg1.cmd = 16;
        //        Lg1.Hwd = uiData.HWD;
        //        Lg1.Indicator = "ADD_RECORD";
        //        string a = JsonConvert.SerializeObject(t, Formatting.None);
        //        string b = JsonConvert.SerializeObject(Updata1, Formatting.None);
        //        string c = b + a;
        //        c = c.Replace("}{", ",");
        //        c = c.Replace("\r\n", "");
        //        c = c.Trim();
        //        Lg1.SerializeData = c;
        //        //Lg1.SerializeData = JsonConvert.SerializeObject(Updata1, Formatting.None);


        //        string strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
        //        string ReturnValue = HttpPost(uiData.Url + "/autogate", strSendValue);
        //        Lg1 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
        //        Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg1.SerializeData);
        //        AddList("GENERATE_SN_BY_WO", strSendValue, ReturnValue, uiData);
        //        if (Updata2.OPResponseInfo["Result"].ToString().Contains("OK"))
        //        {
        //            AudioSystem.AudioMachineMessage.MES.SN_from_WO = Updata2.OPResponseInfo["SN"].ToString();
        //            //XmlSerializeHelper.ClassToXml(FilePath.mFilePath.BZ_NewPar + "MachineMessage.xml", AudioSystem.AudioMachineMessage);
        //            return true;
        //        }
        //        else
        //        {
        //            uiData.errException = "GENERATE_SN_BY_WO NG:" + Updata2.OPResponseInfo["Result"].ToString();
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        uiData.errException = ex.Message.ToString();
        //        return false;
        //    }
        //}
        #endregion


        #region 接口1：由载具获取产品SN  OK  4
        /// <summary>
        /// 由载具获取产品SN
        /// </summary>
        ///接口1: 依据载具码获得产品SN信息
        /// <param name="uiData"></param>
        /// <param name="listBox1">listbox控件，可不填</param>
        /// <param name="tbox_PruductSN">文本控件可不填</param>
        public static bool MesGetSNByProductSN(ref GTK_MESData_Model uiData)
        {
            try
            {
                UpdataSWVersion2 Updata1 = new UpdataSWVersion2();
                tmp t = new tmp();
                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
                Updata1.OPCategory = "GET_SN_BY_SN_FIXTURE";
                t.OPRequestInfo.Add("REF_VALUE", uiData.载具码);
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
                AddList("GET_SN_BY_SN_FIXTURE", strSendValue, ReturnValue, uiData);
                MesLogin Lg2 = new MesLogin();
                Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                UpdataSWVersion Updata2 = new UpdataSWVersion();
                Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg2.SerializeData);
                string ReturnSN = JsonConvert.DeserializeObject<string>(Updata2.OPResponseInfo["SN"].ToString()).ToString();
                if (ReturnSN.Length > 3)
                {
                    uiData.产品SN = ReturnSN.Split(';');
                    return true;
                }
                else
                {
                    uiData.errException = "";
                    uiData.产品SN = null;
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
        /// <summary>
        /// GET_SNLIST_BY_FIXTURESN
        /// 接口12: 根据载具码获得产品SN（一带多）
        /// </summary>
        /// <param name="uiData"></param>
        //public static bool MesGetSnListByFixtureSN(ref GTK_MESData_Model uiData)
        //{
        //    try
        //    {
        //        UpdataSWVersion Updata1 = new UpdataSWVersion();
        //        Updata1.LineCode = uiData.线体编码;
        //        Updata1.SectionCode = uiData.工站编码;
        //        Updata1.StationCode = uiData.工位编码;
        //        Updata1.OPCategory = "GET_SNLIST_BY_FIXTURESN";

        //        Updata1.OPRequestInfo.Add("REF_VALUE", uiData.载具码);
        //        Updata1.OPRequestInfo.Add("REF_TYPE", "SN_FIXTURE");

        //        Updata1.OPResponseInfo = new Dictionary<object, object>();
        //        MesLogin Lg1 = new MesLogin();
        //        Lg1.cmd = 16;
        //        Lg1.Hwd = uiData.HWD;
        //        Lg1.Indicator = "QUERY_RECORD";
        //        Lg1.SerializeData = JsonConvert.SerializeObject(Updata1, Formatting.None);

        //        string strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
        //        string ReturnValue = HttpPost(uiData.Url + "autogate", strSendValue);

        //        AddList("4获取SN", strSendValue, ReturnValue, uiData);
        //        MesLogin Lg2 = new MesLogin();
        //        Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);

        //        UpdataSWVersion Updata2 = new UpdataSWVersion();
        //        Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg2.SerializeData);
        //        string ReturnSN = Updata2.OPResponseInfo["SNLIST"].ToString();

        //        if (ReturnSN.Length > 3)
        //        {
        //            uiData.产品SN = String.Split(ReturnSN, ";");
        //            return true;
        //        }
        //        else
        //        {
        //            uiData.errException = "";
        //            uiData.产品SN = null;
        //            uiData.errException = Lg2.SerializeData;
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        uiData.errException = ex.Message.ToString();
        //        return false;
        //    }
        //}
        /// <summary>
        /// GET_RAWSNLIST_BY_FIXTURESN
        /// 接口18: 根据载具码获得字条码SN（一个工装码绑定2个产品）
        /// </summary>
        /// <param name="uiData"></param>
        public static bool MesGetRawSnListByFixtureSN(ref GTK_MESData_Model uiData)
        {
            try
            {
                UpdataSWVersion Updata1 = new UpdataSWVersion();
                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
                Updata1.OPCategory = "GET_RAWSNLIST_BY_FIXTURESN";

                Updata1.OPRequestInfo.Add("REF_VALUE", uiData.载具码);
                Updata1.OPRequestInfo.Add("REF_TYPE", "SN_FIXTURE");

                Updata1.OPResponseInfo = new Dictionary<object, object>();


                MesLogin Lg1 = new MesLogin();
                Lg1.cmd = 16;
                Lg1.Hwd = uiData.HWD;
                Lg1.Indicator = "QUERY_RECORD";
                Lg1.SerializeData = JsonConvert.SerializeObject(Updata1, Formatting.None);

                string strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
                string ReturnValue = HttpPost(uiData.Url + "autogate", strSendValue);
                MesLogin Lg2 = new MesLogin();
                Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);

                UpdataSWVersion18 Updata2 = new UpdataSWVersion18();
                Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion18>(Lg2.SerializeData);
                Updata_Send_OPResponseInfo UpSOpR = new Updata_Send_OPResponseInfo();
                UpSOpR = JsonConvert.DeserializeObject<Updata_Send_OPResponseInfo>(Updata2.OPResponseInfo);

                if (UpSOpR.Result == "OK")
                {
                    if (UpSOpR.Data["FIXTRUE_SN_L"].ToString() != " " && UpSOpR.Data["FIXTRUE_SN_R"].ToString() != "")
                    {
                        uiData.产品SN[1] = UpSOpR.Data["FIXTRUE_SN_L"].ToString() + ";" + UpSOpR.Data["FIXTRUE_SN_R"].ToString();
                    }
                    return true;
                }
                else
                {
                    uiData.errException = "";
                    uiData.产品SN = null;
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
        private class tmp{
               public Dictionary<object, object> OPRequestInfo = new Dictionary<object, object>();
               public Dictionary<object, object> OPResponseInfo = new Dictionary<object, object>();
        }
        #region 接口3：检查SN路由测试  OK5
        /// <summary>
        /// 接口3: 依据SN检查路由，确认产品是否允许过站
        /// </summary>
        /// <param name="uiData"></param>
        public static bool MesCheckSN(ref GTK_MESData_Model uiData, string sn)
        {
            try
            {
                uiData.errException = "";
                UpdataSWVersion2 Updata1 = new UpdataSWVersion2();
                tmp t = new tmp();
                MesLogin Lg1 = new MesLogin();
                UpdataSWVersion Updata2 = new UpdataSWVersion();
                MesLogin Lg2 = new MesLogin();

                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
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
                AddList("UNIT_PROCESS_CHECK", strSendValue, ReturnValue, uiData);

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
        #endregion

        #region  接口4：上传参数信息测试 0K 10
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiData"></param>
        public static bool MesUpdataPar(ref GTK_MESData_Model uiData, string sn, int cavity)    //接口4,：依据SN，上传设备参数信息
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
                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
                Updata1.OPCategory = "UPLOAD_INFOS";

                
                t.OPRequestInfo.Clear();
                Updata2.OPResponseInfo.Clear();
                //************************************************************************************************************
                t.OPRequestInfo.Add("SN", sn);
                //Updata1.OPRequestInfo.Add("Air_Pressure", uiData.GluePressure);
                t.OPRequestInfo.Add("Cavity_No", cavity);


                if (AudioSystem.AudioMachineMessage.Station == AudioSystem.MachineName_BE010_1)
                {
                    t.OPRequestInfo.Add("Air_pressure", "10#10#10");
                }
                else if (AudioSystem.AudioMachineMessage.Station == AudioSystem.MachineName_TI040_1)
                {
                    t.OPRequestInfo.Add("Air_pressure", "10#10#10");
                }
                    

                //************************************************************************************************************
                t.OPResponseInfo = new Dictionary<object, object>();

                Lg1.cmd = 16;
                Lg1.Hwd = uiData.HWD;
                Lg1.Indicator = "ADD_ATTR";
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
                AddList("UPLOAD_INFOS", strSendValue, ReturnValue, uiData);
                Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg2.SerializeData);
                    //if (!Updata2.OPResponseInfo["Result"].ToString().Contains("OK"))
                    //{
                    //    uiData.errException += sn + ":" + "UpdataPar NG:" + Updata2.OPResponseInfo["Result"].ToString() + "\n";
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
        #endregion

        #region 接口5：根据SN上传BOM测试 ok 8.拿胶水去add bom  9.拿uc去add bom
        public static bool MesUpdataBOM(ref GTK_MESData_Model uiData, string sn, string raw_sn)
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
                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
                Updata1.OPCategory = "ADD_BOM_DATA";

                t.OPRequestInfo.Clear();
                Updata2.OPResponseInfo.Clear();
                t.OPRequestInfo.Add("SN", sn);
                t.OPRequestInfo.Add("RAW_SN", raw_sn);
                //t.OPRequestInfo.Add("RAW_SN2", raw_sn2);
                
                t.OPResponseInfo = new Dictionary<object, object>();

                Lg1.cmd = 16;
                Lg1.Hwd = uiData.HWD;
                Lg1.Indicator = "ADD_ATTR";
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
                AddList("ADD_BOM_DATA", strSendValue, ReturnValue, uiData);
                Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg2.SerializeData);
                if (!Updata2.OPResponseInfo["Result"].ToString().Contains("OK"))
                {
                    uiData.errException += sn + ":" + "BOM NG:" + Updata2.OPResponseInfo["Result"].ToString() + "\n";
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
        //接口：GET_SNLIST_BY_UCSN依据载具码获得穴位对应的产品SN信息
        public static bool MesGET_SNLIST_BY_UCSN(ref GTK_MESData_Model uiData, string sn)
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

                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
                Updata1.OPCategory = "GET_SNLIST_BY_UCSN";

                t.OPRequestInfo.Clear();
                Updata2.OPResponseInfo.Clear();
                sn = sn.Replace("\r","");
                sn = sn.Replace("\n","");
                t.OPRequestInfo.Add("UCSN", sn);
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
                
                AddList("GET_SNLIST_BY_UCSN", strSendValue, ReturnValue, uiData);
                if(ReturnValue.Length >0)
                {
                    ReturnValue = ReturnValue.Replace("=", ":");
                    ReturnValue = ReturnValue.Replace(".", "");
                    string[] tmp = ReturnValue.Split('\\');

                    string value1 = tmp[tmp.Length - 2];
                    value1 = value1.Replace("\"", "");
                    uiData.listCode = new string[4];

                    string[] tnp1 = value1.Split(',');
                    for (int i = 0; i < 4; i++)
                    {
                        uiData.listCode[i] = tnp1[i].Split(':')[1];
                        //uiData.产品SN[i] = tnp1[i].Split(':')[1];
                    }

                }


                // = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);

                //string tmp = Lg2.SerializeData;
                //Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg2.SerializeData);
                //DataCheckResult ReturnData = JsonConvert.DeserializeObject<DataCheckResult>(Updata2.OPResponseInfo.ToString());
                //Dictionary<string, string> ReturnData = JsonConvert.DeserializeObject<Dictionary<string, string>>(tmp.ToString());

                //string tmp = JsonConvert.DeserializeObject<string>(Updata2.OPResponseInfo["RetMsg"].ToString());
                //if (ReturnData != null)
                //{
                //    //uiData.胶水码物料料号 = ReturnData[uiData.胶水物料号];
                //    //uiData.FIXTURE = ReturnData[uiData.FixtureType];
                //    uiData.产品SN[0] = ReturnData["1"];
                //    uiData.产品SN[1] = ReturnData["2"];
                //    uiData.产品SN[2] = ReturnData["3"];
                //    uiData.产品SN[3] = ReturnData["4"];
                //    Console.WriteLine($"{ReturnData}");
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
        

        //接口21: 根据SN,工站清除临时上料数据的接口
        public static bool MesClearInputData(ref GTK_MESData_Model uiData, string sn)
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

                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
                Updata1.OPCategory = "CLEAR_SN_INPUT_TMPDATA";

                t.OPRequestInfo.Clear();
                Updata2.OPResponseInfo.Clear();
                t.OPRequestInfo.Add("SN", sn);
                t.OPRequestInfo.Add("CLEAR_GROUP", "CLEAR_GROUP1");
                t.OPResponseInfo = new Dictionary<object, object>();

                Lg1.cmd = 16;
                Lg1.Hwd = uiData.HWD;
                Lg1.Indicator = "ADD_ATTR";
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
                AddList("CLEAR_SN_INPUT_TMPDATA", strSendValue, ReturnValue, uiData);
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
        //接口23:退还子物料接口
        public static bool MesInputRawReturn(ref GTK_MESData_Model uiData, string sn)
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

                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
                Updata1.OPCategory = "INPUT_RAW_RETURN";

                t.OPRequestInfo.Clear();
                Updata2.OPResponseInfo.Clear();
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
                AddList("INPUT_RAW_RETURN", strSendValue, ReturnValue, uiData);
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
        #region 接口6：根据SN,提交过站测试  
        public static bool MesSNPassStation(ref GTK_MESData_Model uiData, string sn)   //依据SN，提交过站数据
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
                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
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
                AddList("UNIT_PROCESS_COMMIT", strSendValue, ReturnValue, uiData);

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

        #region 接口11：根据SN获取当前工站信息测试   Ok11 检查物料参数是否齐全
        /// <summary>
        /// 接口11: 依据SN获得当前工站工装/设备参数信息
        /// </summary>
        /// <param name="uiData"></param>
        public static bool MesGetStationMsgBySN(ref GTK_MESData_Model uiData, string sn)    //依据SN获得当前工站工装/设备参数信息
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

                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
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
                AddList("GET_INPUT_DATA", strSendValue, ReturnValue, uiData);
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
        #endregion

        #region 接口21: 根据SN,工站清除临时上料数据的接口 ok 6
        /// <summary>
        /// 接口21: 根据SN,工站清除临时上料数据的接口
        /// </summary>
        /// <param name="uiData"></param>
        public static bool ClearUploadInformation(ref GTK_MESData_Model uiData)
        {
            try
            {
                uiData.errException = "";
                string strSendValue;
                string ReturnValue;
                UpdataSWVersion Updata1 = new UpdataSWVersion();
                MesLogin Lg1 = new MesLogin();
                UpdataSWVersion Updata2 = new UpdataSWVersion();
                MesLogin Lg2 = new MesLogin();

                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
                Updata1.OPCategory = "CLEAR_SN_INPUT_TMPDATA";
                foreach (string sn in uiData.产品SN)
                {
                    Updata1.OPRequestInfo.Clear();
                    Updata2.OPResponseInfo.Clear();
                    Updata1.OPRequestInfo.Add("SN", sn);
                    Updata1.OPRequestInfo.Add("CLEAR_GROUP", uiData.工站编码);
                    Updata1.OPResponseInfo = new Dictionary<object, object>();

                    Lg1.cmd = 16;
                    Lg1.Hwd = uiData.HWD;
                    Lg1.Indicator = "Add_Attr";
                    Lg1.SerializeData = JsonConvert.SerializeObject(Updata1, Formatting.None);

                    strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
                    ReturnValue = HttpPost(uiData.Url + "autogate", strSendValue);
                    AddList("6清除中间表", strSendValue, ReturnValue, uiData);
                    Lg2 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                    Updata2 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg2.SerializeData);
                    if (!Updata2.OPResponseInfo["Result"].ToString().Contains("OK"))
                    {
                        uiData.errException += sn + ":" + Updata2.OPResponseInfo["Result"].ToString() + "\n";
                    }
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

        #region 接口19:依据stationCode获取stationName信息
        ///
        /// 接口19:依据stationCode获取stationName信息
        ///
        public static bool MesGetStationMsgByStationCode(ref GTK_MESData_Model uiData)
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

                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
                Updata1.OPCategory = "GET_STATIONNAME_BY_STATIONCODE";

                
                t.OPRequestInfo.Clear();
                Updata2.OPResponseInfo.Clear();
                t.OPRequestInfo.Add("STATIONCODE", uiData.工位编码);
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
                AddList("GET_STATIONNAME_BY_STATIONCODE", strSendValue, ReturnValue, uiData);
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
        #endregion
        //4.18新增接口DT
        #region
        public static bool MESDTUpdatDate(ref GTK_MESData_Model uiData, GTK_潍坊_MESData_DT gtkDT)
        {
            try
            {
                UpdataSWVersion Updata1 = new UpdataSWVersion();
                Updata1.LineCode = uiData.线体编码;
                Updata1.SectionCode = uiData.工站编码;
                Updata1.StationCode = uiData.工位编码;
                Updata1.OPCategory = "DT_REPORT_UPLOAD";

                Updata1.OPRequestInfo.Add("ERROR_CODE", gtkDT.ERROR_CODE);                                //错误代码
                Updata1.OPRequestInfo.Add("ERROR_MESSAGE", gtkDT.ERROR_MESSAGE);          //错误信息（非必填）
                Updata1.OPRequestInfo.Add("MACHINE_NO", gtkDT.MACHINE_NO);                                   //机台号
                Updata1.OPRequestInfo.Add("VENDOR_CODE", gtkDT.VENDOR_CODE);                          //供应商代码
                Updata1.OPRequestInfo.Add("ISSUE_CATEGORY", gtkDT.ISSUE_CATEGORY);                        //问题归类
                Updata1.OPRequestInfo.Add("ROOT_CAUSE", gtkDT.ROOT_CAUSE);                                //根本原因
                Updata1.OPRequestInfo.Add("DRI", gtkDT.DRI);                                                   //责任人
                Updata1.OPRequestInfo.Add("ACTION", gtkDT.ACTION);                                         //解决措施
                Updata1.OPRequestInfo.Add("FUNCTION", gtkDT.FUNCTION);                                          //部门
                Updata1.OPRequestInfo.Add("UPLOAD_TIME", gtkDT.UPLOAD_TIME);                 //异常开始 / 结束时间
                Updata1.OPResponseInfo = new Dictionary<object, object>();


                MesLogin Lg1 = new MesLogin();
                Lg1.cmd = 16;
                Lg1.Hwd = uiData.HWD;
                Lg1.Indicator = "ADD_RECORD";
                Lg1.SerializeData = JsonConvert.SerializeObject(Updata1, Formatting.None);
                //Lg1.Language = "CN";
                string strSendValue = JsonConvert.SerializeObject(Lg1, Formatting.Indented);
                string ReturnValue = HttpPost(uiData.Url + "autogate", strSendValue);
                Lg1 = JsonConvert.DeserializeObject<MesLogin>(ReturnValue);
                Updata1 = JsonConvert.DeserializeObject<UpdataSWVersion>(Lg1.SerializeData);
                AddList("DT数据上传：", strSendValue, ReturnValue, uiData);
                if (Updata1.OPResponseInfo["Result"].ToString().Contains("OK"))
                {
                    return true;
                }
                else
                {
                    uiData.errException = "DT数据上传 NG:" + Updata1.OPResponseInfo["Result"].ToString();
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

        ///
        /// Upload Image to Mes
        ///
        

        /// <summary>
        /// 保存界面参数 save interface manager
        /// </summary>
        /// <param name="MESPar">model</param>
        /// <param name="filePath">路径</param>
        /// <returns></returns>
        public static bool SaveUiXmlData()
        {
            try
            {
                //Save file xml
                //string filePath;
                //filePath = FilePath.mFilePath.BZ_NewPar + "New_Config\\MesPar_GTK.xml";
                //mFunction.WriteXml(filePath, ref GTKMES.TempGtkMesData);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        

    

    #region 接口
    #region"Login"
    public class MesLogin
    {
        public int cmd { get; set; }
        public string Hwd { get; set; }
        public string Indicator { get; set; }
        public string SerializeData { get; set; }
        //public string Language { get; set; }
    }
    public class Login_Send_SerializeData
    {
        public string user { get; set; }
        public string pwd { get; set; }
    }

    public class Login_Rec
    {
        public int cmd { get; set; }
        /// <summary>
        /// {返回令牌}
        /// </summary>
        public string Hwd { get; set; }
        public string Indicator { get; set; }
        public string SerializeData { get; set; }
        public string Display { get; set; }
        public string BindInfo { get; set; }
    }
    #endregion

    #region"接口16:供应商版本上传接口"
    public class UpdataSWVersion
    {
        public string LineCode { get; set; }
        public string SectionCode { get; set; }
        public int StationCode { get; set; }
        public string OPCategory { get; set; }

        public Dictionary<object, object> OPRequestInfo = new Dictionary<object, object>();
        public Dictionary<object, object> OPResponseInfo = new Dictionary<object, object>();

    }
    public class UpdataSWVersion2
    {
        public string LineCode { get; set; }
        public string SectionCode { get; set; }
        public int StationCode { get; set; }
        public string OPCategory { get; set; }

        

    }

        /// <summary>
        /// 接口18:
        /// </summary>
        public class UpdataSWVersion18
    {
        public string LineCode { get; set; }
        public string SectionCode { get; set; }
        public int StationCode { get; set; }
        public string OPCategory { get; set; }

        public Dictionary<object, object> OPRequestInfo = new Dictionary<object, object>();
        //public Dictionary<object, object> OPResponseInfo = new Dictionary<object, object>();
        public string OPResponseInfo { get; set; }

    }
    public class Updata_Send_OPResponseInfo
    {
        public string Result { get; set; }
        public Dictionary<object, object> Data = new Dictionary<object, object>();
    }
    public class DataCheckResult
    {
        public string InputDate { get; set; }
        public Dictionary<object, object> Data = new Dictionary<object, object>();
    }



    #endregion

    #region"接口10:供应商版本上传接口"
    public class UpdataSWVersion10
    {
        public string LineCode { get; set; }
        public string SectionCode { get; set; }
        public int StationCode { get; set; }
        public string ServerVersion { get; set; }
    }


    #endregion


    #endregion



    public class GTK_潍坊_MESData_DT
    {
        public string ERROR_CODE { get; set; }
        public string ERROR_MESSAGE { get; set; }
        public string MACHINE_NO { get; set; }
        public string VENDOR_CODE { get; set; }
        public string ISSUE_CATEGORY { get; set; }
        public string ROOT_CAUSE { get; set; }
        public string DRI { get; set; }
        public string ACTION { get; set; }
        public string FUNCTION { get; set; }
        public string UPLOAD_TIME { get; set; }

    }
    public class ImgUploadInfo
        {
            public string SN { get; set; }
            public string SectionCode { get; set; }
            public string StationCode { get; set; }
            public string User { get; set; }
            public string ImageType { get; set; }
            public DateTime UpdloadDate { get; set; } = System.DateTime.Now;
            public Byte[] ImgByteList { get; set; }
        }
    
    }
}
