using System;
using System.Drawing;
using System.Windows.Forms;
using StickyMemos.Graphic; // MemoColor.cs

namespace StickyMemos
{
    partial class Memo : Form
    {
        private const int title_height = 32, menubar_height = 39; // 타이틀, 메뉴바 높이
        private const int win10 = 8; // 윈도우10 버전의 앱 테두리 크기로 인한 실제 크기 차이값

        public void InitializeComponent()
        {
            // MenuToolTip
            this.MenuToolTip = new ToolTip();
            this.MenuToolTip.AutoPopDelay = 5000;
            this.MenuToolTip.InitialDelay = 1000;
            this.MenuToolTip.ShowAlways = true;

            // Designer
            TitleComponent();
            MenubarComponent();
            NoteComponent();

            //
            // Memo
            //
            this.BackColor = MemoColor.Note;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new Size(208, 108);
            this.MaximizeBox = false;
            this.ControlBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.KeyPreview = true;
            this.DoubleBuffered = true;
            this.Controls.Add(this.menubar);
            this.Controls.Add(this.note);
            this.Controls.Add(this.title);

            this.Load += new EventHandler(this.Memo_Load);
            this.Activated += new EventHandler(this.Memo_Activated);
            this.Deactivate += new EventHandler(this.Memo_Deactivate);
            this.SizeChanged += new EventHandler(this.Memo_SizeChanged);
            this.Resize += new EventHandler(this.Memo_Resize);
            this.ResizeEnd += new EventHandler(this.Memo_ResizeEnd);
        }

        private ToolTip MenuToolTip;

        public event Action<string> Add;
        public event Action<Memo> Exit;
    }
}