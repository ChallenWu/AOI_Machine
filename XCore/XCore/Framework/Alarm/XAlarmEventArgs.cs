using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCore
{
    public delegate bool CallbackAction(); 

    public class XAlarmEventArgs : EventArgs 
    {
        private int code;
        private string category;
        private string m_description;
        private string m_OkOptionText;
        private string m_CancelOptionText;
        private string m_IgnoreOptionText;
        private string m_solution;
        public CallbackAction CallbackAct = null;
        public List<dynamic> Alarmlist = new List<dynamic> { };

        public XAlarmEventArgs(int code, string category, string description,
                string okOptiontext = "确认", string cancelOptionText = "", string ignoreOptionText = "")
        {
            this.code = code;
            this.category = category;
            this.m_description = description;
            this.m_OkOptionText = okOptiontext;
            this.m_CancelOptionText = cancelOptionText;
            this.m_IgnoreOptionText = ignoreOptionText;
            this.m_solution= LoadFileAndCheckAlarm(code);
        }
        public string LoadFileAndCheckAlarm(int Errorcode)
        {
            string num = "";
            Alarmlist = MiniExcel.Query("Solution.xlsx").ToList();
            for (int i = 1; i < Alarmlist.Count; i++)
            {
                if (Alarmlist[i].A == Errorcode)
                {
                    if (Alarmlist[i].C != null)
                    {
                        num = Alarmlist[i].C;
                    }
                    else
                        num = "";
                    return num; ;
                }
            }
            return num; ;
        }
        public int Code
        {
            get { return this.code; }
            set { this.code = value; }
        }
        public string Solution
        {
            get { return this.m_solution; }
            set { this.m_solution = value; }
        }

        public int StationId { get; set; }

        public DateTime StartTime { get; set; }

        public string Category
        {
            get 
            { 
                return MultiLanguage.GetMessage(this.category); 
            }
            set 
            { 
                this.category = value; 
            }
        }

        public string Description
        {
            get
            {
                return MultiLanguage.GetMessage(m_description);
            }
            set
            {
                this.m_description = value;
            }
        }

        public TimeSpan Duration { get; set; }
        public int AlarmLevel { get; set; }
        public int TimeMode { get; set; }

        public string OkOptionText 
        {
            get
            {
                return MultiLanguage.GetMessage(m_OkOptionText);
            }
            set
            {
                m_OkOptionText = value;
            }
        }
        public string IgnoreOptionText
        {
            get
            {
                return MultiLanguage.GetMessage(m_IgnoreOptionText);
            }
            set
            {
                m_IgnoreOptionText = value;
            }
        }
        public string CancelOptionText
        {
            get
            {
                return MultiLanguage.GetMessage(m_CancelOptionText);
            }
            set
            {
                m_CancelOptionText = value;
            }
        }
    }
}
