using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Security;
using Litium.Websites;
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
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.DataArea, ProcessorConstants.ProductArea),
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.DataArea, ProcessorConstants.CustomerArea),
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.AlignContainers, PageFieldOptionValues.Left),
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.AlignContainers, PageFieldOptionValues.Center),
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.AlignContainers, PageFieldOptionValues.Right),
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.DisplayTypes, DataViewFieldNameConstants.Cards),
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.DisplayTypes, DataViewFieldNameConstants.Table),
                };

            UpdateFieldOptions(changes);
        }

        public override void HandleMultiFieldFields()
        {
        }
    }
}
