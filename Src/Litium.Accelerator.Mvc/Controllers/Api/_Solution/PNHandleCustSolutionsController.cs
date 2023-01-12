using System.Collections.Generic;
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

namespace Litium.Accelerator.Mvc.Controllers.Api._Solution
{
    [Route("api/_PandoNexis/handleCustSolutions")]
    public class PNHandleCustSolutionsController : ApiControllerBase
    {
        private readonly FilterViewModelBuilder _filterViewModelBuilder;
        private readonly CategoryFilteringViewModelBuilder _categoryFilteringViewModelBuilder;
        private readonly SubNavigationViewModelBuilder _subNavigationViewModelBuilder;
        private readonly FilterProductViewModelBuilder _filterProductViewModelBuilder;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly InfinityScrollService _infinityScrollService;

        public PNHandleCustSolutionsController(
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
        [HttpPost]
        [Route("test")]
        [ValidateAntiForgeryToken]
        public IActionResult Test()
        {
            var test = new List<string>();
            test.Add("Hepp");
            test.Add("Hopp");
            test.Add("Hipp");
            test.Add("Hupp");
            test.Add("Happ");
            var ret = new Aspen
            {
                Name = "Aspenberg",
                List = test
            };
            return Ok(ret);
        }



    }

    public class Aspen
    {
        public string Name { get; set; }
        public List<string> List { get; set; }
    }
}
