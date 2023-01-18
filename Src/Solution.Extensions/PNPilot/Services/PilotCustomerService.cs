using Litium.Accelerator.Services;
using Litium.Customers;
using Litium.Data;
using Litium.FieldFramework;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using MetadataExtractor.Formats.Gif;
using PandoNexis.AddOns.Extensions.Services.PNStockService;
using Solution.Extensions.PNPilot.Constants;
using Solution.Extensions.PNPilot.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNPilot.Services
{
    [Service(ServiceType = typeof(PilotCustomerService))]
    public class PilotCustomerService
    {

        private readonly OrganizationService _organizationService;
        private readonly DataService _dataService;
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly VariantService _variantService;
        private readonly BaseProductService _baseProductService;

        public PilotCustomerService(OrganizationService organizationService, 
            DataService dataService, 
            FieldTemplateService fieldTemplateService, 
            VariantService variantService,
            BaseProductService baseProductService)
        {
            _organizationService = organizationService;
            _dataService = dataService;
            _fieldTemplateService = fieldTemplateService;
            _variantService = variantService;
            _baseProductService = baseProductService;
        }

        public List<PilotCustomer> GetCustomers()
        {

            var CustomerFieldTemplate = _fieldTemplateService.Get<OrganizationFieldTemplate>(PilotFieldTemplateConstants.PilotCustomer);
            var ProjectFieldTemplate = _fieldTemplateService.Get<OrganizationFieldTemplate>(PilotFieldTemplateConstants.PilotProject);

            var customers = new List<PilotCustomer>();

            using (var query = _dataService.CreateQuery<Organization>())
            {

                var items = query.ToList();

                foreach (var item in items)
                {
                    if (item.FieldTemplateSystemId == CustomerFieldTemplate.SystemId)
                    {
                        customers.Add(new PilotCustomer()
                        {
                            SystemId = item.SystemId,
                            Name = item.Fields.GetValue<string>(SystemFieldDefinitionConstants.NameInvariantCulture),
                            Projects = new List<PilotProject>()
                        });
                    }
                }
                foreach (var item in items)
                {
                    if (item.FieldTemplateSystemId == ProjectFieldTemplate.SystemId)
                    {
                        customers.FirstOrDefault(i => i.SystemId == item.Fields.GetValue<Guid>(PilotFieldNameConstants.Customer))
                                        ?.Projects.
                                        Add(new PilotProject()
                                        {
                                            SystemId = item.SystemId,
                                            Name = item.Fields.GetValue<string>(SystemFieldDefinitionConstants.NameInvariantCulture),
                                            ProjectType = item.Fields.GetValue<string>(PilotFieldNameConstants.ProjectType),
                                            AddOns = GetAddOnList(item.Fields.GetValue<IList<MultiFieldItem>>(PilotFieldNameConstants.AddOns))
                                        });
                    }
                }

            }



            return customers;
        }

        private List<string> GetAddOnList(IList<MultiFieldItem> multiFieldItems)
        {
            var result = new List<string>();
            if (multiFieldItems != null)
            { 
                foreach (var item in multiFieldItems)
                {
                    result.Add(GetProductName(item.Fields.GetValue<Guid>(PilotFieldNameConstants.AddOn)));
                }
            }
            return result;
        }
        private string GetProductName(Guid productId)
        {
            var variant = _variantService.Get(productId);
            if (variant != null)
            {
                return variant.Fields.GetValue<string>(SystemFieldDefinitionConstants.Name, "sv-SE");
            }
            
            var product = _baseProductService.Get(productId);
            if (product != null)
            {
                variant =_variantService.GetByBaseProduct(product.SystemId).FirstOrDefault();
                return variant.Fields.GetValue<string>(SystemFieldDefinitionConstants.Name, "sv-SE");
            }


            return string.Empty;
        }
    }
}
