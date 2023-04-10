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

namespace PandoNexis.AddOns.Extensions.PNPortalPage
{
    internal class PortalPageFieldSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                new FieldDefinition<WebsiteArea>( PortalPageFieldNameConstants.PortalPageTitle, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<WebsiteArea>( PortalPageFieldNameConstants.PortalPageDescription, SystemFieldTypeConstants.Editor)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<WebsiteArea>( PortalPageFieldNameConstants.PortalPageImage, SystemFieldTypeConstants.MediaPointerImage)
                {
                    MultiCulture = false,
                },
                new FieldDefinition<WebsiteArea>( PortalPageFieldNameConstants.PortalPageChildPageButtonText, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<WebsiteArea>( PortalPageFieldNameConstants.PortalPageParentPageButtonText, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<WebsiteArea>( PortalPageFieldNameConstants.PortalPageUseFilter, SystemFieldTypeConstants.Boolean)
                {
                    MultiCulture = false,
                },
                 new FieldDefinition<WebsiteArea>( PortalPageFieldNameConstants.PortalFilterField1Name, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                   new FieldDefinition<WebsiteArea>( PortalPageFieldNameConstants.PortalFilterField2Name, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                     new FieldDefinition<WebsiteArea>( PortalPageFieldNameConstants.PortalFilterField3Name, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                       new FieldDefinition<WebsiteArea>( PortalPageFieldNameConstants.PortalFilterField1Value, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                   new FieldDefinition<WebsiteArea>( PortalPageFieldNameConstants.PortalFilterField2Value, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                     new FieldDefinition<WebsiteArea>( PortalPageFieldNameConstants.PortalFilterField3Value, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                    new FieldDefinition<WebsiteArea>(PortalPageFieldNameConstants.PortalPageLink, SystemFieldTypeConstants.Pointer)
                {
                    Option = new PointerOption { EntityType = PointerTypeConstants.WebsitesPage, MultiSelect = false }
                },
                    new FieldDefinition<WebsiteArea>(PortalPageFieldNameConstants.PortalPageLinkText, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
            };
            return fields;
        }
    }
}
