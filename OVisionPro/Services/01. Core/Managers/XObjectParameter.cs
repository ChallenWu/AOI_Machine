using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Linq;
using System.Runtime.Remoting.Contexts;

namespace OVisionPro
{
    public class XObjectCVParameters
    {
        private static int Xcount = 0;
        [Category("Tool Info")]
        [DisplayName("Tool ID")]
        [Description("ID of tool")]
        public string toolID { get; set; }

        [Category("Tool Pos")]
        [DisplayName("X")]
        [Description("Store information of this tool.")]
        public int posX { get; set; }

        [Category("Tool Pos")]
        [DisplayName("Y")]
        [Description("Store information of this tool.")]
        public int posY { get; set; }

        private NestedDictionaryWrapper _parameters;

        // Hành động sẽ được gọi khi parameters thay đổi
        public Action<NestedDictionaryWrapper> OnParametersChanged;

        [Category("Tool Parameters")]
        [DisplayName("Run Parameters")]
        [Description("Store parameters of this tool.")]
        public NestedDictionaryWrapper Parameters
        {
            get { return _parameters; }
            set
            {
                _parameters = value;
                if (Xcount != XParameterManager.Instance.ucBlockBasicControllers.Count)
                {
                    Xcount = XParameterManager.Instance.ucBlockBasicControllers.Count;
                    PropertyChanged?.Invoke();   
                }
                // Gọi hành động nếu có đăng ký
                
            }
        }

        // Sự kiện PropertyChanged
        public Action PropertyChanged;

        [Category("Tool Parameters")]
        [DisplayName("ROI Parameters")]
        [Description("Store parameters of this tool.")]
        public NestedDictionaryWrapper ROIPositions { get; set; }
        public Dictionary<string, NestedDictionaryWrapper> DictNestAll = new Dictionary<string, NestedDictionaryWrapper>() { };

    }
    public class XObjectCVParamsBasicTool
    {
        [Category("Tool Info")]
        [DisplayName("Tool ID")]
        [Description("ID of tool")]
        public string toolID { get; set; }

        [Category("Tool Info")]
        [DisplayName("Tool name")]
        [Description("Name of tool")]
        public string toolName { get; set; }

        [Category("Tool Info")]
        [DisplayName("Tool Parent Node")]
        [Description("Parent Node of tool")]
        public string toolParent { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }

        private NestedDictionaryWrapper _parameters;

        // Hành động sẽ được gọi khi parameters thay đổi
        public Action<NestedDictionaryWrapper> OnParametersChanged;

        [Category("Tool Parameters")]
        [DisplayName("Run Parameters")]
        [Description("Store parameters of this tool.")]
        public NestedDictionaryWrapper Parameters
        {
            get { return _parameters; }
            set
            {
                _parameters = value;

                // Gọi hành động nếu có đăng ký
                PropertyChanged?.Invoke("");
            }
        }

        // Sự kiện PropertyChanged
        public Action<string> PropertyChanged;

        [Category("Tool Parameters")]
        [DisplayName("ROI Parameters")]
        [Description("Store parameters of this tool.")]
        public NestedDictionaryWrapper ROIPositions { get; set; }
        public Dictionary<string, NestedDictionaryWrapper> DictNestAll = new Dictionary<string, NestedDictionaryWrapper>() { };
   
    }

    public class MyColorEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown; // Modal style để hiển thị ColorDialog
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = Color.Beige;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                value = colorDialog.Color;
            }
            return value; // Trả về giá trị ban đầu nếu không chọn màu
        }
    }
    // Tạo một trình chỉnh sửa để hiển thị ComboBox
    class ComboBoxEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider == null) return value;

            IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (editorService == null) return value;

            // Tạo một ComboBox và thêm các mục vào
            var comboBox = new ComboBox();

            var fname = context.PropertyDescriptor.Name;
            string[] res_cv_func = fname.Split(new string[] { "_1_" }, StringSplitOptions.None);
            string cvname = res_cv_func[0];
            object[] _limit = XParameter.Instance.GetLimitParam(cvname, fname);
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Items.AddRange(_limit);
            comboBox.SelectedIndex = 0;
            // Chọn giá trị hiện tại nếu có
            if (value != null)
                comboBox.SelectedItem = value;

            // Hiển thị ComboBox trong PropertyGrid
            editorService.DropDownControl(comboBox);

            // Trả về giá trị đã chọn
            return comboBox.SelectedItem.ToString();
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // Cung cấp kiểu DropDown để mở ComboBox trực tiếp
            return UITypeEditorEditStyle.DropDown;
        }
    }
    public class ComboBoxColorEditor : UITypeEditor
    {
        // Lớp đại diện cho màu sắc và tên
        public class ColorItem
        {
            public string Name { get; set; }
            public Color Color { get; set; }

            public ColorItem(string name, Color color)
            {
                Name = name;
                Color = color;
            }
        }

        private ComboBox comboBox;
        private static List<ColorItem> colorItems;

        public ComboBoxColorEditor(string fname)
        {
            // Khởi tạo danh sách màu sắc
            // Khởi tạo danh sách màu sắc
            colorItems = new List<ColorItem>();
            string[] res_cv_func = fname.Split(new string[] { "_1_" }, StringSplitOptions.None);
            string cvname = res_cv_func[0];
            object[] _limit = XParameter.Instance.GetLimitParam(cvname, fname);
            foreach (var key1 in _limit)
            {
                KnownColor enumValue = (KnownColor)Enum.Parse(typeof(KnownColor), key1.ToString());
                colorItems.Add(new ColorItem(key1.ToString(), Color.FromKnownColor(enumValue)));
            }
        }

        // Xử lý khi chọn giá trị trong ComboBox
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider == null) return value;

            IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (editorService == null) return value;

            // Tạo mới ComboBox nếu chưa có
            if (comboBox != null) comboBox?.Dispose();

            comboBox = new ComboBox();
            comboBox.DrawMode = DrawMode.OwnerDrawFixed; // Cấu hình ComboBox để vẽ tùy chỉnh
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Chế độ chọn trong danh sách

            // Đăng ký sự kiện DrawItem
            comboBox.DrawItem += ComboBox_DrawItem;

            // Gán dữ liệu vào ComboBox
            comboBox.DataSource = colorItems;
            comboBox.DisplayMember = "Name"; // Hiển thị tên màu

            // Nếu giá trị đã được chọn, đặt lại giá trị trong ComboBox
            if (value != null)
            {
                // Tìm mục có tên màu phù hợp trong danh sách
                comboBox.SelectedItem = colorItems.Find(x => x.Name == value.ToString());
            }

            // Hiển thị ComboBox trong PropertyGrid
            editorService.DropDownControl(comboBox);

            // Trả về tên màu (thuộc tính 'Name' của đối tượng ColorItem)
            string valc = (comboBox.SelectedItem as ColorItem)?.Name;
            return valc; // Trả về tên màu đã chọn
        }


        // Xử lý sự kiện vẽ tùy chỉnh trong ComboBox
        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Lấy đối tượng ColorItem tại chỉ mục hiện tại
            ColorItem item = (ColorItem)comboBox.Items[e.Index];

            // Vẽ nền của mục
            e.DrawBackground();

            // Vẽ ô màu bên trái
            using (Brush brush = new SolidBrush(item.Color))
            {
                e.Graphics.FillRectangle(brush, e.Bounds.Left + 2, e.Bounds.Top + 2, 20, 20); // Vẽ ô màu có kích thước 20x20
            }

            // Vẽ tên màu bên phải ô màu
            using (Brush textBrush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(item.Name, e.Font, textBrush, e.Bounds.Left + 30, e.Bounds.Top); // Dịch chuyển tên sang phải
            }

            // Vẽ viền mục
            e.DrawFocusRectangle();
        }

        // Cung cấp kiểu vẽ là DropDown
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
    }
    class NumericUpDownEditor : UITypeEditor
    {

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider == null) return value;

            IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (editorService == null) return value;

            // Tạo một NumericUpDown và thiết lập các thuộc tính
            var fname = context.PropertyDescriptor.Name;
            string[] res_cv_func = fname.Split(new string[] { "_1_" }, StringSplitOptions.None);
            string cvname = res_cv_func[0];


            // Tạo ComboBox
            using (NumericUpDown numeric = new NumericUpDown())
            {
                object[] _limit = XParameter.Instance.GetLimitParam(cvname, fname);
                numeric.Minimum = Convert.ToDecimal(_limit[0]);
                numeric.Maximum = Convert.ToDecimal(_limit[1]);
                numeric.Increment = Convert.ToDecimal(_limit[2]);

                if (_limit[2].ToString() == "0.1")
                {
                    numeric.Increment = 0.1M;
                    numeric.DecimalPlaces = 1;
                }
                _limit = null;
                numeric.Value = Convert.ToDecimal(value);
                // Hiển thị NumericUpDown trong PropertyGrid
                editorService.DropDownControl(numeric);
                value = numeric.Value;
            }
            fname = null;
            res_cv_func = null;
            //GC.Collect();  // Yêu cầu Garbage Collector thu hồi bộ nhớ ngay lập tức
            //GC.WaitForPendingFinalizers();
            // Trả về giá trị đã chọn
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown; // Chỉ sử dụng Dropdown cho NumericUpDown
        }
    }


    [TypeConverter(typeof(NestedDictionaryWrapperConverter))]
    public class NestedDictionaryWrapper : INotifyPropertyChanged
    {
        private Dictionary<string, object> _dictionary;
        static bool IsNumber(string input)
        {
            return int.TryParse(input, out _) || double.TryParse(input, out _) || decimal.TryParse(input, out _);
        }
        public NestedDictionaryWrapper(Dictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool SetValue(string parentKey, string childKey, object value)
        {
            if (_dictionary.ContainsKey(parentKey) && _dictionary[parentKey] is Dictionary<string, object> nestedDict)
            {
                nestedDict[childKey] = value;
                OnPropertyChanged(parentKey); // Thông báo thay đổi cho node cha
                return true;
            }
            return false;
        }

        public bool Update(string key1, string key2, object newValue)
        {
            if (_dictionary.ContainsKey(key1) && _dictionary[key1] is Dictionary<string, object> item1Dict)
            {
                if (item1Dict.ContainsKey(key2))
                {
                    item1Dict[key2] = newValue;  // Cập nhật giá trị của item con
                    OnPropertyChanged(key1);  // Thông báo thay đổi cho node cha
                    return true;
                }
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
                OnPropertyChanged(key1);  // Thông báo thay đổi cho node cha
            }
            return false;
        }

        [Browsable(false)]
        public Dictionary<string, object> Dictionary => _dictionary;

        public object this[string key]
        {
            get => _dictionary.ContainsKey(key) ? _dictionary[key] : null;
            set
            {
                if (int.TryParse(value.ToString(), out _))
                {
                    value = int.Parse(value.ToString());
                }else if (double.TryParse(value.ToString(), out _))
                {
                    value = double.Parse(value.ToString());  
                }else if (decimal.TryParse(value.ToString(), out _))
                {
                    value = decimal.Parse(value.ToString());
                }
                if (_dictionary.ContainsKey(key))
                    _dictionary[key] = value;
                else
                    _dictionary.Add(key, value);
                OnPropertyChanged(key); // Thông báo thay đổi cho node cha
            }
        }
    }

    class NestedDictionaryWrapperConverter : ExpandableObjectConverter
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

    class DictionaryPropertyDescriptor : PropertyDescriptor
    {
        private NestedDictionaryWrapper _wrapper;
        private string _key;
        private string new_displayName;
        private object _value;
        private static ComboBoxEditor _comboBoxEditor = null;
        private static NumericUpDownEditor _numericUpDownEditor = null;
        private static ComboBoxColorEditor _colorEditor = null;
        private bool _isread = false;

        public DictionaryPropertyDescriptor(NestedDictionaryWrapper wrapper, string key, object value = null) : base(key, null)
        {
            _wrapper = wrapper;
            string[] res_cv_func = key.Split(new string[] { "_1_" }, StringSplitOptions.None);
            new_displayName = res_cv_func.Last();
            _key = key;
            _value = value ?? _wrapper[key];
            if (_key.StartsWith("ROI2") && _key.Contains("BlockID")) _isread = true;
            if (_key.Contains("BlockParentKey") && (_key.StartsWith("ROI1") || _key.StartsWith("ROI2"))) _isread = true;
        }

        public override string DisplayName
        {
            get
            {
                // Ở đây bạn có thể thay đổi cách hiển thị DisplayName, ví dụ thêm tiền tố "Custom"
                return new_displayName;
            }
        }

        public override Type ComponentType => typeof(NestedDictionaryWrapper);

        public override bool IsReadOnly => _isread;

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
        static bool IsNumber(string input)
        {
            return int.TryParse(input, out _) || double.TryParse(input, out _) || decimal.TryParse(input, out _);
        }
        public override void ResetValue(object component) { }

        public override bool ShouldSerializeValue(object component) => false;
        public override object GetEditor(Type context)
        {
            // Áp dụng ComboBoxEditor cho các dictionary cấp 2
            if (_value is Dictionary<string, object> || _value is NestedDictionaryWrapper)
            {
                return base.GetEditor(context);
            }
            if (_key.Contains("Scalar"))
            {
                if (_colorEditor == null)
                {
                    _colorEditor = new ComboBoxColorEditor(_key);
                }
                return _colorEditor;
            }
            if (_key.Contains("CharArray"))
            {
                return base.GetEditor(context);
            }
            if (_key.Contains("BlockID") && _key.StartsWith("ROI2")) return base.GetEditor(context);
            if (_key.Contains("BlockName"))
            {
                if (_key.StartsWith("ROI2")) return base.GetEditor(context);
                object[] _limit = XParameter.Instance.GetLimitParam(_key, "BlockName");
                if (_key.StartsWith("ROI1") && _limit.Length <= 0)
                {
                    return base.GetEditor(context);
                }
            }
            if (IsNumber(_value.ToString()))
            {
                if (_numericUpDownEditor == null)
                {
                    _numericUpDownEditor = new NumericUpDownEditor();
                }
                return _numericUpDownEditor;
            }
            if (_comboBoxEditor == null)
            {
                _comboBoxEditor = new ComboBoxEditor();
            }
            return _comboBoxEditor;
            //return new ComboBoxEditor();  // Sử dụng ComboBoxEditor cho tất cả các Dictionary

            //return base.GetEditor(context);
        }

        //public override object GetEditor(Type context)
        //{
        //    // Tạo và cấu hình ComboBox trực tiếp trong đây
        //    if (_value is Dictionary<string, object> || _value is NestedDictionaryWrapper)
        //    {
        //        var comboBox = new ComboBox();
        //        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

        //        // Giả sử chúng ta đang tạo một ComboBox với các lựa chọn từ _wrapper

        //        if (_value is NestedDictionaryWrapper nestedWrapper)
        //        {
        //            // Nếu là Dictionary cấp 2, cập nhật giá trị
        //            foreach (var key in nestedWrapper.Dictionary.Keys)
        //            {
        //                comboBox.Items.Add(key);
        //            }
        //        }

        //        comboBox.SelectedItem = _value;

        //        return comboBox;  // Trả về ComboBox trực tiếp
        //    }

        //    return base.GetEditor(context); // Nếu không phải Dictionary, trả về trình biên tập mặc định
        //}

    }
}
