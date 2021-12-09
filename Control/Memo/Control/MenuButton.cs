using System.Drawing;
using System.Windows.Forms;

namespace StickyMemos
{
    namespace Control
    {
        public class MenuButton : Button
        {
            //
            // MenuButton
            //
            public MenuButton()
            {
                this.TabStop = false;
                this.FlatAppearance.BorderSize = 0;
                this.FlatStyle = FlatStyle.Flat;
                this.ForeColor = Color.Black;
            }
        }
    }
}