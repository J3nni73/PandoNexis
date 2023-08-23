using Humanizer;
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
    [Service(ServiceType = typeof(PilotProcessorService))]
    public class PilotProcessorService
    {
        private readonly WorkItemService _pilotItemService;
        private readonly OrganizationService _organizationService;
        private readonly SecurityContextService _securityContextService;
        private readonly TimeService _timeService;
        private readonly TimeTypeService _timeTypeService;

        private Guid _estimatedTimeTypeSystemId;
        private Guid _workedTimeTypeSystemId;

        public PilotProcessorService(WorkItemService pilotItemService,
                    OrganizationService organizationService,
                    SecurityContextService securityContextService,
                    TimeService timeService,
                    TimeTypeService timeTypeService)
        {
            _pilotItemService = pilotItemService;
            _organizationService = organizationService;
            _securityContextService = securityContextService;
            _timeService = timeService;
            _timeTypeService = timeTypeService;
            
            
            var timeTypes = _timeTypeService.GetTimeTypes();
            _estimatedTimeTypeSystemId = timeTypes.FirstOrDefault(i => i.Name == TimeTypeConstants.Estimated)?.SystemId ?? Guid.Empty;
            _workedTimeTypeSystemId = timeTypes.FirstOrDefault(i => i.Name == TimeTypeConstants.Worked)?.SystemId ?? Guid.Empty;
           
        }

        public bool UpdateField(Guid systemId, string fieldId, string value, out WorkItem item)
        {
            item = _pilotItemService.GetItem(systemId);
            if (string.IsNullOrEmpty(fieldId)) return false;
            MapWorkItemField(fieldId, value, ref item);


            if (_pilotItemService.AddOrUpdateItem(item)) return true;

            return false;
        }
        public WorkItem UpdateFields(Guid systemId, Dictionary<string, object> fields)
        {
            var item = _pilotItemService.GetItem(systemId);
            if (!fields.Any()) return null;
            foreach (var field in fields)
            {
                MapWorkItemField(field.Key, field.Value?.ToString()??string.Empty, ref item);
            }
            
            if (_pilotItemService.AddOrUpdateItem(item))
            {
                if (fields.ContainsKey(PilotConstants.Estimate))
                {
                    var estimate = _timeService.GetAllTime().FirstOrDefault(i => i.ItemSystemId == systemId && i.TimeTypeSystemId == _estimatedTimeTypeSystemId)
                                                                ?? _timeService.GetNewTime(item.SystemId, _estimatedTimeTypeSystemId, item.SystemId, item.OrganizationSystemId);
                    foreach (var field in fields) 
                    { 
                        MapTimeEstimateField(field.Key, field.Value?.ToString() ?? string.Empty, ref estimate);
                        
                    }
                    _timeService.AddOrUpdateTime(estimate);
                }
                if (fields.ContainsKey(PilotConstants.AddTimeSpent))
                {
                    var timeSpent = _timeService.GetNewTime(item.SystemId, _workedTimeTypeSystemId, item.OrganizationSystemId, item.ReportedByPersonSystemId);
                    foreach (var field in fields)
                    {
                        MapTimeWorkedField(field.Key, field.Value?.ToString() ?? string.Empty, ref timeSpent);

                    }
                    _timeService.AddOrUpdateTime(timeSpent);
                }
            }

            return item;
        }
        public Time UpdateTimeSpentFields(Guid systemId, Guid organzationSystemId, Guid personSystemId, Dictionary<string, object> form)
        {
            var timeSpent = _timeService.GetTime(systemId)?? _timeService.GetNewTime(systemId, _workedTimeTypeSystemId, organzationSystemId, personSystemId);
            foreach (var field in form)
            {
                MapTimeWorkedField(field.Key, field.Value?.ToString() ?? string.Empty, ref timeSpent);

            }
            _timeService.AddOrUpdateTime(timeSpent);
            return timeSpent;
        }
        public void MapTimeEstimateField(string fieldId, string value, ref Time estimate)
        {
            if (string.IsNullOrEmpty(fieldId)) return;
            switch (fieldId)
            {
                case PilotConstants.Estimate:
                    if (decimal.TryParse(value, out decimal estimatedTime))
                    {                        
                        estimate.TimeAsHours = estimatedTime;
                    }
                    break;
                case PilotConstants.TimeRisk:
                    if (decimal.TryParse(value, out decimal risk))
                    {
                        estimate.Risk = risk;
                    }
                    break;
                case PilotConstants.EstimatedComment:
                    estimate.TimeComment = value;
                    break;

            }
        }
        public void MapTimeWorkedField(string fieldId, string value, ref Time timeSpent)
        {
            if (string.IsNullOrEmpty(fieldId)) return;
            switch (fieldId)
            {
                case PilotConstants.AddTimeSpent:
                    if (decimal.TryParse(value, out decimal timeSpentAmout))
                    {
                        timeSpent.TimeAsHours = timeSpentAmout;
                    }
                    break;
               
                case PilotConstants.AddTimeSpentComment:
                    timeSpent.TimeComment = value;
                    break;
                case PilotConstants.AddTimeSpentFrom:
                    if (DateTime.TryParse(value, out DateTime timeSpentFrom))
                    {
                        timeSpent.TimeFrom = timeSpentFrom;
                    }
                    else
                    {
                        return;
                    }
                    break;
                case PilotConstants.AddTimeSpentTo:
                    if (DateTime.TryParse(value, out DateTime timeSpentTo))
                    {
                        timeSpent.TimeTo = timeSpentTo;
                    }
                    else
                    {
                        return;
                    }
                    break;

            }
        }
        public void MapWorkItemField( string fieldId, string value, ref WorkItem item)
        {
            if (string.IsNullOrEmpty(fieldId)) return;

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
                        return;
                    }
                    break;
                case PilotConstants.ParentSystemId:
                    if (Guid.TryParse(value, out Guid parentSystemId))
                    {
                        item.ParentSystemId = parentSystemId;
                    }
                    else
                    {
                        return;
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
                        return;
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
                        return;
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
                        return;
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
                        return;
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
                        return;
                    }
                    break;

            }

            return;

        }
    }
}
