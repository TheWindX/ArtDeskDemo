﻿using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.entity
{
    class ArtAppStore : EntityBase
    {
        public static ArtAppStore Instance
        {
            get
            {
                return Singleton<ArtAppStore>.Instance;
            }
        }
    }
}