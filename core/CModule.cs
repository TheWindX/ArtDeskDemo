/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    public interface CModule
    {
        void onInit();
        void onUpdate();
        void onExit();
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
