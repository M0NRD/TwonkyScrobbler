using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TwonkyScrobbler
{
    class Win32Interop
    {
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
