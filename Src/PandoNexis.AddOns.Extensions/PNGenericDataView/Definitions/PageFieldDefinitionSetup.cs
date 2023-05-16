using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;

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
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.DataArea, SystemFieldTypeConstants.TextOption)
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
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.AreaSource, SystemFieldTypeConstants.TextOption)
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
                
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.DisplayTypes, SystemFieldTypeConstants.TextOption)
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
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.ReactClass, SystemFieldTypeConstants.Text),
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.HasMegaMenu, SystemFieldTypeConstants.Boolean),
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.EnableDropZone, SystemFieldTypeConstants.Boolean),

                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.ColumnsInsideContainerSmall, SystemFieldTypeConstants.Int),
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.ColumnsInsideContainerMedium, SystemFieldTypeConstants.Int),
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.ColumnsInsideContainerLarge, SystemFieldTypeConstants.Int),
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.ColumnsWithContainersSmall, SystemFieldTypeConstants.Int),
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.ColumnsWithContainersMedium, SystemFieldTypeConstants.Int),
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.ColumnsWithContainersLarge, SystemFieldTypeConstants.Int),
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.AlignContainers, SystemFieldTypeConstants.TextOption)
                {
                    Option = new TextOption {
                        MultiSelect = false,
                        Items = new List<TextOption.Item>
                        {
                         
                        }
                    }
                },
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.ButtonPagePointer, SystemFieldTypeConstants.Pointer)
                {
                    Option = new PointerOption { EntityType = PointerTypeConstants.WebsitesPage }
                },

                //sätts per addon
                //new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.ButtonText, SystemFieldTypeConstants.Text)
                //{
                //    MultiCulture = true,
                //},
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.UseConfirmation, SystemFieldTypeConstants.Boolean),
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.ConfirmationText, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },                  
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.ButtonOpenInModal, SystemFieldTypeConstants.Boolean),
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.EndPointMethod, SystemFieldTypeConstants.TextOption)
                {
                    Option = new TextOption {
                        MultiSelect = false,
                        Items = new List<TextOption.Item>
                        {

                        }
                    }
                },
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.FieldTooltipMessage, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                }, 
                new FieldDefinition<WebsiteArea>(DataViewFieldNameConstants.HideButton, SystemFieldTypeConstants.Boolean),
            };
            return fields;
        }
    }
}
