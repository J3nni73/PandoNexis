using Litium.Customers;
using Litium.FieldFramework;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNRegisterMe.Definitions
{
    internal class FieldDefinitionSetup : Litium.Accelerator.Definitions.FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>
            {
                new FieldDefinition<CustomerArea>(RegisterMeConstants.AddedByRegisterMeForm, SystemFieldTypeConstants.Boolean)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<CustomerArea>(RegisterMeConstants.Allergies, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<CustomerArea>(RegisterMeConstants.DateAdded, SystemFieldTypeConstants.DateTime)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<CustomerArea>(RegisterMeConstants.PersonInfo, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                }, 
                new FieldDefinition<CustomerArea>(RegisterMeConstants.UserName, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                }
            };
            return fields;
        }
    }
}
