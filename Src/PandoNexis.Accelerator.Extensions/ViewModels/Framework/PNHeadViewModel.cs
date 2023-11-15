using Litium.Runtime.AutoMapper;
using Litium.Accelerator.ViewModels.Framework;
using AutoMapper;
using PandoNexis.Accelerator.Extensions.ViewModel;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Framework
{
    public class PNHeadViewModel : HeadViewModel, IAutoMapperConfiguration
    {
        public List<string> PNHeadDataList { get; set; } = new List<string>();
        public string CssFileName { get; set; } = string.Empty;

        //PandoNexis MapWithOrganizations begin
        public string GmKey { get; set; } = string.Empty;
        //PandoNexis MapWithOrganizations end

        //PandoNExis CookieConsent begin
        public PNCookieConsentModel CookieConsent { get; set; }
        //PandoNExis CookieConsent end
        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<HeadViewModel, PNHeadViewModel>();
        }
    }
}
