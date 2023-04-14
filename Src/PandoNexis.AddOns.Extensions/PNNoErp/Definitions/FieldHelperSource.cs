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
                GetFieldOption(FieldHelperConstants.WebsiteArea, PageFieldNameConstants.DataArea, NoErpOrderAdminConstants.NoErp),
                GetFieldOption(FieldHelperConstants.WebsiteArea,PageFieldNameConstants.AreaSource, NoErpOrderAdminConstants.OrderView),
                GetFieldOption(FieldHelperConstants.WebsiteArea,PageFieldNameConstants.AreaSource, NoErpOrderAdminConstants.OrderRowView),
                GetFieldOption(FieldHelperConstants.WebsiteArea,NoErpOrderAdminConstants.ButtonName, NoErpButtonConstants.ViewOrder),
            };
        
            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {
            var changes = new List<FieldMultiFieldChanges>()
            {
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoErpOrderAdminConstants.ButtonLinks, NoErpOrderAdminConstants.ButtonName),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoErpOrderAdminConstants.ButtonLinks, NoErpOrderAdminConstants.ButtonPagePointer)
            };
            
            UpdateMultiFieldField(changes);
        }
    }
}
