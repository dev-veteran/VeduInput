using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeduInput
{
    public class VeduInput
    {
        //getting GetAsyncKeyState function from user32 api.
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys targetKey);

        //getting mouse_event function from user32 api.
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        //getting keybd_event function from user32 api.
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        public class Globals
        {
            /// <summary>
            /// Helps to check keystrokes globally.
            /// <para>implemented with GetAsyncKeyState.</para>
            /// </summary>
            public static bool IsKeyPressed(Keys targetKey, int Flag)
            {
                return 0 != (GetAsyncKeyState(targetKey) & Flag);
            }
        }

        public class Mouse
        {
            public enum MouseEventFlags
            {
                MOVE = 0x00000001,

                LEFTDOWN = 0x00000002,
                LEFTUP = 0x00000004,

                MIDDLEDOWN = 0x00000020,
                MIDDLEUP = 0x00000040,

                RIGHTDOWN = 0x00000008,
                RIGHTUP = 0x00000010
            }

            /// <summary>
            /// Helps to perform mouse move event.
            /// <para>implemented with mouse_event.</para>
            /// </summary>
            public static void Move(int x, int y)
            {
                mouse_event((uint)MouseEventFlags.MOVE, x, y, 0, 0);
            }

            /// <summary>
            /// Helps to perform mouse left click event.
            /// 
            /// <paramref name="delayBetweenClick"/> can be null.
            /// 
            /// <para>implemented with mouse_event.</para>
            /// </summary>
            public static void LeftClick(int delayBetweenClick = 0)
            {
                mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
                Task.Delay(delayBetweenClick).Wait();
                mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            }

            /// <summary>
            /// Helps to perform mouse left click event.
            /// 
            /// <paramref name="delayBetweenClick"/> parameter can be null.
            /// 
            /// <para>implemented with mouse_event.</para>
            /// </summary>
            public static void RightClick(int delayBetweenClick = 0)
            {
                mouse_event((uint)MouseEventFlags.RIGHTDOWN, 0, 0, 0, 0);
                Task.Delay(delayBetweenClick).Wait();
                mouse_event((uint)MouseEventFlags.RIGHTUP, 0, 0, 0, 0);
            }

            /// <summary>
            /// Helps to perform mouse left click event.
            /// 
            /// <paramref name="delayBetweenClick"/> parameter can be null.
            ///
            /// <para>implemented with mouse_event.</para>
            /// </summary>
            public static void ScrollClick(int delayBetweenClick = 0)
            {
                mouse_event((uint)MouseEventFlags.MIDDLEDOWN, 0, 0, 0, 0);
                Task.Delay(delayBetweenClick).Wait();
                mouse_event((uint)MouseEventFlags.MIDDLEUP, 0, 0, 0, 0);
            }
        }

        public class Keyboard
        {
            ////////////////////////////////////////////////////////////////////////////////
            //  NOTE: You can find almost every type of virtualkey codes from: 
            //  https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
            ////////////////////////////////////////////////////////////////////////////////

            public enum KeyboardEventFlags
            {
                KEYEVENTF_KEYDOWN = 0,
                KEYEVENTF_KEYUP = 0x0002
            }

            /// <summary>
            /// Helps to perform mouse left click event.
            /// 
            /// <para>
            /// <paramref name="delayBetweenClick"/> parameter can be null.
            /// </para>
            /// 
            /// <para>
            /// <paramref name="targetKey"/> should be a virtualkey code.
            /// <c>You can check MSDN for more info.</c>
            /// </para>
            /// 
            /// <para>implemented with keybd_event.</para>
            /// </summary>
            public static void KeyPress(byte targetKey, int delayBetweenClick = 0)
            {
                keybd_event(targetKey, 0x45, (uint)KeyboardEventFlags.KEYEVENTF_KEYDOWN, 0);
                Task.Delay(delayBetweenClick).Wait();
                keybd_event(targetKey, 0x45, (uint)KeyboardEventFlags.KEYEVENTF_KEYUP, 0);
            }

            /// <summary>
            /// Helps to perform mouse left click event.
            /// 
            /// <para>
            /// <paramref name="delayBeforeEvent"/> parameter can be null.
            /// </para>
            /// 
            /// <para>
            /// <paramref name="targetKey"/> should be a virtualkey code.
            /// <c>You can check MSDN for more info.</c>
            /// </para>
            /// 
            /// <para>implemented with keybd_event.</para>
            /// </summary>
            public static void KeyDown(byte targetKey, int delayBeforeEvent = 0)
            {
                Task.Delay(delayBeforeEvent).Wait();
                keybd_event(targetKey, 0x45, (uint)KeyboardEventFlags.KEYEVENTF_KEYDOWN, 0);
            }

            /// <summary>
            /// Helps to perform mouse left click event.
            /// 
            /// <para>
            /// <paramref name="delayBeforeEvent"/> parameter can be null.
            /// </para>
            /// 
            /// <para>
            /// <paramref name="targetKey"/> should be a virtualkey code.
            /// <c>You can check MSDN for more info.</c>
            /// </para>
            /// 
            /// <para>implemented with keybd_event.</para>
            /// </summary>
            public static void KeyUp(byte targetKey, int delayBeforeEvent = 0)
            {
                Task.Delay(delayBeforeEvent).Wait();
                keybd_event(targetKey, 0x45, (uint)KeyboardEventFlags.KEYEVENTF_KEYUP, 0);
            }
        }
    }
}
