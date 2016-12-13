/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    class WindowsUtil
    {
        public delegate IntPtr LowLevelKeyboardProc(
                int nCode, IntPtr wParam, IntPtr lParam);

        public static int WH_KEYBOARD_LL = 13;
        public static int WM_KEYDOWN = 0x0100;
        public static int WM_KEYUP = 0x0101;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook,
                LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr handle, Int32 flags);


        [DllImport("user32.dll")]
        public static extern Boolean GetMonitorInfo(IntPtr hMonitor, NativeMonitorInfo lpmi);


        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct NativeRectangle
        {
            public Int32 Left;
            public Int32 Top;
            public Int32 Right;
            public Int32 Bottom;


            public NativeRectangle(Int32 left, Int32 top, Int32 right, Int32 bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public sealed class NativeMonitorInfo
        {
            public Int32 Size = Marshal.SizeOf(typeof(NativeMonitorInfo));
            public NativeRectangle Monitor;
            public NativeRectangle Work;
            public Int32 Flags;
        }
    }
}
