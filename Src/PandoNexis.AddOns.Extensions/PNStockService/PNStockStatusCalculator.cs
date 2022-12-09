using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Customers;
//using Litium.Connect.Erp.Import;
using Litium.Products;
using Litium.Products.StockStatusCalculator;
using Litium.Websites;
using Litium.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Variant = Litium.Products.Variant;
using Litium.Runtime.DependencyInjection;

namespace PandoNexis.AddOns.Extensions.Services.PNStockService
{
    [Service(ServiceType = typeof(PNStockStatusCalculator))]
    public class PNStockStatusCalculator : IStockStatusCalculator
    {
        private readonly IStockStatusCalculator _parentResolver;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly PersonStorage _personStorage;
        private readonly VariantService _variantService;

        public PNStockStatusCalculator(IStockStatusCalculator parentResolver,
                                        RequestModelAccessor requestModelAccessor,
                                        PersonStorage personStorage,
                                        VariantService variantService)
        {
            _parentResolver = parentResolver;
            _requestModelAccessor = requestModelAccessor;
            _personStorage = personStorage;
            _variantService = variantService;
        }

        public ICollection<Inventory> GetInventories(StockStatusCalculatorArgs calculatorArgs)
        {
            return _parentResolver.GetInventories(calculatorArgs);
        }

        public IDictionary<Guid, StockStatusCalculatorResult> GetStockStatuses(StockStatusCalculatorArgs calculatorArgs, params StockStatusCalculatorItemArgs[] calculatorItemArgs)
        {

            var stockStatuses = _parentResolver.GetStockStatuses(calculatorArgs, calculatorItemArgs);
           
            return stockStatuses;
        }
    }
}
