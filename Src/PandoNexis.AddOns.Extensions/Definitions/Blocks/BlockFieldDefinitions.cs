using Litium.Accelerator.Definitions;
using Litium.Accelerator.Search;
using Litium.Blocks;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using PandoNexis.AddOns.Extensions.Block.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.Definitions.Blocks
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
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.Summary, SystemFieldTypeConstants.Editor)
                {
                    MultiCulture = true,
                },
                   new FieldDefinition<BlockArea>(BlockFieldNameConstants.BlockImage, SystemFieldTypeConstants.MediaPointerImage)
                {
                    MultiCulture = true,
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.BlockMobilImage, SystemFieldTypeConstants.MediaPointerImage)
                {
                    MultiCulture = true,
                },
                 new FieldDefinition<BlockArea>(BlockFieldNameConstants.BlockOverlayImage, SystemFieldTypeConstants.MediaPointerImage)
                {
                    MultiCulture = false,
                },
                 new FieldDefinition<BlockArea>(BlockFieldNameConstants.BlockOverlayLeft, SystemFieldTypeConstants.Boolean)
                 {
                       MultiCulture = false,
                 },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.QuoteStyle, SystemFieldTypeConstants.Boolean)
                 {
                       MultiCulture = false,
                 },
                   new FieldDefinition<BlockArea>(BlockFieldNameConstants.GradiantOverlay, SystemFieldTypeConstants.Boolean)
                 {
                       MultiCulture = false,
                 },
                   new FieldDefinition<BlockArea>(BlockFieldNameConstants.CenteredText, SystemFieldTypeConstants.Boolean)
                 {
                       MultiCulture = false,
                 },
                   new FieldDefinition<BlockArea>(BlockFieldNameConstants.NegativeMargin, SystemFieldTypeConstants.Boolean)
                   {
                       MultiCulture = false,
                   },
                      new FieldDefinition<BlockArea>(BlockFieldNameConstants.BlockTitle2, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = true,
                },
                   new FieldDefinition<BlockArea>(BlockFieldNameConstants.BlockSubTitle2, SystemFieldTypeConstants.Text)
                   {
                       MultiCulture = true,
                   },
                   new FieldDefinition<BlockArea>(BlockFieldNameConstants.BlockText2, SystemFieldTypeConstants.Editor)
                   {
                       MultiCulture = true,
                   },
                   new FieldDefinition<BlockArea>(BlockFieldNameConstants.Reverse, SystemFieldTypeConstants.Boolean)
                   {
                       MultiCulture = false,
                   },
                    new FieldDefinition<BlockArea>(BlockFieldNameConstants.FullWidth, SystemFieldTypeConstants.Boolean)
                   {
                       MultiCulture = false,
                   },
                   new FieldDefinition<BlockArea>(BlockFieldNameConstants.Background, SystemFieldTypeConstants.TextOption)
                   {
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
                 new FieldDefinition<BlockArea>(BlockFieldNameConstants.InspirationalBlockItem, SystemFieldTypeConstants.MultiField)
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
                             BlockFieldNameConstants.BlockImage,
                             BlockFieldNameConstants.BlockVideo,
                             BlockFieldNameConstants.BlockSubTitle2,
                             BlockFieldNameConstants.BlockText2,
                             BlockFieldNameConstants.NegativeMargin,
                             BlockFieldNameConstants.FullWidth,
                             BlockFieldNameConstants.Reverse,
                             BlockFieldNameConstants.Background,
                             BlockFieldNameConstants.ExtendedLinkText,
                             BlockFieldNameConstants.ExtendedLinkToPage,
                             BlockFieldNameConstants.ExtendedLinkToCategory,
                             BlockFieldNameConstants.ExtendedLinkToProduct,
                             BlockFieldNameConstants.ExtendedLinkToProductList,
                             BlockFieldNameConstants.ExtendedLinkToFile,
                             BlockFieldNameConstants.ExtendedLinkToYouTube,
                             BlockFieldNameConstants.ExtendedLinkToExternalUrl,
                             BlockFieldNameConstants.ExtendedClass
                         }
                     }
                },

                new FieldDefinition<BlockArea>(BlockFieldNameConstants.ColumnBlockItem, SystemFieldTypeConstants.MultiField)
                {
                    MultiCulture = false,
                     Option = new MultiFieldOption
                     {
                         IsArray = true,
                         Fields  = new List<string>()
                         {
                             BlockFieldNameConstants.BlockTitle,
                             BlockFieldNameConstants.BlockText,
                             BlockFieldNameConstants.BlockImage,
                         }
                     }
                },
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.InfoTileBlockItem, SystemFieldTypeConstants.MultiField)
                {
                    MultiCulture = false,
                     Option = new MultiFieldOption
                     {
                         IsArray = true,
                         Fields  = new List<string>()
                         {
                             BlockFieldNameConstants.BlockTitle,
                             BlockFieldNameConstants.BlockText,
                             BlockFieldNameConstants.BlockImage,
                             BlockFieldNameConstants.BlockText2,
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
                new FieldDefinition<BlockArea>(BlockFieldNameConstants.UspBlockItem, SystemFieldTypeConstants.MultiField)
                {
                    MultiCulture = false,
                     Option = new MultiFieldOption
                     {
                         IsArray = true,
                         Fields  = new List<string>()
                         {
                             BlockFieldNameConstants.BlockTitle,
                             BlockFieldNameConstants.BlockText,
                             BlockFieldNameConstants.BlockImage,
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
                 new FieldDefinition<BlockArea>(BlockFieldNameConstants.ImageStatsBlockItem, SystemFieldTypeConstants.MultiField)
                {
                    MultiCulture = false,
                     Option = new MultiFieldOption
                     {
                         IsArray = true,
                         Fields  = new List<string>()
                         {

                             BlockFieldNameConstants.BlockText,
                             BlockFieldNameConstants.BlockText2,
                         }
                     }
                },
                  new FieldDefinition<BlockArea>(BlockFieldNameConstants.HeroBlockItem, SystemFieldTypeConstants.MultiField)
                {
                    MultiCulture = false,
                     Option = new MultiFieldOption
                     {
                         IsArray = true,
                         Fields  = new List<string>()
                         {
                             BlockFieldNameConstants.BlockTitle,
                             BlockFieldNameConstants.BlockText,
                             BlockFieldNameConstants.BlockImage,
                         }
                     }
                },
            };
            return fields;
        }
    }
}
