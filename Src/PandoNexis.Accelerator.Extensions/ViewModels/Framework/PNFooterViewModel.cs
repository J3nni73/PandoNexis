using Litium.Accelerator.ViewModels.Framework;
using Litium.Runtime.AutoMapper;
using AutoMapper;
using PandoNexis.Accelerator.Extensions.ViewModels.Usp;

namespace PandoNexis.Accelerator.Extensions.Framework.ViewModels
{
    public class PNFooterViewModel : FooterViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL

        //PandoNexis FooterUSPs begin
        public string UspThemeCssClass { get; set; } = string.Empty;
        public List<USPModel> FooterUsps { get; set; } = new List<USPModel>();
        //PandoNexis FooterUSPs end

        //PandoNexis: END MODEL

        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<FooterViewModel, PNFooterViewModel>();
        }
    }
}