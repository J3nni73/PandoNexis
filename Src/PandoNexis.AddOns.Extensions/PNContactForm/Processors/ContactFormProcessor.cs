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
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
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

namespace PandoNexis.AddOns.Extensions.PNContactForm.Processors
{
    [Service(Name = "ContactForm")]
    public class ContactFormProcessor : PersonDataViewBase
    {
        private readonly SessionStorage _sessionStorage;
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly PersonService _personService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly SecurityContextService _securityContextService;
        public ContactFormProcessor(FieldDefinitionService fieldDefinitionService,
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
            var container = BuildNewContainer(GetFields(ContactFormConstants.ContactForm));
            view.DataContainers.Add(container);

            return view;
        }
        public async override Task<GenericDataContainer> UpdateField(GenericDataField fieldData)
        {
            
            var container = BuildNewContainer(GetFields(ContactFormConstants.ContactForm));

            return container;
        }

        public GenericDataContainer BuildNewContainer(GenericDataContainer templateContainer)
        {

            var result = JsonConvert.DeserializeObject<GenericDataContainer>(JsonConvert.SerializeObject(templateContainer));
            result.Settings.PostContainer = true;
            result.Settings.PostContainerButtonText = "addons.contactform.postbutton.text".AsWebsiteText(_requestModelAccessor.RequestModel.WebsiteModel.Website);
            
            return result;

        }

        public async override Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            var dataViewResponse = JsonConvert.DeserializeObject<GenericDataViewResponse>(data);

            if (Guid.TryParse(dataViewResponse.EntitySystemId, out Guid personSystemId))
            {
                var container = new GenericDataContainer();


                switch (dataViewResponse.FieldId)
                {
                    case GenericButtonConstants.Post:
                        var template = _fieldTemplateService.Get<PersonFieldTemplate>(typeof(CustomerArea), DefaultWebsiteFieldValueConstants.CustomerTemplateId);
                        var person = new Person(template.SystemId);
                        person.SystemId = Guid.NewGuid();
                        foreach (var field in dataViewResponse.Form)
                        {
                            person.Fields.AddOrUpdateValue(field.Key, field.Value);
                        }
                        person.Fields.AddOrUpdateValue(ContactFormConstants.AddedByContactForm, true);

                        using (_securityContextService.ActAsSystem())
                        {
                            _personService.Create(person);
                        }
                        container.Fields = new List<GenericDataField>
                        {
                            SaveResponseAsGenericField(_requestModelAccessor.RequestModel.WebsiteModel.Website)
                        };
                        

                        return container;
                   

                }
            }
            return null;

        }
   
        public GenericDataField SaveResponseAsGenericField(Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = Guid.Empty.ToString();
            result.FieldId = "Response";
            result.FieldName = "addons.contactform.responsetexts.title".AsWebsiteText(website);
            result.FieldType = "text";
            result.FieldValue = "addons.contactform.responsetexts.text".AsWebsiteText(website);

            return result;
        }
     
       
       
    }
    public class PersonWithProperties
    {
        public Guid SystemId { get; set; }
        public Dictionary<string, string> fields { get; set; } = new Dictionary<string, string>();
    }
}
