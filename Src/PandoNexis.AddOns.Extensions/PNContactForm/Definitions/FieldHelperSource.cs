using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Security;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;

namespace PandoNexis.AddOns.Extensions.PNContactForm.Definitions
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
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.DataArea, ProcessorConstants.PersonArea),
                GetFieldOption(nameof(WebsiteArea),DataViewFieldNameConstants.AreaSource, ContactFormConstants.ContactForm),
            };

            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {
            return ;
        }
    }
}
