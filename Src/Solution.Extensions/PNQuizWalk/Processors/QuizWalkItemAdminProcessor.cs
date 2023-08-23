using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Customers;
using Litium.Runtime.DependencyInjection;
using Litium.Web.Mvc;
using Litium.Websites;
using Newtonsoft.Json;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
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
    [Service(Name = "QuizWalkAdmin")]
    public class QuizWalkItemAdminProcessor : QuizWalkItemBase
    {
        private readonly QuizWalkService _quizWalkService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly GenericButtonService _genericButtonService;

        public QuizWalkItemAdminProcessor(QuizWalkService quizWalkService, 
                                            GenericDataViewService genericDataViewService, 
                                            RequestModelAccessor requestModelAccessor, 
                                            GenericButtonService genericButtonService, 
                                            PersonService personService,
                                            PersonStorage personStorage) : base(genericDataViewService, personService, personStorage)
        {
            _quizWalkService = quizWalkService;
            _requestModelAccessor = requestModelAccessor;
            _genericButtonService = genericButtonService;
        }

        public async override Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            var responseItem = JsonConvert.DeserializeObject<GenericDataViewResponse>(data);
            if (responseItem == null) return null;
            var item = _quizWalkService.Get(Guid.Parse(responseItem.EntitySystemId));
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
            view.Settings.DataViewButtons.Add(_genericButtonService.GetButton(_requestModelAccessor.RequestModel.WebsiteModel.Website, QuizWalkConstants.QuizWalkButtons, QuizWalkConstants.QuizWalkAddNew, QuizWalkConstants.QuizWalkButtonsNames, Guid.NewGuid()));
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
            result.Settings.PostContainerPageSystemId = _currentPageSystemId;

            foreach (var field in result.Fields)
            {
                field.EntitySystemId = item.SystemId.ToString();
                field.FieldValue = GetValue(field.FieldId, item);
            }

            return result;

        }
    }
}
