namespace Utility
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class SystemMenuNativeWindow : NativeWindow, IDisposable
    {
        private IntPtr _hMenu;
        private Dictionary<int, EventHandler> _menuClickEventList;
        private Form _owner;

        public SystemMenuNativeWindow(Form owner)
        {
            this._owner = owner;
            base.AssignHandle(owner.Handle);
            this.GetSystemMenu();
        }

        public bool AppendMenu(int id, string text, EventHandler menuClickEvent)
        {
            return this.InsertMenu(uint.MaxValue, id, MenuItemFlag.MF_BYPOSITION, text, menuClickEvent);
        }

        public bool AppendSeparator()
        {
            return this.InertSeparator(uint.MaxValue);
        }

        public void Dispose()
        {
            base.ReleaseHandle();
            this._owner = null;
            this._hMenu = IntPtr.Zero;
            if (this._menuClickEventList != null)
            {
                this._menuClickEventList.Clear();
                this._menuClickEventList = null;
            }
        }

        private void GetSystemMenu()
        {
            this._hMenu = NativeMethods.GetSystemMenu(base.Handle, false);
            if (this._hMenu == IntPtr.Zero)
            {
                throw new Win32Exception("获取系统菜单失败。");
            }
        }

        public bool InertSeparator(uint position)
        {
            return this.InsertMenu(position, 0, MenuItemFlag.MF_SEPARATOR | MenuItemFlag.MF_BYPOSITION, "", null);
        }

        public bool InsertMenu(uint position, int id, string text, EventHandler menuClickEvent)
        {
            return this.InsertMenu(position, id, MenuItemFlag.MF_BYPOSITION, text, menuClickEvent);
        }

        public bool InsertMenu(uint position, int id, MenuItemFlag flag, string text, EventHandler menuClickEvent)
        {
            if (!(((flag & MenuItemFlag.MF_SEPARATOR) == MenuItemFlag.MF_SEPARATOR) || this.ValidateID(id)))
            {
                throw new ArgumentOutOfRangeException("id", string.Format("菜单ID只能在{0}-{1}之间取值。", 0, 0xf000));
            }
            bool flag2 = NativeMethods.InsertMenu(this._hMenu, position, (int) flag, id, text);
            if (flag2 && (menuClickEvent != null))
            {
                this.MenuClickEventList.Add(id, menuClickEvent);
            }
            return flag2;
        }

        private void OnWmSysCommand(ref Message m)
        {
            EventHandler handler;
            int key = m.WParam.ToInt32();
            if (this.MenuClickEventList.TryGetValue(key, out handler))
            {
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
                m.Result = NativeMethods.TRUE;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        public void Revert()
        {
            NativeMethods.GetSystemMenu(base.Handle, true);
            this.Dispose();
        }

        private bool ValidateID(int id)
        {
            return ((id < 0xf000) && (id > 0));
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x112)
            {
                this.OnWmSysCommand(ref m);
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        protected Dictionary<int, EventHandler> MenuClickEventList
        {
            get
            {
                if (this._menuClickEventList == null)
                {
                    this._menuClickEventList = new Dictionary<int, EventHandler>(10);
                }
                return this._menuClickEventList;
            }
        }
    }

    public enum MenuItemFlag
    {
        MF_BARBREAK = 0x20,
        MF_BREAK = 0x40,
        MF_BYCOMMAND = 0,
        MF_BYPOSITION = 0x400,
        MF_CHECKED = 8,
        MF_DISABLED = 2,
        MF_GRAYED = 1,
        MF_POPUP = 0x10,
        MF_SEPARATOR = 0x800,
        MF_STRING = 0,
        MF_UNCHECKED = 0
    }
}

