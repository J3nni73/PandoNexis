using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Security;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;

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
                GetFieldOption(FieldHelperConstants.WebsiteArea, DataViewFieldNameConstants.DataArea, ProcessorConstants.ProductArea),
                GetFieldOption(FieldHelperConstants.WebsiteArea, DataViewFieldNameConstants.DataArea, ProcessorConstants.CustomerArea),
                GetFieldOption(FieldHelperConstants.WebsiteArea, DataViewFieldNameConstants.AlignContainers, PageFieldOptionValues.Left),
                GetFieldOption(FieldHelperConstants.WebsiteArea, DataViewFieldNameConstants.AlignContainers, PageFieldOptionValues.Center),
                GetFieldOption(FieldHelperConstants.WebsiteArea, DataViewFieldNameConstants.AlignContainers, PageFieldOptionValues.Right),
                };

            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {
        }
    }
}
