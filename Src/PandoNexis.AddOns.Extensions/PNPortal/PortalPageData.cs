using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNPortalPage
{
    public class PortalPageData
    {
        public List<PortalPageFilter> Filters { get; set; }

        public List<PortalPageChild> Children { get; set; }
    }
}
