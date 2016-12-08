using ns_artDesk.core;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace ns_artDesk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = this.Top = 0;
            this.Topmost = true;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowState = WindowState.Maximized;
            //Width = System.Windows.SystemParameters.WorkArea.Width;
            //Height = System.Windows.SystemParameters.WorkArea.Height;

            IntPtr hWnd = new WindowInteropHelper(this).Handle;
            SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
            IntPtr windowHandle = (new WindowInteropHelper(this)).Handle;
            HwndSource src = HwndSource.FromHwnd(windowHandle);
            src.AddHook(new HwndSourceHook(WndProc));
        }

        private IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SETFOCUS)
            {
                hWnd = new WindowInteropHelper(this).Handle;
                SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
                handled = true;
            }
            return IntPtr.Zero;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            CDriver.Instance.init();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            CDriver.Instance.update();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CDriver.Instance.exit();
            IntPtr windowHandle = (new WindowInteropHelper(this)).Handle;
            HwndSource src = HwndSource.FromHwnd(windowHandle);
            src.RemoveHook(new HwndSourceHook(this.WndProc));
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CEventHub.Instance.globalKeydown(e.Key);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            CEventHub.Instance.globalKeyup(e.Key);
        }

        
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 SWP_NOZORDER = 0x0004;
        const int WM_ACTIVATEAPP = 0x001C;
        const int WM_ACTIVATE = 0x0006;
        const int WM_SETFOCUS = 0x0007;
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const int WM_WINDOWPOSCHANGING = 0x0046;

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
           int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        static extern IntPtr DeferWindowPos(IntPtr hWinPosInfo, IntPtr hWnd,
           IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        static extern IntPtr BeginDeferWindowPos(int nNumWindows);
        [DllImport("user32.dll")]
        static extern bool EndDeferWindowPos(IntPtr hWinPosInfo);

    }
}
