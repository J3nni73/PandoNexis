using Litium.Web.Models;
using AutoMapper;
using Litium.Accelerator.ViewModels.Framework;
using Litium.Runtime.AutoMapper;
using Litium.Accelerator.Builders;
using PandoNexis.Accelerator.Extensions.ViewModel;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Framework
{
    /// <summary>
    /// Represents the view model for client-side context.
    /// </summary>
    public class PNClientContextViewModel : Litium.Accelerator.ViewModels.Framework.ClientContextViewModel, IAutoMapperConfiguration
	{
		public IList<LinkModel> TopLinkList { get; set; }

        //PandoNexis MapWithOrganizations begin
        public string GmKey { get; set; } = string.Empty;
        //PandoNexis MapWithOrganizations end

        //PandoNexis PNCookieConsent begin
        public PNCookieConsentModel CookieConsent { get; set; }
        //PandoNexis PNCookieConsent end

        public void Configure(IMapperConfigurationExpression cfg)
		{
			cfg.CreateMap<ClientContextViewModel, PNClientContextViewModel>();
		}
	}
}
