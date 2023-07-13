using Litium.Accelerator.Utilities;
using Litium.Customers;
using Litium.Runtime.DependencyInjection;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNQuizWalk.Processors
{
    [Service(Name = "QuizWalkView")]
    public class QuizWalkViewProcessor : QuizWalkItemBase
    {
        private readonly QuizWalkService _quizWalkService;
        private readonly PersonStorage _personStorage;

        public QuizWalkViewProcessor(QuizWalkService quizWalkService, 
                                    GenericDataViewService genericDataViewService, 
                                    PersonStorage personStorage, 
                                    PersonService personService) : base(genericDataViewService, personService, personStorage)
        {
            _quizWalkService = quizWalkService;
            _personStorage = personStorage;
        }

        public async override Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            
            var responseItem = JsonConvert.DeserializeObject<GenericDataViewResponse>(data);
            if (responseItem == null) return null;
            var item = _quizWalkService.Get(Guid.Parse(responseItem.EntitySystemId));
            if (item == null) item = new QuizWalkItem() { SystemId = Guid.NewGuid() };
            if (buttonId == "Post")
            {
                if (responseItem.Form[QuizWalkConstants.FieldForAnswer].ToString() == item.Answer)
                {
                    item = _quizWalkService.UpdateOrganizationsWalk(_personStorage.CurrentSelectedOrganization.SystemId, item.SystemId);
                }
              
            }

            var result = GetDataView(pageSystemId, data);
            return result;
        }
        public override async Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            
            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);
            var templateContainer = GetFields(QuizWalkConstants.QuizWalkView);
            if (_personStorage.CurrentSelectedOrganization == null) return view;
            var item = _quizWalkService.GetNext(_personStorage.CurrentSelectedOrganization.SystemId);

            if (item == null) return view;
                view.DataContainers.Add(BuildContainer(templateContainer, item));
            

            return view;
        }

        private GenericDataContainer BuildContainer(GenericDataContainer templateContainer, QuizWalkItem item)
        {
            var result = JsonConvert.DeserializeObject<GenericDataContainer>(JsonConvert.SerializeObject(templateContainer));

            result.Settings.PostContainer = true;

            foreach (var field in result.Fields)
            {
                field.EntitySystemId = item.SystemId.ToString();
                field.FieldValue = GetValue(field.FieldId, item);
            }

            return result;

        }
    }
}
