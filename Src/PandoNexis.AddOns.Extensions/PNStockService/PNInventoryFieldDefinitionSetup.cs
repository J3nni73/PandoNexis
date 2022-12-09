using Litium.Accelerator;
using Litium.Accelerator.Definitions;
using Litium.Blocks;
using Litium.FieldFramework;
using Litium.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.Services.PNStockService
{
    internal class PNInventoryFieldDefinitionSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                new FieldDefinition<ProductArea>(PNInventoryConstants.IgnoreInventory, SystemFieldTypeConstants.Boolean)
                {
                    MultiCulture = false,
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                },
            };
            return fields;
        }
    } 
}