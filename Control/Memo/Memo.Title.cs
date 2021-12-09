using System;
using System.Drawing;
using System.Windows.Forms;
using StickyMemos.Manager; // DataManager.cs
using StickyMemos.Control; // MenuButton.cs, Title.cs
using StickyMemos.Graphic; // MemoColor.cs, MemoFont.cs

namespace StickyMemos
{
    partial class Memo
    {
        private void TitleComponent()
        {
            this.title = new Title();

            this.AddButton = new MenuButton();
            this.RemoveButton = new MenuButton();
            this.TitleMenuStripButton = new MenuButton();

            //
            // MenuToolTip
            //
            this.MenuToolTip.SetToolTip(this.AddButton, "새 메모");
            this.MenuToolTip.SetToolTip(this.RemoveButton, "메모 삭제");
            this.MenuToolTip.SetToolTip(this.TitleMenuStripButton, "메뉴");
            //
            // TitleMenuButton Text
            //
            this.AddButton.Text = "+";
            this.RemoveButton.Text = "X";
            this.TitleMenuStripButton.Text = "•••";
            //
            // TitleMenuStrip
            //
            this.TitleMenuStrip = new ContextMenuStrip();
            this.ColorModeItem = new ToolStripMenuItem("색 변경");
            this.Separator1 = new ToolStripSeparator();
            this.PinItem = new ToolStripMenuItem("고정");
            this.FoldItem = new ToolStripMenuItem("접기");
            this.Separator2 = new ToolStripSeparator();
            this.SaveItem = new ToolStripMenuItem("저장");
            this.RemoveItem = new ToolStripMenuItem("삭제");
            //
            // ColorModeItem
            //
            this.YellowModeItem = new ToolStripMenuItem();
            this.GreenModeItem = new ToolStripMenuItem();
            this.PinkModeItem = new ToolStripMenuItem();
            this.PurpleModeItem = new ToolStripMenuItem();
            this.BlueModeItem = new ToolStripMenuItem();
            this.GrayModeItem = new ToolStripMenuItem();
            this.BlackModeItem = new ToolStripMenuItem();
            //
            // ColorModeItem BackColor
            //
            this.YellowModeItem.BackColor = Color.FromArgb(255, 230, 110);
            this.GreenModeItem.BackColor = Color.FromArgb(161, 239, 155);
            this.PinkModeItem.BackColor = Color.FromArgb(255, 175, 223);
            this.PurpleModeItem.BackColor = Color.FromArgb(215, 175, 255);
            this.BlueModeItem.BackColor = Color.FromArgb(158, 223, 255);
            this.GrayModeItem.BackColor = Color.FromArgb(224, 224, 224);
            this.BlackModeItem.BackColor = Color.FromArgb(118, 118, 118);
            //
            // Title
            //
            this.title.Size = new Size(this.Width, title_height);

            this.title.MouseDown += new MouseEventHandler(this.Title_MouseDown);
            this.title.MouseMove += new MouseEventHandler(this.Title_MouseMove);
            this.title.MouseUp += new MouseEventHandler(this.Title_MouseUp);
            this.title.MouseDoubleClick += new MouseEventHandler(this.Title_MouseDoubleClick);
            //
            // Title LeftMenu
            //
            this.title.Add(this.title.LeftMenu, AddButton);

            this.AddButton.Click += new EventHandler(this.Add_Click);
            //
            // Title RightMenu
            //
            this.title.AddRange(this.title.RightMenu, new MenuButton[]
            {
                RemoveButton,
                TitleMenuStripButton
            });

            this.RemoveButton.Click += new EventHandler(this.Remove_Click);
            this.TitleMenuStripButton.Click += new EventHandler(this.TitleMenuStrip_Click);
            //
            // TitleMenuItem
            //
            this.TitleMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                this.ColorModeItem,
                this.Separator1,
                this.PinItem,
                this.FoldItem,
                this.Separator2,
                this.SaveItem,
                this.RemoveItem
            });

            this.PinItem.Click += new EventHandler(this.PinItem_Click);
            this.FoldItem.Click += new EventHandler(this.FoldItem_Click);
            this.SaveItem.Click += new EventHandler(this.SaveItem_Click);
            this.RemoveItem.Click += new EventHandler(this.RemoveItem_Click);
            //
            // ColorModeItem DropDownItems
            //
            this.ColorModeItem.DropDown.Items.AddRange(new ToolStripItem[]
            {
                this.YellowModeItem,
                this.GreenModeItem,
                this.PinkModeItem,
                this.PurpleModeItem,
                this.BlueModeItem,
                this.GrayModeItem,
                this.BlackModeItem
            });

            this.YellowModeItem.Click += new EventHandler(this.YellowModeItem_Click);
            this.GreenModeItem.Click += new EventHandler(this.GreenModeItem_Click);
            this.PinkModeItem.Click += new EventHandler(this.PinkModeItem_Click);
            this.PurpleModeItem.Click += new EventHandler(this.PurpleModeItem_Click);
            this.BlueModeItem.Click += new EventHandler(this.BlueModeItem_Click);
            this.GrayModeItem.Click += new EventHandler(this.GrayModeItem_Click);
            this.BlackModeItem.Click += new EventHandler(this.BlackModeItem_Click);
        }

        private Title title;

        private MenuButton AddButton;
        private MenuButton RemoveButton;
        private MenuButton TitleMenuStripButton;

        private ContextMenuStrip TitleMenuStrip;
        private ToolStripMenuItem ColorModeItem;
        private ToolStripSeparator Separator1;
        private ToolStripMenuItem PinItem;
        private ToolStripMenuItem FoldItem;
        private ToolStripSeparator Separator2;
        private ToolStripMenuItem SaveItem;
        private ToolStripMenuItem RemoveItem;

        private ToolStripMenuItem YellowModeItem;
        private ToolStripMenuItem GreenModeItem;
        private ToolStripMenuItem PinkModeItem;
        private ToolStripMenuItem PurpleModeItem;
        private ToolStripMenuItem BlueModeItem;
        private ToolStripMenuItem GrayModeItem;
        private ToolStripMenuItem BlackModeItem;

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CanMove = true;
                MouseValueX = e.X;
                MouseValueY = e.Y;
            }
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CanMove == true)
                {
                    int x = MousePosition.X - MouseValueX - win10;
                    int y = MousePosition.Y - MouseValueY - win10;
                    this.SetDesktopLocation(x, y);
                }
            }
        }

        private void Title_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CanMove == true)
                {
                    CanMove = false;
                    Data data = new Data(this.Name, this.ColorName, this.Location, this.Size);
                    this.dataManager.Save(data);
                }
            }
        }

        private bool CanMove;
        private int MouseValueX, MouseValueY;

        private void Title_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.TopMost = !this.TopMost;
                this.PinItem.Checked = (this.TopMost == true) ? true : false;
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            this.Add(this.ColorName);
            this.ActiveControl = this.note;
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            this.ClearMessage();
        }

        private void TitleMenuStrip_Click(object sender, EventArgs e)
        {
            this.TitleMenuStrip.Show(MousePosition);
            this.note.Focus();
        }

        private void PinItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            this.PinItem.Checked = (this.TopMost == true) ? true : false;
        }

        private void FoldItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Activate();
        }

        private void SaveItem_Click(object sender, EventArgs e)
        {
            this.note.SaveFile(string.Format(@"{0}/{1}", DataManager.directory, this.Name));
        }

        private void RemoveItem_Click(object sender, EventArgs e)
        {
            this.ClearMessage();
        }

        private void ColorModeChange(IColorMode mode, string colorName)
        {
            mode.change();
            this.title.BackColor = MemoColor.Title;
            this.menubar.BackColor = MemoColor.Note;
            this.menubar.Line.BackColor = MemoColor.Line;
            this.note.BackColor = MemoColor.Note;
            this.BackColor = MemoColor.Note;
            switch (colorName)
            {
                case "Black":
                    WhiteForeColor();
                    break;
                default:
                    BlackForeColor();
                    break;
            }
            this.ColorName = colorName;
        }

        private void BlackForeColor()
        {
            this.note.SelectionColor = Color.Black;
            this.note.ForeColor = Color.Black;
            this.AddButton.ForeColor = Color.Black;
            this.ForeColor = Color.Black;
            this.TitleMenuStripButton.ForeColor = Color.Black;
            this.BoldButton.ForeColor = Color.Black;
            this.ItalicButton.ForeColor = Color.Black;
            this.UnderlineButton.ForeColor = Color.Black;
            this.StrikeoutButton.ForeColor = Color.Black;
        }

        private void WhiteForeColor()
        {
            this.note.SelectionColor = Color.White;
            this.note.ForeColor = Color.White;
            this.AddButton.ForeColor = Color.White;
            this.ForeColor = Color.White;
            this.TitleMenuStripButton.ForeColor = Color.White;
            this.BoldButton.ForeColor = Color.White;
            this.ItalicButton.ForeColor = Color.White;
            this.UnderlineButton.ForeColor = Color.White;
            this.StrikeoutButton.ForeColor = Color.White;
        }

        private void YellowModeItem_Click(object sender, EventArgs e)
        {
            ColorModeChange(new YellowMode(), "Yellow");
        }

        private void GreenModeItem_Click(object sender, EventArgs e)
        {
            ColorModeChange(new GreenMode(), "Green");
        }

        private void PinkModeItem_Click(object sender, EventArgs e)
        {
            ColorModeChange(new PinkMode(), "Pink");
        }

        private void PurpleModeItem_Click(object sender, EventArgs e)
        {
            ColorModeChange(new PurpleMode(), "Purple");
        }

        private void BlueModeItem_Click(object sender, EventArgs e)
        {
            ColorModeChange(new BlueMode(), "Blue");
        }

        private void GrayModeItem_Click(object sender, EventArgs e)
        {
            ColorModeChange(new GrayMode(), "Gray");
        }

        private void BlackModeItem_Click(object sender, EventArgs e)
        {
            ColorModeChange(new BlackMode(), "Black");
        }
    }
}