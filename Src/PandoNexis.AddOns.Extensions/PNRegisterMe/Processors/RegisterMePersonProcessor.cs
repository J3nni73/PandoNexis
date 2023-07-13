using DocumentFormat.OpenXml.Drawing;
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
    public class RegisterMePersonProcessor : PersonDataViewBase
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
            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);

            if (!IsValidated())
            {
                var container = BuildCodeContainer();
                container.Settings.PostContainer = true;
                container.Settings.PostContainerPageSystemId = _currentPageSystemId;
                container.Settings.PostContainerButtonText = "addons.registerme.headertexts.saveperson".AsWebsiteText(_requestModelAccessor.RequestModel.WebsiteModel.Website);
                view.DataContainers.Add(container);
            }
            else
            {
                var person = SetOrCreatePerson(new PersonWithProperties());
                var container = BuildContainer(GetFields(RegisterMeConstants.RegisterMePerson));
                container.Settings.PostContainer = true;
                container.Settings.PostContainerPageSystemId = _currentPageSystemId;
                container.Settings.PostContainerButtonText = "addons.registerme.headertexts.saveperson".AsWebsiteText(_requestModelAccessor.RequestModel.WebsiteModel.Website);
                view.DataContainers.Add(container);
            }
            return view;
        }

        public GenericDataContainer BuildCodeContainer()
        {
            var fields = GetFields(RegisterMeConstants.RegisterMePerson);
            var container = new GenericDataContainer();
            var website = _requestModelAccessor.RequestModel.WebsiteModel.Website;
            container.Fields.Add(CodeField(website));
            container.Settings.PostContainer = true;
            container.Settings.PostContainerPageSystemId = _currentPageSystemId;
            container.Settings.PostContainerButtonText = "addons.registerme.headertexts.saveperson".AsWebsiteText(_requestModelAccessor.RequestModel.WebsiteModel.Website);

            while (fields.Fields.Count() > container.Fields.Count())
            {
                container.Fields.Add(EmptyField(website));
            }


            return container;
        }
        public async override Task<GenericDataContainer> UpdateField(GenericDataField fieldData)
        {
            if (fieldData.FieldId == "codeField")
            {
                var data = fieldData.FieldValue ?? "123123";
                _sessionStorage.SetValue(RegisterMeConstants.Code, data);
                return BuildCodeContainer();
            }
            var person = GetPerson();
            if (person.SystemId.ToString() != fieldData.EntitySystemId) return null;
            if (person.fields.ContainsKey(fieldData.FieldId))
            {
                person.fields[fieldData.FieldId] = fieldData.FieldValue;
            }
            else
            {
                person.fields.Add(fieldData.FieldId, fieldData.FieldValue);
            }


            var container = BuildContainer(GetFields(RegisterMeConstants.RegisterMePerson));

            return container;
        }

        public GenericDataContainer BuildContainer(GenericDataContainer templateContainer)
        {

            var test = JsonConvert.SerializeObject(templateContainer);
            var test2 = JsonConvert.DeserializeObject<GenericDataContainer>(test);
            var result = test2;

            return result;

        }

        public async override Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);
            var dataViewResponse = JsonConvert.DeserializeObject<GenericDataViewResponse>(data);
            if (Guid.TryParse(dataViewResponse.EntitySystemId, out Guid personSystemId))
            {
                var container = new GenericDataContainer();

                if (dataViewResponse.Form.ContainsKey("codeField"))
                {
                    var code = dataViewResponse?.Form["codeField"]?.ToString() ?? string.Empty;
                    Validate(code);
                    return GetDataView(pageSystemId, "")?.Result;
                    //if (Validate(code))
                    //{
                    //    container = BuildContainer(GetFields(RegisterMeConstants.RegisterMePerson));
                    //    container.Settings.PostContainer = true;
                    //    container.Settings.PostContainerButtonText = "addons.registerme.headertexts.saveperson".AsWebsiteText(_requestModelAccessor.RequestModel.WebsiteModel.Website);
                      
                    //    view.DataContainers.Add(container); 
                    //    return view;
                    //}
                    //else
                    //{
                    //    view.DataContainers.Add(BuildCodeContainer());
                    //    return view;
                    //}
                }
                else if (IsValidated()&&!dataViewResponse.Form.ContainsKey("Response"))
                {
                    var template = _fieldTemplateService.Get<PersonFieldTemplate>(typeof(CustomerArea), DefaultWebsiteFieldValueConstants.CustomerTemplateId);
                    var person = new Person(template.SystemId);
                    person.SystemId = Guid.NewGuid();
                    foreach (var field in dataViewResponse.Form)
                    {
                        person.Fields.AddOrUpdateValue(field.Key, field.Value);
                    }
                    person.Fields.AddOrUpdateValue(RegisterMeConstants.AddedByRegisterMeForm, true);

                    using (_securityContextService.ActAsSystem())
                    {
                        _personService.Create(person);
                    }
                    container.Fields = new List<GenericDataField>
                        {
                            SaveResponseAsGenericField(_requestModelAccessor.RequestModel.WebsiteModel.Website)
                        };
                    container.Settings.PostContainer = true;
                    container.Settings.PostContainerPageSystemId = _currentPageSystemId;
                    container.Settings.PostContainerButtonText = "Lägg till ytterligare en deltagare";
                    view.DataContainers.Add(container);
                    return view;
                }
                else
                {
                    container = BuildContainer(GetFields(RegisterMeConstants.RegisterMePerson));
                    container.Settings.PostContainer = true;
                    container.Settings.PostContainerPageSystemId = _currentPageSystemId;
                    container.Settings.PostContainerButtonText = "addons.registerme.headertexts.saveperson".AsWebsiteText(_requestModelAccessor.RequestModel.WebsiteModel.Website);
                    view.DataContainers.Add(container);
                    return view;
                }
            }

            return null;

        }

        public GenericDataField CheckCodeButton(Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = Guid.Empty.ToString();
            result.FieldId = "CheckCode";
            result.FieldName = "addons.registerme.headertexts.checkcode".AsWebsiteText(website);
            result.FieldType = "button";
            result.Settings.ButtonText = "addons.registerme.headertexts.checkcode".AsWebsiteText(website);
            result.Settings.HideButton = false;

            return result;
        }
        public GenericDataField EmptyField(Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = Guid.Empty.ToString();
            result.FieldId = RegisterMeConstants.SavePerson;
            result.FieldName = "addons.registerme.headertexts.saveperson".AsWebsiteText(website);
            result.FieldType = "button";
            result.Settings.ButtonText = "addons.registerme.headertexts.saveperson".AsWebsiteText(website);
            result.Settings.HideButton = true;

            return result;
        }
        public GenericDataField CodeField(Website website)
        {
            var field = new GenericDataField();

            field.FieldName = "Code";
            field.EntitySystemId = Guid.Empty.ToString();
            field.FieldId = "codeField";
            field.FieldType = "string";
            field.FieldValue = _sessionStorage.GetValue<string>(RegisterMeConstants.Code) ?? string.Empty;
            field.Settings.Editable = true;
            return field;
        }
        public GenericDataField SavePersonAsGenericField(PersonWithProperties person, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = person.SystemId.ToString();
            result.FieldId = RegisterMeConstants.SavePerson;
            result.FieldName = "addons.registerme.headertexts.saveperson".AsWebsiteText(website);
            result.FieldType = "button";
            result.Settings.ButtonText = "addons.registerme.headertexts.saveperson".AsWebsiteText(website);
            result.Settings.HideButton = false;

            return result;
        }

        public GenericDataField SaveResponseAsGenericField(Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = Guid.Empty.ToString();
            result.FieldId = "Response";
            result.FieldName = "Tack för din anmälan";
            result.FieldType = "text";
            result.FieldValue = "Festen kommer hållas i Helsingborg 7 Juli 15:00 till sent, du kommer bli kontaktad inför festen på mailadressen du angett för vidare instruktioner.";

            return result;
        }
        public GenericDataField AddPersonAsGenericField(PersonWithProperties person, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = person.SystemId.ToString();
            result.FieldId = "AddPerson";
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
        private bool IsValidated()
        {
            var result = _sessionStorage.GetValue<bool>(RegisterMeConstants.Validated);

            return result;
        }
        private bool Validate(string code)
        {
            if (code.ToUpper() == RegisterMeConstants.ValidCode.ToUpper())
            {
                _sessionStorage.SetValue(RegisterMeConstants.Validated, true);

            }

            return IsValidated();
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
