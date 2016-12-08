using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    public class Singleton<T> where T : new()
    {
        private static readonly object _lock = new object();
        private static T instance;

        protected Singleton()
        {
            System.Diagnostics.Debug.Assert(instance == null);
        }

        public static bool Exists
        {
            get
            {
                return instance != null;
            }
        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new T();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
