namespace Utility
{
    using System;
    using System.Runtime.InteropServices;
    using System.Drawing;
    using System.Text;

    public class NativeMethods
    {
        public const uint LastMenuID = uint.MaxValue;
        public static readonly IntPtr TRUE = new IntPtr(1);
        public const int WM_SYSCOMMAND = 0x112;
        public const int WM_USER = 0x400;
        public const int TB_BUTTONCOUNT = (WM_USER + 24);
        public const int PROCESS_VM_OPERATION = 0x8;
        public const int PROCESS_VM_READ = 0x10;
        public const int PROCESS_VM_WRITE = 0x20;


        public const int MEM_RESERVE = 0x2000;
        public const int MEM_COMMIT = 0x1000;
        public const int MEM_RELEASE = 0x8000;
        public const int PAGE_READWRITE = 0x4;

        public const int TB_GETBUTTONTEXT = WM_USER + 75;


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool InsertMenu(IntPtr hMenu, uint wPosition, int wFlags, int wIDNewItem, string lpNewItem);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetGetCookie(string lpszUrlName, string lbszCookieName, StringBuilder lpszCookieData, ref int lpdwSize);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lPBuffer, int lpdwBufferLength);

        [DllImport("wininet.dll")]
        public extern static bool InternetCheckConnection(string lpszUrl, int dwFlags, int dwReserved);

        [DllImport("sensapi.dll")]
        public extern static bool IsNetworkAlive(out int connectionDescription);

        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        public const int WS_SHOWNORMAL = 1;

        [DllImport("user32.dll", EntryPoint = "AnimateWindow")]
        public static extern bool AnimateWindow(IntPtr handle, int ms, int flags);

        /// <summary>
        /// 采用调治解调器上网
        /// </summary>
        public const int INTERNET_CONNECTION_MODEM = 1;
        /// <summary>
        /// 采用网卡上网
        /// </summary>
        public const int INTERNET_CONNECTION_LAN = 2;
        [DllImport("winInet.dll")]
        public static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);


        [DllImport("user32.dll", EntryPoint = "UpdateWindow")]
        public static extern int UpdateWindow(int hwnd);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, out  Rect lpRect);
        [DllImport("user32")]
        public static extern int GetSystemMetrics(int nIndex);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public static extern int SetCursorPos(int x, int y);
        [DllImport("user32.dll", EntryPoint = "RedrawWindow")]
        public static extern int RedrawWindow(IntPtr hwnd, ref Rect lprcUpdate, int hrgnUpdate, int fuRedraw);
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32", EntryPoint = "GetWindowThreadProcessId")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hwnd, ref IntPtr lpdwProcessId);
        [DllImport("kernel32", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, IntPtr bInheritHandle, IntPtr dwProcessId);
        [DllImport("kernel32", EntryPoint = "VirtualAllocEx")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, int lpAddress, int dwSize, int flAllocationType, int flProtect);

        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemoryEx(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);
        [DllImport("kernel32", EntryPoint = "VirtualFreeEx")]
        public static extern int VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, int dwFreeType);
        [DllImport("kernel32", EntryPoint = "CloseHandle")]
        public static extern int CloseHandle(IntPtr hObject);



        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalAddAtom(String lpString);  //添加原子
        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalFindAtom(String lpString);  //查找原子
        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalDeleteAtom(UInt32 nAtom);  //删除原子

        /// 该函数设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
        //[DllImport("User32.dll")]
        //public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary>
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        //[DllImport("User32.dll")]
        //public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ShowWindow(HandleRef hWnd, int nCmdShow);
    }
}

