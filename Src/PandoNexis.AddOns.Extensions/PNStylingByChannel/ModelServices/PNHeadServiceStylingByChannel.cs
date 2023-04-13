using Litium.Accelerator.Routing;
using PandoNexis.AddOns.Extensions.PNStylingByChannel.Constants;
using PandoNexis.Accelerator.Extensions.ModelServices;
using PandoNexis.Accelerator.Extensions.ViewModels.Framework;
using Litium.Accelerator.Builders.Framework;

namespace PandoNexis.AddOns.Extensions.PNStylingByChannel.PNModelService
{
    internal class PNHeadServiceStylingByChannel : PNHeadModelService
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly FaviconViewModelBuilder _faviconViewModelBuilder;

        public PNHeadServiceStylingByChannel(RequestModelAccessor requestModelAccessor,
                                                    FaviconViewModelBuilder faviconViewModelBuilder)
        {
            _requestModelAccessor = requestModelAccessor;
            _faviconViewModelBuilder = faviconViewModelBuilder;
        }
        public override void BuildPartialModel(ref PNHeadViewModel viewModel)
        {
            var channel = _requestModelAccessor.RequestModel.ChannelModel.Channel;
            var cssFileName = channel.Fields.GetValue<string>(ChannelFieldNameConstants.CssFileName);
            if (!string.IsNullOrEmpty(cssFileName))
            {
                viewModel.CssFileName = cssFileName;
            }

            var favicon = channel.Fields.GetValue<Guid?>(ChannelFieldNameConstants.AlternativeFavicon);
            if (favicon.HasValue)
            {
                viewModel.Favicons = _faviconViewModelBuilder.Build(favicon.Value);
            }
        }
    }
}
