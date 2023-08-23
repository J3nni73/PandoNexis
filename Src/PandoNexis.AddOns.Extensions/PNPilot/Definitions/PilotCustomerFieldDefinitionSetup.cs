using Litium.Accelerator.Definitions;
using Litium.Accelerator.Search;
using Litium.Blocks;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNPilot.Constants;
using PandoNexis.AddOns.PNPilot.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.PNPilot.Definitions
{
    internal class PilotCustomerFieldDefinitionSetup
    {
        internal class CustomersFieldDefinitionSetup : FieldDefinitionSetup
        {
            public readonly FieldHelper _fieldHelper;

            public CustomersFieldDefinitionSetup(FieldHelper fieldHelper)
            {
                _fieldHelper = fieldHelper;
            }

            public override IEnumerable<FieldDefinition> GetFieldDefinitions()
            {
                var fields = new List<FieldDefinition>
                {
                    _fieldHelper.GetCustomerTextOptionField(PilotFieldNameConstants.ProjectType, SystemFieldTypeConstants.TextOption),
                    _fieldHelper.GetCustomerPointerField(PilotFieldNameConstants.Customer, SystemFieldTypeConstants.Pointer, PointerTypeConstants.CustomersOrganization),
                    _fieldHelper.GetCustomerFieldDefinition(PilotFieldNameConstants.ErpId, SystemFieldTypeConstants.Text),
                    _fieldHelper.GetCustomerFieldDefinition(PilotFieldNameConstants.WorkItemPrefix, SystemFieldTypeConstants.Text),
                    _fieldHelper.GetCustomerFieldDefinition(PilotFieldNameConstants.NextId, SystemFieldTypeConstants.Int),
                    _fieldHelper.GetCustomerPointerField(PilotFieldNameConstants.AddOn, SystemFieldTypeConstants.Pointer, PointerTypeConstants.ProductsProduct),
                    _fieldHelper.GetCustomerTextOptionField(PilotFieldNameConstants.AddOnStatus, SystemFieldTypeConstants.TextOption),
                    _fieldHelper.GetCustomerFieldDefinition(PilotFieldNameConstants.OrderedDate, SystemFieldTypeConstants.DateTime),
                    _fieldHelper.GetCustomerPointerField(PilotFieldNameConstants.OrderedBy, SystemFieldTypeConstants.Pointer, PointerTypeConstants.CustomersPerson),
                    _fieldHelper.GetCustomerFieldDefinition(PilotFieldNameConstants.ImplementedDate, SystemFieldTypeConstants.DateTime),
                    _fieldHelper.GetCustomerMultiField(PilotFieldNameConstants.AddOns, SystemFieldTypeConstants.MultiField, true),
                    _fieldHelper.GetCustomerFieldDefinition(ContactLoggConstants.ContactDateTime, SystemFieldTypeConstants.DateTime),
                    _fieldHelper.GetCustomerTextOptionField(ContactLoggConstants.ContactType, SystemFieldTypeConstants.TextOption),
                    _fieldHelper.GetCustomerFieldDefinition(ContactLoggConstants.Title, SystemFieldTypeConstants.Text),
                    _fieldHelper.GetCustomerFieldDefinition(ContactLoggConstants.InvolvedPersons, SystemFieldTypeConstants.MultirowText),
                    _fieldHelper.GetCustomerFieldDefinition(ContactLoggConstants.Description, SystemFieldTypeConstants.Editor),
                    _fieldHelper.GetCustomerTextOptionField(ContactLoggConstants.ContactStatus, SystemFieldTypeConstants.TextOption),
                    _fieldHelper.GetCustomerMultiField(ContactLoggConstants.ContactLogg, SystemFieldTypeConstants.MultiField, true),
                    _fieldHelper.GetWebsiteTextOptionField(PilotProcessorConstants.PilotButtonNames, SystemFieldTypeConstants.TextOption),
                    _fieldHelper.GetWebsiteMultiField(PilotProcessorConstants.PilotButtonLinks, SystemFieldTypeConstants.MultiField, true),
                    _fieldHelper.GetWebsitePointerField(PilotFieldNameConstants.PilotAdminGroup, SystemFieldTypeConstants.Pointer, PointerTypeConstants.CustomersGroup),
                };
                return fields;
            }
        }
    }
}
