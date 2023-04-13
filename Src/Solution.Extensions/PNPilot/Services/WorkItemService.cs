using Litium.Customers;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Newtonsoft.Json;
using Solution.Extensions.PNPilot.Constants;
using Solution.Extensions.PNPilot.Objects;
using Solution.Extensions.PNPilot.Services.DALServices;

namespace Solution.Extensions.PNPilot.Services
{
    [Service(ServiceType = typeof(WorkItemService))]
    public class WorkItemService
    {
        private readonly WorkItemDALService _pilotItemDALService;
        private readonly OrganizationService _organizationService;
        private readonly SecurityContextService _securityContextService;

        public WorkItemService(WorkItemDALService pilotItemDALService,
                    OrganizationService organizationService,
                    SecurityContextService securityContextService)
        {
            _pilotItemDALService = pilotItemDALService;
            _organizationService = organizationService;
            _securityContextService = securityContextService;
        }

        public IEnumerable<WorkItem> GetItems()
        {
            return _pilotItemDALService.GetAll();
        }

        public bool AddOrUpdateItem(string jsonItem)
        {
            var item = JsonConvert.DeserializeObject<WorkItem>(jsonItem);
            if (item == null) return false;

            //creating id
            if (string.IsNullOrEmpty(item.Id))
            {
                item.Id = GetNextId(item.OrganizationSystemId);
            }
            return _pilotItemDALService.AddOrUpdate(item);

        }

        public string GetNextId(Guid organizationSystemId)
        {
            var organization = _organizationService.Get(organizationSystemId);

            var prefix = organization.Fields.GetValue<string>(PilotFieldNameConstants.WorkItemPrefix);
            if (string.IsNullOrEmpty(prefix))
            {
                var parent = organization.Fields.GetValue<Guid>(PilotFieldNameConstants.Customer);
                if (parent != Guid.Empty)
                {
                    organization = _organizationService.Get(parent);
                    prefix = organization.Fields.GetValue<string>(PilotFieldNameConstants.WorkItemPrefix);
                }
            }
            var index = organization.Fields.GetValue<int>(PilotFieldNameConstants.NextId);
            if (index == 0)
            {
                index = 1;
            }

            organization = organization.MakeWritableClone();
            organization.Fields.AddOrUpdateValue(PilotFieldNameConstants.NextId, index + 1);
            using (_securityContextService.ActAsSystem())
            {
                _organizationService.Update(organization);
            }



            return prefix + index.ToString();

        }

    }
}
