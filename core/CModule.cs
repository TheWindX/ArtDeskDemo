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
}
