using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseEvent2HtmlElementTest
{
    class MouseEvents
    {
        static System.Drawing.Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
        static readonly int ScreenLength = rect.Width;
        static readonly int ScreenHeight = rect.Height;
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        const int MOUSEEVENTF_MOVE = 0x0001;      //移动鼠标 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        //const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        //const int MOUSEEVENTF_MIDDLEUP = 0x0040; //模拟鼠标中键抬起 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000; //标示是否采用绝对坐标

        public static string GetMousePosition()
        {
            return Cursor.Position.X.ToString() + ", " + Cursor.Position.Y.ToString();
        }

        public static bool IsMouseInWindow(int[] WindowRect)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            if (x < WindowRect[0] || x > WindowRect[1])
            {
                return false;
            }
            if (y < WindowRect[2] || y > WindowRect[3])
            {
                return false;
            }
            return true;
        }

        public static void MouseMove(int x, int y)//绝对移动
        {
            mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, x * 65535 / ScreenLength, y * 65535 / ScreenHeight, 0, 0);
        }

        public static void MouseMoveRE(int x, int y)//相对移动
        {
            mouse_event(MOUSEEVENTF_MOVE, x, y, 0, 0);
        }

        public static void MouseMoveTo(int aimx, int aimy, int step, int sleep)
        {
            int x;
            int y;
            int i = 0;
            while ((aimx - Cursor.Position.X != 0) || (aimy - Cursor.Position.Y != 0))
            {
                i += 1;
                if (i >= step) { MouseMove(aimx, aimy); break; }
                x = (aimx - Cursor.Position.X) / (step - i);
                y = (aimy - Cursor.Position.Y) / (step - i);
                MouseMoveRE(x, y);
                System.Threading.Thread.Sleep(sleep);
            }
        }


        public static void MouseLeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void MouseRightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        public static void MouseLeftDoubleClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void MouseRightDoubleClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        public static void MouseLeftDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        public static void MouseLeftUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void MouseRightDown()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
        }

        public static void MouseRightUp()
        {
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }
    }
}
