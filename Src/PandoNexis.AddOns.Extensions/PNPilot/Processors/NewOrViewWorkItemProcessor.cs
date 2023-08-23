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
    [Service(Name = "NewOrViewWorkItem")]
    public class NewOrViewWorkItemProcessor : PilotProcessorBase
    {
        private readonly WorkItemService _workItemService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly GenericDataViewService _genericDataViewService;
        private readonly PersonStorage _personStorage;
        private readonly SecurityContextService _securityContextService;
        private readonly PilotProcessorService _pilotProcessorService;
        public NewOrViewWorkItemProcessor(GenericDataViewService genericDataViewService,
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
        }

        public override async Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            if (buttonId == GenericButtonConstants.Post)
            {
                var response = JsonConvert.DeserializeObject<GenericDataViewResponse>(data);
                if (response == null) { return null; }
                if (Guid.TryParse(response.EntitySystemId, out Guid systemId))
                {

                    var workItem = _pilotProcessorService.UpdateFields(systemId, response.Form);
                    if (workItem!=null)
                    { 
                        var containter = BuildWorkItemContainer(GetFields(PilotProcessorConstants.WorkItems), workItem);

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
            var templateContainer = GetFields(PilotProcessorConstants.NewOrViewWorkItem);
            var personSystemId = _securityContextService.GetIdentityUserSystemId();
            var organizationSystemId = _personStorage.CurrentSelectedOrganization.SystemId;
            if (organizationSystemId == Guid.Empty) return null;
            if (!personSystemId.HasValue && personSystemId == Guid.Empty) return null;
            var item = entitySystemId == Guid.Empty ? _workItemService.GetNewItem((Guid)organizationSystemId, (Guid)personSystemId) : _workItemService.GetItem(entitySystemId);
            
            view.DataContainers.Add(BuildWorkItemContainer(templateContainer, item));

            return view;
        }
        public GenericDataContainer BuildWorkItemContainer(GenericDataContainer templateContainer, WorkItem workItem)
        {
            var result = base.BuildWorkItemContainer(templateContainer, workItem);

            result.Settings.PostContainer = true;
            result.Settings.PostContainerPageSystemId = _currentPageSystemId;
            

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
