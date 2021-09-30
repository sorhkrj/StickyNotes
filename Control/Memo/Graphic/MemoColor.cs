using System.Drawing;

namespace AFMemo
{
	namespace Graphic
	{
    	public class MemoColor 
    	{
    		public static void ModeChange(string colorName)
    		{
    			IColorMode mode;
    			switch (colorName)
    			{
    				case "Yellow":
    					mode = new YellowMode();
    					break;
    				case "Green":
    					mode = new GreenMode();
    					break;
    				case "Pink":
    					mode = new PinkMode();
    					break;
    				case "Purple":
    					mode = new PurpleMode();
    					break;
    				case "Blue":
    					mode = new BlueMode();
    					break;
    				case "Gray":
    					mode = new GrayMode();
    					break;
    				case "Black":
    					mode = new BlackMode();
    					break;
    				default:
    					mode = new YellowMode();
    					break;
    			}
    			mode.change();
    			
    			public static Color Title;
    			public static Color Note;
    			public static Color Line;
    		}
    		
    		interface IColorMode
    		{
    			void change();
    		}
    		
    		class YellowMode : IColorMode
    		{
    			public void change()
    			{
    				MemoColor.Title = Color.FromArgb(255, 235, 129);
    				MemoColor.Note = Color.FromArgb(255, 242, 171);
    				MemoColor.Line = Color.FromArgb(237, 225, 159);
    			}
    		}
    		
    		class GreenMode : IColorMode
    		{
    			public void change()
    			{
    				MemoColor.Title = Color.FromArgb(175, 236, 164);
    				MemoColor.Note = Color.FromArgb(203, 241, 196);
    				MemoColor.Line = Color.FromArgb(189, 224, 182);
    			}
    		}
    		
    		class PinkMode : IColorMode
    		{
    			public void change()
    			{
    				MemoColor.Title = Color.FromArgb(255, 187, 221);
    				MemoColor.Note = Color.FromArgb(255, 204, 229);
    				MemoColor.Line = Color.FromArgb(237, 190, 213);
    			}
    		}
    		
    		class PurpleMode : IColorMode
    		{
    			public void change()
    			{
    				MemoColor.Title = Color.FromArgb(219, 183, 255);
    				MemoColor.Note = Color.FromArgb(231, 207, 255);
    				MemoColor.Line = Color.FromArgb(215, 192, 237);
    			}
    		}
    		
    		class BlueMode : IColorMode
    		{
    			public void change()
    			{
    				MemoColor.Title = Color.FromArgb(183, 223, 255);
    				MemoColor.Note = Color.FromArgb(205, 233, 255);
    				MemoColor.Line = Color.FromArgb(191, 217, 237);
    			}
    		}
    		
    		class GrayMode : IColorMode
    		{
    			public void change()
    			{
    				MemoColor.Title = Color.FromArgb(229, 229, 229);
    				MemoColor.Note = Color.FromArgb(249, 249, 249);
    				MemoColor.Line = Color.FromArgb(231, 231, 231);
    			}
    		}
    		
    		class BlackMode : IColorMode
    		{
    			public void change()
    			{
    				MemoColor.Title = Color.FromArgb(62, 62, 62);
    				MemoColor.Note = Color.FromArgb(68, 68, 68);
    				MemoColor.Line = Color.FromArgb(81, 81, 81);
    			}
    		}
    	}
    }
}