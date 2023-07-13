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
    [Service(Name = "QuizWalkAddNew")]
    public class QuizWalkAddNewProcessor : QuizWalkItemBase
    {
        private readonly QuizWalkService _quizWalkService;

        public QuizWalkAddNewProcessor(QuizWalkService quizWalkService, 
                                        GenericDataViewService genericDataViewService, 
                                        PersonService personService,
                                        PersonStorage personStorage) : base(genericDataViewService, personService, personStorage)
        {
            _quizWalkService = quizWalkService;
        }

        public async override Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            var responseItem = JsonConvert.DeserializeObject<GenericDataViewResponse>(data);
            if (responseItem == null) return null;
            var item = _quizWalkService.Get(Guid.Parse(responseItem.EntitySystemId));
            if (item == null) item = new QuizWalkItem() { SystemId = Guid.NewGuid() };
            if (buttonId == "Post")
            {
                foreach (var response in responseItem.Form)
                {
                    SetValue(response.Key, response.Value, ref item);
                }
                _quizWalkService.AddOrUpdate(item);
            }

            var result = BuildContainer(GetFields(QuizWalkConstants.QuizWalkAdmin), item);
            return result;
        }
        public override async Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {

            var view = new GenericDataView();
            view.Settings = GetDataViewSettings(pageSystemId);
            var templateContainer = GetFields(QuizWalkConstants.QuizWalkAdmin);

            var items = _quizWalkService.GetAll();

            foreach (var item in items)
            {
                view.DataContainers.Add(BuildContainer(templateContainer, item));
            }

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
