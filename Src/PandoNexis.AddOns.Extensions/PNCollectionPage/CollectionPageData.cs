using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNCollectionPage
{
    public class CollectionPageData
    {
        public List<CollectionPageFilter> Filters { get; set; }

        public List<CollectionPageChild> Children { get; set; }
    }
}
