/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 驱动
 * */

namespace ns_artDesk.core
{
    using System;
    using System.CodeDom.Compiler;
    using System.Reflection;
    using Microsoft.CSharp;
    using System.Threading;
    using System.Runtime.InteropServices;
    using System.IO;
    using Microsoft.Win32.SafeHandles;
    using System.Text;

    public class CSRepl
    {
        static CSRepl mIns = null;
        private CSRepl()
        {
            mIns = this;
        }

        [DllImport("kernel32.dll",
            EntryPoint = "GetStdHandle",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        private const int STD_OUTPUT_HANDLE = -11;
        private const int MY_CODE_PAGE = 437;


        public static CSRepl Instance
        {
            get
            {
                if (mIns == null)
                {
                    mIns = new CSRepl();
                }
                return mIns;
            }
        }

        string mScript = null;
        public static string mResult = null;

        static bool mExit = false;
        public static void exit() { mExit = true; }

        public enum EState { e_normal, e_warning, e_error }
        EState mState = EState.e_normal;

        Thread mThread = null;

        String mCodeFrame = @"
using System;
using System.Collections.Generic;
using ns_artDesk;//note: 这个随namespace改
using ns_artDesk.core;//note: 这个随namespace改
public static class Wrapper
{{
    public static void print(Object o)
    {{
        Console.WriteLine(o.ToString() );
        //ns_GameViewer.CSRepl.mResult = o.ToString();
    }}
    public static void exit()
    {{
        CSRepl.stop();
    }}
    public static void PerformAction()
    {{  
        {0};// user code goes here
    }}
}}";

        CSharpCodeProvider mCodeProvide = null;
        CompilerParameters mCompileOptions = null;

        void init()
        {
            mCodeProvide = new CSharpCodeProvider();
            mCompileOptions = new CompilerParameters();

            mCompileOptions.GenerateInMemory = true;
            mCompileOptions.GenerateExecutable = false;

            // bring in system libraries
            mCompileOptions.ReferencedAssemblies.Add(typeof(CSRepl).Assembly.Location);
        }


        void threadRun()
        {
            mExit = false;
            do
            {
                try
                {
                    if (!HasConsole)
                    {
                        Thread.Sleep(200);
                        continue;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("<<< ");
                    Console.ForegroundColor = ConsoleColor.White;

                    mScript = Console.ReadLine();
                    if (mResult == null)
                    {
                        Thread.Sleep(200);
                    }
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(">>> ");
                    if (mState == EState.e_error) Console.ForegroundColor = ConsoleColor.Red;
                    else if (mState == EState.e_warning) Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (mState == EState.e_normal) Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(mResult);
                    mResult = null;
                }
                catch(Exception ex)
                {
                    CLogger.Instance.error("CSRepl", ex.ToString());
                }
                
            } while (!mExit);
            mThread = null;
        }

        public void print(string str, EState st = EState.e_normal)
        {
            if (str == "") return;
            mResult = str;
            mState = st;


            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\n>>> ");
            if (mState == EState.e_error) Console.ForegroundColor = ConsoleColor.Red;
            else if (mState == EState.e_warning) Console.ForegroundColor = ConsoleColor.Yellow;
            else if (mState == EState.e_normal) Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(mResult);
            mResult = null;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("<<< ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void start()
        {
            init();
            mThread = new Thread(() => { threadRun(); });
            mThread.Start();
        }

        public void runOnce()
        {
            if (mScript != null)
            {
                var res = mCodeProvide.CompileAssemblyFromSource(mCompileOptions, String.Format(mCodeFrame, mScript));

                if (res.Errors.Count > 0)
                {
                    foreach (CompilerError error in res.Errors)
                    {
                        mResult = string.Format("Compiler Error ({0}): {1}", error.Line - 18, error.ErrorText);// 17 is location in whole codeFormat, 
                        mState = EState.e_error;
                    }
                }
                else
                {
                    var codeObject = res.CompiledAssembly.GetType("Wrapper");
                    var scriptFunc = codeObject.GetMethod("PerformAction", BindingFlags.Public | BindingFlags.Static);
                    if (scriptFunc != null)
                    {
                        try
                        {
                            mResult = "done";
                            scriptFunc.Invoke(null, null);
                        }
                        catch (Exception ex)
                        {
                            mResult = ex.ToString();
                        }

                        mState = EState.e_normal;
                    }
                    else
                    {
                        mResult = "runntime Error: scirptFunc == null";
                        mState = EState.e_error;
                    }
                }
                mScript = null;
            }
        }

        static public void stop()
        {
            mExit = true;
        }

        private const string Kernel32_DllName = "kernel32.dll";

        //[DllImport(Kernel32_DllName)]
        //private static extern bool AllocConsole();

        [DllImport(Kernel32_DllName)]
        private static extern bool FreeConsole();

        [DllImport(Kernel32_DllName)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport(Kernel32_DllName)]
        private static extern int GetConsoleOutputCP();

        public static bool HasConsole
        {
            get { return GetConsoleWindow() != IntPtr.Zero; }
        }
        /// <summary>
        /// Creates a new console instance if the process is not attached to a console already.
        /// </summary>
        public void Show()
        {
            //#if DEBUG
            if (!HasConsole)
            {
                AllocConsole();
                InvalidateOutAndError();
            }
            //#endif
        }

        /// <summary>
        /// If the process has a console attached to it, it will be detached and no longer visible. Writing to the System.Console is still possible, but no output will be shown.
        /// </summary>
        public static void Hide()
        {
            //#if DEBUG
            if (HasConsole)
            {
                SetOutAndErrorNull();
                FreeConsole();
            }
            //#endif
        }

        public void Toggle()
        {
            if (HasConsole)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        TextWriter origWriter = null;
        void InvalidateOutAndError()
        {
            origWriter = Console.Out;
            IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            Encoding encoding = System.Text.Encoding.Default;
            StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }

        static void SetOutAndErrorNull()
        {
            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);
        }

    };
}