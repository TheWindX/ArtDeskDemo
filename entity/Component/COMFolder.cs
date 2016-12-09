﻿using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    [RequireCom(typeof(COMFoldItem))]
    class COMFolder : CComponent
    {
        public ListEx<COMFoldItem> items = new ListEx<COMFoldItem>();
    }
}