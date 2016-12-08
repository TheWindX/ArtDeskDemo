using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ns_artDesk.core
{
    class CEventHub : Singleton<CEventHub>
    {
        public event System.Action<Key> evtKeyDown = null;
        public event System.Action<Key> evtKeyUp = null;

        public event System.Action<Key> evtGlobalKeyDown = null;
        public event System.Action<Key> evtGlobalKeyUp = null;

        internal void keydown(Key k)
        {
            evtKeyDown?.Invoke(k);
        }

        internal void keyup(Key k)
        {
            evtKeyUp?.Invoke(k);
        }

        internal void globalKeydown(Key k)
        {
            evtGlobalKeyDown?.Invoke(k);
        }

        internal void globalKeyup(Key k)
        {
            evtGlobalKeyUp?.Invoke(k);
        }
        //public static event System.Action<double, double> evtLeftMouseUp = null;
        //public static event System.Action<double, double> evtLeftMouseDown = null;
        //public static event System.Action<double, double> evtLeftMouseDrag = null;

        //public static event System.Action<double, double> evtRightMouseUp = null;
        //public static event System.Action<double, double> evtRightMouseDown = null;
        //public static event System.Action<double, double> evtRightMouseDrag = null;

        KeyboardListener listener = new KeyboardListener();
    }

    public class KeyboardListener : IDisposable
    {
        private static IntPtr hookId = IntPtr.Zero;
        private static WindowsUtil.LowLevelKeyboardProc HookCallbackProc;
        [MethodImpl(MethodImplOptions.NoInlining)]
        private IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                return HookCallbackInner(nCode, wParam, lParam);
            }
            catch
            {
                Console.WriteLine("There was some error somewhere...");
            }
            return WindowsUtil.CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        private IntPtr HookCallbackInner(int nCode, IntPtr wParam, IntPtr lParam)
        {
            Trace.WriteLine(nCode.ToString());
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)WindowsUtil.WM_KEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    
                    var key = KeyInterop.KeyFromVirtualKey(vkCode);
                    CEventHub.Instance.globalKeydown(key);
                }
                else if (wParam == (IntPtr)WindowsUtil.WM_KEYUP)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    var key = KeyInterop.KeyFromVirtualKey(vkCode);
                    CEventHub.Instance.globalKeyup(key);
                }
            }
            return WindowsUtil.CallNextHookEx(hookId, nCode, wParam, lParam);
        }
        
        public KeyboardListener()
        {
            HookCallbackProc = (WindowsUtil.LowLevelKeyboardProc)HookCallback;
            hookId = InterceptKeys.SetHook(HookCallbackProc);
        }

        ~KeyboardListener()
        {
            Dispose();
        }

        #region IDisposable Members

        public void Dispose()
        {
            WindowsUtil.UnhookWindowsHookEx(hookId);
        }

        #endregion


        internal static class InterceptKeys
        {
            public static IntPtr SetHook(WindowsUtil.LowLevelKeyboardProc proc)
            {
                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return WindowsUtil.SetWindowsHookEx(WindowsUtil.WH_KEYBOARD_LL, proc,
                        WindowsUtil.GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }
    }
}