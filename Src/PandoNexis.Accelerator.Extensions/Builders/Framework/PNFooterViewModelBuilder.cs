using Litium.Accelerator.Builders.Framework;
using Litium.Accelerator.Routing;
using Litium.Accelerator.ViewModels.Framework;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.Framework.ViewModels;
using PandoNexis.Accelerator.Extensions.ModelServices;

namespace PandoNexis.Accelerator.Extensions.Builders.Framework
{
    [Service(ServiceType = typeof(FooterViewModelBuilder), Lifetime = DependencyLifetime.Scoped)]
    public class PNFooterViewModelBuilder : FooterViewModelBuilder
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly IEnumerable<PNFooterModelService> _pnFooterModelService;

        public PNFooterViewModelBuilder(RequestModelAccessor requestModelAccessor, IEnumerable<PNFooterModelService> pnFooterModelService) : base(requestModelAccessor)
        {
            _requestModelAccessor = requestModelAccessor;
            _pnFooterModelService = pnFooterModelService;
        }
        public override FooterViewModel Build()
        {
            var viewModel = base.Build().MapTo<PNFooterViewModel>();
            foreach (var item in _pnFooterModelService)
            {
                item.BuildPartialModel(ref viewModel);
            }

            return viewModel;
        }
    }
}
