using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XCore.Framework.MultiLang
{
    public class PropertyStub : PropertyDescriptor
    {
        PropertyInfo info;
        public PropertyStub(PropertyInfo propertyInfo, Attribute[] attrs)
            : base(propertyInfo.Name, attrs)
        {
            this.info = propertyInfo;
        }
        public override Type ComponentType
        {
            get { return this.info.ReflectedType; }
        }
        public override bool IsReadOnly
        {
            get
            {
                ReadOnlyAttribute readOnly = (ReadOnlyAttribute)Attribute.GetCustomAttribute(info, typeof(ReadOnlyAttribute));
                if (readOnly != null)
                {
                    return ((ReadOnlyAttribute)Attribute.GetCustomAttribute(info, typeof(ReadOnlyAttribute))).IsReadOnly;
                }
                return this.info.CanWrite == false;
            }
        }
        public override Type PropertyType
        {
            get { return this.info.PropertyType; }
        }
        public override bool CanResetValue(object component)
        {
            return false;
        }
        public override object GetValue(object component)
        {
            //Console.WriteLine("GetValue: " + component.GetHashCode());
            try
            {
                return this.info.GetValue(component, null);
            }
            catch
            {
                return null;
            }
        }
        public override void ResetValue(object component)
        {
        }
        public override void SetValue(object component, object value)
        {
            //Console.WriteLine("SetValue: " + component.GetHashCode());
            this.info.SetValue(component, value, null);
        }
        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
        //通过重载下面这个属性，可以将属性在PropertyGrid中的显示设置成中文
        public override string DisplayName
        {
            get
            {
                if (info != null)
                {
                    MyPropertyAttribute uicontrolattibute = (MyPropertyAttribute)Attribute.GetCustomAttribute(info, typeof(MyPropertyAttribute));
                    if (uicontrolattibute != null)
                        return uicontrolattibute.PropertyName;
                    else
                    {
                        return info.Name;
                    }
                }
                else
                    return "";
            }
        }

        public override string Category
        {
            get
            {
                if(info != null)
                {
                    MyPropertyAttribute uicontrolattibute = (MyPropertyAttribute)Attribute.GetCustomAttribute(info, typeof(MyPropertyAttribute));
                    if (uicontrolattibute != null)
                        return uicontrolattibute.CategoryName;
                    else
                    {
                        return info.Name;
                    }
                }
                else
                    return "";
            }
            }
        }
}
