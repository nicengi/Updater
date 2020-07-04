using System;
using System.Runtime.InteropServices;

namespace Nicengi.Update
{
    internal static class NativeMethods
    {
        #region Consts
        public const int WM_CLOSE = 0x10;
        public const int WM_APP_UPDATE = 0x8010;
        #endregion

        #region Methods
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool PostThreadMessage(int threadId, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetClassName(IntPtr hWnd, IntPtr lpClassName, int nMaxCount);

        public static int GetClassName(IntPtr hWnd, out string lpClassName, int nMaxCount = 255)
        {
            IntPtr ptr = Marshal.StringToHGlobalAuto(new string(new char[nMaxCount]));
            int length = NativeMethods.GetClassName(hWnd, ptr, nMaxCount);
            lpClassName = Marshal.PtrToStringAuto(ptr);
            Marshal.FreeHGlobal(ptr);
            return length;
        }
        #endregion
    }
}
