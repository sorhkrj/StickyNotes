using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sticky_Notes
{
    partial class Sticky : Form
    {
        public Sticky()
        {
            InitializeComponent();
        }

        // Sticky Events

        public StickyEventHandler CountEvent;
        public StickyEventHandler ClearEvent;
        public DataEventHandler SaveEvent;

        bool VMenuBar;

        private void Sticky_Load(object sender, EventArgs e)
        {
            VMenuBar = (this.Size.Width < 270 || this.Size.Height < 200) ? false : true;
            this.MenuBar.Visible = VMenuBar;

            if (!File.Exists(this.Name))
            {
                this.Note.SaveFile(this.Name);
            }
            this.Note.LoadFile(this.Name);
        }

        private void Sticky_Activated(object sender, EventArgs e)
        {
            this.Title.Visible = true;
            this.MenuBar.Visible = MenuBar_SizeCheck();
            int height = (VMenuBar) ? this.MenuBar.Location.Y - this.Title.Size.Height : this.ClientSize.Height - this.Title.Size.Height;
            this.Note.Size = new Size(this.ClientSize.Width, height);
            this.Note.SelectionRightIndent = 16;
        }

        private void Sticky_Deactivate(object sender, EventArgs e)
        {
            this.Title.Visible = false;
            this.MenuBar.Visible = false;
            int height = this.ClientSize.Height - this.Title.Size.Height;
            this.Note.Size = new Size(this.Size.Width, height);
            this.Note.SelectionRightIndent = 30;
        }

        private void Sticky_SizeChanged(object sender, EventArgs e)
        {
            int width = this.Title.Size.Width;
            for(int index = 0; index < this.Title.RightMenu.Count; index++)
            {
                int size = this.Title.RightMenu[index].Size.Width;
                int pointWay = width - size - (size * index);
                this.Title.RightMenu[index].Location = new Point(pointWay, 0);
            }
            int height = (VMenuBar) ? this.MenuBar.Location.Y - this.Title.Size.Height : this.ClientSize.Height - this.Title.Size.Height;
            this.Note.Size = new Size(this.ClientSize.Width, height);

            this.MenuLine.Size = new Size(this.Size.Width, 1);
            this.MenuBar.Visible = MenuBar_SizeCheck();
        }

        // MenuBar.Visible
        private bool MenuBar_SizeCheck()
        {
            VMenuBar = (this.Size.Width < 270 || this.Size.Height < 200) ? false : true;
            return VMenuBar;
        }

        // Title Events

        bool BMove;
        int MValX, MValY;
        readonly int Win10X = 8, Win10Y = 8;

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            BMove = true;
            MValX = e.X;
            MValY = e.Y;
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if(BMove == true)
            {
                this.SetDesktopLocation(MousePosition.X - MValX - Win10X, MousePosition.Y - MValY - Win10Y);
            }
        }

        private void Title_MouseUp(object sender, MouseEventArgs e)
        {
            BMove = false;
        }

        private void Title_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        // Title Button Events

        private void StickyAdd_Click(object sender, EventArgs e)
        {
            this.CountEvent(1);
        }

        private void StickyClose_Click(object sender, EventArgs e)
        {
            StatusMessageBox statusMessage = new StatusMessageBox
            {
                ClientSize = this.ClientSize
            };
            statusMessage.ShowDialog();
            if (statusMessage.DialogResult == DialogResult.Yes)
            {
                statusMessage.Close();
                Note_Clear(this.Name);
                this.Close();
            }
        }

        private void Note_Clear(string file)
        {
            File.Delete(file);
            this.ClearEvent(file);
        }

        private void NoteSave_Click(object sender, EventArgs e)
        {
            this.Note.SaveFile(this.Name);
            this.SaveEvent(this.Name, this.Location, this.Size);
        }
    }
}