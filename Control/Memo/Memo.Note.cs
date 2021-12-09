using System;
using System.Drawing;
using System.Windows.Forms;
using StickyMemos.Manager;
using StickyMemos.Control; // Note.cs
using StickyMemos.Graphic; // MemoFont.cs

namespace StickyMemos
{
    partial class Memo
    {
        private void NoteComponent()
        {
            this.note = new Note();

            //
            // Note
            //
            this.note.Location = new Point(0, title_height);
            this.note.Size = new Size(this.Size.Width, menubar.Location.Y - title_height);

            this.note.KeyDown += new KeyEventHandler(this.Note_KeyDown);
            this.note.MouseWheel += new MouseEventHandler(this.Note_MouseWheel);
            this.note.MouseUp += new MouseEventHandler(this.Note_MouseUp);
            //
            // NoteMenuStrip
            //
            this.NoteMenuStrip = new ContextMenuStrip();
            this.UndoItem = new ToolStripMenuItem("실행 취소");
            this.PasteItem = new ToolStripMenuItem("붙여넣기");
            //
            // MenuItem
            //
            this.NoteMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                this.UndoItem,
                this.PasteItem
            });

            this.UndoItem.Click += new EventHandler(this.UndoItem_Click);
            this.PasteItem.Click += new EventHandler(this.PasteItem_Click);
        }

        internal Note note;

        private ContextMenuStrip NoteMenuStrip;
        private ToolStripMenuItem UndoItem;
        private ToolStripMenuItem PasteItem;

        private void Note_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.A:
                    this.note.SelectAll();
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.S:
                    Data data = new Data(this.Name, this.ColorName, this.Location, this.Size);
                    this.dataManager.Save(data);
                    this.note.SaveFile(string.Format(@"{0}/{1}", DataManager.directory, this.Name));
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.Z:
                    this.note.Undo();
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.X:
                    if (this.note.SelectedText != string.Empty)
                    {
                        Clipboard.SetText(this.note.SelectedText);
                        this.note.SelectedText = string.Empty;
                    }
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.C:
                    if (this.note.SelectedText != string.Empty)
                    {
                        Clipboard.SetText(this.note.SelectedText);
                    }
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.V:
                    this.note.SelectedText = Clipboard.GetText();
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.B:
                    this.FontModeChange(new BoldMode());
                    break;
                case Keys.Control | Keys.I:
                    this.FontModeChange(new ItalicMode());
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Control | Keys.U:
                    this.FontModeChange(new UnderlineMode());
                    break;
                case Keys.Control | Keys.T:
                    this.FontModeChange(new StrikeoutMode());
                    break;
                case Keys.Control | Keys.Shift | Keys.Oemcomma:
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.Shift | Keys.OemPeriod:
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.Shift | Keys.L:
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.L:
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.E:
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.R:
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private void Note_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                ((HandledMouseEventArgs)e).Handled = true;
            }
        }

        private void Note_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.NoteMenuStrip.Show(MousePosition);
            }
        }

        private void UndoItem_Click(object sender, EventArgs e)
        {
            this.note.Undo();
        }

        private void PasteItem_Click(object sender, EventArgs e)
        {
            this.note.SelectedText = Clipboard.GetText();
        }
    }
}