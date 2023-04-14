using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Security;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Definitions
{
    public class FieldHelperSource : FieldHelper
    {
        public FieldHelperSource(FieldDefinitionService fieldDefinitionService, SecurityContextService securityContextService, LanguageService languageService) : base(fieldDefinitionService, securityContextService, languageService)
        {
        }

        public override void HandleFieldOptions()
        {
            var changes = new List<FieldOptionChanges>();
            changes.Add(new FieldOptionChanges
            {
                FieldDefinitionId = PageFieldNameConstants.DataArea,
                Area = "WebsiteArea",
                Options = new List<TextOption.Item>
                        {
                            new TextOption.Item
                            {
                                Value = ProcessorConstants.ProductArea,
                                Name = new Dictionary<string, string> { { "en-US", ProcessorConstants.ProductArea }, { "sv-SE", ProcessorConstants.ProductArea } }
                            },
                            new TextOption.Item
                            {
                                Value = ProcessorConstants.CustomerArea,
                                Name = new Dictionary<string, string> { { "en-US", ProcessorConstants.CustomerArea }, { "sv-SE", ProcessorConstants.CustomerArea } }
                            },
                        }
            });

            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {
           
        }
    }
}
