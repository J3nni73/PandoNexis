using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;

namespace PandoNexis.AddOns.Extensions.PNMediaCatalog.Definitions.Pages
{
    internal class PageFieldDefinitionSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>();

            //fields.AddRange(GeneralFields());
            fields.AddRange(MediaCatalogFields());

            return fields;
        }

        private IEnumerable<FieldDefinition> GeneralFields()
        {
            var fields = new List<FieldDefinition>
            {

            };

            return fields;
        }

        private IEnumerable<FieldDefinition> MediaCatalogFields()
        {
            var fields = new List<FieldDefinition>
            {
                 new FieldDefinition<WebsiteArea>(Constants.PageFieldNameConstants.MediaCatalogPointer, SystemFieldTypeConstants.MediaPointerFile)
                {
                    Option = new PointerOption { EntityType = PointerTypeConstants.MediaFile, MultiSelect = false }
                },

                new FieldDefinition<WebsiteArea>(Constants.PageFieldNameConstants.AlternativeFirstCatalogName, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true
                },

                new FieldDefinition<WebsiteArea>(Constants.PageFieldNameConstants.AlternativeFolderView, SystemFieldTypeConstants.Boolean)
                {
                    MultiCulture = false
                },
            };
            return fields;
        }
    }
}
