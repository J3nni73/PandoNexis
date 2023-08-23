using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Security;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;

namespace PandoNexis.AddOns.Extensions.PNNoCrm.Definitions
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
                GetFieldOption(nameof(WebsiteArea),DataViewFieldNameConstants.DataArea, NoCrmProcessorConstants.NoCrm),
                GetFieldOption(nameof(WebsiteArea),DataViewFieldNameConstants.AreaSource, NoCrmProcessorConstants.Groups),
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.AreaSource, NoCrmProcessorConstants.PersonListByGroup),
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.AreaSource, NoCrmProcessorConstants.SendMail),
                GetFieldOption(nameof(WebsiteArea),NoCrmProcessorConstants.NoCrmButtonNames, NoCrmProcessorConstants.ViewPersonListByGroup),
                GetFieldOption(nameof(WebsiteArea),NoCrmProcessorConstants.NoCrmButtonNames, NoCrmProcessorConstants.CreatePerson),
                GetFieldOption(nameof(WebsiteArea),NoCrmProcessorConstants.NoCrmButtonNames, NoCrmProcessorConstants.AddLogin),
                GetFieldOption(nameof(WebsiteArea),NoCrmProcessorConstants.NoCrmButtonNames, NoCrmProcessorConstants.ResetPassword),
                GetFieldOption(nameof(WebsiteArea),NoCrmProcessorConstants.NoCrmButtonNames, NoCrmProcessorConstants.SendMail),

            };

            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {

            var changes = new List<FieldMultiFieldChanges>()
            {
                GetMultiFieldChange(nameof(WebsiteArea), NoCrmProcessorConstants.NoCrmButtonLinks, NoCrmProcessorConstants.NoCrmButtonNames),
                GetMultiFieldChange(nameof(WebsiteArea), NoCrmProcessorConstants.NoCrmButtonLinks, DataViewFieldNameConstants.ButtonPagePointer),
                GetMultiFieldChange(nameof(WebsiteArea), NoCrmProcessorConstants.NoCrmButtonLinks, DataViewFieldNameConstants.UseConfirmation),
                GetMultiFieldChange(nameof(WebsiteArea), NoCrmProcessorConstants.NoCrmButtonLinks, DataViewFieldNameConstants.ConfirmationText),
                GetMultiFieldChange(nameof(WebsiteArea), NoCrmProcessorConstants.NoCrmButtonLinks, DataViewFieldNameConstants.ButtonOpenInModal),
                GetMultiFieldChange(nameof(WebsiteArea), NoCrmProcessorConstants.NoCrmButtonLinks, DataViewFieldNameConstants.EndPointMethod),
                GetMultiFieldChange(nameof(WebsiteArea), NoCrmProcessorConstants.NoCrmButtonLinks, DataViewFieldNameConstants.FieldTooltipMessage),
            };

            UpdateMultiFieldField(changes);
        }
    }
}
