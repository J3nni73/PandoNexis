using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
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
        private readonly NoCrmOrganizationService _noCrmOrganizationService;
        private readonly PersonStorage _personStorage;
        private readonly PersonService _personService;
        private readonly SecurityContextService _securityContextService;
        private readonly RoleService _roleService;
        public PersonListByGroupProcessor(FieldDefinitionService fieldDefinitionService,
                                            FieldTemplateService fieldTemplateService,
                                            GenericDataViewService genericDataViewService,
                                            RequestModelAccessor requestModelAccessor,
                                            NoCrmPersonGroupService personGroupService,
                                            GenericButtonService genericButtonService,
                                            NoCrmPersonService noCrmPersonService,
                                            NoCrmOrganizationService noCrmOrganizationService,
                                            PersonStorage personStorage,
                                            PersonService personService,
                                            SecurityContextService securityContextService,
                                            RoleService roleService) : base(fieldDefinitionService, fieldTemplateService, genericDataViewService, requestModelAccessor)
        {
            _personGroupService = personGroupService;
            _genericButtonService = genericButtonService;
            _requestModelAccessor = requestModelAccessor;
            _noCrmPersonService = noCrmPersonService;
            _noCrmOrganizationService = noCrmOrganizationService;
            _personStorage = personStorage;
            _personService = personService;
            _securityContextService = securityContextService;
            _roleService = roleService;
        }

        public override async Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            return await GetDataView(pageSystemId, Guid.Parse(data.Replace("?entitySystemId=", "")));
        }
        public async Task<GenericDataView> GetDataView(Guid pageSystemId, Guid groupId)
        {
            _currentPageSystemId = pageSystemId;
            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);
            var website = _requestModelAccessor.RequestModel.WebsiteModel.Website;

            var persons = _personGroupService.GetPersonsInGroup(groupId);
            var templateFields = GetFields(NoCrmProcessorConstants.PersonListByGroup);

            foreach (var person in persons)
            {
                var container = BuildContainer(templateFields, person);

                view.DataContainers.Add(container);
            }

            return view;

        }
        public override GenericDataContainer BuildContainer(GenericDataContainer templateField, Person person)
        {
            var container = base.BuildContainer(templateField, person);

            container.Fields.Add(_noCrmOrganizationService.GetFieldCustomers(_requestModelAccessor.RequestModel.WebsiteModel.Website, _personStorage.CurrentSelectedOrganizationSystemId, person));

            var viewButton = new GenericDataField();
            viewButton.EntitySystemId = person.SystemId.ToString();
            viewButton.FieldId = NoCrmProcessorConstants.ViewPersonListByGroup;
            viewButton.FieldName = NoCrmProcessorConstants.ViewPersonListByGroup;
            viewButton.FieldType = DataFieldTypes.ButtonDGType;
            viewButton.Settings.GenericButtons.Add(_noCrmPersonService.GetAddLoginButton(_requestModelAccessor.RequestModel.WebsiteModel.Website, person));
            container.Fields.Add(viewButton);

            return container;

        }
        public override async Task<GenericDataContainer> UpdateField(GenericDataField fieldData)
        {
            if (fieldData == null) return null;
            if (Guid.TryParse(fieldData.EntitySystemId, out var entitySystemId))
            {
                var person = _personService.Get(entitySystemId)?.MakeWritableClone();
                if (person == null) return null;

                if (fieldData.FieldId == NoCrmProcessorConstants.ChildOrganizations)
                {
                    if (Guid.TryParse(fieldData.FieldValue, out Guid customerSystemId))
                    {
                        if (person.OrganizationLinks.FirstOrDefault(i=>i.OrganizationSystemId == customerSystemId)==null)
                        {
                           
                            var link = new PersonToOrganizationLink(customerSystemId)
                            {
                                RoleSystemIds = _roleService.GetAll()?.Select(i => i.SystemId)?.ToHashSet<Guid>() 
                            };
                            person.OrganizationLinks.Add(link);
                        }
                       
                    }
                }
                using (_securityContextService.ActAsSystem())
                {
                    _personService.Update(person);
                }
                var container =  BuildContainer(GetFields(NoCrmProcessorConstants.PersonListByGroup), person);
                return container;
            }
            return null;
        }
        public async override Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            var dataViewResponse = JsonConvert.DeserializeObject<GenericDataViewResponse>(data);
            switch (buttonId)
            {
                case NoCrmProcessorConstants.AddLogin:
                    _noCrmPersonService.CreateLogin(dataViewResponse.EntitySystemId);
                    break;
                case NoCrmProcessorConstants.ResetPassword:
                    _noCrmPersonService.ResetPassword(dataViewResponse.EntitySystemId);
                    break;




            }
            return await GetDataView(pageSystemId, data);
        }
    }
}
