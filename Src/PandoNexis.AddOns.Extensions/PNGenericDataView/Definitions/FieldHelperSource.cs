using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Security;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;
using static PandoNexis.AddOns.Extensions.PNGenericDataView.Constants.PageFieldNameConstants;

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
                GetFieldOption(FieldHelperConstants.WebsiteArea, PageFieldNameConstants.DataArea, ProcessorConstants.ProductArea),
                GetFieldOption(FieldHelperConstants.WebsiteArea, PageFieldNameConstants.DataArea, ProcessorConstants.CustomerArea),
                GetFieldOption(FieldHelperConstants.WebsiteArea, PageFieldNameConstants.AlignContainers, PageFieldOptionValues.Left),
                GetFieldOption(FieldHelperConstants.WebsiteArea, PageFieldNameConstants.AlignContainers, PageFieldOptionValues.Center),
                GetFieldOption(FieldHelperConstants.WebsiteArea, PageFieldNameConstants.AlignContainers, PageFieldOptionValues.Right),
                };

            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {
            throw new NotImplementedException();
        }
    }
}
