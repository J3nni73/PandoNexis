using AutoMapper;
using Litium.Accelerator.ViewModels.Product;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using PandoNexis.Accelerator.Extensions.ViewModel;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Product
{
    public class PNProductPageViewModel : ProductPageViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL

        //PandoNexis GtmEnhancedEcom begin"
        public PNProductItemViewModel PNProductItem { get; set; }
        public List<PNProductItemViewModel> PNBundleProducts { get; set; } = new List<PNProductItemViewModel>();
        public KeyValuePair<RelationshipType, List<PNProductItemViewModel>> PNAccessoriesProducts { get; set; }
        public KeyValuePair<RelationshipType, List<PNProductItemViewModel>> PNSimilarProducts { get; set; }
        public List<PNProductItemViewModel> PNMostSoldProducts { get; set; }
        public List<PNProductItemViewModel> PNVariants { get; set; } = new List<PNProductItemViewModel>();

        public PNCookieConsentModel CookieConsent { get; set; }
        public string GtmImpressionMostSoldProductsTracking { get; set; } = string.Empty;
        public string GtmImpressionCrossSaleProductsTracking { get; set; } = string.Empty;
        public string GtmImpressionAccessoriesProductsTracking { get; set; } = string.Empty;
        public string GtmImpressionSimilarProductsTracking { get; set; } = string.Empty;
        public string GtmImpressionBundleProductsTracking { get; set; } = string.Empty;
        public string GtmDetailsTracking { get; set; } = string.Empty;
        //PandoNexis GtmEnhancedEcom end"

        //PandoNexis PNGlobalProductsUsps begin
        public List<PNGlobalProductsUspModel> GlobalProductsUsps { get; set; }
        public string UspThemeCssClass { get; set; } = string.Empty;
        //PandoNexis PNGlobalProductsUsps end

        //PandoNexis: END MODEL


        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProductPageViewModel, PNProductPageViewModel>()
                .ForMember(x => x.PNProductItem, m => m.MapFrom(x => x.ProductItem))
                .ForMember(x => x.PNBundleProducts, m => m.MapFrom(x => x.BundleProducts))
                .ForMember(x => x.PNAccessoriesProducts, m => m.MapFrom(x => x.AccessoriesProducts))
                .ForMember(x => x.PNSimilarProducts, m => m.MapFrom(x => x.SimilarProducts))
                .ForMember(x => x.PNMostSoldProducts, m => m.MapFrom(x => x.MostSoldProducts))
                .ForMember(x => x.PNVariants, m => m.MapFrom(x => x.Variants))
                ;
        }
    }
}
