using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Security;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Definitions
{
    public class FieldHelperSource : FieldHelper
    {
        public FieldHelperSource(FieldDefinitionService fieldDefinitionService, SecurityContextService securityContextService, LanguageService languageService) : base(fieldDefinitionService, securityContextService, languageService)
        {
        }

        public override void HandleFieldOptions()
        {
            var changes = new List<FieldOptionChanges>()
            {
                GetFieldOption(FieldHelperConstants.WebsiteArea, DataViewFieldNameConstants.DataArea, NoErpOrderAdminConstants.NoErp),
                GetFieldOption(FieldHelperConstants.WebsiteArea,DataViewFieldNameConstants.AreaSource, NoErpOrderAdminConstants.OrderView),
                GetFieldOption(FieldHelperConstants.WebsiteArea,DataViewFieldNameConstants.AreaSource, NoErpOrderAdminConstants.OrderRowView),
                GetFieldOption(FieldHelperConstants.WebsiteArea,NoErpOrderAdminConstants.NoErpButtonNames, NoErpButtonConstants.ViewOrder),
            };
        
            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {
            var changes = new List<FieldMultiFieldChanges>()
            {
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoErpOrderAdminConstants.NoErpButtonLinks, NoErpOrderAdminConstants.NoErpButtonNames),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoErpOrderAdminConstants.NoErpButtonLinks, DataViewFieldNameConstants.ButtonPagePointer),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoErpOrderAdminConstants.NoErpButtonLinks, DataViewFieldNameConstants.UseConfirmation),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoErpOrderAdminConstants.NoErpButtonLinks, DataViewFieldNameConstants.ConfirmationText),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoErpOrderAdminConstants.NoErpButtonLinks, DataViewFieldNameConstants.ButtonOpenInModal),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoErpOrderAdminConstants.NoErpButtonLinks, DataViewFieldNameConstants.EndPointMethod),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoErpOrderAdminConstants.NoErpButtonLinks, DataViewFieldNameConstants.FieldTooltipMessage),
            };
            
            UpdateMultiFieldField(changes);
        }
    }
}
