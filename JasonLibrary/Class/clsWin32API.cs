using System;
using System.Runtime.InteropServices;

namespace JasonLibrary.Class
{
    internal struct Lastinputinfo
    {
        public uint CbSize;
        public readonly uint dwTime;
    }

    public class Win32API
    {
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref Lastinputinfo plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        public static uint GetIdleTime()
        {
            var lastInPut = new Lastinputinfo();
            lastInPut.CbSize = (uint)Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }

        public static long GetTickCount()
        {
            return Environment.TickCount;
        }

        public static long GetLastInputTime()
        {
            var lastInPut = new Lastinputinfo();
            lastInPut.CbSize = (uint)Marshal.SizeOf(lastInPut);

            if (!GetLastInputInfo(ref lastInPut))
            {
                throw new Exception(GetLastError().ToString());
            }

            return lastInPut.dwTime;
        }
    }
}