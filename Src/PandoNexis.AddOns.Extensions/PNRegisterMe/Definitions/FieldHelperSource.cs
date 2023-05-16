using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Security;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;

namespace PandoNexis.AddOns.Extensions.PNRegisterMe.Definitions
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
                GetFieldOption(FieldHelperConstants.WebsiteArea, DataViewFieldNameConstants.DataArea, ProcessorConstants.CustomerArea),
                GetFieldOption(FieldHelperConstants.WebsiteArea,DataViewFieldNameConstants.AreaSource, RegisterMeConstants.RegisterMePerson),
                GetFieldOption(FieldHelperConstants.WebsiteArea,DataViewFieldNameConstants.AreaSource, RegisterMeConstants.RegisterMeOrganization),
            };

            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {
            return ;
        }
    }
}
