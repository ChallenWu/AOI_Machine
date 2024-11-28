using System;
using System.Windows.Forms;


namespace OVisionPro
{
    public class CustomPropertyGrid : PropertyGrid
    {
        // Tùy chỉnh PropertyGrid tại đây nếu cần
        public CustomPropertyGrid()
        {
            // Ví dụ: Bạn có thể thay đổi một số thuộc tính của PropertyGrid tại đây.
            this.PropertySort = PropertySort.Categorized;  // Thay đổi cách sắp xếp các thuộc tính.
        }
    }
}
