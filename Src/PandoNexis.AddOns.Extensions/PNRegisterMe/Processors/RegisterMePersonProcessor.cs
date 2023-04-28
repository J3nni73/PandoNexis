﻿using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using JetBrains.Annotations;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Web;
using Litium.Websites;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Processors;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;
using PandoNexis.AddOns.Extensions.PNNoErp.Services;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNRegisterMe.Processors
{
    [Service(Name = "RegisterMePerson")]
    public class RegisterMePersonProcessor : CustomerDataViewBase
    {
        private readonly SessionStorage _sessionStorage;
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly PersonService _personService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly SecurityContextService _securityContextService;
        public RegisterMePersonProcessor(FieldDefinitionService fieldDefinitionService,
                                    FieldTemplateService fieldTemplateService,
                                    GenericDataViewService genericDataViewService,
                                    SessionStorage sessionStorage,
                                    PersonService personService,
                                    RequestModelAccessor requestModelAccessor,
                                    SecurityContextService securityContextService) : base(fieldDefinitionService, fieldTemplateService, genericDataViewService, requestModelAccessor)
        {
            _sessionStorage = sessionStorage;
            _fieldTemplateService = fieldTemplateService;
            _personService = personService;
            _requestModelAccessor = requestModelAccessor;
            _securityContextService = securityContextService;
        }

        public async override Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            var person = SetOrCreatePerson(new PersonWithProperties());


            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);
            var container = BuildContainer(GetFields(RegisterMeConstants.RegisterMePerson), person);
            view.DataContainers.Add(container);

            return view;
        }
        public async override Task<GenericDataContainer> UpdateField(GenericDataField fieldData)
        {
            var person = GetPerson();
            if (person.SystemId.ToString() != fieldData.EntitySystemId) return null;
            if (person.fields.ContainsKey(fieldData.FieldID))
            {
                person.fields[fieldData.FieldID] = fieldData.FieldValue;
            }
            else
            {
                person.fields.Add(fieldData.FieldID, fieldData.FieldValue);
            }

            SetOrCreatePerson(person);

            var container = BuildContainer(GetFields(RegisterMeConstants.RegisterMePerson), person);

            return container;
        }

        public GenericDataContainer BuildContainer(GenericDataContainer templateContainer, PersonWithProperties person)
        {

            var test = JsonConvert.SerializeObject(templateContainer);
            var test2 = JsonConvert.DeserializeObject<GenericDataContainer>(test);
            var result = test2;

            foreach (var field in result.Fields)
            {
                if (person.fields.TryGetValue(field.FieldID, out string value))
                {
                    field.FieldValue = value;
                }
                field.EntitySystemId = person.SystemId.ToString();
            }
            result.Fields.Add(SavePersonAsGenericField(person, _requestModelAccessor.RequestModel.WebsiteModel.Website));

            return result;

        }

        public async override Task<GenericDataContainer> ButtonClick(GenericDataField fieldData)
        {

            if (Guid.TryParse(fieldData.EntitySystemId, out Guid personSystemId))
            {
                var container = new GenericDataContainer();


                switch (fieldData.FieldID)
                {
                    case RegisterMeConstants.SavePerson:
                        var person = GetPerson();
                        var template = _fieldTemplateService.Get<PersonFieldTemplate>(typeof(CustomerArea), DefaultWebsiteFieldValueConstants.CustomerTemplateId);
                        var newPerson = new Person(template.SystemId);
                        newPerson.SystemId = person.SystemId;
                        foreach (var field in person.fields)
                        {
                            newPerson.Fields.AddOrUpdateValue(field.Key, field.Value);
                        }
                        newPerson.Fields.AddOrUpdateValue(RegisterMeConstants.AddedByRegisterMeForm, true);
                        using (_securityContextService.ActAsSystem())
                        {
                            _personService.Create(newPerson);
                        }
                        container.Fields = new List<GenericDataField>
                        {
                            SaveResponseAsGenericField(person, _requestModelAccessor.RequestModel.WebsiteModel.Website) ,
                            AddPersonAsGenericField(person, _requestModelAccessor.RequestModel.WebsiteModel.Website)
                        };
                        return container;
                    case "AddPerson":
                        var addPerson = SetOrCreatePerson(new PersonWithProperties());
                        container = BuildContainer(GetFields(RegisterMeConstants.RegisterMePerson), addPerson);
                        return container;

                }
            }
            return null;

        }
        public GenericDataField SavePersonAsGenericField(PersonWithProperties person, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = person.SystemId.ToString();
            result.FieldID = RegisterMeConstants.SavePerson;
            result.FieldName = "addons.registerme.headertexts.saveperson".AsWebsiteText(website);
            result.FieldType = "button";
            result.Settings.ButtonText = "addons.registerme.headertexts.saveperson".AsWebsiteText(website);
            result.Settings.HideButton = false;

            return result;
        }

        public GenericDataField SaveResponseAsGenericField(PersonWithProperties person, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = person.SystemId.ToString();
            result.FieldID = "Response";
            result.FieldName = "Tack för din anmälan";
            result.FieldType = "text";
            result.FieldValue = "Festen kommer hållas i Helsingborg 7 Juli 15:00 till sent, du kommer bli kontaktad inför på mailadressen du angett för vidare instruktioner.";

            return result;
        }
        public GenericDataField AddPersonAsGenericField(PersonWithProperties person, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = person.SystemId.ToString();
            result.FieldID = "AddPerson";
            result.FieldName = "addons.registerme.headertexts.saveperson".AsWebsiteText(website);
            result.FieldType = "button";
            result.Settings.ButtonText = "Lägg till ytterligare en deltagare";
            result.Settings.HideButton = false;

            return result;
        }
        private PersonWithProperties GetPerson()
        {
            var personFromSession = _sessionStorage.GetValue<PersonWithProperties>(RegisterMeConstants.RegisterMePerson);
            if (personFromSession == null)
                personFromSession = new PersonWithProperties() { SystemId = Guid.NewGuid() };
            return personFromSession;
        }
        private PersonWithProperties SetOrCreatePerson(PersonWithProperties person)
        {
            if (person.SystemId == null || person.SystemId == Guid.Empty)
                person.SystemId = Guid.NewGuid();
            _sessionStorage.SetValue(RegisterMeConstants.RegisterMePerson, person);

            return person;
        }
    }
    public class PersonWithProperties
    {
        public Guid SystemId { get; set; }
        public Dictionary<string, string> fields { get; set; } = new Dictionary<string, string>();
    }
}