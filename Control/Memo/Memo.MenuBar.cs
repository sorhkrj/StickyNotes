using System;
using System.Drawing;
using System.Windows.Forms;
using StickyMemos.Control; // MenuButton.cs, Menubar.cs
using StickyMemos.Graphic; // MenuFont.cs

namespace StickyMemos
{
    partial class Memo
    {
        private void MenubarComponent()
        {
            this.menubar = new Menubar();

            this.BoldButton = new MenuButton();
            this.ItalicButton = new MenuButton();
            this.UnderlineButton = new MenuButton();
            this.StrikeoutButton = new MenuButton();

            // Bold
            this.BoldButton.Text = "B";
            this.BoldButton.Font = new Font(this.BoldButton.Font, FontStyle.Bold);
            // Italic
            this.ItalicButton.Text = "I";
            this.ItalicButton.Font = new Font(this.ItalicButton.Font, FontStyle.Italic);
            // Underline
            this.UnderlineButton.Text = "U";
            this.UnderlineButton.Font = new Font(this.UnderlineButton.Font, FontStyle.Underline);
            // Strikeout
            this.StrikeoutButton.Text = "ab";
            this.StrikeoutButton.Font = new Font(this.StrikeoutButton.Font, FontStyle.Strikeout);
            //
            // MenuToolTip
            //
            this.MenuToolTip.SetToolTip(this.BoldButton, "굵게");
            this.MenuToolTip.SetToolTip(this.ItalicButton, "기울임꼴");
            this.MenuToolTip.SetToolTip(this.UnderlineButton, "밑줄");
            this.MenuToolTip.SetToolTip(this.StrikeoutButton, "취소선");
            //
            // Menubar
            //
            this.menubar.Size = new Size(this.Size.Width, menubar_height);
            this.menubar.Line.Size = new Size(this.Size.Width, 1);
            //
            // Menubar Menu
            //
            this.menubar.AddRange(this.menubar.LeftMenu, new MenuButton[]
            {
                BoldButton,
                ItalicButton,
                UnderlineButton,
                StrikeoutButton
            });

            this.BoldButton.Click += new EventHandler(this.Bold_Click);
            this.ItalicButton.Click += new EventHandler(this.Italic_Click);
            this.UnderlineButton.Click += new EventHandler(this.Underline_Click);
            this.StrikeoutButton.Click += new EventHandler(this.Strikeout_Click);
        }

        private Menubar menubar;

        private MenuButton BoldButton;
        private MenuButton ItalicButton;
        private MenuButton UnderlineButton;
        private MenuButton StrikeoutButton;

        private IFontMode FontMode;

        private void FontModeChange(IFontMode mode)
        {
            var font = this.note.SelectionFont;
            this.FontMode = mode;
            this.note.SelectionFont = this.FontMode.change(font);
            this.note.Focus();
        }

        private void Bold_Click(object sender, EventArgs e)
        {
            this.FontModeChange(new BoldMode());
        }

        private void Italic_Click(object sender, EventArgs e)
        {
            this.FontModeChange(new ItalicMode());
        }

        private void Underline_Click(object sender, EventArgs e)
        {
            this.FontModeChange(new UnderlineMode());
        }

        private void Strikeout_Click(object sender, EventArgs e)
        {
            this.FontModeChange(new StrikeoutMode());
        }
    }
}