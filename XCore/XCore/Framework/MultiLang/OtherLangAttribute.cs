using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCore
{
    [AttributeUsage(AttributeTargets.All)]
    public class OtherLangAttribute : Attribute
    {
        private string[] m_DescriptionArr = new string[2];

        // 摘要:
        //     不带参数初始化 System.ComponentModel.DescriptionAttribute 类的新实例。
        public OtherLangAttribute(string enDescrip, string vietDescip)
        {
            m_DescriptionArr[0] = enDescrip;
            m_DescriptionArr[1] = vietDescip;

        }

        // 摘要:
        //     不带参数初始化 System.ComponentModel.DescriptionAttribute 类的新实例。
        public OtherLangAttribute(string enDescrip)
        {
            m_DescriptionArr[0] = enDescrip;
            m_DescriptionArr[1] = "";

        }

        // 摘要:
        //     获取存储在此特性中的说明。
        //
        // 返回结果:
        //     存储在此特性中的说明。
        public string[] DescriptionArr 
        {
            get
            {
                return m_DescriptionArr;
            }
        }
    }
}
