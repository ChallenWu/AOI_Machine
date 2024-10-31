using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.IO;

namespace XCore.Framework.MultiLang
{
    public class MyPropertyAttribute : Attribute
    {
        private string _PropertyName;
        private string categoryName;
        public MyPropertyAttribute(string name)
        {
            this._PropertyName = GetSettingName(name);
        }
        public MyPropertyAttribute(string name, string category)
        {
            this._PropertyName = GetSettingName(name);
            this.categoryName = GetCategoryName(category);
        }

        private string GetSettingName(string name)
        {
            return MultiLanguage.GetSettingName(name);
        }

        private string GetCategoryName(string name)
        {
            if (!MultiLanguage.categoryDic.ContainsKey(name)) return name;
            switch (MultiLanguage.CurrentLanguage)
            {
                case LanguageType.Chinese:
                    return MultiLanguage.categoryDic[name].Chinese;
                case LanguageType.English:
                    return MultiLanguage.categoryDic[name].English;
                case LanguageType.Vietnamese:
                    return MultiLanguage.categoryDic[name].Vietnamese;
                default:
                    return MultiLanguage.categoryDic[name].Chinese;
            }
        }
        public string PropertyName
        {
            get { return this._PropertyName; }
        }

        public string CategoryName {
            get { return this.categoryName; }
        }
    }
}
