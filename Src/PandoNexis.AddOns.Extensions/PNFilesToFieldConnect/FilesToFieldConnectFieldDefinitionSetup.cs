using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Products;
using PandoNexis.AddOns.Extensions.Services.PNStockService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNFilesToFieldConnect
{

    internal class FilesToFieldConnectFieldDefinitionSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                new FieldDefinition<ProductArea>(FilesToFieldConnectConstants.ProductCertificates, SystemFieldTypeConstants.TextOption)
                {
                    MultiCulture = false,
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,

                    Option = new TextOption
                    {
                        MultiSelect = false,
                        Items = new List<TextOption.Item>()
                    }
                },
            };
            return fields;
        }
    }
}
