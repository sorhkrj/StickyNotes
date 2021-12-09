using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StickyMemos
{
    namespace Control
    {
        public class Border : Panel
        {
            public Border()
            {
                LeftMenu = new List<MenuButton>();
                RightMenu = new List<MenuButton>();
            }

            public List<MenuButton> LeftMenu;
            public List<MenuButton> RightMenu;

            private const int win10 = 8; // 윈도우10 버전의 앱 테두리 크기로 인한 실제 크기 차이값

            public void Add(List<MenuButton> menu, MenuButton button)
            {
                // 초기값
                int width = this.Size.Width - win10, height = this.Size.Height;
                Size size = new Size(height, height);
                bool way = (menu == this.LeftMenu) ? true : false;

                // 방향
                int index = menu.Count;
                int length = height * index;
                int left = length, right = width - height - length - win10;
                int x = (way) ? left : right;

                // 생성
                button.TabStop = false;
                button.Size = size;
                button.Location = new Point(x, 0);
                menu.Add(button);

                // 추가
                this.Controls.Add(menu[index]);
            }

            public void AddRange(List<MenuButton> menu, MenuButton[] button)
            {
                // 초기값
                int width = this.Size.Width - win10, height = this.Size.Height;
                Size size = new Size(height, height);
                bool way = (menu == this.LeftMenu) ? true : false;

                // 크기
                int count = 0;
                int index = menu.Count;
                int range = menu.Count + button.Length;
                while (index < range)
                {
                    // 방향
                    int length = height * index;
                    int left = length, right = width - height - length - win10;
                    int x = (way) ? left : right;

                    // 생성
                    button[index].TabStop = false;
                    button[index].Size = size;
                    button[index].Location = new Point(x, 0);
                    menu.Add(button[count++]);

                    // 추가
                    this.Controls.Add(menu[index++]);
                }
            }
        }
    }
}