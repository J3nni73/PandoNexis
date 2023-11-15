using Litium.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Product
{
    public class PNGlobalProductsUspModel
    {
        public string UspTitle { get; set; }
        public string UspDescription { get; set; }
        public ImageModel UspIcon { get; set; }
        public ExtendedLinkViewModel UspLink { get; set; }
        public string UspThemeCssClass { get; set; }
    }
}
