using DocumentFormat.OpenXml.Wordprocessing;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Search;
using Litium.Accelerator.Utilities;
using Litium.Customers;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Websites;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
using PandoNexis.AddOns.Extensions.PNPilot.Constants;
using PandoNexis.AddOns.Extensions.PNPilot.Services;
using PandoNexis.AddOns.PNPilot.Constants;
using PandoNexis.AddOns.PNPilot.Objects;
using PandoNexis.AddOns.PNPilot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNPilot.Processors
{
    [Service(Name = "PilotOverview")]
    public class PilotOverviewProcessor : PilotProcessorBase
    {
        private readonly WorkItemService _workItemService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly GenericButtonService _genericButtonService;
        private readonly PilotProcessorService _pilotProcessorService;

        public PilotOverviewProcessor(GenericDataViewService genericDataViewService,
                                    WorkItemService workItemService,
                                    RequestModelAccessor requestModelAccessor,
                                    ItemStatusService itemStatusService,
                                    ItemTypeService itemTypeService,
                                    TimeService timeService,
                                    PilotCustomerService pilotCustomerService,
                                    PilotUserService pilotUserService,
                                    GenericButtonService genericButtonService,
                                    PersonService personService,
                                    OrganizationService organizationService,
                                    TimeTypeService timeTypeService,
                                    PilotProcessorService pilotProcessorService, 
                                    SecurityContextService securityContextService, 
                                    PersonStorage personStorage) : base(genericDataViewService,                                                                                    itemStatusService,
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
                                                                                    personStorage, 
                                                                                    genericButtonService)
        {
            _workItemService = workItemService;
            _requestModelAccessor = requestModelAccessor;
            _genericButtonService = genericButtonService;
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
                    var item = _pilotProcessorService.UpdateFields(systemId, response.Form);
                    RefreshDataLayer();
                    return BuildWorkItemContainer(GetFields(PilotProcessorConstants.WorkItems), item);
                }
            }
            return null;
        }
        public override async Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);
            var templateContainer = GetFields(PilotProcessorConstants.PilotOverview);
            view.Settings.DataViewButtons.Add(_genericButtonService.GetButton(_requestModelAccessor.RequestModel.WebsiteModel.Website, PilotProcessorConstants.PilotButtonLinks, PilotProcessorConstants.NewWorkItem, PilotProcessorConstants.PilotButtonNames, Guid.Empty));
            
            view.DataContainers.AddRange(BuildOverWiewContainer(templateContainer));
           

            return view;
        }
        public List<GenericDataContainer> BuildOverWiewContainer(GenericDataContainer templateContainer)
        {
            var result = JsonConvert.DeserializeObject<GenericDataContainer>(JsonConvert.SerializeObject(templateContainer));

            return GetOverViewPerStatusContainer(result);
            
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
            var item = await Update(fieldData);
            if (item != null)
            {
                return BuildWorkItemContainer(GetFields(PilotProcessorConstants.PilotOverview), item);
            }
            return null;
        }
    }
}
