using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ggd
{
    class win_api
    {
        /// <summary>
        /// 根据1或2个条件查找窗口句柄
        /// </summary>
        /// <param name="lpClassName">类名 可null</param>
        /// <param name="lpWindowName">窗口标题 可null</param>
        /// <returns>句柄,未找到为0</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        /// <summary>
        /// 获取窗口位置大小
        /// </summary>
        /// <param name="hWnd">句柄</param>
        /// <param name="rect">LPRECT结构指针</param>
        /// <returns>是否成功</returns>
        [DllImport("user32")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref LPRECT rect);

        /// <summary>
        /// 窗口矩形结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private readonly struct LPRECT
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;
        }
        /// <summary>
        /// 根据句柄 获取窗口位置和尺寸
        /// </summary>
        /// <param name="hWnd">句柄</param>
        /// <param name="rr">Point指针</param>
        /// <returns>是否成功</returns>
        public static bool find_win(IntPtr hWnd, ref Point rr)
        {
            if(hWnd != IntPtr.Zero)
            {
                // 获取窗口位置和尺寸。
                LPRECT rect = default;          
                if(GetWindowRect(hWnd, ref rect))
                {
                    rr.X = rect.Left;
                    rr.Y = rect.Top;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取当前 活动窗口 的句柄
        /// </summary>
        /// <returns>句柄,未找到为0</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// 设置为 活动窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        //private delegate bool WndEnumProc(IntPtr hWnd, int lParam);

        //[DllImport("user32")]
        //private static extern bool EnumWindows(WndEnumProc lpEnumFunc, int lParam);

        //[DllImport("user32")]
        //private static extern IntPtr GetParent(IntPtr hWnd);

        //[DllImport("user32")]
        //private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lptrString, int nMaxCount);

        //[DllImport("user32")]
        //private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

       

       
    }
}
