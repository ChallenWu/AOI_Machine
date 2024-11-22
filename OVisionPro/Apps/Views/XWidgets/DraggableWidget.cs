using System;
using System.Drawing;
using System.Windows.Forms;


namespace AoIMachinE.Apps.Views.XWidgets
{
    public class DraggableWidget : Panel
    {
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        public DraggableWidget()
        {
            this.BackColor = Color.LightBlue;
            this.Size = new Size(100, 100);
            this.MouseDown += DraggableWidget_MouseDown;
            this.MouseMove += DraggableWidget_MouseMove;
            this.MouseUp += DraggableWidget_MouseUp;
        }

        private void DraggableWidget_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursor = Cursor.Position;
            lastForm = this.Location;
        }

        private void DraggableWidget_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var diff = new Point(Cursor.Position.X - lastCursor.X, Cursor.Position.Y - lastCursor.Y);
                this.Location = new Point(lastForm.X + diff.X, lastForm.Y + diff.Y);
            }
        }

        private void DraggableWidget_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
