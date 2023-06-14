using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Security;
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
                GetFieldOption(FieldHelperConstants.WebsiteArea,DataViewFieldNameConstants.AreaSource, NoCrmProcessorConstants.Groups),
                GetFieldOption(FieldHelperConstants.WebsiteArea, DataViewFieldNameConstants.AreaSource, NoCrmProcessorConstants.PersonListByGroup),
                GetFieldOption(FieldHelperConstants.WebsiteArea,NoCrmProcessorConstants.NoCrmButtonNames, NoCrmProcessorConstants.ViewPersonListByGroup),
            };

            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {

            var changes = new List<FieldMultiFieldChanges>()
            {
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoCrmProcessorConstants.NoCrmButtonLinks, NoCrmProcessorConstants.NoCrmButtonNames),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoCrmProcessorConstants.NoCrmButtonLinks, DataViewFieldNameConstants.ButtonPagePointer),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoCrmProcessorConstants.NoCrmButtonLinks, DataViewFieldNameConstants.UseConfirmation),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoCrmProcessorConstants.NoCrmButtonLinks, DataViewFieldNameConstants.ConfirmationText),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoCrmProcessorConstants.NoCrmButtonLinks, DataViewFieldNameConstants.ButtonOpenInModal),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoCrmProcessorConstants.NoCrmButtonLinks, DataViewFieldNameConstants.EndPointMethod),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, NoCrmProcessorConstants.NoCrmButtonLinks, DataViewFieldNameConstants.FieldTooltipMessage),
            };

            UpdateMultiFieldField(changes);
        }
    }
}
