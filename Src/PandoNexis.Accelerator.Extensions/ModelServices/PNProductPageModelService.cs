using Litium.Accelerator.ViewModels.Product;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Web.Models.Products;
using PandoNexis.Accelerator.Extensions.ViewModels.Product;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNProductPageModelService), Lifetime = DependencyLifetime.Transient)]
    public abstract class PNProductPageModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNProductPageViewModel model, Variant variant, decimal? price);
        public abstract void BuildPartialModel(ref PNProductPageViewModel model, BaseProduct baseProduct, decimal? price);
    }
}
