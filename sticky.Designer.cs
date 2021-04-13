using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sticky_Notes
{
    partial class Sticky
    {
        private void InitializeComponent()
        {
            this.Note = new RichTextBox();
            this.Title = new Border();
            this.MenuBar = new Border();
            this.MenuLine = new PictureBox();

            // Size
            int height = 311;
            int width = 304;
            int titleheight = 33;
            int menubarheight = 38;

            // Yellow
            this.TitleColor = Color.FromArgb(255, 242, 171);
            this.NoteColor = Color.FromArgb(255, 247, 209);

            // 
            // Title
            // 
            this.Title.BackColor = TitleColor;
            this.Title.Dock = DockStyle.Top;
            this.Title.TabStop = false;
            this.Title.Size = new Size(width, titleheight);
            this.Title.MouseDoubleClick += new MouseEventHandler(this.Title_MouseDoubleClick);
            this.Title.MouseDown += new MouseEventHandler(this.Title_MouseDown);
            this.Title.MouseMove += new MouseEventHandler(this.Title_MouseMove);
            this.Title.MouseUp += new MouseEventHandler(this.Title_MouseUp);
            //
            // Title LeftMenu
            //
            this.Title.Add(this.Title.LeftMenu, 1, this.Title.Size.Height, true);
            // Add
            this.Title.LeftMenu[0].Text = "+";
            this.Title.LeftMenu[0].Click += new EventHandler(this.StickyAdd_Click);
            //
            // Title RightMenu
            //
            this.Title.Add(this.Title.RightMenu, 2, this.Title.Size.Height, false);
            // Clear
            this.Title.RightMenu[0].Text = "X";
            this.Title.RightMenu[0].Click += new EventHandler(this.StickyClose_Click);
            // Save
            this.Title.RightMenu[1].Text = "...";
            this.Title.RightMenu[1].Click += new EventHandler(this.NoteSave_Click);
            // 
            // MenuBar
            // 
            this.MenuBar.BackColor = NoteColor;
            this.MenuBar.Dock = DockStyle.Bottom;
            this.MenuBar.TabStop = false;
            this.MenuBar.Location = new Point(0, 273);
            this.MenuBar.Size = new Size(width, menubarheight);
            this.MenuBar.Controls.Add(this.MenuLine);
            // 
            // MenuLine
            // 
            this.MenuLine.BackColor = Color.FromArgb(237, 230, 194);
            this.MenuLine.TabStop = false;
            this.MenuLine.Location = new Point(0, 0);
            this.MenuLine.Size = new Size(width, 1);
            //
            // MenuBar Menu
            //
            this.MenuBar.Add(this.MenuBar.LeftMenu, 6, this.MenuBar.Size.Height, true);
            //
            // Note
            //
            this.Note.BackColor = NoteColor;
            this.Note.BorderStyle = BorderStyle.None;
            this.Note.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.Note.SelectionIndent = 12;
            this.Note.SelectionRightIndent = 16;
            this.Note.Font = new Font("굴림", 10F);
            this.Note.Location = new Point(0, titleheight);
            this.Note.Size = new Size(width, MenuBar.Location.Y - titleheight);
            this.Note.EnableAutoDragDrop = true;
            this.Note.TabStop = false;
            // 
            // Sticky
            // 
            this.BackColor = NoteColor;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.ClientSize = new Size(width, height);
            this.MinimumSize = new Size(208, 108);
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.Controls.Add(this.MenuBar);
            this.Controls.Add(this.Note);
            this.Controls.Add(this.Title);
            this.Load += new EventHandler(this.Sticky_Load);
            this.Activated += new EventHandler(this.Sticky_Activated);
            this.Deactivate += new EventHandler(this.Sticky_Deactivate);
            this.SizeChanged += new EventHandler(this.Sticky_SizeChanged);
        }

        private RichTextBox Note;
        private Border Title;
        private Border MenuBar;
        private PictureBox MenuLine;
        private Color TitleColor;
        private Color NoteColor;
    }

    class Border : Panel
    {
        public void Add(List<MenuButton> menu, int lenght, int size, bool way)
        {
            int width = this.Size.Width;
            for (int index = 0; index < lenght; index++)
            {
                menu.Add(new MenuButton());
                menu[index].Size = new Size(size, this.Size.Height);
                int pointWay = (way) ? size * index : width - size - (size * index); //(way) ? Left : Right
                menu[index].Location = new Point(pointWay, 0);
                this.Controls.Add(menu[index]);
            }
        }

        public List<MenuButton> LeftMenu = new List<MenuButton>();
        public List<MenuButton> RightMenu = new List<MenuButton>();
    }

    class MenuButton : Button
    {
        public MenuButton()
        {
            this.TabStop = false;
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = FlatStyle.Flat;
            this.ForeColor = Color.Black;
        }
    }

    class StatusMessageBox : Form
    {
        public StatusMessageBox()
        {
            this.Keep = new MenuButton();
            this.Clear = new MenuButton();
            this.Message = new Label();

            //
            // Status
            //
            this.BackColor = Color.FromArgb(255, 247, 209);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.Opacity = 0.75;
            this.Controls.Add(this.Keep);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Message);
            this.Load += new EventHandler(this.StatusMessageBox_Load);
        }

        private Point CenterPoint(Size center, Size button, int width, int height)
        {
            int px, py;
            px = (center.Width / 2) - (button.Width / 2) + width;
            py = (center.Height / 2) - (button.Height / 2) + height;

            return new Point(px, py);
        }

        private void StatusMessageBox_Load(object sender, EventArgs e)
        {
            // Buton Location
            int width = 40;
            int height = 20;

            //
            // Keep
            //
            this.Keep.FlatAppearance.BorderSize = 1;
            this.Keep.Location = CenterPoint(this.Size, this.Keep.Size, width, height);
            this.Keep.Text = "유지";
            this.Keep.Click += new EventHandler(this.Keep_Click);
            //
            // Clear
            //
            this.Clear.FlatAppearance.BorderSize = 1;
            this.Clear.Location = CenterPoint(this.Size, this.Clear.Size, -width, height);
            this.Clear.Text = "삭제";
            this.Clear.Click += new EventHandler(this.Clear_Click);
            //
            // Message
            //
            this.Message.Size = new Size(240, 60);
            this.Message.Location = CenterPoint(this.Size, this.Message.Size, 0, -10);
            this.Message.TextAlign = ContentAlignment.MiddleCenter;
            this.Message.Font = new Font("굴림", 10f);
            this.Message.Text = "이 메모를 삭제하시겠습니까?";
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void Keep_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private MenuButton Keep;
        private MenuButton Clear;
        private Label Message;
    }
}