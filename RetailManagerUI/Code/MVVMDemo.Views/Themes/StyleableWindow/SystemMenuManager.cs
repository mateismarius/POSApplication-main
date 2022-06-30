/// Written by: Ciprian Horeanu
/// Creation Date: 20th of October, 2019
/// Purpose: Shows the system menu when clicking on the Title Bar icons of Windows
#region ========================================================================= USING =====================================================================================
using System;
using System.Windows;
using System.Windows.Interop;
using System.Runtime.InteropServices;
#endregion

namespace RetailManagerUI.StyleableWindow
{
    public static class SystemMenuManager
    {
        #region ================================================================= METHODS ===================================================================================
        public static void ShowMenu(Window _targetWindow, Point _menuLocation)
        {
            if (_targetWindow == null)
                throw new ArgumentNullException("TargetWindow is null.");
            int x, y;
            try
            {
                x = Convert.ToInt32(_menuLocation.X);
                y = Convert.ToInt32(_menuLocation.Y);
            }
            catch (OverflowException)
            {
                x = 0;
                y = 0;
            }
            uint WM_SYSCOMMAND = 0x112, TPM_LEFTALIGN = 0x0000, TPM_RETURNCMD = 0x0100;
            IntPtr window = new WindowInteropHelper(_targetWindow).Handle;
            IntPtr wMenu = NativeMethods.GetSystemMenu(window, false);
            int command = NativeMethods.TrackPopupMenuEx(wMenu, TPM_LEFTALIGN | TPM_RETURNCMD, x, y, window, IntPtr.Zero);
            if (command == 0)
                return;
            NativeMethods.PostMessage(window, WM_SYSCOMMAND, new IntPtr(command), IntPtr.Zero);
        }
        #endregion
    }

    /// <summary>
    /// Native methods interop 
    /// </summary>
    internal static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        internal static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("user32.dll")]
        internal static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);
    }
}
