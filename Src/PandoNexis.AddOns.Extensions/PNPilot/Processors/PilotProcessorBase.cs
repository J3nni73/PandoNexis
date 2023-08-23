using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Customers;
using Litium.Data.Queryable.ExpressionInfos;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Web;
using Litium.Websites;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Processors;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNPilot.Constants;
using PandoNexis.AddOns.Extensions.PNPilot.Services;
using PandoNexis.AddOns.PNPilot.Constants;
using PandoNexis.AddOns.PNPilot.Objects;
using PandoNexis.AddOns.PNPilot.Services;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PandoNexis.AddOns.Extensions.PNPilot.Processors
{
    [Service(ServiceType = typeof(PilotProcessorBase), Lifetime = DependencyLifetime.Transient, NamedService = true)]
    public abstract class PilotProcessorBase : IGenericDataViewProcessor
    {
        private readonly GenericDataViewService _genericDataViewService;
        private readonly ItemStatusService _itemStatusService;
        private readonly ItemTypeService _itemTypeService;
        private readonly TimeService _timeService;
        private readonly PilotCustomerService _pilotCustomerService;
        private readonly PilotUserService _pilotUserService;
        private readonly PersonService _personService;
        private readonly OrganizationService _organizationService;
        private readonly WorkItemService _workItemService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly TimeTypeService _timeTypeService;
        private readonly SecurityContextService _securityContextService;
        private readonly PersonStorage _personStorage;

        private IEnumerable<ItemStatus> _itemStatuses;
        private IEnumerable<ItemType> _itemTypes;
        private IEnumerable<WorkItem> _workItems;
        private IEnumerable<Time> _times;
        private Guid _estimatedTimeTypeSystemId;
        private Guid _workedTimeTypeSystemId;

        public Guid _currentPageSystemId { get; set; }
        protected PilotProcessorBase(GenericDataViewService genericDataViewService,
                                        ItemStatusService itemStatusService,
                                        ItemTypeService itemTypeService,
                                        TimeService timeService,
                                        PilotCustomerService pilotCustomerService,
                                        PilotUserService pilotUserService,
                                        PersonService personService,
                                        OrganizationService organizationService,
                                        WorkItemService workItemService,
                                        RequestModelAccessor requestModelAccessor,
                                        TimeTypeService timeTypeService,
                                        SecurityContextService securityContextService,
                                        PersonStorage personStorage)
        {
            _genericDataViewService = genericDataViewService;
            _itemStatusService = itemStatusService;
            _itemTypeService = itemTypeService;
            _timeService = timeService;
            _pilotCustomerService = pilotCustomerService;
            _pilotUserService = pilotUserService;
            _personService = personService;
            _organizationService = organizationService;
            _workItemService = workItemService;
            _requestModelAccessor = requestModelAccessor;
            _timeTypeService = timeTypeService;
            _securityContextService = securityContextService;
            _personStorage = personStorage;

            _itemStatuses = _itemStatusService.GetItemStatuses();
            _itemTypes = _itemTypeService.GetItemTypes();
            _workItems = _workItemService.GetItems();
            _times = _timeService.GetAllTime();
            var timeTypes = _timeTypeService.GetTimeTypes();
            _estimatedTimeTypeSystemId = timeTypes.FirstOrDefault(i => i.Name == TimeTypeConstants.Estimated)?.SystemId ?? Guid.Empty;
            _workedTimeTypeSystemId = timeTypes.FirstOrDefault(i => i.Name == TimeTypeConstants.Worked)?.SystemId ?? Guid.Empty;

        }

        public abstract Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data);
        public abstract Task<GenericDataView> GetDataView(Guid pageSystemId, string data);
        public virtual GenericDataViewSettings GetDataViewSettings(Guid pageSystemId)
        {
            return _genericDataViewService.GetDataViewSettings(pageSystemId);
        }
        public virtual GenericDataContainer BuildWorkItemContainer(GenericDataContainer templateContainer, WorkItem workItem)
        {
            var result = JsonConvert.DeserializeObject<GenericDataContainer>(JsonConvert.SerializeObject(templateContainer));

            foreach (var field in result.Fields)
            {
                field.EntitySystemId = workItem.SystemId.ToString();
                field.FieldValue = GetValue(field, workItem);
            }
            
            return result;
        }
        public virtual GenericDataContainer BuildTimeSpentContainer(GenericDataContainer templateContainer, Time time)
        {
            var result = JsonConvert.DeserializeObject<GenericDataContainer>(JsonConvert.SerializeObject(templateContainer));

            foreach (var field in result.Fields)
            {
                field.EntitySystemId = time.SystemId.ToString();
                field.FieldValue = GetValue(field, time);
            }

            return result;
        }
        public Time? GetTime(Guid systemId)
        {
            return _times?.FirstOrDefault(i => i.SystemId == systemId);
        }
        public Time GetNewTimeSpent(Guid itemSystemId )
        {
            var personSystemId = _securityContextService.GetIdentityUserSystemId();
            var organizationSystemId = _personStorage.CurrentSelectedOrganization.SystemId;
            if (organizationSystemId == Guid.Empty) return null;
            if (!personSystemId.HasValue && personSystemId == Guid.Empty) return null;
            return _timeService.GetNewTime(itemSystemId, _workedTimeTypeSystemId, organizationSystemId, (Guid)personSystemId);
        }
        public WorkItem? GetWorkItem(Guid systemId)
        {
            return _workItems?.FirstOrDefault(i => i.SystemId == systemId);
        }

        public virtual GenericDataContainer GetFields(string templateId)
        {
            var website = _requestModelAccessor.RequestModel.WebsiteModel.Website;
            var result = new GenericDataContainer();
            switch (templateId)
            {
                case PilotProcessorConstants.NewOrViewTimeSpent:
                    result.Fields.Add(GetFieldAddTimeSpentFrom(website, true));
                    result.Fields.Add(GetFieldAddTimeSpentTo(website, true));
                    result.Fields.Add(GetFieldAddTimeSpent(website, true));
                    result.Fields.Add(GetFieldAddTimeSpentComment(website, true));
                    break;
                case PilotProcessorConstants.NewOrViewWorkItem:
                    result.Fields.Add(GetFieldWorkItemId(website));
                    result.Fields.Add(GetFieldItemType(website, true));
                    result.Fields.Add(GetFieldItemStatus(website, true));
                    result.Fields.Add(GetFieldItemTitle(website, true));
                    result.Fields.Add(GetFieldItemDescription(website, true));
                    result.Fields.Add(GetFieldDueDate(website, true));
                    result.Fields.Add(GetFieldCustomers(website, true));
                    result.Fields.Add(GetFieldAssignedPerson(website, true));
                    result.Fields.Add(GetFieldReportedByPerson(website, true));
                    result.Fields.Add(GetFieldEstimate(website, true));
                    result.Fields.Add(GetFieldSumTimeSpent(website));
                    result.Fields.Add(GetFieldCreatedDate(website));
                    result.Fields.Add(GetFieldCreatedBy(website));
                    result.Fields.Add(GetFieldUpdatedDate(website));
                    result.Fields.Add(GetFieldUpdatedBy(website));
                    break;
                case PilotProcessorConstants.WorkItems:
                    result.Fields.Add(GetFieldWorkItemId(website));
                    result.Fields.Add(GetFieldItemTitle(website));
                    result.Fields.Add(GetFieldItemStatus(website));
                    result.Fields.Add(GetFieldItemType(website));
                    result.Fields.Add(GetFieldItemDescription(website));
                    result.Fields.Add(GetFieldDueDate(website));
                    result.Fields.Add(GetFieldEstimate(website, true));
                    result.Fields.Add(GetFieldRisk(website, true));
                    result.Fields.Add(GetFieldEstimatedComment(website, true));
                    result.Fields.Add(GetFieldAddTimeSpent(website, true));
                    result.Fields.Add(GetFieldAddTimeSpentComment(website, true));

                    result.Fields.Add(GetFieldSumTimeSpent(website));
                    result.Fields.Add(GetFieldCustomers(website, true));
                    result.Fields.Add(GetFieldAssignedPerson(website));
                    result.Fields.Add(GetFieldReportedByPerson(website));
                    result.Fields.Add(GetFieldCreatedDate(website));
                    result.Fields.Add(GetFieldUpdatedDate(website));

                    break;
            }
            return result;
        }
        public abstract Task<object> GetGridViewForExport(string data);
        public virtual async Task<GenericDataContainer> UpdateField(GenericDataField fieldData)
        {
            if (Guid.TryParse(fieldData.EntitySystemId, out Guid systemId))
            {
                var item = _workItems.FirstOrDefault(i => i.SystemId == systemId);
                if (item == null) return null;
                if (fieldData.FieldValue == null) return null;
                switch (fieldData.FieldId)
                {
                    case PilotConstants.Estimate:
                        if (decimal.TryParse(fieldData.FieldValue.ToString(), out decimal estimatedTime))
                        {
                            var estimate = _times.FirstOrDefault(i => i.ItemSystemId == systemId && i.TimeTypeSystemId == _workedTimeTypeSystemId) 
                                                                    ?? _timeService.GetNewTime(item.SystemId, _estimatedTimeTypeSystemId, item.SystemId, item.OrganizationSystemId);
                            estimate.TimeAsHours = estimatedTime;
                            _timeService.AddOrUpdateTime(estimate);
                        }
                        break;
                    case PilotConstants.AddTimeSpent:
                        if (decimal.TryParse(fieldData.FieldValue.ToString(), out decimal addTimeSpent))
                        {
                            var timeSpent = _times.FirstOrDefault(i => i.ItemSystemId == systemId && i.TimeTypeSystemId== _workedTimeTypeSystemId) 
                                                                    ?? _timeService.GetNewTime(item.SystemId, _workedTimeTypeSystemId, item.SystemId, item.OrganizationSystemId);
                            timeSpent.TimeAsHours = addTimeSpent;
                            _timeService.AddOrUpdateTime(timeSpent);
                        }
                        break;
                    default:
                        if (!_workItemService.UpdateField(systemId, fieldData.FieldId, fieldData.FieldValue, out item))
                            return null;
                        break;
                }
                return BuildWorkItemContainer(GetFields(PilotProcessorConstants.WorkItems), item);
            }

            return null;
        }
        public async Task<WorkItem> Update(GenericDataField fieldData)
        {
            if (Guid.TryParse(fieldData.EntitySystemId, out Guid systemId))
            {
                var item = _workItems.FirstOrDefault(i => i.SystemId == systemId);
                if (item == null) return null;
                if (fieldData.FieldValue == null) return null;
                switch (fieldData.FieldId)
                {
                    case PilotConstants.Estimate:
                        if (decimal.TryParse(fieldData.FieldValue.ToString(), out decimal estimatedTime))
                        {
                            var estimate = _times.FirstOrDefault(i => i.ItemSystemId == systemId && i.TimeTypeSystemId == _workedTimeTypeSystemId)
                                                                    ?? _timeService.GetNewTime(item.SystemId, _estimatedTimeTypeSystemId, item.SystemId, item.OrganizationSystemId);
                            estimate.TimeAsHours = estimatedTime;
                            _timeService.AddOrUpdateTime(estimate);
                        }
                        break;
                    case PilotConstants.AddTimeSpent:
                        if (decimal.TryParse(fieldData.FieldValue.ToString(), out decimal addTimeSpent))
                        {
                            var timeSpent = _times.FirstOrDefault(i => i.ItemSystemId == systemId && i.TimeTypeSystemId == _workedTimeTypeSystemId)
                                                                    ?? _timeService.GetNewTime(item.SystemId, _workedTimeTypeSystemId, item.SystemId, item.OrganizationSystemId);
                            timeSpent.TimeAsHours = addTimeSpent;
                            _timeService.AddOrUpdateTime(timeSpent);
                        }
                        break;
                    default:
                        if (!_workItemService.UpdateField(systemId, fieldData.FieldId, fieldData.FieldValue, out item))
                            return null;
                        break;
                }
                return item;
            }

            return null;
        }

        public IEnumerable<WorkItem> GetItems()
        {
            return _workItems;
        }

        public IEnumerable<Time> GetTimeSpentOnWorkItem(Guid itemSystemId)
        {
            var time = _times.Where(i=>i.ItemSystemId==itemSystemId&& i.TimeTypeSystemId==_workedTimeTypeSystemId)?.OrderByDescending(i=>i.TimeFrom)?.ToList()??new List<Time>();
            return time;
        }

        #region GetFields
        public string GetValue(GenericDataField field, WorkItem workItem)
        {
            var result = string.Empty;
            switch (field.FieldId)
            {
                case PilotConstants.Id:
                    result = workItem.Id;
                    break;
                case PilotConstants.ItemTitle:
                    result = workItem.ItemTitle;
                    break;
                case PilotConstants.ItemDescription:
                    result = workItem.ItemDescription;
                    break;
                case PilotConstants.ItemStatus:
                    if (field.Settings.Editable)
                        result = _itemStatuses?.FirstOrDefault(i => i.SystemId == workItem.ItemStatusSystemId)?.SystemId.ToString() ?? Guid.Empty.ToString();
                    else
                        result = _itemStatuses?.FirstOrDefault(i => i.SystemId == workItem.ItemStatusSystemId)?.Name ?? string.Empty;
                    break;
                case PilotConstants.ItemType:
                    if (field.Settings.Editable)
                        result = _itemTypes?.FirstOrDefault(i => i.SystemId == workItem.ItemTypeSystemId)?.SystemId.ToString() ?? Guid.Empty.ToString();
                    else
                        result = _itemTypes?.FirstOrDefault(i => i.SystemId == workItem.ItemTypeSystemId)?.Name ?? string.Empty;
                    break;
                case PilotConstants.DueDateTime:
                    result = workItem?.DueDateTime != null && workItem.DueDateTime != DateTime.MinValue ? workItem.DueDateTime.ToString(DataFieldFormats.DateTimeFormat) : string.Empty;
                    break;
                case PilotConstants.Estimate:
                    result = (_times?.Where(i => i.ItemSystemId == workItem.SystemId && i.TimeTypeSystemId == _estimatedTimeTypeSystemId)?.Sum(i => i.TimeAsHours))?.ToString(DataFieldFormats.DecimalFormat) ?? string.Empty;
                    break;
                case PilotConstants.TimeRisk:
                    result = (_times?.FirstOrDefault(i => i.ItemSystemId == workItem.SystemId && i.TimeTypeSystemId == _estimatedTimeTypeSystemId)?.Risk.ToString(DataFieldFormats.DecimalFormat)) ?? string.Empty;
                    break;
                case PilotConstants.EstimatedComment:
                    result = (_times?.FirstOrDefault(i => i.ItemSystemId == workItem.SystemId && i.TimeTypeSystemId == _estimatedTimeTypeSystemId)?.TimeComment)?? string.Empty;
                    break;
                case PilotConstants.SumTimeSpent:
                    //ToDo: select only timeSpent
                    result = (_times?.Where(i => i.ItemSystemId == workItem.SystemId && i.TimeTypeSystemId == _workedTimeTypeSystemId)?.Sum(i => i.TimeAsHours))?.ToString(DataFieldFormats.DecimalFormat) ?? string.Empty;
                    break;
                case DatabaseConstants.CreatedDateTime:
                    result = workItem?.CreatedDateTime != null && workItem.CreatedDateTime != DateTime.MinValue ? workItem.CreatedDateTime.ToString(DataFieldFormats.DateTimeFormat) : string.Empty;
                    break;
                case DatabaseConstants.UpdatedDateTime:
                    result = workItem?.UpdatedDateTime != null && workItem.UpdatedDateTime != DateTime.MinValue ? workItem.UpdatedDateTime.ToString(DataFieldFormats.DateTimeFormat) : string.Empty;
                    break;
                case PilotConstants.Customer:
                    if (field.Settings.Editable)
                    {
                        result = workItem.OrganizationSystemId.ToString();
                    }
                    else
                    {
                        result = _organizationService.Get(workItem.OrganizationSystemId)?.Name ?? string.Empty;
                    }
                    break;
                case DatabaseConstants.CreatedBy:
                    var createdPerson = _personService.Get(workItem.CreatedBy);
                    result = createdPerson != null ? createdPerson.FirstName + " " + createdPerson.LastName : string.Empty;
                    break;
                case DatabaseConstants.UpdatedBy:
                    var updatedPerson = _personService.Get(workItem.CreatedBy);
                    result = updatedPerson != null ? updatedPerson.FirstName + " " + updatedPerson.LastName : string.Empty;
                    break;
                case PilotConstants.ReportedBy:
                case PilotConstants.ReportedByPersonSystemId:
                    if (field.Settings.Editable)
                    {
                        result = workItem.ReportedByPersonSystemId.ToString();
                    }
                    else
                    {
                        var reportedByPerson = _personService.Get(workItem.ReportedByPersonSystemId);
                        result = reportedByPerson != null ? reportedByPerson.FirstName + " " + reportedByPerson.LastName : string.Empty;
                    }
                    break;
                case PilotConstants.Assigned:
                case PilotConstants.AssignedPersonSystemId:
                    if (field.Settings.Editable)
                    {
                        result = workItem.AssignedPersonSystemId.ToString();
                    }
                    else
                    {
                        var assignedPerson = _personService.Get(workItem.AssignedPersonSystemId);
                        result = assignedPerson != null ? assignedPerson.FirstName + " " + assignedPerson.LastName : string.Empty;
                    }
                    break;

            }
            return result;
        }

        public string GetValue(GenericDataField field, Time time)
        {
            var result = string.Empty;
            switch (field.FieldId)
            {
                case PilotConstants.AddTimeSpent:
                    result = time.TimeAsHours.ToString(DataFieldFormats.DecimalFormat);
                    break;
                case PilotConstants.AddTimeSpentComment:
                    result = time.TimeComment;
                    break;
                case PilotConstants.AddTimeSpentFrom:
                    result = time.TimeFrom.ToString(DataFieldFormats.DateTimeFormat);
                    break;
                case PilotConstants.AddTimeSpentTo:
                    result = time.TimeTo.ToString(DataFieldFormats.DateTimeFormat);
                    break;
            }
            return result;
        }
        public GenericDataField GetFieldWorkItemId(Website website, bool editable = false)
        {
            var field = new GenericDataField();
            field.FieldId = PilotConstants.Id;
            field.FieldName = "addons.pilot.headertexts.workitemid".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.StringDGType;
            field.Settings.Editable = editable;

            return field;
        }
        public GenericDataField GetFieldItemTitle(Website website, bool editable = false)
        {
            var field = new GenericDataField();
            field.FieldId = PilotConstants.ItemTitle;
            field.FieldName = "addons.pilot.headertexts.itemtitle".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.StringDGType;
            field.Settings.Editable = editable;

            return field;
        }
        public GenericDataField GetFieldItemDescription(Website website, bool editable = false)
        {
            var field = new GenericDataField();
            field.FieldId = PilotConstants.ItemDescription;
            field.FieldName = "addons.pilot.headertexts.itemdescription".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.TextAreaDGType;
            field.Settings.Editable = editable;

            return field;
        }
        public GenericDataField GetFieldDueDate(Website website, bool editable = false)
        {
            var field = new GenericDataField();
            field.FieldId = PilotConstants.DueDateTime;
            field.FieldName = "addons.pilot.headertexts.duedate".AsWebsiteText(website);
            if (editable)
            {
                field.FieldType = DataFieldTypes.DateTimeDGType;
                field.Settings.Editable = editable;
            }
            else
            {
                field.FieldType = DataFieldTypes.StringDGType;
                field.Settings.Editable = editable;
            }
            return field;
        }


        public GenericDataField GetFieldCreatedDate(Website website)
        {
            var field = new GenericDataField();
            field.FieldId = DatabaseConstants.CreatedDateTime;
            field.FieldName = "addons.pilot.headertexts.createddate".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.StringDGType;
            field.Settings.Editable = false;

            return field;
        }
        public GenericDataField GetFieldCreatedBy(Website website)
        {
            var field = new GenericDataField();
            field.FieldId = DatabaseConstants.CreatedBy;
            field.FieldName = "addons.pilot.headertexts.createdby".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.StringDGType;
            field.Settings.Editable = false;

            return field;
        }
        public GenericDataField GetFieldUpdatedBy(Website website)
        {
            var field = new GenericDataField();
            field.FieldId = DatabaseConstants.UpdatedBy;
            field.FieldName = "addons.pilot.headertexts.updatedby".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.StringDGType;
            field.Settings.Editable = false;

            return field;
        }
        public GenericDataField GetFieldUpdatedDate(Website website)
        {
            var field = new GenericDataField();
            field.FieldId = DatabaseConstants.UpdatedDateTime;
            field.FieldName = "addons.pilot.headertexts.updateddate".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.StringDGType;
            field.Settings.Editable = false;

            return field;
        }
        public GenericDataField GetFieldItemStatus(Website website, bool editable = false)
        {

            var field = new GenericDataField();
            field.FieldId = PilotConstants.ItemStatus;
            field.FieldName = "addons.pilot.headertexts.itemstatus".AsWebsiteText(website);
            if (editable)
            {
                field.FieldType = DataFieldTypes.DropDownDGType;
                var options = _itemStatusService.GetItemStatuses();
                foreach (var option in options)
                {
                    field.Options.Add(new GenericOption() { Key = option.SystemId.ToString(), Value = option.Name });
                }
            }
            else
            {
                field.FieldType = DataFieldTypes.StringDGType;
            }
            field.Settings.Editable = editable;

            return field;
        }
        public GenericDataField GetFieldItemType(Website website, bool editable = false)
        {
            var field = new GenericDataField();
            field.FieldId = PilotConstants.ItemType;
            field.FieldName = "addons.pilot.headertexts.itemtype".AsWebsiteText(website);
            if (editable)
            {
                field.FieldType = DataFieldTypes.DropDownDGType;

                var options = _itemTypeService.GetItemTypes();
                foreach (var option in options)
                {
                    field.Options.Add(new GenericOption() { Key = option.SystemId.ToString(), Value = option.Name });
                }
            }
            else
            {
                field.FieldType = DataFieldTypes.StringDGType;
            }
            field.Settings.Editable = editable;
            return field;
        }
        public GenericDataField GetFieldCustomers(Website website, bool editable = false)
        {
            var field = new GenericDataField();
            field.FieldId = PilotConstants.Customer;
            field.FieldName = "addons.pilot.headertexts.customer".AsWebsiteText(website);
            if (editable)
            {
                field.FieldType = DataFieldTypes.DropDownDGType;
                var customers = _pilotCustomerService.GetCustomers();

                foreach (var customer in customers)
                {
                    field.Options.Add(new GenericOption() { Key = customer.SystemId.ToString(), Value = customer.Name });

                    foreach (var project in customer.Projects)
                    {
                        field.Options.Add(new GenericOption() { Key = project.SystemId.ToString(), Value = customer.Name + " - " + project.Name });
                    }

                }
            }
            else
            {
                field.FieldType = DataFieldTypes.StringDGType;
            }

            field.Settings.Editable = editable;

            return field;
        }
        public GenericDataField GetFieldAssignedPerson(Website website, bool editable = false)
        {
            var field = new GenericDataField();
            field.FieldId = PilotConstants.Assigned;
            field.FieldName = "addons.pilot.headertexts.assigned".AsWebsiteText(website);
            if (editable)
            {
                field.FieldType = DataFieldTypes.DropDownDGType;

                var persons = _pilotUserService.GetAvailablePersons(website);

                foreach (var person in persons)
                {
                    field.Options.Add(new GenericOption() { Key = person.SystemId.ToString(), Value = person.FirstName + " " + person.LastName });
                }
            }
            else
            {
                field.FieldType = DataFieldTypes.StringDGType;

            }

            field.Settings.Editable = editable;
            return field;
        }
        public GenericDataField GetFieldReportedByPerson(Website website, bool editable = false)
        {
            var field = new GenericDataField();
            field.FieldId = PilotConstants.ReportedBy;
            field.FieldName = "addons.pilot.headertexts.reportedby".AsWebsiteText(website);
            if (editable)
            {
                field.FieldType = DataFieldTypes.DropDownDGType;
                var persons = _pilotUserService.GetAvailablePersons(website);

                foreach (var person in persons)
                {
                    field.Options.Add(new GenericOption() { Key = person.SystemId.ToString(), Value = person.FirstName + " " + person.LastName });
                }
            }
            else
            {
                field.FieldType = DataFieldTypes.StringDGType;
            }

            field.Settings.Editable = editable;
            return field;
        }

        public GenericDataField GetFieldEstimate(Website website, bool editable = false)
        {

            var field = new GenericDataField();
            field.FieldId = PilotConstants.Estimate;
            field.FieldName = "addons.pilot.headertexts.estimate".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.DecimalDGType;
            field.Settings.Editable = editable;

            return field;
        }
        public GenericDataField GetFieldRisk(Website website, bool editable = false)
        {

            var field = new GenericDataField();
            field.FieldId = PilotConstants.TimeRisk;
            field.FieldName = "addons.pilot.headertexts.estimaterisk".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.DecimalDGType;
            field.Settings.Editable = editable;

            return field;
        }
        public GenericDataField GetFieldEstimatedComment(Website website, bool editable = false)
        {

            var field = new GenericDataField();
            field.FieldId = PilotConstants.EstimatedComment;
            field.FieldName = "addons.pilot.headertexts.estimatecomment".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.StringDGType;
            field.Settings.Editable = editable;

            return field;
        }

        public GenericDataField GetFieldSumTimeSpent(Website website, bool editable = false)
        {

            var field = new GenericDataField();
            field.FieldId = PilotConstants.SumTimeSpent;
            field.FieldName = "addons.pilot.headertexts.sumtimespent".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.DecimalDGType;
            field.Settings.Editable = editable;

            return field;
        }
        public GenericDataField GetFieldAddTimeSpent(Website website, bool editable = false)
        {

            var field = new GenericDataField();
            field.FieldId = PilotConstants.AddTimeSpent;
            field.FieldName = "addons.pilot.headertexts.addtimespent".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.DecimalDGType;
            field.Settings.Editable = editable;

            return field;
        }
        public GenericDataField GetFieldAddTimeSpentFrom(Website website, bool editable = false)
        {

            var field = new GenericDataField();
            field.FieldId = PilotConstants.AddTimeSpentFrom;
            field.FieldName = "addons.pilot.headertexts.addtimespentfrom".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.DateTimeDGType;
            field.Settings.Editable = editable;

            return field;
        }
        public GenericDataField GetFieldAddTimeSpentTo(Website website, bool editable = false)
        {

            var field = new GenericDataField();
            field.FieldId = PilotConstants.AddTimeSpentTo;
            field.FieldName = "addons.pilot.headertexts.addtimespentto".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.DateTimeDGType;
            field.Settings.Editable = editable;

            return field;
        }
        public GenericDataField GetFieldAddTimeSpentComment(Website website, bool editable = false)
        {

            var field = new GenericDataField();
            field.FieldId = PilotConstants.AddTimeSpentComment;
            field.FieldName = "addons.pilot.headertexts.addtimespentcomment".AsWebsiteText(website);
            field.FieldType = DataFieldTypes.StringDGType;
            field.Settings.Editable = editable;

            return field;
        }
        #endregion
    }
}
