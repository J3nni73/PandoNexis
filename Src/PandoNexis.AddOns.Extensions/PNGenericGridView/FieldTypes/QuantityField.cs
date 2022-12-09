using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using Litium.Accelerator.Builders.Framework;
using Litium.Accelerator.Routing;
using Litium.Products;
using Litium.Sales;
using Litium.Owin.InversionOfControl;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.FieldTypes
{
    [Plugin("Quantity")]
    public class QuantityField : GridSpecialFieldDataBase
    {
        private readonly CartContextAccessor _cartContextAccessor;
        private readonly CartViewModelBuilder _cartViewModelBuilder;
        private readonly RequestModelAccessor _requestModelAccessor;
        public QuantityField(CartContextAccessor cartContextAccessor,
                                        CartViewModelBuilder cartViewModelBuilder,
                                        RequestModelAccessor requestModelAccessor)
        {
            _cartContextAccessor = cartContextAccessor;
            _cartViewModelBuilder = cartViewModelBuilder;
            _requestModelAccessor = requestModelAccessor;
        }
        public override string GetValue(GenericGridViewField field, Variant variant)
        {
            var res = _requestModelAccessor.RequestModel.CurrentPageModel.SystemId;

            var cartContext = _cartContextAccessor.CartContext;

            var result = _cartViewModelBuilder.Build(cartContext).OrderRows.Where(x => x.ArticleNumber == variant.Id).Select(x => x.Quantity).FirstOrDefault();

            return result.ToString();
        }
        public override string GetGridViewFieldType()
        {
            return "decimal";
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
