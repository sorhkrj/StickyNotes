using System.Drawing;
using System.Windows.Forms;
using System.Graphic; // MemoColor.cs

namespace StickyMemos
{
	namespace Control
	{
    	public class Title : Border
    	{
    		public Title()
    		{
    			//
    			// Title
    			//
    			this.BackColor = MemoColor.Title;
    			this.Dock = DockStyle.Top;
    			this.TabStop = false;
    		}
    	}
    }
}