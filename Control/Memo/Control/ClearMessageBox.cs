using System;
using System.Drawing;
using System.Windows.Forms;
using StickyMemos.Graphic; // MemoColor.cs

namespace StickyMemos
{
    namespace Control
    {
        public class ClearMessageBox : Form
        {
            public ClearMessageBox()
            {
                this.Keep = new MenuButton();
                this.Clear = new MenuButton();
                this.Message = new Label();

                //
                // ClearMessageBox
                //
                this.FormBorderStyle = FormBorderStyle.None;
                this.StartPosition = FormStartPosition.CenterParent;
                this.ControlBox = false;
                this.ShowInTaskbar = false;
                this.Opacity = 0.75;
                this.Controls.Add(this.Keep);
                this.Controls.Add(this.Clear);
                this.Controls.Add(this.Message);

                this.Load += new EventHandler(this.ClearMessageBox_Load);
            }

            private MenuButton Keep;
            private MenuButton Clear;
            private Label Message;

            private const int X = 40, Y = 20;

            private void ClearMessageBox_Load(object sender, EventArgs e)
            {
                //
                // ClearMessageBox
                //
                MemoColor.ModeChange((string)this.Tag);
                this.BackColor = MemoColor.Note;
                //
                // Keep
                //
                this.Keep.FlatAppearance.BorderSize = 1;
                this.Keep.TabStop = false;
                this.Keep.Location = BoxPoint(this.Keep.Size, new Point(X, Y));
                this.Keep.Text = "유지";
                this.Keep.ForeColor = ((string)this.Tag == "Black") ? Color.White : Color.Black;

                this.Keep.Click += new EventHandler(this.Keep_Click);
                //
                // Clear
                //
                this.Clear.FlatAppearance.BorderSize = 1;
                this.Clear.TabStop = false;
                this.Clear.Location = BoxPoint(this.Keep.Size, new Point(-X, Y));
                this.Clear.Text = "삭제";
                this.Clear.ForeColor = ((string)this.Tag == "Black") ? Color.White : Color.Black;

                this.Clear.Click += new EventHandler(this.Clear_Click);
                //
                // Message
                //
                this.Message.Size = new Size(200, 60);
                this.Message.Location = BoxPoint(this.Message.Size, new Point(0, -10));
                this.Message.TextAlign = ContentAlignment.MiddleCenter;
                this.Message.Font = MemoFont.Word;
                this.Message.Text = "이 메모를 삭제하시겠습니까?";
                this.Message.ForeColor = ((string)this.Tag == "Black") ? Color.White : Color.Black;
            }

            private void Keep_Click(object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.No;
            }

            private void Clear_Click(object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.Yes;
            }

            // box를 Form가운데로 이동 후 move만큼 추가 이동
            private Point BoxPoint(Size box, Point move)
            {
                int x = (this.Size.Width / 2) - (box.Width / 2) + move.X;
                int y = (this.Size.Height / 2) - (box.Height / 2) + move.Y;

                return new Point(x, y);
            }
        }
    }
}