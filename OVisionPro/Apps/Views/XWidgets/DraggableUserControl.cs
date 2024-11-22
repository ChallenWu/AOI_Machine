using System;
using System.Drawing;
using System.Windows.Forms;


namespace AoIMachinE.Apps.Views.XWidgets
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DraggableUserControl : UserControl
    {
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastControl;

        private Panel draggablePanel;

        public DraggableUserControl()
        {
            this.Size = new Size(200, 200);
            this.BackColor = Color.LightGray;

            // Tạo Panel trong UserControl
            draggablePanel = new Panel
            {
                Size = new Size(100, 100),
                BackColor = Color.LightBlue,
                Location = new Point(50, 50)
            };
            this.Controls.Add(draggablePanel);

            // Gắn sự kiện chuột cho Panel
            draggablePanel.MouseDown += DraggablePanel_MouseDown;
            draggablePanel.MouseMove += DraggablePanel_MouseMove;
            draggablePanel.MouseUp += DraggablePanel_MouseUp;
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
