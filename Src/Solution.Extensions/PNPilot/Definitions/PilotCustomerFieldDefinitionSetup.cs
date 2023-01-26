using Litium.Accelerator.Definitions;
using Litium.Accelerator.Search;
using Litium.Blocks;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Solution.Extensions.PNPilot.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNPilot.Definitions
{
    internal class PilotCustomerFieldDefinitionSetup
    {
        internal class CustomersFieldDefinitionSetup : FieldDefinitionSetup
        {
            public override IEnumerable<FieldDefinition> GetFieldDefinitions()
            {
                var fields = new List<FieldDefinition>
            {

                new FieldDefinition<CustomerArea>(PilotFieldNameConstants.ProjectType, SystemFieldTypeConstants.TextOption)
                {
                    Option = new TextOption
                    {
                        MultiSelect = false,
                        Items = new List<TextOption.Item>
                        {
                            new TextOption.Item
                            {
                                Value = ProjectTypeConstants.PandoNexisAccelerator,
                                Name = new Dictionary<string, string> { { "en-US", "Pando Nexis Accelerator" }, { "sv-SE", "Pando Nexis Accelerator" } }
                            },
                            new TextOption.Item
                            {
                                Value = ProjectTypeConstants.LitiumAccelerator,
                                Name = new Dictionary<string, string> { { "en-US", "Litium Accelerator" }, { "sv-SE", "Litium Accelerator" } }
                            },
                            new TextOption.Item
                            {
                                Value = ProjectTypeConstants.SubContractor,
                                Name = new Dictionary<string, string> { { "en-US", "Sub contractor" }, { "sv-SE", "Underkonsult" } }
                            }
                        }
                    }
                },
                new FieldDefinition<CustomerArea>(PilotFieldNameConstants.Customer, SystemFieldTypeConstants.Pointer)
                {
                    Option = new PointerOption { EntityType = PointerTypeConstants.CustomersOrganization }
                },
                new FieldDefinition<CustomerArea>(PilotFieldNameConstants.ErpId, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = false
                }, 
                new FieldDefinition<CustomerArea>(PilotFieldNameConstants.WorkItemPrefix, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = false
                },  
                new FieldDefinition<CustomerArea>(PilotFieldNameConstants.NextId, SystemFieldTypeConstants.Int)
                {
                    MultiCulture = false, 
                    Editable= false
                },
                new FieldDefinition<CustomerArea>(PilotFieldNameConstants.AddOn, SystemFieldTypeConstants.Pointer)
                {
                    Option = new PointerOption { EntityType = PointerTypeConstants.ProductsProduct }
                },
                new FieldDefinition<CustomerArea>(PilotFieldNameConstants.AddOnStatus, SystemFieldTypeConstants.TextOption)
                {
                   Option = new TextOption
                    {
                        MultiSelect = false,
                        Items = new List<TextOption.Item>
                        {
                            new TextOption.Item
                            {
                                Value = AddonStatusConstants.Intended,
                                Name = new Dictionary<string, string> { { "en-US", "Intended" }, { "sv-SE", "Avsedd" } }
                            },
                            new TextOption.Item
                            {
                                Value = AddonStatusConstants.Ordered,
                                Name = new Dictionary<string, string> { { "en-US", "Ordered" }, { "sv-SE", "Beställd" } }
                            },
                            new TextOption.Item
                            {
                                Value = AddonStatusConstants.Implemented,
                                Name = new Dictionary<string, string> { { "en-US", "Implemented" }, { "sv-SE", "Implementerad" } }
                            },
                            new TextOption.Item
                            {
                                Value = AddonStatusConstants.Disconnected,
                                Name = new Dictionary<string, string> { { "en-US", "Disconnected" }, { "sv-SE", "Bortkopplad" } }
                            }
                        }
                    }
                },
                new FieldDefinition<CustomerArea>(PilotFieldNameConstants.OrderedDate, SystemFieldTypeConstants.DateTime)
                {
                    MultiCulture = false
                },
                new FieldDefinition<CustomerArea>(PilotFieldNameConstants.OrderedBy, SystemFieldTypeConstants.Pointer)
                {
                    Option = new PointerOption { EntityType = PointerTypeConstants.CustomersPerson }
                },
                new FieldDefinition<CustomerArea>(PilotFieldNameConstants.ImplementedDate, SystemFieldTypeConstants.DateTime)
                {
                    MultiCulture = false
                },
                  new FieldDefinition<CustomerArea>(PilotFieldNameConstants.AddOns, SystemFieldTypeConstants.MultiField)
                {
                    MultiCulture = false,
                     Option = new MultiFieldOption
                     {
                         IsArray = true,
                         Fields  = new List<string>()
                         {
                             PilotFieldNameConstants.AddOn,
                             PilotFieldNameConstants.AddOnStatus,
                             PilotFieldNameConstants.OrderedDate,
                             PilotFieldNameConstants.OrderedBy,
                             PilotFieldNameConstants.ImplementedDate,

                         }
                     }
                },
            };
                return fields;
            }
        }
    }
}
