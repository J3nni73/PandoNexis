using Litium.Accelerator.ViewModels.Framework;
using Litium.Runtime.AutoMapper;
using AutoMapper;

namespace PandoNexis.Accelerator.Extensions.Framework.ViewModels
{
    public class PNHeaderViewModel : HeaderViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL
        //PandoNexis Websiteselector begin
        public string Area { get; set; } = String.Empty;
        public string ChannelId { get; set; }
        public Dictionary<Guid, (string CountryName, string Url, string IconUrl, bool LinkToNewWindow)> Channels { get; set; } = new Dictionary<Guid, (string CountryName, string Url, string IconUrl, bool LinkToNewWindow)>();
        public PortalData PortalData { get; set; } = new PortalData();
        //PandoNexis Websiteselector end

        //PandoNexis Visibility begin
        public bool VisibleLogin { get; set; } = true;
        public bool VisibleCheckout { get; set; }
        //PandoNexis Visibility end

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