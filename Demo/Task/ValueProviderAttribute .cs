using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[AttributeUsage(AttributeTargets.Property)]
public class ValueProviderAttribute : Attribute
{
    public string ProviderMethodName { get; }

    public ValueProviderAttribute(string providerMethodName)
    {
        ProviderMethodName = providerMethodName;
    }
}
