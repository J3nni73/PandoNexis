using Litium.Accelerator.Builders;
using Litium.Web.Models.Websites;
using Litium.Runtime.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Builders
{
    public class GenericGridViewModelBuilder : IViewModelBuilder<GenericGridViewModel>
    {
        public virtual GenericGridViewModel Build(PageModel pageModel)
        {

            return pageModel.MapTo<GenericGridViewModel>();
        }
    }
}
