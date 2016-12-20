/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 模块管理器
 * */
using System;
using System.Collections.Generic;
using System.Reflection;
namespace ns_artDesk.core
{
    public interface CModule
    {
        void onInit();
        void onUpdate();
        void onExit();
    }

    class CModuleManager : Singleton<CModuleManager>
    {
        internal static List<ModuleState> modules = new List<ModuleState>();
        internal static List<Action> mActions = new List<Action>();

        internal class ModuleState
        {
            public CModule mod;
            public enum EState
            {
                ePreInited,
                eInited,
                //eUpdate,
                eExited,
                eInvalid,
            }

            public EState state = EState.ePreInited;
        }


        public static void regModule(CModule mod)
        {
            mActions.Add(() =>
            {
                modules.Add(new ModuleState() { mod = mod, state = ModuleState.EState.ePreInited });
            });
        }

        public static void unregModule(CModule mod)
        {
            foreach (var m in modules)
            {
                if (m.mod == mod)
                {
                    if (m.state == ModuleState.EState.eInited)
                    {
                        m.state = ModuleState.EState.eExited;
                    }
                    return;
                }
            }
        }

        static void addAssemblyModules()
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            var ts = myAssembly.GetTypes();

            List<Type> mAttrModules = new List<Type>();
            foreach (var t in ts)
            {
                if (typeof(CModule).IsAssignableFrom(t))
                {
                    var attrs = t.GetCustomAttribute<ModuleInstance>();
                    if (attrs != null)
                    {
                        //MModule instance = (MModule)Activator.CreateInstance(t);
                        mAttrModules.Add(t);
                    }
                }
            }
            mAttrModules.Sort((t1, t2) =>
            {
                var att1 = t1.GetCustomAttribute<ModuleInstance>();
                var att2 = t2.GetCustomAttribute<ModuleInstance>();
                if (att1.level < att2.level) return -1;
                else if (att1.level < att2.level) return 0;
                else return 1;
            });

            foreach (var t in mAttrModules)
            {
                CModule instance = (CModule)Activator.CreateInstance(t);
                regModule(instance);
            }
        }

        public void init()
        {
            addAssemblyModules();
        }
        List<ModuleState> toRemoved = new List<ModuleState>();
        public void update()
        {
            toRemoved.Clear();

            while (mActions.Count != 0)
            {
                int idx = mActions.Count - 1;
                try
                {
                    mActions[idx]();
                }
                catch (Exception ex)
                {
                    CLogger.Instance.error("CRuntime", ex.ToString());
                }
                mActions.RemoveAt(idx);
            }

            foreach (var mod in modules)
            {
                if (mod.state == ModuleState.EState.eInvalid)
                {
                    toRemoved.Add(mod);
                }

                if (mod.state == ModuleState.EState.eExited)
                {
                    toRemoved.Add(mod);
                    try
                    {
                        mod.mod.onExit();
                    }
                    catch (Exception ex)
                    {
                        CLogger.Instance.error("CRutime", ex.ToString());
                        mod.state = ModuleState.EState.eInvalid;
                    }
                }
                else if (mod.state == ModuleState.EState.ePreInited)
                {
                    try
                    {
                        mod.mod.onInit();
                        mod.state = ModuleState.EState.eInited;
                    }
                    catch (Exception ex)
                    {
                        CLogger.Instance.error("CRutime", ex.ToString());
                        mod.state = ModuleState.EState.eInvalid;
                    }
                }
                else if (mod.state == ModuleState.EState.eInited)
                {
                    try
                    {
                        mod.mod.onUpdate();
                    }
                    catch (Exception ex)
                    {
                        CLogger.Instance.error("CRutime", ex.ToString());
                        //mod.state = MRuntime.ModuleState.EState.eInvalid;
                    }
                }
            }

            foreach (var rmv in toRemoved)
            {
                modules.Remove(rmv);
            }
        }

        public void exit()
        {
            foreach (var mod in modules)
            {
                if (mod.state == ModuleState.EState.eExited)
                {
                    try
                    {
                        mod.mod.onExit();
                    }
                    catch (Exception ex)
                    {
                        CLogger.Instance.error("CRuntime", ex.ToString());
                        mod.state = CModuleManager.ModuleState.EState.eInvalid;
                    }
                }
                else if (mod.state == CModuleManager.ModuleState.EState.eInited)
                {
                    try
                    {
                        mod.mod.onExit();
                    }
                    catch (Exception ex)
                    {
                        CLogger.Instance.error("CRuntime", ex.ToString());
                        mod.state = ModuleState.EState.eInvalid;
                    }
                }
            }
            modules.Clear();
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = true)]
    public class ModuleInstance : System.Attribute
    {
        public int level;

        public ModuleInstance(int level)
        {
            this.level = level;
        }
    }

}
