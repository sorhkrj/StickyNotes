using System.Drawing;
using System.Windows.Forms;
using System.Graphic; // MemoFont.cs

namespace StickyMemos
{
	namespace Control
	{
    	public class Note : RichTextBox
    	{
    		public Note()
    		{
    			//
    			// Note
    			//
    			this.BackColor = MemoColor.Note;
    			this.BorderStyle = BorderStyle.None;
    			this.TabStop = false;
    			this.ScrollBars = RichTextBoxScrollBars.Vertical;
    			this.Font = MemoFont.Word;
    		}
    	}
    }
}