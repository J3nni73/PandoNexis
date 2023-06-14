using DocumentFormat.OpenXml.Wordprocessing;
using Litium.Accelerator.Routing;
using Litium.Customers;
using Litium.Data;
using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Runtime.DependencyInjection;
using Litium.Web;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Processors;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNNoCrm.Processors
{
    [Service(Name = "Groups")]
    public class GroupsProcessor : GroupDataViewBase
    {
        private readonly GroupService _groupService;
        private readonly PersonService _personService;
        private readonly DataService _dataService;
        private readonly GenericDataViewService _genericDataViewService;
        private readonly GenericButtonService _genericButtonService;
        private readonly PersonGroupService _personGroupService;
        private readonly RequestModelAccessor _requestModelAccessor;
        public GroupsProcessor(FieldTemplateService fieldTemplateService,
                               FieldDefinitionService fieldDefinitionService,
                               GenericDataViewService genericDataViewService,
                               RequestModelAccessor requestModelAccessor,
                               GroupService groupService,
                               DataService dataService,
                               PersonService personService,
                               PersonGroupService personGroupService,
                               GenericButtonService genericButtonService) : base(fieldTemplateService, fieldDefinitionService, genericDataViewService, requestModelAccessor)
        {
            _groupService = groupService;
            _dataService = dataService;
            _genericDataViewService = genericDataViewService;
            _personService = personService;
            _personGroupService = personGroupService;
            _genericButtonService = genericButtonService;
            _requestModelAccessor = requestModelAccessor;
        }

        public async override Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            //switch (buttonId)
            //{
            //    case "ViewPersons":
            //        return await GetDataView(pageSystemId, "");




            //}
            //throw new NotImplementedException();
            return null;
        }

        public override Task<object> GetDataForm(string data)
        {
            throw new NotImplementedException();
        }

        public async override Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            var website = _requestModelAccessor.RequestModel.WebsiteModel.Website;
            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);

            var templateContainer = GetFields(NoCrmProcessorConstants.Groups);
            var groups = _personGroupService.GetGroups();
            foreach (var group in groups)
            {

                var container = BuildContainer(templateContainer, group);
                var persons = _personGroupService.CountPersonsInGroup(group.SystemId);

                var countField = new GenericDataField();
                countField.EntitySystemId = group.SystemId.ToString();
                countField.FieldId = "Count";
                countField.FieldType = DataFieldTypes.StringDGType;
                countField.FieldValue = persons.ToString();
                countField.FieldName = "Antal personer";
                container.Fields.Add(countField);

                var viewGroupButton = new GenericDataField();
                viewGroupButton.EntitySystemId = group.SystemId.ToString();
                viewGroupButton.FieldId = NoCrmProcessorConstants.ViewPersonListByGroup;
                viewGroupButton.FieldName = NoCrmProcessorConstants.ViewPersonListByGroup;
                viewGroupButton.FieldType= DataFieldTypes.ButtonDGType;
                viewGroupButton.Settings.GenericButtons.Add(_genericButtonService.GetButton(website, NoCrmProcessorConstants.NoCrmButtonLinks, NoCrmProcessorConstants.ViewPersonListByGroup, NoCrmProcessorConstants.NoCrmButtonNames, group.SystemId));

                container.Fields.Add(viewGroupButton);
                view.DataContainers.Add(container);

            }


            return view;
        }
        public override GenericDataContainer GetFields(string templateId)
        {
            return base.GetFields(templateId);
        }

        public override GenericDataViewSettings GetDataViewSettings(Guid pageSystemId)
        {
            return _genericDataViewService.GetDataViewSettings(pageSystemId);
        }

        public override Task<object> GetGridViewForExport(string data)
        {
            throw new NotImplementedException();
        }

        public override Task<object> HandleFormData(string data)
        {
            throw new NotImplementedException();
        }

        public override Task<GenericDataContainer> UpdateField(GenericDataField fieldData)
        {
            throw new NotImplementedException();
        }
    }
}
