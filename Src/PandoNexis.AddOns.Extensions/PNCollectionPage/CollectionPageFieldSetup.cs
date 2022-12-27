using Litium.Accelerator.Definitions;
using Litium.Blocks;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNCollectionPage
{
    internal class CollectionPageFieldSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                new FieldDefinition<WebsiteArea>( CollectionPageFieldNameConstants.CollectionPageTitle, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<WebsiteArea>( CollectionPageFieldNameConstants.CollectionPageDescription, SystemFieldTypeConstants.Editor)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<WebsiteArea>( CollectionPageFieldNameConstants.CollectionPageImage, SystemFieldTypeConstants.MediaPointerImage)
                {
                    MultiCulture = false,
                },
                new FieldDefinition<WebsiteArea>( CollectionPageFieldNameConstants.CollectionPageChildPageButtonText, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<WebsiteArea>( CollectionPageFieldNameConstants.CollectionPageParentPageButtonText, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<WebsiteArea>( CollectionPageFieldNameConstants.CollectionPageUseFilter, SystemFieldTypeConstants.Boolean)
                {
                    MultiCulture = false,
                },
                 new FieldDefinition<WebsiteArea>( CollectionPageFieldNameConstants.CollectionFilterField1Name, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                   new FieldDefinition<WebsiteArea>( CollectionPageFieldNameConstants.CollectionFilterField2Name, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                     new FieldDefinition<WebsiteArea>( CollectionPageFieldNameConstants.CollectionFilterField3Name, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                       new FieldDefinition<WebsiteArea>( CollectionPageFieldNameConstants.CollectionFilterField1Value, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                   new FieldDefinition<WebsiteArea>( CollectionPageFieldNameConstants.CollectionFilterField2Value, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                     new FieldDefinition<WebsiteArea>( CollectionPageFieldNameConstants.CollectionFilterField3Value, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                    new FieldDefinition<WebsiteArea>(CollectionPageFieldNameConstants.CollectionPageLink, SystemFieldTypeConstants.Pointer)
                {
                    Option = new PointerOption { EntityType = PointerTypeConstants.WebsitesPage, MultiSelect = false }
                },
                    new FieldDefinition<WebsiteArea>(CollectionPageFieldNameConstants.CollectionPageLinkText, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
            };
            return fields;
        }
    }
}
