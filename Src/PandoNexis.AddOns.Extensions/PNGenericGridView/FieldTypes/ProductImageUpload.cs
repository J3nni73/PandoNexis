using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using Litium.Accelerator.Builders.Framework;
using Litium.Accelerator.Routing;
using Litium.FieldFramework;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using Litium.Sales;
using Litium.Web.Models;
using Litium.Web.Models.Products;
using System.Drawing;
using Litium.Owin.InversionOfControl;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.FieldTypes
{
    [Plugin("ProductImageUpload")]
    public class ProductImageUpload : GridSpecialFieldDataBase
    {
        private readonly CartContextAccessor _cartContextAccessor;
        private readonly CartViewModelBuilder _cartViewModelBuilder;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly ProductModelBuilder _productModelBuilder;
        private readonly BaseProductService _baseProductService;
        public ProductImageUpload(CartContextAccessor cartContextAccessor,
                                        CartViewModelBuilder cartViewModelBuilder,
                                        RequestModelAccessor requestModelAccessor, ProductModelBuilder productModelBuilder, BaseProductService baseProductService)
        {
            _cartContextAccessor = cartContextAccessor;
            _cartViewModelBuilder = cartViewModelBuilder;
            _requestModelAccessor = requestModelAccessor;
            _productModelBuilder = productModelBuilder;
            _baseProductService = baseProductService;
        }
        public override string GetValue(GenericGridViewField field, Variant variant)
        {
            return string.Empty;
            // Start

            var productModel = _productModelBuilder.BuildFromVariant(variant);
            var baseProduct = _baseProductService.Get(variant?.BaseProductSystemId ?? Guid.Empty);

            var image = (variant?.Fields.GetValue<IList<Guid>>(SystemFieldDefinitionConstants.Images)?.FirstOrDefault()
               ?? baseProduct?.Fields.GetValue<IList<Guid>>(SystemFieldDefinitionConstants.Images)?.FirstOrDefault()).MapTo<ImageModel>();


            return image?.GetUrlToImage(Size.Empty, new Size(200, 120)).Url.ToString();
        }
        public override string GetGridViewFieldType()
        {
            return "productimageupload";
        }

        public override void AddSpecialFieldToCartAsync(string fieldId, decimal quantity)
        {
            var cartContext = _cartContextAccessor.CartContext;
            cartContext.AddOrUpdateItemAsync(new AddOrUpdateCartItemArgs
            {
                ArticleNumber = fieldId,
                Quantity = quantity,
                ConstantQuantity = true,
            }).Wait();

            _cartViewModelBuilder.Build(cartContext);

        }
    }
}
