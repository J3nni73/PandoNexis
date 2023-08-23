using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;

namespace PandoNexis.AddOns.Extensions.PNCollectionPage
{
    internal class CollectionPageFieldSetup : FieldDefinitionSetup
    {
        public readonly FieldHelper _fieldHelper;

        public CollectionPageFieldSetup(FieldHelper fieldHelper)
        {
            _fieldHelper = fieldHelper;
        }

        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionPageTitle, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionPageDescription, SystemFieldTypeConstants.Editor, true),
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionPageImage, SystemFieldTypeConstants.MediaPointerImage),
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionPageChildPageButtonText, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionPageParentPageButtonText, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionPageUseFilter, SystemFieldTypeConstants.Boolean),
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionFilterField1Name, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionFilterField2Name, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionFilterField3Name, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionFilterField1Value, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionFilterField2Value, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionFilterField3Value, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetWebsitePointerField(CollectionPageFieldNameConstants.CollectionPageLink, SystemFieldTypeConstants.Pointer, PointerTypeConstants.WebsitesPage),
                _fieldHelper.GetWebsiteFieldDefinition(CollectionPageFieldNameConstants.CollectionPageLinkText, SystemFieldTypeConstants.Text, true),
                
            };
            return fields;
        }
    }
}
