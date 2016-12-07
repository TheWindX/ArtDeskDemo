using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
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
