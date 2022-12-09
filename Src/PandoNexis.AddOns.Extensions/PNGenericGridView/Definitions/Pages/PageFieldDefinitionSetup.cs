using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Definitions.Pages
{
    internal class PageFieldDefinitionSetup : Litium.Accelerator.Definitions.FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>();

            fields.AddRange(GenericGridView());

            return fields;
        }

        private IEnumerable<FieldDefinition> GenericGridView()
        {
            var fields = new List<FieldDefinition>
            {
                new FieldDefinition<WebsiteArea>(Constants.PageFieldNameConstants.DataSource, SystemFieldTypeConstants.TextOption)
                {
                    Option = new TextOption { MultiSelect = true}
                },
                
                new FieldDefinition<WebsiteArea>(Constants.PageFieldNameConstants.ReactClass, SystemFieldTypeConstants.Text),
                new FieldDefinition<WebsiteArea>(Constants.PageFieldNameConstants.HasMegaMenu, SystemFieldTypeConstants.Boolean),
                new FieldDefinition<WebsiteArea>(Constants.PageFieldNameConstants.ExtraElementAttributes, SystemFieldTypeConstants.Text),
                new FieldDefinition<WebsiteArea>(Constants.PageFieldNameConstants.EnableDropZone, SystemFieldTypeConstants.Boolean),
            };
            return fields;
        }
    }
}
