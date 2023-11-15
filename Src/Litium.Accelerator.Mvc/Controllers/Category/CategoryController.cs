using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Litium.Accelerator.Builders.Product;
using Litium.Web.Models.Products;
using Litium.Web.Rendering;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.Accelerator.Extensions.Builders.Product;
using PandoNexis.Accelerator.Extensions.ViewModels.Product;

namespace Litium.Accelerator.Mvc.Controllers.Category
{
    public class CategoryController : ControllerBase
    {
        private readonly PNCategoryPageViewModelBuilder _categoryPageViewModelBuilder;
        private readonly IEnumerable<IRenderingValidator<Products.Category>> _renderingValidators;

        public CategoryController(
            PNCategoryPageViewModelBuilder categoryPageViewModelBuilder,
            IEnumerable<IRenderingValidator<Products.Category>> renderingValidators)
        {
            _categoryPageViewModelBuilder = categoryPageViewModelBuilder;
            _renderingValidators = renderingValidators;
        }

        public async Task<ActionResult> Index(CategoryModel currentCategoryModel)
        {
            if (!_renderingValidators.Validate(currentCategoryModel.Category))
            {
                return NotFound();
            }

            var model = await _categoryPageViewModelBuilder.BuildAsync(currentCategoryModel.SystemId);
            var pnModel = (PNCategoryPageViewModel)model;
            if (pnModel != null)
            {
                return View(pnModel);
            }
            return View(model);
        }
    }
}
