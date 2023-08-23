using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Security;
using Litium.Websites;
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
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.DataArea, NoErpOrderAdminConstants.NoErp),
                GetFieldOption(nameof(WebsiteArea),DataViewFieldNameConstants.AreaSource, NoErpOrderAdminConstants.OrderView),
                GetFieldOption(nameof(WebsiteArea),DataViewFieldNameConstants.AreaSource, NoErpOrderAdminConstants.OrderRowView),
                GetFieldOption(nameof(WebsiteArea),NoErpOrderAdminConstants.NoErpButtonNames, NoErpButtonConstants.ViewOrder),
            };
        
            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {
            var changes = new List<FieldMultiFieldChanges>()
            {
                GetMultiFieldChange(nameof(WebsiteArea), NoErpOrderAdminConstants.NoErpButtonLinks, NoErpOrderAdminConstants.NoErpButtonNames),
                GetMultiFieldChange(nameof(WebsiteArea), NoErpOrderAdminConstants.NoErpButtonLinks, DataViewFieldNameConstants.ButtonPagePointer),
                GetMultiFieldChange(nameof(WebsiteArea), NoErpOrderAdminConstants.NoErpButtonLinks, DataViewFieldNameConstants.UseConfirmation),
                GetMultiFieldChange(nameof(WebsiteArea), NoErpOrderAdminConstants.NoErpButtonLinks, DataViewFieldNameConstants.ConfirmationText),
                GetMultiFieldChange(nameof(WebsiteArea), NoErpOrderAdminConstants.NoErpButtonLinks, DataViewFieldNameConstants.ButtonOpenInModal),
                GetMultiFieldChange(nameof(WebsiteArea), NoErpOrderAdminConstants.NoErpButtonLinks, DataViewFieldNameConstants.EndPointMethod),
                GetMultiFieldChange(nameof(WebsiteArea), NoErpOrderAdminConstants.NoErpButtonLinks, DataViewFieldNameConstants.FieldTooltipMessage),
            };
            
            UpdateMultiFieldField(changes);
        }
    }
}
