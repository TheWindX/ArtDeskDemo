using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ns_artDesk
{
    class ArtAppMeta
    {
        public string ID { get; set; }
        public string version { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public BitmapImage image { get; set; }
    }


    class ArtApp
    {
        public ArtAppMeta meta { get; set; }
    }

    class ArtAppInternal : ArtApp
    {

    }

    class ArtAppInternalStandAlone : ArtAppInternal
    {

    }

    class ArtAppExternal : ArtApp
    {

    }

    class ArtAppExternalStandAlone : ArtAppExternal
    {

    }
}
