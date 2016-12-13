using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    [RequireCom(typeof(COMLister))]
    class COMDesktop : CComponent
    {
        public void append(COMArtApp app)
        {
            getComponent<COMLister>().items.pushBack(app.getComponent<COMListItem>());
        }
    }
}
