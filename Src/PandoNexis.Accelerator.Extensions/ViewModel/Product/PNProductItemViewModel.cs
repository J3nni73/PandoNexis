using AutoMapper;
using Litium.Accelerator.ViewModels.Product;
using Litium.Runtime.AutoMapper;

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

        //PandoNexis: END MODEL


        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProductItemViewModel, PNProductItemViewModel>();
        }
    }
}
