using Litium.Accelerator.Constants;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Security;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.Accelerator.Extensions.Constants;
using HeaderLayoutConstants = PandoNexis.Accelerator.Extensions.Constants.HeaderLayoutConstants;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Definitions
{
    public class FieldHelperSource : FieldHelper
    {
        public FieldHelperSource(FieldDefinitionService fieldDefinitionService, SecurityContextService securityContextService, LanguageService languageService) : base(fieldDefinitionService, securityContextService, languageService)
        {
        }

        public override void HandleFieldOptions()
        {
            var changes = new List<FieldOptionChanges>(){
                GetFieldOption(nameof(WebsiteArea), AcceleratorWebsiteFieldNameConstants.HeaderLayout, HeaderLayoutConstants.OneRow),
                GetFieldOption(nameof(WebsiteArea), AcceleratorWebsiteFieldNameConstants.HeaderLayout, HeaderLayoutConstants.TwoRows),
               };

            UpdateFieldOptions(changes);
        }

        public override void HandleMultiFieldFields()
        {
        }
    }
}
