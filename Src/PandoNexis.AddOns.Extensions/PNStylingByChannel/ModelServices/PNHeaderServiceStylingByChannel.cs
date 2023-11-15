using Litium.Accelerator.Routing;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models;
using PandoNexis.Accelerator.Extensions.ModelServices;
using PandoNexis.AddOns.Extensions.PNStylingByChannel.Constants;
using PandoNexis.Accelerator.Extensions.ViewModels.Framework;

namespace PandoNexis.AddOns.Extensions.PNStylingByChannel.ModelServices
{
    public class PNHeaderServiceStylingByChannel : PNHeaderModelService
    {
        private readonly RequestModelAccessor _requestModelAccessor;

        public PNHeaderServiceStylingByChannel(RequestModelAccessor requestModelAccessor)
        {
            _requestModelAccessor = requestModelAccessor;
        }

        public override void BuildPartialModel(ref PNHeaderViewModel viewModel)
        {
            var channel = _requestModelAccessor.RequestModel.ChannelModel;
            var logo = channel.Channel.Fields.GetValue<Guid?>(ChannelFieldNameConstants.AlternativeLogo)?.MapTo<ImageModel>();
            if (logo != null)
            {
                var alt = viewModel.Logo.Alt;
                viewModel.Logo = logo;
                logo.Alt = alt;
            }
        }
    }
}
