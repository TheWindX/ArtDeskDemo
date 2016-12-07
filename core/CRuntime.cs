using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    class CRuntime : Singleton<CRuntime>
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
                    CLogger.Instance.error("CRuntime", ex.Message);
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
                        mod.state = CRuntime.ModuleState.EState.eInvalid;
                    }
                }
                else if (mod.state == CRuntime.ModuleState.EState.eInited)
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
}
