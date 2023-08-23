using DocumentFormat.OpenXml.Wordprocessing;
using IdentityServer4.Models;
using JetBrains.Annotations;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Services;
using Litium.Auditing;
using Litium.Customers;
using Litium.Data;
using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Runtime.DependencyInjection;
using Litium.Web;
using Litium.Websites;
using Newtonsoft.Json;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Processors;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
using PandoNexis.AddOns.Extensions.PNNoCrm.Definitions;
using PandoNexis.AddOns.Extensions.PNNoCrm.Services;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNNoCrm.Processors
{
    [Service(Name = "SendMail")]
    public class SendMailProcessor : GroupDataViewBase
    {
       
        private readonly DataService _dataService;
        private readonly GenericDataViewService _genericDataViewService;
        private readonly GenericButtonService _genericButtonService;
        private readonly NoCrmPersonGroupService _personGroupService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly MailService _mailService;
        private readonly NoCrmPersonService _noCrmPersonService;
        public SendMailProcessor(FieldTemplateService fieldTemplateService,
                               FieldDefinitionService fieldDefinitionService,
                               GenericDataViewService genericDataViewService,
                               RequestModelAccessor requestModelAccessor,
                               DataService dataService,
                               NoCrmPersonGroupService personGroupService,
                               GenericButtonService genericButtonService,
                               MailService mailService,
                               NoCrmPersonService noCrmPersonService) : base(fieldTemplateService, fieldDefinitionService, genericDataViewService, requestModelAccessor)
        {

            _dataService = dataService;
            _genericDataViewService = genericDataViewService;
            _personGroupService = personGroupService;
            _genericButtonService = genericButtonService;
            _requestModelAccessor = requestModelAccessor;
            _mailService = mailService;
            _noCrmPersonService = noCrmPersonService;
        }

        public async override Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            if (buttonId== GenericButtonConstants.Post) 
            {
                SendMail(data);

                return GetResponse();
            }
            return GetFields("");
        }

        private void SendMail(string data)
        {
            var response = JsonConvert.DeserializeObject<GenericDataViewResponse>(data);
            if (response==null) { return; }

            var addresses = _noCrmPersonService.GetEmailAddresses(Guid.Parse(response.EntitySystemId), true);

            var subject = response.Form[NoCrmProcessorConstants.Subject].ToString();
            var message = response.Form[NoCrmProcessorConstants.Body].ToString();

            if (addresses!=null && addresses.Any())
            {
                foreach (var address in addresses)
                {
                    var mailDefinition = new NoCrmMailDefinition(address, subject, message, Guid.Parse("BCA2AF71-8EDE-45A4-9D6E-F153A3B597CC"));

                    _mailService.SendEmail(mailDefinition, false);
                }
            }          
        }

        public async override Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            _currentPageSystemId = pageSystemId;
            var website = _requestModelAccessor.RequestModel.WebsiteModel.Website;
            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);
            view.DataContainers.Add(GetFields(data));
            



            return view;
        }
        public override GenericDataContainer GetFields(string templateId)
        {
            var entitySystemId = Guid.Parse(templateId.Replace("?entitySystemId=", ""));
            var container = new GenericDataContainer();
            container.Fields.Add(GetToAddresesMessage(entitySystemId));
            container.Fields.Add(GetSubject(entitySystemId));
            container.Fields.Add(GetBody(entitySystemId));
            container.Settings.PostContainer= true;
            container.Settings.PostContainerPageSystemId = _currentPageSystemId;

            return container;
        }
        public GenericDataContainer GetResponse()
        {
            var container = new GenericDataContainer();
            container.Fields.Add(GetSentMessage());
            

            return container;
        }
        public GenericDataField GetToAddresesMessage(Guid entitySystemId)
        {
            var field = new GenericDataField
            {
                FieldId = "address",
                FieldName = "address",
                FieldType = DataFieldTypes.TextAreaDGType,
                EntitySystemId = entitySystemId.ToString(),
            };
            field.Settings.Editable = false;

            field.FieldValue =  string.Join("; ", _noCrmPersonService.GetEmailAddresses(entitySystemId).ToArray());

            return field;
        }
        public GenericDataField GetSentMessage()
        {
            var field = new GenericDataField
            {
                FieldId = NoCrmProcessorConstants.Body,
                FieldName = NoCrmProcessorConstants.Body,
                FieldType = DataFieldTypes.TextAreaDGType,
                FieldValue = "Skickat"
            };
            field.Settings.Editable = false;
            return field;
        }
        public GenericDataField GetSubject(Guid entitySystemId)
        {
            var field = new GenericDataField
            {
                FieldId = NoCrmProcessorConstants.Subject,
                FieldName = NoCrmProcessorConstants.Subject,
                FieldType = DataFieldTypes.StringDGType,
                EntitySystemId = entitySystemId.ToString(),
            };
            field.Settings.Editable= true;
            return field;
        }
        public GenericDataField GetBody(Guid entitySystemId)
        {
            var field = new GenericDataField
            {
                FieldId = NoCrmProcessorConstants.Body,
                FieldName = NoCrmProcessorConstants.Body,
                FieldType = DataFieldTypes.TextAreaDGType,
                EntitySystemId = entitySystemId.ToString(),
            };
            field.Settings.Editable = true;
            return field;
        }

        public override GenericDataViewSettings GetDataViewSettings(Guid pageSystemId)
        {
            return _genericDataViewService.GetDataViewSettings(pageSystemId);
        }

        public override Task<object> GetGridViewForExport(string data)
        {
            throw new NotImplementedException();
        }
        public override Task<GenericDataContainer> UpdateField(GenericDataField fieldData)
        {
            throw new NotImplementedException();
        }
    }
}
