using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace AoIMachinE.Apps.Views.XWidgets
{
    class PannelLink: Panel
    {
        private Label leftLabel;
        private Label rightLabel;
        private PictureBox leftIcon;
        private PictureBox rightIcon;

        public PannelLink()
        {
            this.Size = new Size(400, 50);
            this.BackColor = Color.LightGray;

     
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "AoIMachinE.Assets.Icons.link.png";
            Image LImage;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                LImage = Image.FromStream(stream);
            }
            // Khởi tạo Icon bên trái
            leftIcon = new PictureBox
            {
                Size = new Size(18, 18),
                Location = new Point(5, 13), // Canh lề trái
                Image = LImage, // Đường dẫn icon trái
                SizeMode = PictureBoxSizeMode.Zoom
            };

            // Khởi tạo Label bên trái
            leftLabel = new Label
            {
                AutoSize = true,
                Location = new Point(leftIcon.Right + 5, 15),
                Text = "Left Icon Description"
            };

            // Khởi tạo Icon bên phải
            rightIcon = new PictureBox
            {
                Size = new Size(18, 18),
                Location = new Point(this.Width - 29, 13), // Canh lề phải
                //Image = Image.FromFile("path/to/right_icon.png"), // Đường dẫn icon phải
                SizeMode = PictureBoxSizeMode.Zoom,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // Khởi tạo Label bên phải
            rightLabel = new Label
            {
                AutoSize = true,
                Location = new Point(rightIcon.Left - 5 - 100, 15), // Căn vị trí sao cho label bên trái icon phải
                Text = "Right Icon Description",
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // Thêm các điều khiển vào Panel
            this.Controls.Add(leftIcon);
            this.Controls.Add(leftLabel);
            this.Controls.Add(rightIcon);
            this.Controls.Add(rightLabel);
        }
    }
}
