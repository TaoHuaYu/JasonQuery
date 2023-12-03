using System;
using System.Text;
using System.Runtime.InteropServices;

namespace JasonQuery
{
    public static class MessageBoxManager
    {
        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        private delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

        private const int WH_CALLWNDPROCRET = 12;
        private const int WM_DESTROY = 0x0002;
        private const int WM_INITDIALOG = 0x0110;
        private const int WM_TIMER = 0x0113;
        private const int WM_USER = 0x400;
        private const int DM_GETDEFID = WM_USER + 0;

        private const int MBOK = 1;
        private const int MBCancel = 2;
        private const int MBAbort = 3;
        private const int MBRetry = 4;
        private const int MBIgnore = 5;
        private const int MBYes = 6;
        private const int MBNo = 7;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll")]
        private static extern int UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowTextLengthW", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowTextW", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength);

        [DllImport("user32.dll")]
        private static extern int EndDialog(IntPtr hDlg, IntPtr nResult);

        [DllImport("user32.dll")]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetClassNameW", CharSet = CharSet.Unicode)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern int GetDlgCtrlID(IntPtr hwndCtl);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll", EntryPoint = "SetWindowTextW", CharSet = CharSet.Unicode)]
        private static extern bool SetWindowText(IntPtr hWnd, string lpString);

        //20191116 add
        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();

        [StructLayout(LayoutKind.Sequential)]
        private struct CWPRETSTRUCT
        {
            private IntPtr lResult;
            private IntPtr lParam;
            private IntPtr wParam;
            public uint message;
            public IntPtr hwnd;
        };

        private static HookProc hookProc;
        private static EnumChildProc enumProc;
        [ThreadStatic]
        private static IntPtr hHook;
        [ThreadStatic]
        private static int nButton;

        public static string OK = "&OK";
        public static string Cancel = "&Cancel";
        public static string Abort = "&Abort";
        public static string Retry = "&Retry";
        public static string Ignore = "&Ignore";
        public static string Yes = "&Yes";
        public static string No = "&No";

        static MessageBoxManager()
        {
            hookProc = MessageBoxHookProc;
            enumProc = MessageBoxEnumProc;
            hHook = IntPtr.Zero;
        }

        public static void Register()
        {
            if (hHook != IntPtr.Zero)
            {
                throw new NotSupportedException("One hook per thread allowed.");
            }

            //原版範例使用「AppDomain.GetCurrentThreadId()」，會有「已過時」的警告
            //hHook = SetWindowsHookEx(WH_CALLWNDPROCRET, hookProc, IntPtr.Zero, AppDomain.GetCurrentThreadId());

            //20191116 以下改用 [DllImport("kernel32.dll")] GetCurrentThreadId，測試結果OK
            hHook = SetWindowsHookEx(WH_CALLWNDPROCRET, hookProc, IntPtr.Zero, GetCurrentThreadId());

            //修改成以下的「System.Threading.Thread.CurrentThread.ManagedThreadId」，會變成沒效果！
            //hHook = SetWindowsHookEx(WH_CALLWNDPROCRET, hookProc, IntPtr.Zero, System.Threading.Thread.CurrentThread.ManagedThreadId);
        }

        public static void Unregister()
        {
            if (hHook == IntPtr.Zero)
            {
                return;
            }

            UnhookWindowsHookEx(hHook);
            hHook = IntPtr.Zero;
        }

        private static IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(hHook, nCode, wParam, lParam);
            }

            var msg = (CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT));
            var hook = hHook;

            if (msg.message != WM_INITDIALOG)
            {
                return CallNextHookEx(hook, nCode, wParam, lParam);
            }

            var nLength = GetWindowTextLength(msg.hwnd);
            var className = new StringBuilder(10);
            GetClassName(msg.hwnd, className, className.Capacity);

            if (className.ToString() != "#32770")
            {
                return CallNextHookEx(hook, nCode, wParam, lParam);
            }

            nButton = 0;
            EnumChildWindows(msg.hwnd, enumProc, IntPtr.Zero);

            if (nButton != 1)
            {
                return CallNextHookEx(hook, nCode, wParam, lParam);
            }

            var hButton = GetDlgItem(msg.hwnd, MBCancel);

            if (hButton != IntPtr.Zero)
            {
                SetWindowText(hButton, OK);
            }

            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        private static bool MessageBoxEnumProc(IntPtr hWnd, IntPtr lParam)
        {
            var className = new StringBuilder(10);
            GetClassName(hWnd, className, className.Capacity);

            if (className.ToString() != "Button")
            {
                return true;
            }

            var ctlId = GetDlgCtrlID(hWnd);

            switch (ctlId)
            {
                case MBOK:
                    SetWindowText(hWnd, OK);
                    break;
                case MBCancel:
                    SetWindowText(hWnd, Cancel);
                    break;
                case MBAbort:
                    SetWindowText(hWnd, Abort);
                    break;
                case MBRetry:
                    SetWindowText(hWnd, Retry);
                    break;
                case MBIgnore:
                    SetWindowText(hWnd, Ignore);
                    break;
                case MBYes:
                    SetWindowText(hWnd, Yes);
                    break;
                case MBNo:
                    SetWindowText(hWnd, No);
                    break;
            }
            nButton++;

            return true;
        }
    }
}