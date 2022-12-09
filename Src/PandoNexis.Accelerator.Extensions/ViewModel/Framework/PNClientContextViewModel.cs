using Litium.Accelerator.ViewModels.Search;
using System.Collections.Generic;
using Litium.Accelerator.Builders;
using Litium.Web.Models;
using AutoMapper;
using Litium.Accelerator.ViewModels.Framework;
using Litium.Runtime.AutoMapper;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Framework
{
    /// <summary>
    /// Represents the view model for client-side context.
    /// </summary>
    public class PNClientContextViewModel : Litium.Accelerator.ViewModels.Framework.ClientContextViewModel, IAutoMapperConfiguration
	{
		public IList<LinkModel> TopLinkList { get; set; }

		public void Configure(IMapperConfigurationExpression cfg)
		{
			cfg.CreateMap<ClientContextViewModel, PNClientContextViewModel>();
		}
	}
}
