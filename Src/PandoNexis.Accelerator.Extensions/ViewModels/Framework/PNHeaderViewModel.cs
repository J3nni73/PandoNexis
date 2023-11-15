using Litium.Accelerator.ViewModels.Framework;
using Litium.Runtime.AutoMapper;
using AutoMapper;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Framework
{
    public class PNHeaderViewModel : HeaderViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL
        public bool CenteredNavigation { get; set; }
        public bool ShowIconTitles { get; set; }

        //PandoNexis Websiteselector begin
        public string Area { get; set; } = string.Empty;
        public string ChannelId { get; set; }
        public Dictionary<Guid, (string CountryName, string Url, string IconUrl, bool LinkToNewWindow)> Channels { get; set; } = new Dictionary<Guid, (string CountryName, string Url, string IconUrl, bool LinkToNewWindow)>();
        public PortalData PortalData { get; set; } = new PortalData();
        //PandoNexis Websiteselector end

        //PandoNexis Visibility begin
        public bool VisibleLogin { get; set; } = true;
        public bool VisibleCheckout { get; set; }
        //PandoNexis Visibility end

        //PandoNexis HeaderBannerMessage begin
        public bool ShowHeaderBannerMessage { get; set; } = false;
        public string HeaderBannerMessage { get; set; } = string.Empty;
        //PandoNexis HeaderBannerMessage end

        //PandoNexis HeaderUSPs begin
        public string HeaderUsps { get; set; } = string.Empty;
        //PandoNexis HeaderUSPs end

        //PandoNExis CookieConsent begin
        public bool ShowCookieBar { get; set; }
        //PandoNExis CookieConsent end

        //PandoNexis: END MODEL
        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<HeaderViewModel, PNHeaderViewModel>();
        }
    }

    public class PortalData
    {
        public string LargeBackgroundImageUrl { get; set; } = string.Empty;
        public bool IsPortal { get; set; } = false;
    }
}