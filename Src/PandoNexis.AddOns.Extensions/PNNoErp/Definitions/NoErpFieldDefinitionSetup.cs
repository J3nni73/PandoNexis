using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Definitions
{
    internal class NoErpFieldDefinitionSetup : Litium.Accelerator.Definitions.FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>
            {
                new FieldDefinition<CustomerArea>(NoErpOrderAdminConstants.Authorization, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<CustomerArea>(NoErpOrderAdminConstants.BaseUrl, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<WebsiteArea>(NoErpOrderAdminConstants.NoErpButtonNames, SystemFieldTypeConstants.TextOption)
                {
                      Option = new TextOption 
                      {
                        MultiSelect = false,
                        Items = new List<TextOption.Item>()
                      }
                }, 
                
                new FieldDefinition<WebsiteArea>(NoErpOrderAdminConstants.NoErpButtonLinks, SystemFieldTypeConstants.MultiField)
                {
                    MultiCulture = false,
                     Option = new MultiFieldOption
                     {
                         IsArray = true,
                         Fields  = new List<string>()
                     }
                },
            };
            return fields;
        }
    }
}
