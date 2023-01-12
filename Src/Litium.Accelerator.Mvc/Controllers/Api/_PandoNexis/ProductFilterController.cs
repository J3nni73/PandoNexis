using System.Linq;
using System.Threading.Tasks;
using Litium.Accelerator.Builders.Product;
using Litium.Accelerator.Builders.Search;
using Litium.Accelerator.Extensions;
using Litium.Accelerator.Mvc.Controllers.Api;
using Litium.Accelerator.Mvc.Extensions;
using Litium.Accelerator.Routing;
using Litium.Accelerator.ViewModels.Search;
using Litium.Runtime.AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.PNInfinityScroll.Services;

namespace Litium.Accelerator.Mvc.Controllers._PandoNexis.Api
{
    [Route("api/_PandoNexis/productFilter")]
    public class ProductFilterController : ApiControllerBase
    {
        private readonly FilterViewModelBuilder _filterViewModelBuilder;
        private readonly CategoryFilteringViewModelBuilder _categoryFilteringViewModelBuilder;
        private readonly SubNavigationViewModelBuilder _subNavigationViewModelBuilder;
        private readonly FilterProductViewModelBuilder _filterProductViewModelBuilder;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly InfinityScrollService _infinityScrollService;

        public ProductFilterController(
            FilterViewModelBuilder filterViewModelBuilder,
            CategoryFilteringViewModelBuilder categoryFilteringViewModelBuilder,
            SubNavigationViewModelBuilder subNavigationViewModelBuilder,
            FilterProductViewModelBuilder filterProductViewModelBuilder,
            RequestModelAccessor requestModelAccessor,
            InfinityScrollService infinityScrollService)
        {
            _filterViewModelBuilder = filterViewModelBuilder;
            _categoryFilteringViewModelBuilder = categoryFilteringViewModelBuilder;
            _subNavigationViewModelBuilder = subNavigationViewModelBuilder;
            _filterProductViewModelBuilder = filterProductViewModelBuilder;
            _requestModelAccessor = requestModelAccessor;
            _infinityScrollService = infinityScrollService;
        }

        /// <summary>
        /// Gets the product filter without the HTML result.
        /// </summary>
        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAsync()
        {
            return GetAsync(false);
        }

        /// <summary>
        /// Gets the product filter with the result as HTML in the returned object.
        /// </summary>
        [HttpGet]
        [Route("withHtmlResult")]
        public Task<IActionResult> WithHtmlResultAsync()
        {
            return GetAsync(true);
        }

        private async Task<IActionResult> GetAsync(bool withHtmlResult)
        {
            if (_requestModelAccessor.RequestModel.CurrentPageModel == null)
            {
                return Ok();
            }
            var result = new FacetSearchResult();
            var productFilter = await _filterProductViewModelBuilder.BuildAsync();
            if (productFilter != null)
            {
                if (withHtmlResult)
                {
                    result.ProductsView = await this.RenderViewAsync(GetViewName(), productFilter.ViewData, true);
                }
                result.SortCriteria = _categoryFilteringViewModelBuilder.Build(productFilter.TotalCount);
            }
            result.FacetFilters = (await _filterViewModelBuilder.BuildAsync())?.Items.Select(c => c.MapTo<FacetGroupFilter>());
            result.SubNavigation = await _subNavigationViewModelBuilder.BuildAsync();
            result.NavigationTheme = _requestModelAccessor.RequestModel.WebsiteModel.GetNavigationType().ToString().ToCamelCase();

            return Ok(result);
        }

        private string GetViewName()
        {
            HttpContext.Session.SetString("PaginationType", _infinityScrollService.GetPaginationType());
            var page = _requestModelAccessor.RequestModel.CurrentPageModel;
            if (page.IsBrandPageType())
            {
                //Brand Page Type
                return "~/Views/_PandoNexis/Brand/Index.cshtml";
            }
            else if (page.IsSearchResultPageType())
            {
                //Search Result Page Type
                return "~/Views/_PandoNexis/Search/Index.cshtml";
            }
            else if (page.IsProductListPageType())
            {
                //Product List Page Type
                return "~/Views/_PandoNexis/ProductList/Index.cshtml";
            }
            else
            {
                //Category page
                return "~/Views/_PandoNexis/Category/Index.cshtml";
            }
        }
    }
}
