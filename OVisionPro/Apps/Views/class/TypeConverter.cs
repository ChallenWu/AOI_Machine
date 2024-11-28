using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro
{
    // Tạo một trình chỉnh sửa để hiển thị ComboBox
    public class ComboBoxEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            // Tạo ComboBox
            var comboBox = new ComboBox();
            var dictionary = value as IDictionary;

            if (dictionary != null)
            {
                // Thêm tất cả các giá trị trong dictionary vào ComboBox
                comboBox.Items.AddRange(dictionary.Values.Cast<object>().ToArray());
                comboBox.SelectedItem = value;
            }

            // Tạo Form để hiển thị ComboBox
            var form = new Form
            {
                Text = "Select Value",
            };
            comboBox.Dock = DockStyle.Fill;
            form.Controls.Add(comboBox);

            // Khi người dùng chọn giá trị, trả lại giá trị đó
            var dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                return comboBox.SelectedItem;
            }

            return value;  // Nếu không chọn, trả về giá trị cũ
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;  // Hiển thị ComboBox khi chỉnh sửa
        }
    }
    public class MyDropdownConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // Cho phép hiển thị danh sách các giá trị.
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; // Chỉ cho phép chọn từ danh sách (Dropdown không cho phép nhập tự do).
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            // Trả về danh sách giá trị trong Dropdown
            return new StandardValuesCollection(new string[] { "Option1", "Option2", "Option3" });
        }
    }

    [TypeConverter(typeof(NestedDictionaryWrapperConverter))]
    public class NestedDictionaryWrapper
    {
        private Dictionary<string, Dictionary<string, object>> _dictionary;

        public NestedDictionaryWrapper(Dictionary<string, Dictionary<string, object>> dictionary)
        {
            _dictionary = dictionary;
        }
        public object GetValue(string key1, string key2)
        {
            if (_dictionary.ContainsKey(key1) && _dictionary[key1] is Dictionary<string, object> item1Dict)
            {
                if (item1Dict.ContainsKey(key2))
                {
                    return item1Dict[key2];
                }
                return true;
            }
            return null;
        }

        [Editor(typeof(ComboBoxEditor), typeof(UITypeEditor))]
        public bool SetValue(string ParentKey, string ChildKey)
        {
            try
            {

                Dictionary<string, object> tmpDict = new Dictionary<string, object>();
                foreach (var key in _dictionary.Keys)
                {
                    // Kiểm tra nếu giá trị là một Dictionary cấp 2
                    if (_dictionary[key] is Dictionary<string, object> nestedDict)
                    {
                        foreach (var key1 in nestedDict.Keys)
                        {
                            tmpDict.Add(key1, nestedDict[key1]);
                        }
                    }
                    else
                    {
                        //
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        [Browsable(false)]
        [Editor(typeof(ComboBoxEditor), typeof(UITypeEditor))]
        public Dictionary<string, Dictionary<string, object>> Dictionary => _dictionary;

        public object this[string key]
        {
            get => _dictionary.ContainsKey(key) ? _dictionary[key] : null;
            set
            {
                if (_dictionary.ContainsKey(key))
                    _dictionary[key] = value;
                else
                    _dictionary.Add(key, value);
            }
        }
        public bool Update(string key1, string key2, object newValue)
        {
            if (_dictionary.ContainsKey(key1) && _dictionary[key1] is Dictionary<string, object> item1Dict)
            {
                if (item1Dict.ContainsKey(key2))
                {
                    item1Dict[key2] = newValue;  // Cập nhật giá trị Key1 của Item1
                }
                return true;
            }
            return false;
        }

        public bool Add(string key1, string key2, object newValue)
        {
            if (!_dictionary.ContainsKey(key1))
            {
                _dictionary.Add(key1, new Dictionary<string, object>());
            }
            if (_dictionary[key1] is Dictionary<string, object> item1Dict)
            {
                item1Dict[key2] = newValue;
            }
            return false;
        }
    }



    public class NestedDictionaryWrapperConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                // Trả về một chuỗi trống hoặc mô tả tùy chỉnh
                return "";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var wrapper = (NestedDictionaryWrapper)value;
            var properties = new List<PropertyDescriptor>();

            foreach (var key in wrapper.Dictionary.Keys)
            {
                var propValue = wrapper.Dictionary[key];

                // Kiểm tra nếu giá trị là một Dictionary cấp 2
                if (propValue is Dictionary<string, object> nestedDict)
                {
                    // Bao bọc Dictionary cấp 2 trong một DictionaryWrapper
                    var nestedWrapper = new NestedDictionaryWrapper(nestedDict);
                    properties.Add(new DictionaryPropertyDescriptor(wrapper, key, nestedWrapper));
                }
                else
                {
                    properties.Add(new DictionaryPropertyDescriptor(wrapper, key));
                }
            }

            return new PropertyDescriptorCollection(properties.ToArray());
        }
    }

    public class DictionaryPropertyDescriptor : PropertyDescriptor
    {
        private NestedDictionaryWrapper _wrapper;
        private string _key;
        private object _value;

        public DictionaryPropertyDescriptor(NestedDictionaryWrapper wrapper, string key, object value = null) : base(key, null)
        {
            _wrapper = wrapper;
            _key = key;
            _value = value ?? _wrapper[key];
        }

        public override Type ComponentType => typeof(NestedDictionaryWrapper);

        public override bool IsReadOnly => false;

        public override Type PropertyType => _value.GetType();

        public override bool CanResetValue(object component) => false;

        public override object GetValue(object component) => _value;

        public override void SetValue(object component, object value)
        {
            if (_value is NestedDictionaryWrapper nestedWrapper)
            {
                // Nếu là Dictionary cấp 2, cập nhật giá trị
                _wrapper[_key] = value;
            }
            else
            {
                _wrapper[_key] = value;
            }
        }

        public override void ResetValue(object component) { }

        public override bool ShouldSerializeValue(object component) => false;
        public override object GetEditor(Type context)
        {
            // Áp dụng ComboBoxEditor cho các dictionary cấp 2
            if (_value is Dictionary<string, object> || _value is NestedDictionaryWrapper)
            {
                return base.GetEditor(context);  // Sử dụng ComboBoxEditor cho tất cả các Dictionary
            }
            return new ComboBoxEditor();  // Sử dụng ComboBoxEditor cho tất cả các Dictionary

            //return base.GetEditor(context);
        }
    }

    public class ChildPropertyConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                // Trả về một chuỗi trống hoặc mô tả tùy chỉnh
                return "";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
    public class ModelTypeConverter : TypeConverter
    {
        private readonly string _brand;

        private static readonly Dictionary<string, List<string>> BrandToModels = new Dictionary<string, List<string>>()
        {
            { "Toyota", new List<string> { "Corolla", "Camry", "RAV4" } },
            { "Honda", new List<string> { "Civic", "Accord", "CR-V" } },
            { "Ford", new List<string> { "Focus", "Mustang", "Explorer" } }
        };

        public ModelTypeConverter(string brand)
        {
            _brand = brand;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string model)
            {
                return model;
            }
            return base.ConvertFrom(context, culture, value);
        }

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
            return new StandardValuesCollection(BrandToModels[_brand]);
        }
    }
}
