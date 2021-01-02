using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace get_window_pos
{
    class Program
    {
        // FindWindow
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        // GetWindowRect
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        // RECT
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        static void Main(string[] args)
        {
            if (args.Length > 0 && args.Length <= 2)
            {
                if (SpinWait.SpinUntil(() => FindWindow(null, args[0]) != IntPtr.Zero, TimeSpan.FromSeconds(30)))
                {
                    IntPtr hwnd = FindWindow(null, args[0]);
                    RECT rct = new RECT();
                    GetWindowRect(hwnd, ref rct);

                    int x = rct.Left;
                    int y = rct.Top;
                    int cx = rct.Right - x;
                    int cy = rct.Bottom - y;
                    string pos = x + ";" + y + ";" + cx + ";" + cy;

                    if (args.Length > 1 && !String.IsNullOrEmpty(args[1]))
                    {
                        switch (args[1])
                        {
                            case "x":
                            case "left":
                                Console.WriteLine(x);
                                break;
                            case "y":
                            case "top":
                                Console.WriteLine(y);
                                break;
                            case "cx":
                            case "width":
                                Console.WriteLine(cx);
                                break;
                            case "cy":
                            case "height":
                                Console.WriteLine(cy);
                                break;
                            default:
                                Console.WriteLine(pos);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine(pos);
                    }
                }
            }
        }
    }
}
