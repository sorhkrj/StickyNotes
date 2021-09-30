using System.Draiwng;
using System.Windows.Forms;

namespace StickyMemos
{
	namespace Control
	{
    	public class Menubar : Border
    	{
    		public Menubar()
    		{
    			this.Line = new PictureBox();
    			
    			//
    			// Line
    			//
    			this.Line.BackColor = MemoColor.Line;
    			this.Line.TabStop = false;
    			this.Line.Location = new Point(0,0);
    			//
    			// MenuBar
    			//
    			this.BackColor = MemoColor.Note;
    			this.Dock = DockStyle.Bottom;
    			this.TabStop = false;
    			this.Controls.Add(this.Line);
    		}
    		
    		public PictureBox Line;
    	}
    }
}