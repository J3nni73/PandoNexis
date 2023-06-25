using DocumentFormat.OpenXml.Wordprocessing;
using Litium.Accelerator.Routing;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using Litium.Websites;
using Newtonsoft.Json;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Processors;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
using PandoNexis.AddOns.Extensions.PNNoCrm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNNoCrm.Processors
{
    [Service(Name = "PersonListByGroup")]
    public class PersonListByGroupProcessor : PersonDataViewBase
    {
        private readonly NoCrmPersonGroupService _personGroupService;
        private readonly GenericButtonService _genericButtonService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly NoCrmPersonService _noCrmPersonService;
        public PersonListByGroupProcessor(FieldDefinitionService fieldDefinitionService,
                                            FieldTemplateService fieldTemplateService,
                                            GenericDataViewService genericDataViewService,
                                            RequestModelAccessor requestModelAccessor,
                                            NoCrmPersonGroupService personGroupService,
                                            GenericButtonService genericButtonService,
                                            NoCrmPersonService noCrmPersonService) : base(fieldDefinitionService, fieldTemplateService, genericDataViewService, requestModelAccessor)
        {
            _personGroupService = personGroupService;
            _genericButtonService = genericButtonService;
            _requestModelAccessor = requestModelAccessor;
            _noCrmPersonService = noCrmPersonService;
        }

        public override async Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);
            var website = _requestModelAccessor.RequestModel.WebsiteModel.Website;

            var groupId = Guid.Parse(data.Replace("?entitySystemId=", ""));
            var persons = _personGroupService.GetPersonsInGroup(groupId);
            var templateFields = GetFields(NoCrmProcessorConstants.PersonListByGroup);

            foreach (var person in persons)
            {
                var container = BuildContainer(templateFields, person);
                var viewButton = new GenericDataField();
                viewButton.EntitySystemId = person.SystemId.ToString();
                viewButton.FieldId = NoCrmProcessorConstants.ViewPersonListByGroup;
                viewButton.FieldName = NoCrmProcessorConstants.ViewPersonListByGroup;
                viewButton.FieldType = DataFieldTypes.ButtonDGType;
                viewButton.Settings.GenericButtons.Add(_genericButtonService.GetButton(website, NoCrmProcessorConstants.NoCrmButtonLinks, NoCrmProcessorConstants.AddLogin, NoCrmProcessorConstants.NoCrmButtonNames, person.SystemId));
                container.Fields.Add(viewButton);
                view.DataContainers.Add(container);
            }




            return view;

        }
        public override GenericDataContainer BuildContainer(GenericDataContainer templateField, Person person)
        {
            return base.BuildContainer(templateField, person);

        }
        public override Task<GenericDataContainer> UpdateField(GenericDataField fieldData)
        {
            throw new NotImplementedException();
        }
        public async override Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            var dataViewResponse = JsonConvert.DeserializeObject<GenericDataViewResponse>(data);
            switch (buttonId)
            {
                case NoCrmProcessorConstants.AddLogin:
                    _noCrmPersonService.CreateLogin(dataViewResponse.EntitySystemId);
                    break;




            }
            return null;
        }
    }
}
