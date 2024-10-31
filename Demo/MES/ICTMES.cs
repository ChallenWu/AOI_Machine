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
    public class ICTMES
    {
        private static string URL = "http://10.191.246.244/api/gate"; //api/Assy10.191.246.244

       
        //private static string Post(string url, string postData)
        //{
        //    try
        //    {
        //        var request = (HttpWebRequest)WebRequest.Create(url);

        //        var data = Encoding.ASCII.GetBytes(postData);
        //        request.Method = "POST";
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.ContentLength = data.Length;

        //        using (var stream = request.GetRequestStream())
        //        {
        //            stream.Write(data, 0, data.Length);
        //        }
        //        var response = (HttpWebResponse)request.GetResponse();
        //        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //        return responseString;
        //    }
        //    catch (Exception)
        //    {
        //        return "-1";
        //    }
        //}


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


        #region BE010-1
        public static bool AssyCheck(UCMessage UCM,int unitindex,out BackMessage bm)
        {
            AssyCheckData ACD = new AssyCheckData();
            ACD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            ACD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;           
            ACD.serial_Number = UCM.UC_SN;
            ACD.machine = "";
            ACD.toolingNo = "";
            ACD.lotNo = UCM.Units[unitindex].TR_NO; 
            ACD.kpsn = "";
            ACD.reelNo = "";
            ACD.workOrder = AudioSystem.AudioMachineMessage.MES.workOrder;

            string jsonData = JsonConvert.SerializeObject(ACD, Formatting.Indented);//设置json显示格式
            //WriteLog("PC-->MES AssyCheck" +jsonData.Replace("\\r", "").Replace("\\n", ""));
            string backMessage = "";
            BackMessage BM = new BackMessage();
            if (HttpPost(jsonData, ref backMessage,PostType.AssyCheck))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage,BM.GetType());
                //WriteLog("MES-->PC AssyCheck" + backMessage);
                bm = BM;
                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC AssyCheck" + backMessage);
                bm = BM;
                return false;
            }               
        }

        public static bool AssyGo(UCMessage UCM, int unitindex, out BackMessage bm)
        {
            AssyGoData AGD = new AssyGoData();
            AGD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            AGD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            AGD.serial_Number = UCM.Units[unitindex].UnitSN;
            AGD.machine = "";
            AGD.toolingNo = UCM.UC_SN;
            AGD.lotNo = UCM.Units[unitindex].TR_NO;
            AGD.kpsn = "";
            AGD.reelNo = "";
            AGD.workOrder = AudioSystem.AudioMachineMessage.MES.workOrder;

            string jsonData = JsonConvert.SerializeObject(AGD, Formatting.Indented);//设置json显示格式
            string backMessage = "";
            BackMessage BM = new BackMessage();
            //WriteLog("PC-->MES AssyGo" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost(jsonData, ref backMessage,PostType.AssyGo))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC AssyGo" + backMessage);
                bm = BM;
                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC AssyGo" + backMessage);
                bm = BM;
                return false;
            }
        }
 
        public static bool GetCmdResult(UCMessage UCM, int unitindex,string pCMd, out BackMessage bm)
        {
            GetCmdResultData GCRD = new GetCmdResultData();
            GCRD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            GCRD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            GCRD.serial_Number = UCM.Units[unitindex].WO;
            GCRD.machine = "";
            GCRD.toolingNo = "";
            GCRD.lotNo = "";
            GCRD.kpsn = "";
            GCRD.workOrder = UCM.Units[unitindex].WO;
            GCRD.cavity = "";
            GCRD.testData = "";
            GCRD.results = "";
            GCRD.status = "";
            GCRD.pCmd = pCMd;
            GCRD.defectCode = "";


            string jsonData = JsonConvert.SerializeObject(GCRD, Formatting.Indented);//设置json显示格式
            string backMessage = "";
            BackMessage BM = new BackMessage();
            //WriteLog("PC-->MES GetCmdResult" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost( jsonData, ref backMessage,PostType.GetCmdResult))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC GetCmdResult" + backMessage);
                bm = BM;

                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC GetCmdResult" + backMessage);
                bm = BM;
                return false;
            }
        }

        /*用于查询载具有没有解绑；*/
        /// <summary>
        /// 返回true,说明有穴位SN没解绑；否则应该是false;
        /// </summary>
        /// <param name="UCM"></param>
        /// <param name="unitindex"></param>
        /// <param name="pCMd"></param>
        /// <param name="bm"></param>
        /// <returns></returns>
        public static bool GetCmdResult_RFID(UCMessage UCM, int unitindex, string pCMd, out BackMessage bm)
        {
            GetCmdResultData GCRD = new GetCmdResultData();
            GCRD.empNo = "";
            GCRD.terminalName = "";
            GCRD.serial_Number = UCM.UC_SN;
            GCRD.machine = "";
            GCRD.toolingNo = "";
            GCRD.lotNo = "";
            GCRD.kpsn = "";
            GCRD.workOrder = "";
            GCRD.cavity = "";
            GCRD.testData = "";
            GCRD.results = "";
            GCRD.status = "";
            GCRD.pCmd = pCMd;
            GCRD.defectCode = "";


            string jsonData = JsonConvert.SerializeObject(GCRD, Formatting.Indented);//设置json显示格式
            string backMessage = "";
            BackMessage BM = new BackMessage();
            //WriteLog("PC-->MES GetCmdResult" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost(jsonData, ref backMessage, PostType.GetCmdResult))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC GetCmdResult" + backMessage);
                bm = BM;
                if (BM.RetMsg.Contains("RFID NOT LINK SN"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
                //if (BM.Result == "true")
                //    // return true;
                //    return false;
                //else
                //    //return false;
                //    return true;
            }
            else
            {
                //WriteLog("MES-->PC GetCmdResult" + backMessage);
                bm = BM;
                return false;
            }
        }



        public static bool CollectTest(UCMessage UCM, int unitindex, out BackMessage bm)
        {
            //Hashtable testData = new Hashtable();
            Dictionary<string, string> testData = new Dictionary<string, string>();
            testData.Add("test_result", UCM.Units[unitindex].Pass);
            testData.Add("unit_sn", UCM.Units[unitindex].UnitSN);
            testData.Add("uut_start", UCM.Units[unitindex].StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            testData.Add("uut_stop", UCM.Units[unitindex].EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            testData.Add("limits_version", "v1.0");//v1.0
            testData.Add("software_name", "BZ1.0");
            testData.Add("software_version", AudioSystem.AudioMachineMessage.SW_version.Trim());
            testData.Add("station_id", AudioSystem.AudioMachineMessage.MES.terminalName);
            testData.Add("fixture_id", UCM.UC_SN);
            string testDataStr = ParsToString(testData);

            //Hashtable results = new Hashtable();
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
            CTD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            CTD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            CTD.serial_Number = UCM.Units[unitindex].UnitSN;
            CTD.machine = "";
            CTD.toolingNo = UCM.UC_SN;
            CTD.lotNo = UCM.Units[unitindex].TR_NO; //UCM.TR_Lot_No;
            CTD.kpsn = "";
            CTD.workOrder = AudioSystem.AudioMachineMessage.MES.workOrder;
            CTD.cavity = (unitindex + 1).ToString();
            CTD.testData = testDataStr;
            CTD.results = resulrsStr;
            CTD.collectType = AudioSystem.AudioMachineMessage.MES.collectType;

            string jsonData = JsonConvert.SerializeObject(CTD, Formatting.Indented);//设置json显示格式
            string backMessage = "";
            BackMessage BM = new BackMessage();
            //WriteLog("PC-->MES CollectTest" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost(jsonData, ref backMessage, PostType.CollectTestData))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC CollectTest" + backMessage);
                bm = BM;

                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC CollectTest" + backMessage);
                bm = BM;
                return false;
            }
        }
        #endregion


        #region TI040;
        public static bool AssyCheck_TI040(UCMessage UCM, int unitindex, out BackMessage bm)
        {
            AssyCheckData ACD = new AssyCheckData();
            ACD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            ACD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            ACD.serial_Number = UCM.Units[unitindex].UnitSN;
            ACD.machine = "";
            ACD.toolingNo = "";
            ACD.lotNo = "";
            ACD.kpsn = "";
            ACD.reelNo = "";
            ACD.workOrder = "";// AudioSystem.AudioMachineMessage.MES.workOrder;

            string jsonData = JsonConvert.SerializeObject(ACD, Formatting.Indented);//设置json显示格式
            //WriteLog("PC-->MES AssyCheck" +jsonData.Replace("\\r", "").Replace("\\n", ""));
            string backMessage = "";
            BackMessage BM = new BackMessage();
            if (HttpPost(jsonData, ref backMessage, PostType.AssyCheck))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC AssyCheck" + backMessage);
                bm = BM;
                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC AssyCheck" + backMessage);
                bm = BM;
                return false;
            }



        }


        public static bool AssyGo_TI040(UCMessage UCM, int unitindex, out BackMessage bm)
        {
            AssyGoData AGD = new AssyGoData();
            AGD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            AGD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            AGD.serial_Number = UCM.Units[unitindex].UnitSN;
            AGD.machine = "";
            AGD.toolingNo = "";
            AGD.lotNo = "";
            AGD.kpsn = "";
            AGD.reelNo = "";
            AGD.workOrder = "";// AudioSystem.AudioMachineMessage.MES.workOrder;

            string jsonData = JsonConvert.SerializeObject(AGD, Formatting.Indented);//设置json显示格式
            string backMessage = "";
            BackMessage BM = new BackMessage();
            //WriteLog("PC-->MES AssyGo" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost(jsonData, ref backMessage, PostType.AssyGo))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC AssyGo" + backMessage);
                bm = BM;
                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC AssyGo" + backMessage);
                bm = BM;
                return false;
            }


        }

        /// <summary>
        /// TI040;
        /// </summary>
        /// <param name="UCM"></param>
        /// <param name="unitindex"></param>
        /// <param name="pCMd"></param>
        /// <param name="bm"></param>
        /// <returns></returns>
        public static bool GetCmdResult_TI040(UCMessage UCM, int unitindex, string pCMd, out BackMessage bm)
        {
            GetCmdResultData GCRD = new GetCmdResultData();
            GCRD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            GCRD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            GCRD.serial_Number = UCM.UC_SN;
            GCRD.machine = "";
            GCRD.toolingNo = "";
            GCRD.lotNo = "";
            GCRD.kpsn = "";
            GCRD.workOrder = "";// UCM.Units[unitindex].WO;//TI040这个可以没有；
            GCRD.cavity = "";
            GCRD.testData = "";
            GCRD.results = "";
            GCRD.status = "";
            GCRD.pCmd = pCMd;
            GCRD.defectCode = "";

            string jsonData = JsonConvert.SerializeObject(GCRD, Formatting.Indented);//设置json显示格式
            string backMessage = "";
            BackMessage BM = new BackMessage();
            //WriteLog("PC-->MES GetCmdResult" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost(jsonData, ref backMessage, PostType.GetCmdResult))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC GetCmdResult" + backMessage);
                bm = BM;

                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC GetCmdResult" + backMessage);
                bm = BM;
                return false;
            }
        }


        public static bool CollectTest_TI040(UCMessage UCM, int unitindex, out BackMessage bm)
        {
            //Hashtable testData = new Hashtable();
            Dictionary<string, string> testData = new Dictionary<string, string>();
            testData.Add("test_result", UCM.Units[unitindex].Pass);
            testData.Add("unit_sn", UCM.Units[unitindex].UnitSN);
            testData.Add("uut_start", UCM.Units[unitindex].StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            testData.Add("uut_stop", UCM.Units[unitindex].EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            testData.Add("limits_version", "v1.0");//v1.0
            testData.Add("software_name", "BZ1.0");
            testData.Add("software_version", AudioSystem.AudioMachineMessage.SW_version.Trim());
            testData.Add("station_id", AudioSystem.AudioMachineMessage.MES.terminalName);
            testData.Add("fixture_id", UCM.UC_SN);
            string testDataStr = ParsToString(testData);

            //Hashtable results = new Hashtable();
            Dictionary<string, string> results = new Dictionary<string, string>();
            results.Add("lower_limit", "0.5");
            results.Add("parametric_key", "");
            results.Add("priority", "");
            results.Add("result", UCM.Units[unitindex].Pass);
            results.Add("sub_sub_test", "");
            results.Add("sub_test", "");
            results.Add("units", "Mpa");
            results.Add("upper_limit", "0.7");
            results.Add("value", "0.6");
            results.Add("Message", "");
            if (AudioSystem.ladRead.Mode == 3 && AudioSystem.HiveStateIndex == 4)
            {
                results.Add("Tray_Turnover_Time", (AudioSystem.ChangeTray_CT / 10).ToString());
                results.Add("Installation_Time", (AudioSystem.Unit_set_CT / 10).ToString());
            }
            
            string resulrsStr = ParsToString(results);

            CollectTestData CTD = new CollectTestData();
            CTD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            CTD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            CTD.serial_Number = UCM.Units[unitindex].UnitSN;
            CTD.machine = "";
            CTD.toolingNo = UCM.UC_SN;//"";
            CTD.lotNo = "";
            CTD.kpsn = "";
            CTD.workOrder = "";// AudioSystem.AudioMachineMessage.MES.workOrder;
            CTD.cavity = (unitindex + 1).ToString();
            CTD.testData = testDataStr;
            CTD.results = resulrsStr;
            CTD.collectType = AudioSystem.AudioMachineMessage.MES.collectType;

            string jsonData = JsonConvert.SerializeObject(CTD, Formatting.Indented);//设置json显示格式
            string backMessage = "";
            BackMessage BM = new BackMessage();
            //WriteLog("PC-->MES CollectTest" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost(jsonData, ref backMessage, PostType.CollectTestData))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC CollectTest" + backMessage);
                bm = BM;
                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC CollectTest" + backMessage);
                bm = BM;
                return false;
            }
        }
        #endregion


        /// <summary>
        /// 获得OP权限；
        /// </summary>
        /// <param name="UCM"></param>
        /// <param name="unitindex"></param>
        /// <param name="pCMd"></param>
        /// <param name="bm"></param>
        /// <returns></returns>
        public static bool GetOpAccess(string pCMd, out BackMessage bm,string badgeID)
        {
            GetCmdResultData GCRD = new GetCmdResultData();
            GCRD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            GCRD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            GCRD.serial_Number = badgeID;///
            GCRD.machine = "";
            GCRD.toolingNo = "";
            GCRD.lotNo = "";
            GCRD.kpsn = "";
            GCRD.workOrder = "";// AudioSystem.AudioMachineMessage.MES.workOrder;
            GCRD.cavity = "";
            GCRD.testData = "";
            GCRD.results = "";
            GCRD.status = "";
            GCRD.pCmd = pCMd;
            GCRD.defectCode = "";


            string jsonData = JsonConvert.SerializeObject(GCRD, Formatting.Indented);//设置json显示格式
            string backMessage = "";
            BackMessage BM = new BackMessage();
            //WriteLog("PC-->MES GetCmdResult" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost(jsonData, ref backMessage, PostType.GetCmdResult))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC GetCmdResult" + backMessage);
                bm = BM;

                if (BM.Result == "true")
                { return true; }
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC GetCmdResult" + backMessage);
                bm = BM;
                return false;
            }
        }


        public static bool HttpPost(string SendData,ref string BackMessage, PostType pt = PostType.AssyCheck, string Url="")
        {
            BackMessage = "";
            string tUrl = "";
            //if (Url.Length<10)
            //    tUrl = URL+ "/"+pt.ToString();
            //else
            //    tUrl = Url;
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
                BackMessage = $"{{\"Result\":false,\"RetMsg\":\"Funtion(HttpPost) catch aError:{ex.Message.ToString()}\"}}";
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
            //string filePath = FilePath.mFilePath.BZ_MachineLogPath + DateTime.Now.ToString("yyyyMMdd");
            string filePath = MES_log + DateTime.Now.ToString("yyyyMMdd");
            string fileName = filePath + "\\MES_Hour" + DateTime.Now.ToString("HH") + ".txt";
            //Ghi du lieu vao trong file text
            //logServer.Instance.WriteLine(filePath, fileName, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff ") + log);

            string str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " => " + log;
            CsvServer.Instance.WriteLine(fileName, str);

        }


    }


    public class ICTMES_TI
    {
        private static string URL = "http://10.33.30.55:8080/api/Assy";

        public static string[] TestDataUpload(string tt, string str1, string str2, string str3, string str4)
        {
            var SS = new string[3];

            Hashtable pars = new Hashtable();
            pars.Add("SN", "");//从产品上读取到的条码信息 
            pars.Add("lineNumber", "");
            pars.Add("station", "");
            pars.Add("machineNO", "");
            pars.Add("testData", str1);//测试数据 
            pars.Add("testResult", str2);//测试结果 
            pars.Add("softwareVER", str4);//程序版本
            pars.Add("moName", "");
            pars.Add("coilWinding", str3);//绕线机编号 
            pars.Add("axis", "");//绕线机轴号 
            string postData = ParsToString(pars);
            string ret = Post("http://172.19.144.1:8011/CE012.asmx/TestDataUploadOne", postData);
            if (ret == "-1")
            {
                SS[0] = "-1";
                SS[1] = "MES服务器连接异常！";
            }
            else
            {
                string[] SNArry;
                SNArry = SplitString(ret, "<string>");
                if (SNArry.Length >= 2)
                {
                    SS[0] = SplitString(SNArry[1], "</string>")[0];//MES返回0代表OK，其他则失败
                    SS[1] = SplitString(SNArry[2], "</string>")[0];
                }
                else
                {
                    SS[0] = "-1";
                    SS[1] = "MES服务器返回数据异常！";
                }
            }
            return SS;
        }

        private static string Post(string url, string postData)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);

                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (Exception)
            {
                return "-1";
            }
        }
        private static string ParsToString(Hashtable Pars)
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
        public static bool AssyCheck(UCMessage UCM, int unitindex, out BackMessage bm)
        {
            AssyCheckData ACD = new AssyCheckData();
            ACD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            ACD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            ACD.serial_Number = UCM.Units[unitindex].UnitSN;
            ACD.machine = "";
            ACD.toolingNo = "";
            ACD.lotNo = "";
            ACD.kpsn = "";
            ACD.reelNo = "";
            ACD.workOrder = AudioSystem.AudioMachineMessage.MES.workOrder;

            string jsonData = JsonConvert.SerializeObject(ACD, Formatting.Indented);//设置json显示格式
            //WriteLog("PC-->MES AssyCheck" +jsonData.Replace("\\r", "").Replace("\\n", ""));
            string backMessage = "";
            BackMessage BM = new BackMessage();
            if (HttpPost(jsonData, ref backMessage, PostType.AssyCheck))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC AssyCheck" + backMessage);
                bm = BM;
                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC AssyCheck" + backMessage);
                bm = BM;
                return false;
            }



        }
        public static bool AssyGo(UCMessage UCM, int unitindex, out BackMessage bm)
        {
            AssyGoData AGD = new AssyGoData();
            AGD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            AGD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            AGD.serial_Number = UCM.Units[unitindex].UnitSN;
            AGD.machine = "";
            AGD.toolingNo = "";
            AGD.lotNo = "";
            AGD.kpsn = "";
            AGD.reelNo = "";
            AGD.workOrder = AudioSystem.AudioMachineMessage.MES.workOrder;

            string jsonData = JsonConvert.SerializeObject(AGD, Formatting.Indented);//设置json显示格式
            string backMessage = "";
            BackMessage BM = new BackMessage();
            //WriteLog("PC-->MES AssyGo" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost(jsonData, ref backMessage, PostType.AssyGo))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC AssyGo" + backMessage);
                bm = BM;
                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC AssyGo" + backMessage);
                bm = BM;
                return false;
            }


        }
        public static bool GetCmdResult(UCMessage UCM, int unitindex, string pCMd, out BackMessage bm)
        {
            GetCmdResultData GCRD = new GetCmdResultData();
            GCRD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            GCRD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            GCRD.serial_Number = UCM.UC_SN;
            GCRD.machine = "";
            GCRD.toolingNo = "";
            GCRD.lotNo = "";
            GCRD.kpsn = "";
            GCRD.workOrder = "";
            GCRD.cavity = "";
            GCRD.testData = "";
            GCRD.results = "";
            GCRD.status = "";
            GCRD.pCmd = pCMd;
            GCRD.defectCode = "";


            string jsonData = JsonConvert.SerializeObject(GCRD, Formatting.Indented);//设置json显示格式
            string backMessage = "";
            BackMessage BM = new BackMessage();
            //WriteLog("PC-->MES GetCmdResult" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost(jsonData, ref backMessage, PostType.GetCmdResult))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC GetCmdResult" + backMessage);
                bm = BM;

                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC GetCmdResult" + backMessage);
                bm = BM;
                return false;
            }
        }
        public static bool CollectTest(UCMessage UCM, int unitindex, out BackMessage bm)
        {


            //CollectTestData.TestData CTD_TD = new CollectTestData.TestData();
            //CTD_TD.test_result = "";
            //CTD_TD.unit_sn = "";
            //CTD_TD.uut_start = "";
            //CTD_TD.uut_stop = "";
            //CTD_TD.limits_version = "";
            //CTD_TD.software_name = "";
            //CTD_TD.software_version = "";
            //CTD_TD.station_id = "";
            //CTD_TD.fixture_id = "";

            //CollectTestData.Results CTD_R = new CollectTestData.Results();
            //CTD_R.lower_limit = "";
            //CTD_R.parametric_key = "";
            //CTD_R.priority = "";
            //CTD_R.result = "";
            //CTD_R.sub_sub_test = "";
            //CTD_R.sub_test = "";
            //CTD_R.test = "";
            //CTD_R.upper_limit = "";
            //CTD_R.value = "";
            //CTD_R.Message = "";
            Hashtable testData = new Hashtable();
            //UCM.Units[unitindex].Pass = "PASS";
            testData.Add("test_result", UCM.Units[unitindex].Pass);
            testData.Add("unit_sn", UCM.Units[unitindex].UnitSN);
            testData.Add("uut_start", UCM.Units[unitindex].StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            testData.Add("uut_stop", UCM.Units[unitindex].EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            testData.Add("limits_version", "v1.0");
            testData.Add("software_name", "BZ1.0");
            testData.Add("software_version", AudioSystem.AudioMachineMessage.SW_version.Trim());
            testData.Add("station_id", AudioSystem.AudioMachineMessage.MES.terminalName);
            testData.Add("fixture_id", AudioSystem.AudioMachineMessage.MES.terminalName);
            string testDataStr = ParsToString(testData);

            Hashtable results = new Hashtable();
            results.Add("lower_limit", "0.5");
            results.Add("parametric_key", "");
            results.Add("priority", "");
            results.Add("result", "PASS");
            results.Add("sub_sub_test", "");
            results.Add("sub_test", "");
            results.Add("test", "Air_Pressure");
            results.Add("units", "Mpa");
            results.Add("upper_limit", "0.7");
            results.Add("value", "0.6");
            results.Add("Message", "");
            string resulrsStr = ParsToString(results);

            CollectTestData CTD = new CollectTestData();
            CTD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            CTD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            CTD.serial_Number = UCM.Units[unitindex].UnitSN;
            CTD.machine = "";
            CTD.toolingNo = "";
            CTD.lotNo = "";
            CTD.kpsn = "";
            CTD.workOrder = AudioSystem.AudioMachineMessage.MES.workOrder;
            CTD.cavity = (unitindex + 1).ToString();
            CTD.testData = testDataStr;
            CTD.results = resulrsStr;
            CTD.collectType = AudioSystem.AudioMachineMessage.MES.collectType;

            string jsonData = JsonConvert.SerializeObject(CTD, Formatting.Indented);//设置json显示格式
            string backMessage = "";
            BackMessage BM = new BackMessage();
            //WriteLog("PC-->MES CollectTest" + jsonData.Replace("\\r", "").Replace("\\n", ""));
            if (HttpPost(jsonData, ref backMessage, PostType.CollectTestData))
            {
                BM = (BackMessage)JsonConvert.DeserializeObject(backMessage, BM.GetType());
                //WriteLog("MES-->PC CollectTest" + backMessage);
                bm = BM;
                if (BM.Result == "true")
                    return true;
                else
                    return false;
            }
            else
            {
                //WriteLog("MES-->PC CollectTest" + backMessage);
                bm = BM;
                return false;
            }
        }

        public static bool HttpPost(string SendData, ref string BackMessage, PostType pt = PostType.AssyCheck, string Url = "")
        {
            BackMessage = "";
            string tUrl = "";
            if (Url.Length < 10)
                tUrl = URL + "/" + pt.ToString();
            else
                tUrl = Url;
            WriteLog("PC-->MES " + pt.ToString() + " [" + tUrl + "] " + SendData.Replace("\\r", "").Replace("\\n", ""));
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(tUrl);
                request.Method = "POST";
                byte[] bytes = Encoding.UTF8.GetBytes(SendData);
                request.ContentType = "application/json"; /*"application/x-www-form-urlencoded; charset=UTF-8";*/
                request.ContentLength = bytes.Length;
                Stream myResponseStream = request.GetRequestStream();
                myResponseStream.Write(bytes, 0, bytes.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();

                BackMessage = retString;
                WriteLog("MES-->PC " + pt.ToString() + " " + BackMessage + "\r\n");
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
                BackMessage = $"{{\"Result\":false,\"RetMsg\":\"Funtion(HttpPost) catch aError:{ex.Message.ToString()}\"}}";
                WriteLog("MES-->PC " + pt.ToString() + " " + BackMessage + "\r\n");
                return false;
            }
            finally
            {

            }

        }

        private static void WriteLog(string log)
        {
            //string filePath = FilePath.mFilePath.BZ_MachineLogPath + DateTime.Now.ToString("yyyyMMdd");
            //string fileName = "\\MES_Hour" + DateTime.Now.ToString("HH") + ".txt";
            //logServer.Instance.WriteLine(filePath, fileName, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff ") + log);
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
        /// 通信结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 返回详细信息
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
}
