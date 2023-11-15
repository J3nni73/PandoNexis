using AutoMapper;
using Litium.Accelerator.ViewModels.Product;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models;
using PandoNexis.Accelerator.Extensions.ViewModel;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Product
{
    public class PNProductItemViewModel : ProductItemViewModel, IAutoMapperConfiguration
    {
        //PandoExtensions: begin
        public string ExtendedDescription { get; set; }
        //PandoExtensions: end


        //PandoNexis: BEGIN MODEL

        //PandoNexis Visibility begin
        public bool EcomEnabled { get; set; }
        //PandoNexis Visibility end

        public PNCookieConsentModel CookieConsent { get; set; }

        //PandoNexis GtmEnhancedEcom begin
        public string GtmTrackingData { get; set; } = string.Empty;
        public string FbTrackingData { get; set; } = string.Empty;
        public string SnapTrackingData { get; set; } = string.Empty;
        public string PinTrackingData { get; set; } = string.Empty;

        public string GtmClickTracking { get; set; } = string.Empty;
        //PandoNexis GtmEnhancedEcom end

        //PandoNexis ProductIcons begin
        public List<ImageModel> ProductIcons { get; set; }
        //PandoNexis ProductIcons end

        //PandoNexis: END MODEL

        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProductItemViewModel, PNProductItemViewModel>();
        }
    }
}
