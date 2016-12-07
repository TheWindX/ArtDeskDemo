using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ns_artDesk.core
{
    class CEventHub : Singleton<CEventHub>
    {
        public event System.Action<Key> evtKeyDown = null;
        public event System.Action<Key> evtKeyUp = null;

        internal void keydown(Key k)
        {
            evtKeyDown?.Invoke(k);
        }

        internal void keyup(Key k)
        {
            evtKeyUp?.Invoke(k);
        }
        //public static event System.Action<double, double> evtLeftMouseUp = null;
        //public static event System.Action<double, double> evtLeftMouseDown = null;
        //public static event System.Action<double, double> evtLeftMouseDrag = null;

        //public static event System.Action<double, double> evtRightMouseUp = null;
        //public static event System.Action<double, double> evtRightMouseDown = null;
        //public static event System.Action<double, double> evtRightMouseDrag = null;
    }
}
