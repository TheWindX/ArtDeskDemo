﻿using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    [RequireCom(typeof(COMLister))]
    [RequireCom(typeof(COMListItem))]
    class COMFolder : CComponent
    {
        public void appItem(COMListItem item)
        {
            item.location = this;
        }
    }
}
