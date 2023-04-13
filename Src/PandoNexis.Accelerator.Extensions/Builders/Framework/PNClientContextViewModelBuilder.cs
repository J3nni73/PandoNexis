using Litium.Accelerator.Builders.Framework;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.FieldFramework.FieldTypes;
using Litium.Runtime.AutoMapper;
using Litium.Sales;
using Litium.Web.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using PandoNexis.Accelerator.Extensions.ViewModels.Framework;

namespace PandoNexis.Accelerator.Extensions.Builders.Framework
{
    public class PNClientContextViewModelBuilder : ClientContextViewModelBuilder
	{
		private readonly RequestModelAccessor _requestModelAccessor;
		public PNClientContextViewModelBuilder(IAntiforgery antiforgery,
			IHttpContextAccessor httpContextAccessor,
			RequestModelAccessor requestModelAccessor,
			CartContextAccessor cartContextAccessor,
			PersonStorage personStorage,
			SiteSettingViewModelBuilder siteSettingViewModelBuilder,
			CartViewModelBuilder cartViewModelBuilder,
			NavigationViewModelBuilder navigationViewModelBuilder) : base(antiforgery,
												httpContextAccessor,
												requestModelAccessor,
												cartContextAccessor,
												personStorage, siteSettingViewModelBuilder, cartViewModelBuilder, navigationViewModelBuilder
												)
        {
			_requestModelAccessor = requestModelAccessor;

		}

		public async Task<PNClientContextViewModel> BuildAsync()
		{
			var baseModel = await base.BuildAsync();
			var model = baseModel.MapTo<PNClientContextViewModel>();
			var website = _requestModelAccessor.RequestModel.WebsiteModel;
			var topLinkList = website.GetValue<IList<PointerItem>>(AcceleratorWebsiteFieldNameConstants.AdditionalHeaderLinks)?.OfType<PointerPageItem>().ToList().Select(x => x.MapTo<LinkModel>()).Where(x => x != null && x.AccessibleByUser == true).ToList();
			if (topLinkList != null)
			{
				model.TopLinkList = topLinkList;
			}

			return model;
		}
    }
}
