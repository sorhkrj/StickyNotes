using System.Drawing;

namespace AFMemo
{
	namespace Graphic
	{
    	public class MemoFont 
    	{
    		public static Font Word = new Font("굴림", 10F);
    	}
    	
    	interface IFontMode
    	{
    		Font change(Font font);
    	}
    	
    	class BoldMode : IFontMode
    	{
    		public Font change(Font font)
    		{
    			return (font.Bold) ? new Font(font, FontStyle.Regular) : new Font(font, FontStyle.Bold);
    		}
    	}
    	
    	class ItalicMode : IFontMode
    	{
    		public Font change(Font font)
    		{
    			return (font.Italic) ? new Font(font, FontStyle.Regular) : new Font(font, FontStyle.Italic);
    		}
    	}
    	
    	class UnderlineMode : IFontMode
    	{
    		public Font change(Font font)
    		{
    			return (font.Underline) ? new Font(font, FontStyle.Regular) : new Font(font, FontStyle.Underline);
    		}
    	}
    	
    	class StrikeoutMode : IFontMode
    	{
    		public Font change(Font font)
    		{
    			return (font.Strikeout) ? new Font(font, FontStyle.Regular) : new Font(font, FontStyle.Strikeout);
    		}
    	}
    }
}