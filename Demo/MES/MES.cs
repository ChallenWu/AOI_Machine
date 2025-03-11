using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HB_IWatch;
using System.Windows.Forms;

namespace BoTech
{
    public class MES
    {
        private static string URL = "http://10.191.246.244/api/gate"; //api/Assy10.191.246.244

        private static string ParsToString(Dictionary<string,string> Pars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("$");
                }
                //sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
                sb.Append(k.ToString() + "=" + Pars[k].ToString());
            }
            return sb.ToString();
        }
        private static string[] SplitString(string needSplitstring, string searchstring)
        {
            string temp = needSplitstring.Replace(searchstring, ",");
            return temp.Split(',');
        }
        #region Fuyu mes
        //AssyCheck > collectTest > AssyGo
        public static bool AssyCheck_AOI(string SN, out BackMessage bm)
        {
            AssyCheck_AOI assyCheck = new AssyCheck_AOI();
            assyCheck.empNo = "6E72TEST";
            assyCheck.terminalName = "ITKS_EXX-3FT-MES-XXX";
            assyCheck.serial_Number = SN;
            assyCheck.machine = "";
            assyCheck.toolingNo = "";
            assyCheck.lotNo = "";
            assyCheck.kpsn = "";
            assyCheck.reelNo = "";
            assyCheck.workOrder = "WO0011";

            string jsonData = JsonConvert.SerializeObject(assyCheck, Formatting.Indented);//Set the json display format
            WriteLog("PC-->MES AssyCheck" +jsonData.Replace("\\r", "").Replace("\\n", ""));
            string backMessage = "";
            BackMessage BM = new BackMessage();
            if (HttpPost(jsonData, ref backMessage, PostType.AssyCheck))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                WriteLog("MES-->PC AssyCheck" + backMessage);
                bm = BM;
                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                WriteLog("MES-->PC AssyCheck" + backMessage);
                bm = BM;
                return false;
            }
        }

        public static bool CollectTest_AOI(UCMessage UCM, int unitindex, out BackMessage bm)
        {
            //Hashtable testData = new Hashtable();
            Dictionary<string, string> testData = new Dictionary<string, string>();
            testData.Add("test_result", UCM.Units[unitindex].Pass);
            testData.Add("unit_sn", UCM.Units[unitindex].UnitSN);
            testData.Add("uut_start", UCM.Units[unitindex].StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            testData.Add("uut_stop", UCM.Units[unitindex].EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            testData.Add("limits_version", "v1.0");//v1.0
            testData.Add("software_name", "BZ1.0");
            testData.Add("software_version", "V1.0");
            testData.Add("station_id", "L304");
            testData.Add("fixture_id", UCM.UC_SN);
            string testDataStr = ParsToString(testData);

            Dictionary<string, string> results = new Dictionary<string, string>();
            results.Add("lower_limit", "0.5");
            results.Add("parametric_key", "");
            results.Add("priority", "");
            results.Add("result", UCM.Units[unitindex].Pass);
            results.Add("sub_sub_test", "");
            results.Add("sub_test", "");
            results.Add("test", "Air_Pressure");
            results.Add("units", "Mpa");
            results.Add("upper_limit", "0.7");
            results.Add("value", "0.6");
            results.Add("Message", "");
            string resulrsStr = ParsToString(results);

            CollectTestData CTD = new CollectTestData();
   

            string jsonData = JsonConvert.SerializeObject(CTD, Formatting.Indented);//设置json显示格式
            string backMessage = "";
            BackMessage BM = new BackMessage();
            WriteLog("PC-->MES CollectTest" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost(jsonData, ref backMessage, PostType.CollectTestData))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                WriteLog("MES-->PC CollectTest" + backMessage);
                bm = BM;

                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                WriteLog("MES-->PC CollectTest" + backMessage);
                bm = BM;
                return false;
            }
        }

        public static bool AssyGo_AOI(ProductInformation Unit, out BackMessage bm)
        {
            AssyGoData AGD = new AssyGoData();
            AGD.empNo = SystemVariable.InforMachine.MES.empNo;
            AGD.terminalName = SystemVariable.InforMachine.MES.terminalName;
            AGD.serial_Number = Unit.UnitSN;
            AGD.machine = "";
            AGD.toolingNo = "";
            AGD.lotNo = "";
            AGD.kpsn = "";
            AGD.reelNo = "";
            AGD.workOrder = "";

            string jsonData = JsonConvert.SerializeObject(AGD, Formatting.Indented);//mã hóa thành file Json
            string backMessage = "";
            BackMessage BM = new BackMessage();
            WriteLog("PC-->MES AssyGo" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost(jsonData, ref backMessage, PostType.AssyGo))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                WriteLog("MES-->PC AssyGo" + backMessage);
                bm = BM;
                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                WriteLog("MES-->PC AssyGo" + backMessage);
                bm = BM;
                return false;
            }
        }
        #endregion
        public static bool HttpPost(string SendData,ref string BackMessage, PostType pt = PostType.AssyCheck, string Url="")
        {
            BackMessage = "";
            string tUrl = "";
            tUrl = URL;
            WriteLog("PC-->MES " + pt .ToString()+ " [" +tUrl+"] " + SendData.Replace("\\r", "").Replace("\\n", ""));
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(tUrl);
                request.Method = "POST";
                byte[] bytes = Encoding.UTF8.GetBytes(SendData);
                request.ContentType ="application/json"; /*"application/x-www-form-urlencoded; charset=UTF-8";*/
                request.ContentLength = bytes.Length;
                Stream myResponseStream = request.GetRequestStream();
                myResponseStream.Write(bytes, 0, bytes.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();

                BackMessage = retString;
                WriteLog("MES-->PC " + pt.ToString() + " " + BackMessage+"\r\n");
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
                return true;
            }
            catch (Exception ex)
            {
                BackMessage = $"{{\"Result\":false,\"RetMsg\":\"Funtion(HttpPost) catch a Error:{ex.Message.ToString()}\"}}";
                WriteLog("MES-->PC " + pt.ToString() + " " + BackMessage + "\r\n");
                return false;
            } 
            finally
            {
            }
            
        }


        private static void  WriteLog(string log)
        {
            string MES_log = @"D:\MES_log\";
            string filePath = MES_log + DateTime.Now.ToString("yyyyMMdd");
            string fileName = filePath + "\\MES_Hour" + DateTime.Now.ToString("HH") + ".txt";
            string str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " => " + log;
            CsvServer.Instance.WriteLine(fileName, str);
        }
    }

    public enum PostType
    {
        AssyCheck,
        AssyGo,
        GetCmdResult,
        CollectTestData
    }

    public enum PCmd
    {
        GET_WO_INFO,
        GET_SN_PART_TYPE,
        RFID_GET_SN,
        KPSN_TO_SN,
        AUTO_DELINKRFID,
        GET_GLUE_USE_STATUS,
        MGID_GETSN_FROM_CAVITY,
        GET_OP_ACCESS,
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
    public class AssyGoData
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
    public class GetCmdResultData
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
        /// 可空  首站、投入站必传工单号；无特殊情况下，其他站点可传空 工单号
        /// </summary>
        public string workOrder { get; set; }
        /// <summary>
        /// 可空  根据当站需求而定 模穴号
        /// </summary>
        public string cavity { get; set; }
        /// <summary>
        /// 可空  根据当站需求而定 测试站主属性参数
        /// </summary>
        public string testData { get; set; }
        /// <summary>
        /// 可空  根据当站需求而定 测试项目信息及相对应参数值
        /// </summary>
        public string results { get; set; }
        /// <summary>
        /// 可空  根据当站需求而定 传状态 OK\FAIL\START\STOP
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 可空  根据当站需求而定 GetCmdResult,子命令（根据用户需求提供）
        /// </summary>
        public string pCmd { get; set; }
        /// <summary>
        /// 可空  根据当站需求而定 不良代码
        /// </summary>
        public string defectCode { get; set; }
    }
    public class CollectTestData
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
        /// 可空  首站、投入站必传工单号；无特殊情况下，其他站点可传空 工单号
        /// </summary>
        public string workOrder { get; set; }
        /// <summary>
        /// 可空  根据当站需求而定 模穴号
        /// </summary>
        public string cavity { get; set; }
        /// <summary>
        /// 测试站主属性参数
        /// </summary>
        public string testData { get; set; } //
        /// <summary>
        /// 测试项目信息及相对应参数值
        /// </summary>
        public string results { get; set; } //
        // <summary>
        // 不可空  CollectTestData,子命令（REC(SN 已投入，记录测试数据)\COL（SN 未投入，记录测试数据））
        /// </summary>
        public string collectType { get; set; }
        /// <summary>
        /// 测试站主属性参数
        /// </summary>
        public class TestData
        {
            /// <summary>
            /// 不可空  PASS 或 FAIL 必须大写  
            /// </summary>
            public string test_result { get; set; }
            /// <summary>
            /// 不可空 SN
            /// </summary>
            public string unit_sn { get; set; }
            /// <summary>
            /// 不可空 开始时间(2022-10-24 10:03:25)
            /// </summary>
            public string uut_start { get; set; }
            /// <summary>
            /// 不可空 结束时间(2022-10-24 10:04:25)
            /// </summary>
            public string uut_stop { get; set; }
            /// <summary>
            /// 可空 限制版本
            /// </summary>
            public string limits_version { get; set; }
            /// <summary>
            /// 可空 软件名称
            /// </summary>
            public string software_name { get; set; }
            /// <summary>
            /// 不可空 软件版本
            /// </summary>
            public string software_version { get; set; }
            /// <summary>
            /// 不可空 站点名
            /// </summary>
            public string station_id { get; set; }
            /// <summary>
            /// 可空 治具号，最大 32 字节
            /// </summary>
            public string fixture_id { get; set; }
            
        }
        /// <summary>
        /// 测试站各测试项目信息及对应参数
        /// </summary>
        public class Results
        {
            /// <summary>
            /// 可空 下限；必须纯数字
            /// </summary>
            public string lower_limit { get; set; }
            /// <summary>
            /// 可空 关键参数
            /// </summary>
            public string parametric_key { get; set; }
            /// <summary>
            /// 可空 优先级
            /// </summary>
            public string priority { get; set; }
            /// <summary>
            /// 可空 上传：PASS 或 FAIL 必须大写
            /// </summary>
            public string result { get; set; }
            /// <summary>
            /// 可空 子子测试
            /// </summary>
            public string sub_sub_test { get; set; }
            /// <summary>
            /// 可空 子测试
            /// </summary>
            public string sub_test { get; set; }
            /// <summary>
            /// 不可空  上传：纯英文字母，最大 40 字节
            /// </summary>
            public string test { get; set; }
            /// <summary>
            /// 可空 [计量]单位
            /// </summary>
            public string units { get; set; }
            /// <summary>
            /// 可空 上限;必须纯数字
            /// </summary>
            public string upper_limit { get; set; }
            /// <summary>
            /// 不可空 上传：必须纯数字
            /// </summary>
            public string value { get; set; }
            /// <summary>
            /// 可空 结果信息
            /// </summary>
            public string Message { get; set; }
        }
        
    }
    public class BackMessage
    {
        /// <summary>
        /// Kết quả
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// Thông tin Mes gửi trả về
        /// </summary>
        public string RetMsg { get; set; }
    }
    public class CheckLogin
    {
        public int cmd { get; set; }
        public string Hwd { get; set; }
        public string Indicator { get; set; }

        //public Dictionary<string, string> SerializeData = new Dictionary<string, string> { };
        public string SerializeData { get; set; }
    }
    public class EquipmentStage
    {
        public int cmd { get; set; }
        public string machine { get; set; }
        public string mainSn { get; set; }
        public string SerializeData { get; set; }
        public Dictionary<string, string> lstSerialNumber { get; set; }
    }
    public class AssyCheck_AOI
    {
        /// <summary>
        /// Tên người vận hành
        /// </summary>
        public string empNo {  get; set; }
        /// <summary>
        /// tên máy trạm
        /// </summary>
        public string terminalName {  get; set; }
        /// <summary>
        /// Số sê-ri sản phẩm
        /// </summary>
        public string serial_Number { get; set; }
        /// <summary>
        /// Mã máy
        /// </summary>
        public string machine {  set; get; }
        /// <summary>
        /// Mã của tool đang dùng trên máy
        /// </summary>
        public string toolingNo { get; set; }
        /// <summary>
        /// Batch material
        /// </summary>
        public string lotNo {  get; set; }
        /// <summary>
        /// Mã vật liệu sử dụng
        /// </summary>
        public string kpsn {  get; set; }
        /// <summary>
        /// Mã cuộn liệu
        /// </summary>
        public string reelNo { get; set; }  
        /// <summary>
        /// Mã làm việc
        /// </summary>
        public string workOrder {  get; set; }

    }
    public class AssyGo_AOI
    {
        public string cmd { set; get; }
        public string lineCode { set; get; }
        public string machineName { set; get; }
        public string snProduct { set; get; }
        public Dictionary<string, string> dic { get; set; } //List 12 SN được gán trên sản phẩm
    }
}
