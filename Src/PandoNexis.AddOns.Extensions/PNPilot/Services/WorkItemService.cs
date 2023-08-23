using Litium.Customers;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Web.Security;
using Newtonsoft.Json;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.PNPilot.Constants;
using PandoNexis.AddOns.PNPilot.Objects;
using PandoNexis.AddOns.PNPilot.Services.DALServices;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.PNPilot.Services
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

            return AddOrUpdateItem(item);
        }
        public bool AddOrUpdateItem(WorkItem item)
        {
            if (item == null) return false;

            var personSystemId = _securityContextService.GetIdentityUserSystemId()??Guid.Empty;
            item.UpdatedBy = personSystemId;
            //creating id
            if (string.IsNullOrEmpty(item.Id))
            {
                item.Id = GetNextId(item.OrganizationSystemId);
            }
            if (item.SystemId == Guid.Empty)
            {
                item.SystemId = Guid.NewGuid();
                item.CreatedBy = personSystemId;
            }
            return _pilotItemDALService.AddOrUpdate(item);
        }
        public bool UpdateField(Guid systemId, string fieldId, string value, out WorkItem item)
        {
            item = GetItem(systemId);
            if (string.IsNullOrEmpty(fieldId)) return false;
            MapField(fieldId, value, ref item);


            if (AddOrUpdateItem(item)) return true;

            return false;
        }
        public bool UpdateFields(Guid systemId, Dictionary<string, object> fields, out WorkItem item)
        {
            item = GetItem(systemId);
            if (!fields.Any()) return false;
            foreach (var field in fields)
            {
                MapField(field.Key, field.Value?.ToString()??string.Empty, ref item);
            }
            
            if (AddOrUpdateItem(item)) return true;

            return false;
        }
        public bool MapField( string fieldId, string value, ref WorkItem item)
        {
            if (string.IsNullOrEmpty(fieldId)) return false;

            switch (fieldId)
            {
                case PilotConstants.Customer:
                case PilotConstants.OrganizationSystemId:
                    if (Guid.TryParse(value, out Guid organizationSystemId))
                    {
                        item.OrganizationSystemId = organizationSystemId;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case PilotConstants.ParentSystemId:
                    if (Guid.TryParse(value, out Guid parentSystemId))
                    {
                        item.ParentSystemId = parentSystemId;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case PilotConstants.Id:
                    item.Id = value;
                    break;
                case PilotConstants.ItemType:
                case PilotConstants.ItemTypeSystemId:
                    if (Guid.TryParse(value, out Guid itemTypeSystemId))
                    {
                        item.ItemTypeSystemId = itemTypeSystemId;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case PilotConstants.ItemStatus:
                case PilotConstants.ItemStatusSystemId:
                    if (Guid.TryParse(value, out Guid itemStatusSystemId))
                    {
                        item.ItemStatusSystemId = itemStatusSystemId;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case PilotConstants.ItemTitle:
                    item.ItemTitle = value;
                    break;
                case PilotConstants.ItemDescription:
                    item.ItemDescription = value;
                    break;
                case PilotConstants.DueDateTime:
                    if (DateTime.TryParse(value, out DateTime dueDate))
                    {
                        item.DueDateTime = dueDate;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case PilotConstants.Assigned:
                case PilotConstants.AssignedPersonSystemId:
                    if (Guid.TryParse(value, out Guid assignedPersonSystemId))
                    {
                        item.AssignedPersonSystemId = assignedPersonSystemId;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case PilotConstants.ReportedBy:
                case PilotConstants.ReportedByPersonSystemId:
                    if (Guid.TryParse(value, out Guid reportedByPersonSystemId))
                    {
                        item.ReportedByPersonSystemId = reportedByPersonSystemId;
                    }
                    else
                    {
                        return false;
                    }
                    break;

            }

            return false;

        }
        public WorkItem GetItem(Guid systemId)
        {
            return GetItems()?.FirstOrDefault(i => i.SystemId == systemId) ?? new WorkItem();
        }
        public WorkItem GetNewItem(Guid organizationSystemId, Guid personSystemId)
        {
            var item = new WorkItem();
            item.OrganizationSystemId= organizationSystemId;
            item.ReportedByPersonSystemId = personSystemId;
            item.CreatedBy = personSystemId;
            item.UpdatedBy = personSystemId;
            item.CreatedDateTime = DateTime.Now;
            item.UpdatedDateTime = DateTime.Now;
            return item;


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
