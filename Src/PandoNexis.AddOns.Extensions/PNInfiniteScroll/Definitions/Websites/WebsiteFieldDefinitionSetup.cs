using Litium.Websites;
using Litium.FieldFramework;
using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.FieldFramework.FieldTypes;
using Litium.Accelerator.Definitions;
using PandoNexis.AddOns.Extensions;
using PandoNexis.AddOns.Extensions.PNInfiniteScroll;

namespace PandoNexis.AddOns.Extensions.PNInfiniteScroll.Definitions.Websites
{
    internal class WebsiteFieldDefinitionSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            { 
                new FieldDefinition<WebsiteArea>(Constants.PNInfiniteScrollConstants.PaginationType, SystemFieldTypeConstants.TextOption)
                {
                    Option = new TextOption
                    {
                        MultiSelect = false,
                        Items = new List<TextOption.Item>
                        {
                            new TextOption.Item
                            {
                                Value = Constants.PNInfiniteScrollConstants.DefaultPagination,
                                Name = new Dictionary<string, string> { { "en-US", "Default Pagination" }, { "sv-SE", "Standard Paginering" } }
                            },
                            new TextOption.Item
                            {
                                Value = Constants.PNInfiniteScrollConstants.ShowMoreButton,
                                Name = new Dictionary<string, string> { { "en-US", "Show more button" }, { "sv-SE", "Visa mer knapp" } }
                            },
                            new TextOption.Item
                            {
                                Value = Constants.PNInfiniteScrollConstants.InfiniteScroll,
                                Name = new Dictionary<string, string> { { "en-US", "Infinity Scroll" }, { "sv-SE", "Infinity Scroll" } }
                            }
                        }
                    }
                },
            };
            return fields;
        }
    }
}
