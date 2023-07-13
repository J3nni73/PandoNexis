using Litium.Web;
using Litium.Websites;
using Litium.Customers;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Litium.Data;
using Litium.Data.Queryable;
using Litium.Products;
using System.Globalization;
using Litium.Products.Queryable;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;

namespace PandoNexis.AddOns.Extensions.PNNoCrm.Services
{
    [Service(ServiceType = typeof(NoCrmOrganizationService))]
    public class NoCrmOrganizationService
    {
        private readonly DataService _dataService;

        public NoCrmOrganizationService(DataService dataService)
        {
            _dataService = dataService;
        }

        public GenericDataField GetFieldCustomers(Website website, Guid? selectedOrganizationSystemId,Person person)
        {
            if (selectedOrganizationSystemId == null) return null;
            //hämta organizationer
            var field = new GenericDataField();
            field.FieldId = NoCrmProcessorConstants.ChildOrganizations;
            field.FieldName = "addons.nocrm.headertexts.organizations".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.DropDownDGType;
            field.Settings.Editable = true;
            var customers = GetChildOrganizations((Guid)selectedOrganizationSystemId);
            var selected = person.OrganizationLinks.Select(i => i.OrganizationSystemId).ToList();
            

            foreach (var customer in customers)
            {
                field.Options.Add(new GenericOption() { Key = customer.SystemId.ToString(), Value = customer.Name, Selected = selected.Contains(customer.SystemId) });
                if (selected.Contains(customer.SystemId))
                    field.FieldValue = customer.SystemId.ToString();
                

            }

            return field;
        }
        public List<Organization> GetChildOrganizations(Guid organizationSystemId)
        {
            var result = new List<Organization>();

            using (var query = _dataService.CreateQuery<Organization>())
            {
                var q = query.Filter(filter => filter
                    .Bool(boolFilter => boolFilter
                        .Must(boolFilterMust => boolFilterMust
                            .Field("Customer", "eq", organizationSystemId))
                        ));


                result = q.ToList();
            }

            return result;
        }
    }
}
