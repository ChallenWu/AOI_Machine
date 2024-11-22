using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro.apps.Views.UserControls
{
    public partial class ucBlockBasic : UserControl
    {
        // variables store to dragg
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastControl;
        private Point originalPanelLocationR;

        // define a dragg panel
        private Panel draggablePanel;

        //
        private Image originalIconL;
        private Image originalIconR;
        private Size originalSizeL;
        private Size originalSizeR;
        private Size originalPanelSizeR;
        private Size originalPanelSizeL;

        public ucBlockBasic()
        {
            InitializeComponent();
            originalIconL = lblR.Image;
            originalSizeL = lblR.Image.Size;

            originalIconR = lblR.Image;
            originalSizeR = lblR.Image.Size;

            originalPanelSizeR = pnHeaderBlock.Size;
            originalPanelLocationR = pnHeaderBlock.Location;
            draggablePanel = pnHeaderBlock;
        }

        private void lblR_MouseEnter(object sender, EventArgs e)
        {
            // Tăng kích thước icon khi di chuột vào
            int zoomFactor = 2; // Hệ số zoom
            lblR.Image = new Bitmap(originalIconR, new Size(originalSizeL.Width * zoomFactor, originalSizeL.Height * zoomFactor));
            pnHeaderBlock_MouseEnter(sender, e);

        }

        private void lblR_MouseLeave(object sender, EventArgs e)
        {
            // Khôi phục icon về kích thước ban đầu khi di chuột ra
            lblR.Image = originalIconR;
        }

        private void lblL_MouseEnter(object sender, EventArgs e)
        {
            // Tăng kích thước icon khi di chuột vào
            int zoomFactor = 2; // Hệ số zoom
            lblL.Image = new Bitmap(originalIconL, new Size(originalSizeR.Width * zoomFactor, originalSizeR.Height * zoomFactor));
            pnHeaderBlock_MouseEnter(sender, e);
        }

        private void lblL_MouseLeave(object sender, EventArgs e)
        {
            lblL.Image = originalIconL;
        }

        private void lblR_Click(object sender, EventArgs e)
        {

        }

        private void lblL_Click(object sender, EventArgs e)
        {

        }

        private void pnHeaderBlock_MouseLeave(object sender, EventArgs e)
        {
            pnHeaderBlock.Size = originalPanelSizeR;
            pnHeaderBlock.Location = originalPanelLocationR;
        }

        private void pnHeaderBlock_MouseEnter(object sender, EventArgs e)
        {
            pnHeaderBlock.Size = new Size(pnHeaderBlock.Width+2, pnHeaderBlock.Height+2);
            pnHeaderBlock.Location = new Point(pnHeaderBlock.Location.X - 1, pnHeaderBlock.Location.Y-1);
        }

        private void DraggablePanel_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursor = Cursor.Position;
            lastControl = this.Location;
        }

        private void DraggablePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var diff = new Point(Cursor.Position.X - lastCursor.X, Cursor.Position.Y - lastCursor.Y);
                this.Location = new Point(lastControl.X + diff.X, lastControl.Y + diff.Y);
            }
        }

        private void DraggablePanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
