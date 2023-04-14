using Litium.Accelerator.Builders;
using Litium.Web.Models.Websites;
using Litium.Runtime.AutoMapper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Builders
{
    public class GenericDataViewModelBuilder : IViewModelBuilder<GenericDataViewModel>
    {
        public virtual GenericDataViewModel Build(PageModel pageModel)
        {

            return pageModel.MapTo<GenericDataViewModel>();
        }
    }
}
