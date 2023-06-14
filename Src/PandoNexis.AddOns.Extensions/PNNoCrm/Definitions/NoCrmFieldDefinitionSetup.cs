using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;

namespace PandoNexis.AddOns.Extensions.PNNoCrm.Definitions
{
    internal class NoCrmFieldDefinitionSetup : Litium.Accelerator.Definitions.FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>
            {
                new FieldDefinition<WebsiteArea>(NoCrmProcessorConstants.NoCrmButtonNames, SystemFieldTypeConstants.TextOption)
                {
                      Option = new TextOption 
                      {
                        MultiSelect = false,
                        Items = new List<TextOption.Item>()
                      }
                }, 
                
                new FieldDefinition<WebsiteArea>(NoCrmProcessorConstants.NoCrmButtonLinks, SystemFieldTypeConstants.MultiField)
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
