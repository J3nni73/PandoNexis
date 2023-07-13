using Litium.Accelerator.Utilities;
using Litium.Blocks;
using Litium.Customers;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Web.Mvc;
using Newtonsoft.Json;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using Solution.Extensions.PNPilot.Objects;
using Solution.Extensions.PNPilot.Services;
using Solution.Extensions.PNQuizWalk.Constants;
using Solution.Extensions.PNQuizWalk.Objects;
using Solution.Extensions.PNQuizWalk.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNQuizWalk.Processors
{
    [Service(Name = "QuizWalkChat")]
    public class QuizWalkChatProcessor : QuizWalkItemBase
    {
        private readonly QuizWalkChatService _quizWalkChatService;
        private readonly PersonStorage _personStorage;
        private readonly GenericButtonService _genericButtonService;
        private readonly SecurityContextService _securityContextService;


        public QuizWalkChatProcessor(QuizWalkChatService quizWalkChatService,
                                    GenericDataViewService genericDataViewService,
                                    PersonStorage personStorage,
                                    PersonService personService,
                                    GenericButtonService genericButtonService,
                                    SecurityContextService securityContextService) : base(genericDataViewService, personService, personStorage)
        {
            _quizWalkChatService = quizWalkChatService;
            _personStorage = personStorage;
            _genericButtonService = genericButtonService;
            _securityContextService = securityContextService;
        }

        public async override Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {

            var responseItem = JsonConvert.DeserializeObject<GenericDataViewResponse>(data);
            if (responseItem == null) return null;
            var item = _quizWalkChatService.Get(Guid.Parse(responseItem.EntitySystemId));
            if (item == null) item = new QuizWalkChatItem() 
            { 
                 SystemId = Guid.NewGuid(), 
                 PersonSystemId = (Guid)_securityContextService.GetIdentityUserSystemId(),
                 OrganizationSystemId = _personStorage.CurrentSelectedOrganization.SystemId
        };
            if (buttonId == "Post")
            {
                foreach (var field in responseItem.Form)
                {
                    SetValue(field.Key, field.Value, ref item);
                }
                _quizWalkChatService.AddOrUpdate(item);
                var result = GetDataView(pageSystemId, data);
                return result;
            }
            if (buttonId == QuizWalkConstants.QuizWalkAddNew)
            {
                _quizWalkChatService.AddOrUpdate(item);
                item = _quizWalkChatService.Get(item.SystemId);
                var container = BuildContainer(GetFields(QuizWalkConstants.QuizWalkChat), item);
            }

            return null;
        }
        public override async Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {

            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);
            var templateContainer = GetFields(QuizWalkConstants.QuizWalkChat);
         
            if (_personStorage.CurrentSelectedOrganization == null) return view;
            var items = _quizWalkChatService.GetChatsForOrganization(_personStorage.CurrentSelectedOrganization.SystemId);
            view.DataContainers.Add(BuildNewContainer(templateContainer));
          
                foreach (var item in items)
                {
                    view.DataContainers.Add(BuildContainer(templateContainer, item));
                }
          
            return view;
        }
        private GenericDataContainer BuildNewContainer(GenericDataContainer templateContainer) 
        {
            var item = new QuizWalkChatItem();
            item.OrganizationSystemId = _personStorage.CurrentSelectedOrganization.SystemId;
            item.PersonSystemId = (Guid)_securityContextService.GetIdentityUserSystemId();
            var container = BuildContainer(templateContainer, item);
            container.Settings.PostContainerButtonText = "Posta inlägg";
            container.Fields.FirstOrDefault(i => i.FieldId == QuizWalkConstants.Chat).Settings.Editable = true;

            return container;
        }
        private GenericDataContainer BuildContainer(GenericDataContainer templateContainer, QuizWalkChatItem item)
        {
            var result = JsonConvert.DeserializeObject<GenericDataContainer>(JsonConvert.SerializeObject(templateContainer));

            result.Settings.PostContainer = true;
            result.Settings.PostContainerPageSystemId = _currentPageSystemId;

            foreach (var field in result.Fields)
            {
                field.EntitySystemId = item.SystemId.ToString();
                field.FieldValue = GetValue(field.FieldId, item);
            }
            result.Settings.PostContainerButtonText = "Posta inlägg";
            return result;

        }
    }
}
