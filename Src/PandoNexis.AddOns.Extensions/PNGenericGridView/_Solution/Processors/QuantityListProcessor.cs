using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Services;
using Litium.Accelerator.Builders.Framework;
using Litium.Runtime.DependencyInjection;
using Litium.Sales;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Processors;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.Processors
{
    [Service(Name = "QuantityList")]
    public class QuantityListProcessor : GridViewDataProcessorBase
    {
        private const string _processorName = GenericGridView_ProductTemplateNameConstants.QuickBuy;
        private readonly GenericGridViewService _genericGridViewService;
        private readonly CartContextAccessor _cartContextAccessor;
        private readonly CartViewModelBuilder _cartViewModelBuilder;

        public QuantityListProcessor(GenericGridViewService genericGridViewService, CartContextAccessor cartContextAccessor, CartViewModelBuilder cartViewModelBuilder)
        {
            _genericGridViewService = genericGridViewService;
            _cartContextAccessor = cartContextAccessor;
            _cartViewModelBuilder = cartViewModelBuilder;

        }

        public override async Task<object> HandleFormData(string data)
        {
            throw new NotImplementedException();
        }

        public override async Task<object> GetGridForm(string data)
        {
            // Skapa dropdown månader i 
            var fields = _genericGridViewService.GetFieldsToForm(_processorName);
            return fields;
        }

        public override async Task<object> GetGridView(string data)
        {
            var gridView = new GenericGridView
            {
                DataRows = new List<GenericGridViewRow>(),
                Settings = new GenericGridViewSettings(50, 50)
            };

            var variantSystemIds = _genericGridViewService.BuildAsync(_processorName)?.Result;
            if (variantSystemIds == null || variantSystemIds.Count == 0) return gridView;
            var fields = _genericGridViewService.GetFields(_processorName);
            if (fields == null || fields.Count == 0) return gridView;
            gridView.DataRows.AddRange(_genericGridViewService.BuildProductRows(variantSystemIds, fields));

            return gridView;
        }

        public override async Task<object> UpdateRow(string data)
        {
            if (data == null) return null;
            var rowFields = _genericGridViewService.UpdateProduct(data, _processorName);
            //return _genericGridViewService.UpdateProduct(data, _processorName);
            var cartContext = _cartContextAccessor.CartContext;

            return new
            {
                Cart = _cartViewModelBuilder.Build(cartContext),
                Row = rowFields
            };
        }

        public override Task<object> GetGridViewForExport(string data)
        {
            throw new NotImplementedException();
        }

    }
}

