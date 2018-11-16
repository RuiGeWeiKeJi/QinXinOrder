using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;

namespace Utility
{
    /// <summary>
    /// 系统托盘
    /// </summary>
    public class SystemTray
    {
        private const int SM_CXSMICON = 49;
        private const int SM_CYSMICON = 50;
        /// <summary>
        /// 刷新系统托盘的图标
        /// </summary>
        public static void RefreshSystemTray()
        {
            try
            {
                int TrayWindow;
                NativeMethods.Rect WindowRect;
                int SmallIconWidth;
                int SmallIconHeight;
                Point CursorPos = Point.Empty;
                int Row;
                int Col;
                //{ 获得任务栏句柄和边框}
                int hwd = NativeMethods.FindWindow("Shell_TrayWnd", null);
                TrayWindow = NativeMethods.FindWindowEx(hwd, 0, "TrayNotifyWnd", null);
                int isGet = NativeMethods.GetWindowRect(new IntPtr(TrayWindow), out WindowRect);
                if (isGet == 0) return;
                //{ 获得小图标大小}
                SmallIconWidth = NativeMethods.GetSystemMetrics(SM_CXSMICON);
                SmallIconHeight = NativeMethods.GetSystemMetrics(SM_CYSMICON);
                //{ 保存当前鼠标位置}
                NativeMethods.GetCursorPos(ref CursorPos);
                //{ 使鼠标快速划过每个图标 }
                for (Row = 0; Row <= (WindowRect.Bottom - WindowRect.Top) / SmallIconHeight; Row++)
                {
                    for (Col = 0; Col <= (WindowRect.Right - WindowRect.Left) / SmallIconWidth; Col++)
                    {
                        NativeMethods.SetCursorPos(WindowRect.Left + Col * SmallIconWidth, WindowRect.Top + Row * SmallIconHeight);
                        Thread.Sleep(1);
                    }
                }                
                //{恢复鼠标位置}
                NativeMethods.SetCursorPos(CursorPos.X, CursorPos.Y);
                //{ 重画任务栏 }
                NativeMethods.Rect tempRect = new NativeMethods.Rect();
                NativeMethods.RedrawWindow(new IntPtr(TrayWindow), ref tempRect, 0, 0x85);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
            }
        }

        /// <summary>
        /// 检测是否存在指定标题的系统托盘图标,由参数caption指定图标 
        /// <summary>        
        public static bool CheckTrayIcon(string caption )
        {
            IntPtr vHandle = TrayToolbarWindow32();
            int vCount = NativeMethods.SendMessage(vHandle, NativeMethods.TB_BUTTONCOUNT, 0, 0);
            IntPtr vProcessId = IntPtr.Zero;
            NativeMethods.GetWindowThreadProcessId(vHandle, ref vProcessId);
            IntPtr vProcess = NativeMethods.OpenProcess(NativeMethods.PROCESS_VM_OPERATION | NativeMethods.PROCESS_VM_READ |
            NativeMethods.PROCESS_VM_WRITE, IntPtr.Zero, vProcessId);
            IntPtr vPointer = NativeMethods.VirtualAllocEx(vProcess, (int)IntPtr.Zero, 0x1000,
            NativeMethods.MEM_RESERVE | NativeMethods.MEM_COMMIT, NativeMethods.PAGE_READWRITE);
            char[] vBuffer = new char[256];
            IntPtr pp = Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0);
            uint vNumberOfBytesRead = 0;
            try
            {
                for (int i = 0; i < vCount; i++)
                {
                    NativeMethods.SendMessage(vHandle, NativeMethods.TB_GETBUTTONTEXT, i, vPointer.ToInt32());

                    NativeMethods.ReadProcessMemoryEx(vProcess, vPointer,
                    Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0),
                    vBuffer.Length * sizeof(char), ref vNumberOfBytesRead);

                    int l = 0;
                    for (int j = 0; j < vBuffer.Length; j++)
                    {
                        if (vBuffer[j] == (char)0)
                        {
                            l = j;
                            break;
                        }
                    }
                    string s = new string(vBuffer, 0, l);

                    if (s.IndexOf(caption) >= 0)
                    {
                        //if (isShow)
                        //    Win32API.SendMessage(vHandle, Win32API.TB_HIDEBUTTON, i, 0);
                        //else
                        //    Win32API.SendMessage(vHandle, Win32API.TB_HIDEBUTTON, i, 1);
                        return true;
                    }
                    Console.WriteLine(s);
                    //return false;
                }
                return false;
            }
            finally
            {
                NativeMethods.VirtualFreeEx(vProcess, vPointer, 0, NativeMethods.MEM_RELEASE);
                NativeMethods.CloseHandle(vProcess);
            }
        }

        /// <summary>
        /// 获取托盘指针
        /// <summary> 
        private static IntPtr TrayToolbarWindow32()
        {
            //IntPtr h = IntPtr.Zero;
            int hTemp = 0;

            int ih = NativeMethods.FindWindow("Shell_TrayWnd", null); //托盘容器 
            //h = new IntPtr(ih);
            ih = NativeMethods.FindWindowEx(ih, 0 , "TrayNotifyWnd", null);//找到托盘 
            //h = new IntPtr(ih);
            ih = NativeMethods.FindWindowEx(ih, 0 , "SysPager", null);
            //h = new IntPtr(ih);

            hTemp = NativeMethods.FindWindowEx(ih,0 , "ToolbarWindow32", null);

            return new IntPtr( hTemp );
        }
    }
}
