using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XCore.Framework.MultiLang
{
    public class PropertyBase : ICustomTypeDescriptor
    {
        #region ICustomTypeDescriptor 显式接口定义
        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }
        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }
        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }
        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }
        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }
        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return null;
        }
        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }
        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }
        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }
        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
        }
        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            ArrayList props = new ArrayList();
            Type thisType = this.GetType();
            PropertyInfo[] pis = thisType.GetProperties();
            foreach (PropertyInfo p in pis)
            {
                if (p.DeclaringType == thisType || p.PropertyType.ToString() == "System.Drawing.Color")
                {
                    //判断属性是否显示
                    BrowsableAttribute Browsable = (BrowsableAttribute)Attribute.GetCustomAttribute(p, typeof(BrowsableAttribute));
                    if (Browsable != null)
                    {
                        if (Browsable.Browsable == true || p.PropertyType.ToString() == "System.Drawing.Color")
                        {
                            PropertyStub psd = new PropertyStub(p, attributes);
                            props.Add(psd);
                        }
                    }
                    else
                    {
                        PropertyStub psd = new PropertyStub(p, attributes);
                        props.Add(psd);
                    }
                }
            }
            PropertyDescriptor[] propArray = (PropertyDescriptor[])props.ToArray(typeof(PropertyDescriptor));
            return new PropertyDescriptorCollection(propArray);
        }
        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
        #endregion
    }
}
