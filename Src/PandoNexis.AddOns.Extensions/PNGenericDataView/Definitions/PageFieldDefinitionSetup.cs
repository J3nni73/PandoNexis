using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Definitions
{
    internal class PageFieldDefinitionSetup : Litium.Accelerator.Definitions.FieldDefinitionSetup
    {
        public readonly FieldHelper _fieldHelper;

        public PageFieldDefinitionSetup(FieldHelper fieldHelper)
        {
            _fieldHelper = fieldHelper;
        }

        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>();

            fields.AddRange(GenericDataView());

            return fields;
        }

        private IEnumerable<FieldDefinition> GenericDataView()
        {
            var fields = new List<FieldDefinition>
            {
                _fieldHelper.GetWebsiteTextOptionField(DataViewFieldNameConstants.DataArea, SystemFieldTypeConstants.TextOption),
                _fieldHelper.GetWebsiteTextOptionField(DataViewFieldNameConstants.AreaSource, SystemFieldTypeConstants.TextOption),
                _fieldHelper.GetWebsiteTextOptionField(DataViewFieldNameConstants.DisplayTypes, SystemFieldTypeConstants.TextOption, true, true),

                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.ReactClass, SystemFieldTypeConstants.Text),
                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.HasMegaMenu, SystemFieldTypeConstants.Boolean),
                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.EnableDropZone, SystemFieldTypeConstants.Boolean),

                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.ColumnsInsideContainerSmall, SystemFieldTypeConstants.Int),
                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.ColumnsInsideContainerMedium, SystemFieldTypeConstants.Int),
                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.ColumnsInsideContainerLarge, SystemFieldTypeConstants.Int),
                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.ColumnsWithContainersSmall, SystemFieldTypeConstants.Int),
                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.ColumnsWithContainersMedium, SystemFieldTypeConstants.Int),
                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.ColumnsWithContainersLarge, SystemFieldTypeConstants.Int),
                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.AlignContainers, SystemFieldTypeConstants.TextOption),
                _fieldHelper.GetWebsitePointerField(DataViewFieldNameConstants.ButtonPagePointer, SystemFieldTypeConstants.Pointer, PointerTypeConstants.WebsitesPage ),
                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.UseConfirmation, SystemFieldTypeConstants.Boolean),
                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.ConfirmationText, SystemFieldTypeConstants.Text, true),

                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.ButtonOpenInModal, SystemFieldTypeConstants.Boolean),
                _fieldHelper.GetWebsiteTextOptionField(DataViewFieldNameConstants.EndPointMethod, SystemFieldTypeConstants.TextOption),
                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.FieldTooltipMessage, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetWebsiteFieldDefinition(DataViewFieldNameConstants.HideButton, SystemFieldTypeConstants.Boolean),
            };
            return fields;
        }
    }
}
