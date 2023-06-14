using DocumentFormat.OpenXml.Wordprocessing;
using Litium.Accelerator.Routing;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Processors;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
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
        private readonly PersonGroupService _personGroupService;
        public PersonListByGroupProcessor(FieldDefinitionService fieldDefinitionService,
                                            FieldTemplateService fieldTemplateService,
                                            GenericDataViewService genericDataViewService,
                                            RequestModelAccessor requestModelAccessor,
                                            PersonGroupService personGroupService) : base(fieldDefinitionService, fieldTemplateService, genericDataViewService, requestModelAccessor)
        {
            _personGroupService = personGroupService;
        }

        public override async Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);


            var groupId = Guid.Parse(data.Replace("?entitySystemId=", ""));
            var persons = _personGroupService.GetPersonsInGroup(groupId);
            var templateFields = GetFields(NoCrmProcessorConstants.PersonListByGroup);

            foreach ( var person in persons ) 
            { 
                view.DataContainers.Add(BuildContainer(templateFields, person));
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
    }
}
