/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 桌面消息
 * */
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

        //public event System.Action<float, float> evtScreenMouseMove = null;
        //public event System.Action leftKey  = null;

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
        //KeyboardListener listener = new KeyboardListener();
    }

    public class KeyboardListener : IDisposable
    {
        private static IntPtr hookId = IntPtr.Zero;
        private static WindowsUtil.lowLevel.LowLevelKeyboardProc HookCallbackProc;
        [MethodImpl(MethodImplOptions.NoInlining)]
        private IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                return HookCallbackInner(nCode, wParam, lParam);
            }
            catch(Exception ex)
            {
                CLogger.Instance.error("CEventHub", ex.ToString());
            }
            return WindowsUtil.lowLevel.CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        private IntPtr HookCallbackInner(int nCode, IntPtr wParam, IntPtr lParam)
        {
            CLogger.Instance.info("CEventHub", "receive keyboard code of:{1}", nCode);
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)WindowsUtil.lowLevel.WM_KEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    
                    var key = KeyInterop.KeyFromVirtualKey(vkCode);
                    CEventHub.Instance.globalKeydown(key);
                }
                else if (wParam == (IntPtr)WindowsUtil.lowLevel.WM_KEYUP)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    var key = KeyInterop.KeyFromVirtualKey(vkCode);
                    CEventHub.Instance.globalKeyup(key);
                }
            }
            return WindowsUtil.lowLevel.CallNextHookEx(hookId, nCode, wParam, lParam);
        }
        
        public KeyboardListener()
        {
            HookCallbackProc = (WindowsUtil.lowLevel.LowLevelKeyboardProc)HookCallback;
            hookId = InterceptKeys.SetHook(HookCallbackProc);
        }

        ~KeyboardListener()
        {
            Dispose();
        }

        #region IDisposable Members

        public void Dispose()
        {
            WindowsUtil.lowLevel.UnhookWindowsHookEx(hookId);
        }

        #endregion


        internal static class InterceptKeys
        {
            public static IntPtr SetHook(WindowsUtil.lowLevel.LowLevelKeyboardProc proc)
            {
                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return WindowsUtil.lowLevel.SetWindowsHookEx(WindowsUtil.lowLevel.WH_KEYBOARD_LL, proc,
                        WindowsUtil.lowLevel.GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }
    }
}