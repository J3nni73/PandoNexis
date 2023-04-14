using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PageFieldNameConstants = PandoNexis.AddOns.Extensions.PNGenericDataView.Constants.PageFieldNameConstants;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Definitions
{
    internal class PageFieldDefinitionSetup : Litium.Accelerator.Definitions.FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>();

            fields.AddRange(GenericDataView());

            return fields;
        }

        private IEnumerable<FieldDefinition> GenericDataView()
        {
            var fields = new List<FieldDefinition>
            {
                new FieldDefinition<WebsiteArea>(PageFieldNameConstants.DataArea, SystemFieldTypeConstants.TextOption)
                {
                    Option = new TextOption { 
                        MultiSelect = false, 
                        Items = new List<TextOption.Item>
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
                    }
                },
                new FieldDefinition<WebsiteArea>(PageFieldNameConstants.AreaSource, SystemFieldTypeConstants.TextOption)
                {
                    Option = new TextOption 
                    { 
                        MultiSelect = false,
                        Items = new List<TextOption.Item>
                        {
                            new TextOption.Item
                            {
                                Value = ProcessorConstants.ProductData,
                                Name = new Dictionary<string, string> { { "en-US", ProcessorConstants.ProductData }, { "sv-SE", ProcessorConstants.ProductArea} }
                            },
                        }
                    }
                },
                
                new FieldDefinition<WebsiteArea>(PageFieldNameConstants.DisplayTypes, SystemFieldTypeConstants.TextOption)
                {
                    Option = new TextOption
                    {
                        MultiSelect = true,
                        ManualSort= true,
                        Items = new List<TextOption.Item>
                        {
                            new TextOption.Item
                            {
                                Value = "table",
                                Name = new Dictionary<string, string> { { "en-US", "table" }, { "sv-SE", "table" } }
                            },
                            new TextOption.Item
                            {
                                Value = "cards",
                                Name = new Dictionary<string, string> { { "en-US", "cards" }, { "sv-SE", "cards" } }
                            },
                        }
                    }
                },
                new FieldDefinition<WebsiteArea>(PageFieldNameConstants.ReactClass, SystemFieldTypeConstants.Text),
                new FieldDefinition<WebsiteArea>(PageFieldNameConstants.HasMegaMenu, SystemFieldTypeConstants.Boolean),
                new FieldDefinition<WebsiteArea>(PageFieldNameConstants.EnableDropZone, SystemFieldTypeConstants.Boolean),

                new FieldDefinition<WebsiteArea>(PageFieldNameConstants.ColumnsInsideContainerSmall, SystemFieldTypeConstants.Int),
                new FieldDefinition<WebsiteArea>(PageFieldNameConstants.ColumnsInsideContainerMedium, SystemFieldTypeConstants.Int),
                new FieldDefinition<WebsiteArea>(PageFieldNameConstants.ColumnsInsideContainerLarge, SystemFieldTypeConstants.Int),
                new FieldDefinition<WebsiteArea>(PageFieldNameConstants.ColumnsWithContainersSmall, SystemFieldTypeConstants.Int),
                new FieldDefinition<WebsiteArea>(PageFieldNameConstants.ColumnsWithContainersMedium, SystemFieldTypeConstants.Int),
                new FieldDefinition<WebsiteArea>(PageFieldNameConstants.ColumnsWithContainersLarge, SystemFieldTypeConstants.Int),
            };
            return fields;
        }
    }
}
