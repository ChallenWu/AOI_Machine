using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class PropertyConverter : StringConverter
{
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
        return true;
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
        return true;
    }
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
        if (context.PropertyDescriptor != null)
        {
            var valueProviderAttr = (ValueProviderAttribute)context.PropertyDescriptor.Attributes[typeof(ValueProviderAttribute)];
            if (valueProviderAttr != null)
            {
                MethodInfo method = context.Instance.GetType().GetMethod(valueProviderAttr.ProviderMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (method != null)
                {
                    var values = method.Invoke(context.Instance, null) as List<string>;
                    if (values != null)
                    {
                        return new StandardValuesCollection(values);
                    }
                }
            }
        }
        return base.GetStandardValues(context);
    }
}
