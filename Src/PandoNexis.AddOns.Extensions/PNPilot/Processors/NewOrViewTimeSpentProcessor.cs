using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Search;
using Litium.Accelerator.Utilities;
using Litium.Customers;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNPilot.Constants;
using PandoNexis.AddOns.Extensions.PNPilot.Services;
using PandoNexis.AddOns.PNPilot.Constants;
using PandoNexis.AddOns.PNPilot.Objects;
using PandoNexis.AddOns.PNPilot.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNPilot.Processors
{
    [Service(Name = "NewOrViewTimeSpent")]
    public class NewOrViewTimeSpentProcessor : PilotProcessorBase
    {
        private readonly WorkItemService _workItemService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly GenericDataViewService _genericDataViewService;
        private readonly PersonStorage _personStorage;
        private readonly SecurityContextService _securityContextService;
        private readonly PilotProcessorService _pilotProcessorService;
        private readonly TimeService _timeService;
        
        public NewOrViewTimeSpentProcessor(GenericDataViewService genericDataViewService,
                                    WorkItemService workItemService,
                                    RequestModelAccessor requestModelAccessor,
                                    ItemStatusService itemStatusService,
                                    ItemTypeService itemTypeService,
                                    TimeService timeService,
                                    PilotCustomerService pilotCustomerService,
                                    PilotUserService pilotUserService,
                                    PersonStorage personStorage,
                                    SecurityContextService securityContextService,
                                    PersonService personService,
                                    OrganizationService organizationService,
                                    TimeTypeService timeTypeService,
                                    PilotProcessorService pilotProcessorService) : base(genericDataViewService,
                                                                                    itemStatusService,
                                                                                    itemTypeService,
                                                                                    timeService,
                                                                                    pilotCustomerService,
                                                                                    pilotUserService,
                                                                                    personService,
                                                                                    organizationService,
                                                                                    workItemService,
                                                                                    requestModelAccessor,
                                                                                    timeTypeService, 
                                                                                    securityContextService, 
                                                                                    personStorage)
        {
            _workItemService = workItemService;
            _requestModelAccessor = requestModelAccessor;
            _genericDataViewService = genericDataViewService;
            _personStorage = personStorage;
            _securityContextService = securityContextService;
            _pilotProcessorService = pilotProcessorService;
            _timeService = timeService;
        }

        public override async Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            if (buttonId == GenericButtonConstants.Post)
            {
                var response = JsonConvert.DeserializeObject<GenericDataViewResponse>(data);
                if (response == null) { return null; }
                var personSystemId = _securityContextService.GetIdentityUserSystemId();
                var organizationSystemId = _personStorage.CurrentSelectedOrganization.SystemId;
                if (organizationSystemId == Guid.Empty) return null;
                if (!personSystemId.HasValue && personSystemId == Guid.Empty) return null;

                if (Guid.TryParse(response.EntitySystemId, out Guid systemId))
                {

                    var time = _pilotProcessorService.UpdateTimeSpentFields(systemId, organizationSystemId, (Guid)personSystemId, response.Form);
                    if (time != null)
                    {
                        var containter = BuildTimeSpentContainer(GetFields(PilotProcessorConstants.NewOrViewTimeSpent), time);

                        return containter;
                    }
                }
            }
            return null;
        }
        public override async Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            var entitySystemId = _genericDataViewService.GetEntitySystemIdFromQuerystring(data);
            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);
            var templateContainer = GetFields(PilotProcessorConstants.NewOrViewTimeSpent);
            var personSystemId = _securityContextService.GetIdentityUserSystemId();
            var organizationSystemId = _personStorage.CurrentSelectedOrganization.SystemId;
            if (organizationSystemId == Guid.Empty) return null;
            if (!personSystemId.HasValue && personSystemId == Guid.Empty) return null;

            var item = GetWorkItem(entitySystemId);
            var times = GetTimeSpentOnWorkItem(entitySystemId);

            view.DataContainers.Add(BuildNewTimeSpentContainer(templateContainer, GetNewTimeSpent(item.SystemId)));

            if (times != null)
            {
                foreach (var time in times)
                {
                    view.DataContainers.Add(BuildTimeSpentContainer(templateContainer, time));
                }
            }
            return view;
        }
        public GenericDataContainer BuildTimeSpentContainer(GenericDataContainer templateContainer, Time time)
        {
            var result = base.BuildTimeSpentContainer(templateContainer, time);

            result.Settings.PostContainer = true;

            result.Settings.PostContainerButtonText = "Update";


            result.Settings.PostContainerPageSystemId = _currentPageSystemId;


            return result;
        }
        public GenericDataContainer BuildNewTimeSpentContainer(GenericDataContainer templateContainer, Time time)
        {
            time.SystemId = time.ItemSystemId;
            var result = BuildTimeSpentContainer(templateContainer, time);

            result.Settings.PostContainerButtonText = "Save new";


            return result;
        }

        public override GenericDataViewSettings GetDataViewSettings(Guid pageSystemId)
        {
            return base.GetDataViewSettings(pageSystemId);
        }
        public override Task<object> GetGridViewForExport(string data)
        {
            throw new NotImplementedException();
        }
        public async override Task<GenericDataContainer> UpdateField(GenericDataField fieldData)
        {
            if (Guid.TryParse(fieldData.EntitySystemId, out Guid systemId))
            {
                if (_workItemService.UpdateField(systemId, fieldData.FieldId, fieldData.FieldValue, out WorkItem item))
                {
                    return BuildWorkItemContainer(GetFields(PilotProcessorConstants.WorkItems), item);
                }
            }
            return null;

        }
    }
}
