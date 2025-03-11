using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Xml;
using XCore;
using Demo;

namespace Demo
{
    public enum Privilige
    {
        Administrator,
        Engineer,
        Operator
    }

    public class UserAccount
    {
        public Dictionary<string, string> UserAccount_DIc = new Dictionary<string, string>();


        public UserAccount(string account, string name, string password)
        {
            this.account = account;
            this.name = name;
            this.passWord = password;
            this.userPermission = Privilige.Engineer;
            

            UserAccount_DIc.Add("Account", account);
            UserAccount_DIc.Add("Name", name);
            UserAccount_DIc.Add("PassWord", password);
            UserAccount_DIc.Add("Privilige", userPermission.ToString());
        }

        /// <summary>
        /// 生成特殊账号"Administrator","BOTECH","Operator"
        /// </summary>
        /// <param name="account"></param>
        public UserAccount(string account)
        {
            switch (account)
            {
                case "Administrator":
                    this.account = account;
                    this.name = "Administrator";
                    this.passWord = UserAccountControl.GetAdministratorPassWord();
                    this.userPermission = Privilige.Administrator;
                    break;
                case "BOTECH":
                    this.account = account;
                    this.name = "BOTECH";
                    this.passWord = UserAccountControl.GetBOTECHPassWord();
                    this.userPermission = Privilige.Engineer;
                    break;
                case "Operator":
                    this.account = account;
                    this.name = "OP";
                    this.passWord = "";
                    this.userPermission = Privilige.Operator;
                    break;
            }
        }

        private string account;
        private string name;
        private string passWord;
        private Privilige userPermission;

             
        public string Account
        {
            get { return account; }
        }

        public string Name
        {
            get { return name; }
        }

        public string PassWord
        {
            get
            {
                switch (this.account)
                {
                    case "Administrator":
                        this.passWord = UserAccountControl.GetAdministratorPassWord();
                        break;
                    case "BOTECH":
                        this.passWord = UserAccountControl.GetBOTECHPassWord();
                        break;
                }
                return this.passWord;
            }
        }
        

        public Privilige UserPermission
        {
            get { return userPermission; }
        }

    }
    public class UserAccountControl
    {
        public static Dictionary<string, UserAccount> AllUserAccounts = new Dictionary<string, UserAccount>();
        private static string rootName = "UserAccount";
        private static string path = Globals.Dir_UserAccount+ "UserAccount.xml";

        public static UserAccount Administrator = new UserAccount("Administrator");
        public static UserAccount BOTECH = new UserAccount("BOTECH");
        public static UserAccount Operator = new UserAccount("Operator");

        public static UserAccount currentAccount = Operator;

        public static string GetBOTECHPassWord()
        {
            int i = DateTime.Now.DayOfWeek - DayOfWeek.Monday;
            if (i < 0)
                i = 7 +i;
            DateTime SundayDate = DateTime.Now.AddDays(-i);
            return SundayDate.ToString("yyyyMMdd");
        }

        public static string GetAdministratorPassWord()
        {
            int i = DateTime.Now.DayOfWeek - DayOfWeek.Sunday;
            if (i != 0)
                i = 7 - i;
            DateTime SundayDate = DateTime.Now.AddDays(i);
            return SundayDate.ToString("yyyyMMdd");
        }


        public static bool AddAccount(UserAccount account)
        {
            string key = account.Account;
            AllUserAccounts.Add(key, account);
            if (SaveXml() == 0)
                return true;
            else
            {
                AllUserAccounts.Remove(key);
                return false;
            }
        }

        public static bool RemoveCount(UserAccount account)
        {
            string key = account.Account;
            if (AllUserAccounts.ContainsKey(key))
                AllUserAccounts.Remove(key);
            if (SaveXml() == 0)
                return true;
            else
            {
                AllUserAccounts.Add(key, account);
                return false;
            }
        }

        public static bool RemoveCount(string account)
        {
            UserAccount ua = null;
            if (AllUserAccounts.ContainsKey(account))
            {
                ua = AllUserAccounts[account];
                AllUserAccounts.Remove(account);
            }
            if (SaveXml() == 0)
                return true;
            else
            {
                AllUserAccounts.Add(account, ua);
                return false;
            }
        }


        public static bool IsRepeatCount(UserAccount u)
        {
            return AllUserAccounts.ContainsKey(u.Account);
        }

        public static bool IsRepeatCount(string account)
        {
            return AllUserAccounts.ContainsKey(account);
        }


        private static int SaveXml()
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Thread.Sleep(10);
                }

                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement(rootName);
                doc.AppendChild(root);
                foreach (KeyValuePair<string, UserAccount> kvp in AllUserAccounts)
                {
                    XmlElement userCount = doc.CreateElement(kvp.Key);
                    foreach (KeyValuePair<string, string> kvpAccountInfo in kvp.Value.UserAccount_DIc)
                    {
                        XmlElement userCount_Count = doc.CreateElement(kvpAccountInfo.Key);
                        userCount_Count.InnerText = kvpAccountInfo.Value;
                        userCount.AppendChild(userCount_Count);
                    }
                    root.AppendChild(userCount);
                }

                doc.Save(path);
                return 0;
            }
            catch (Exception ex)
            {
                BzMessagebox.Show(ex.ToString(), MultiLanguage.GetMessage("错误"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

        }

        public static int ReadFromXml()
        {
            AllUserAccounts.Clear();
            if (!File.Exists(path))  // 如果路径不存在，需要创建一个新文件
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement(rootName);
                doc.AppendChild(root);
                doc.Save(path);
            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode parent = doc.SelectSingleNode(rootName);
                if (parent == null)
                {
                    return 1;
                }
                XmlNodeList AllCounts = parent.ChildNodes;
                foreach (XmlElement Count in AllCounts)
                {
                    string name = null;
                    string account = null;
                    string passWord=null;

                    XmlNodeList CountInfo = Count.ChildNodes;
                    foreach (XmlElement CountInfoValue in CountInfo)
                    {
                        switch (CountInfoValue.Name)
                        {
                            case "Name":
                                name = CountInfoValue.InnerText;
                                break;
                            case "Account":
                                account = CountInfoValue.InnerText;
                                break;
                            case "PassWord":
                                passWord = CountInfoValue.InnerText;
                                break;
                        }
                    }

                    UserAccount user = new UserAccount(account, name, passWord);
                    if (account != null && name != null && passWord != null)
                        AllUserAccounts.Add(user.Account, user);

                }
                return 0;
            }
            catch (Exception ex)
            {
                BzMessagebox.Show(ex.ToString(), MultiLanguage.GetMessage("错误"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

        }
    }

}
