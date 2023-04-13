using Litium.Accelerator.Definitions;
using Litium.Blocks;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using PandoNexis.Accelerator.Extensions.Constants;

namespace PandoNexis.Accelerator.Extensions.Block.FieldDefinitions
{
    internal class BlockFieldDefinitions : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.BlockTitle, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.BlockSubTitle, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.BlockText, SystemFieldTypeConstants.Editor)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.Background, SystemFieldTypeConstants.TextOption)
                {
                   MultiCulture = false,
                   Option = new TextOption
                    {
                        MultiSelect = false,
                        Items = new List<TextOption.Item>
                        {
                            new TextOption.Item
                            {
                                Value = "primaryColor",
                                Name = new Dictionary<string, string> { { "en-US", "primaryColor" }, { "sv-SE", "primaryColor" } }
                            },
                            new TextOption.Item
                            {
                                Value = "secondaryColor",
                                Name = new Dictionary<string, string> { { "en-US", "secondaryColor" }, { "sv-SE", "secondaryColor" } }
                            },
                            new TextOption.Item
                            {
                                Value = "thirdColor",
                                Name = new Dictionary<string, string> { { "en-US", "thirdColor" }, { "sv-SE", "thirdColor" } }
                            }
                        }
                    }
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.ExtendedLinkText, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.ExtendedLinkToPage, SystemFieldTypeConstants.Pointer)
                {
                    MultiCulture = false,
                    Option = new PointerOption { EntityType = PointerTypeConstants.WebsitesPage }
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.ExtendedLinkToCategory, SystemFieldTypeConstants.Pointer)
                {
                    MultiCulture = false,
                    Option = new PointerOption { EntityType = PointerTypeConstants.ProductsCategory }
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.ExtendedLinkToProduct, SystemFieldTypeConstants.Pointer)
                {
                    MultiCulture = false,
                    Option = new PointerOption { EntityType = PointerTypeConstants.ProductsProduct }
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.ExtendedLinkToProductList, SystemFieldTypeConstants.Pointer)
                {
                    MultiCulture = false,
                    Option = new PointerOption { EntityType = PointerTypeConstants.ProductsProductList }
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.ExtendedLinkToFile, SystemFieldTypeConstants.Pointer)
                {
                    MultiCulture = false,
                    Option = new PointerOption { EntityType = PointerTypeConstants.MediaFile }
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.ExtendedLinkToYouTube, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = false,
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.ExtendedLinkToExternalUrl, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.ExtendedButtonSubText, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.ExtendedClass, SystemFieldTypeConstants.TextOption)
                {
                    Option = new TextOption
                    {
                        MultiSelect = false,
                        Items = new List<TextOption.Item>
                        {
                            new TextOption.Item
                            {
                                Value = "primaryButton",
                                Name = new Dictionary<string, string> { { "en-US", "primaryButton" }, { "sv-SE", "primaryButton" } }
                            },
                            new TextOption.Item
                            {
                                Value = "secondaryButton",
                                Name = new Dictionary<string, string> { { "en-US", "secondaryButton" }, { "sv-SE", "secondaryButton" } }
                            },
                            new TextOption.Item
                            {
                                Value = "thirdButton",
                                Name = new Dictionary<string, string> { { "en-US", "thirdButton" }, { "sv-SE", "thirdButton" } }
                            }
                        }
                    }
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.TextBlockItem, SystemFieldTypeConstants.MultiField)
                {
                    MultiCulture = false,
                     Option = new MultiFieldOption
                     {
                         IsArray = true,
                         Fields  = new List<string>()
                         {
                             BlockFieldNameConstants.BlockTitle,
                             BlockFieldNameConstants.BlockSubTitle,
                             BlockFieldNameConstants.BlockText,
                             BlockFieldNameConstants.Background,
                             BlockFieldNameConstants.ExtendedLinkText,
                             BlockFieldNameConstants.ExtendedLinkToPage,
                             BlockFieldNameConstants.ExtendedLinkToCategory,
                             BlockFieldNameConstants.ExtendedLinkToProduct,
                             BlockFieldNameConstants.ExtendedLinkToProductList,
                             BlockFieldNameConstants.ExtendedLinkToFile,
                             BlockFieldNameConstants.ExtendedLinkToYouTube,
                             BlockFieldNameConstants.ExtendedLinkToExternalUrl,
                             BlockFieldNameConstants.ExtendedClass,
                         }
                     }
                },
            };
            return fields;
        }
    }
}
