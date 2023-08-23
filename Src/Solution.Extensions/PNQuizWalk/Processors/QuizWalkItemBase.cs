using Litium.Accelerator.Utilities;
using Litium.Customers;
using Litium.Runtime.DependencyInjection;
using Litium.Web;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Processors;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;
using Solution.Extensions.PNQuizWalk.Constants;
using Solution.Extensions.PNQuizWalk.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNQuizWalk.Processors
{
    [Service(ServiceType = typeof(QuizWalkItemBase), Lifetime = DependencyLifetime.Transient, NamedService = true)]
    public abstract class QuizWalkItemBase : IGenericDataViewProcessor
    {
        private readonly GenericDataViewService _genericDataViewService;
        private readonly PersonService _personService;
        private readonly PersonStorage _personStorage;
        public Guid _currentPageSystemId { get; set; }
        protected QuizWalkItemBase(GenericDataViewService genericDataViewService, 
                                    PersonService personService, 
                                    PersonStorage personStorage)
        {
            _genericDataViewService = genericDataViewService;
            _personService = personService;
            _personStorage = personStorage;
        }

        public abstract Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data);

        public Task<object> GetDataForm(string data)
        {
            throw new NotImplementedException();
        }

        public virtual Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            throw new NotImplementedException();
        }

        public GenericDataViewSettings GetDataViewSettings(Guid pageSystemId)
        {
            return _genericDataViewService.GetDataViewSettings(pageSystemId);
        }

        public GenericDataContainer GetFields(string templateId)
        {
            var result = new GenericDataContainer();
            switch (templateId)
            {
                case QuizWalkConstants.QuizWalkAdmin:
                    result.Fields.Add(GetIdField(true));
                    result.Fields.Add(GetQuestion(true));
                    result.Fields.Add(GetAnswer(true));
                    break;
                case QuizWalkConstants.QuizWalkView:
                    result.Fields.Add(GetQuestion(false));
                    result.Fields.Add(GetFieldForAnswer(true));
                    break;
                case QuizWalkConstants.QuizWalkChat:
                    result.Fields.Add(GetOrganizaton());
                    result.Fields.Add(GetPerson());
                    result.Fields.Add(GetChat());
                    result.Fields.Add(GetCreatedDateTime());
                    break;
            }

            return result;
        }

        private GenericDataField GetAnswer(bool isEditable = false)
        {
            var field = new GenericDataField();
            field.FieldId = QuizWalkConstants.Answer;
            field.FieldName = QuizWalkConstants.Answer;
            field.FieldType = DataFieldTypes.TextAreaDGType;
            field.Settings.Editable = isEditable;
            return field;
        }
        private GenericDataField GetFieldForAnswer(bool isEditable = false)
        {
            var field = new GenericDataField();
            field.FieldId = QuizWalkConstants.FieldForAnswer;
            field.FieldName = QuizWalkConstants.FieldForAnswer;
            field.FieldType = DataFieldTypes.StringDGType;
            field.Settings.Editable = isEditable;
            return field;
        }

        private GenericDataField GetQuestion(bool isEditable = false)
        {
            var field = new GenericDataField();
            field.FieldId = QuizWalkConstants.Question;
            field.FieldName = QuizWalkConstants.Question;
            field.FieldType = DataFieldTypes.TextAreaDGType;
            field.Settings.Editable = isEditable;

            return field;
        }

        private GenericDataField GetIdField(bool isEditable = false)
        {
            var field = new GenericDataField();
            field.FieldId = QuizWalkConstants.Id;
            field.FieldName = QuizWalkConstants.Id;
            field.FieldType = DataFieldTypes.StringDGType;
            field.Settings.Editable = isEditable;

            return field;
        }
        private GenericDataField GetChat(bool isEditable = false)
        {
            var field = new GenericDataField();
            field.FieldId = QuizWalkConstants.Chat;
            field.FieldName = "Här kan du skriva ett meddelande till dina lagkamrater, enbart dom i laget kan läsa";
            field.FieldType = DataFieldTypes.TextAreaDGType;
            field.Settings.Editable = isEditable;
            field.Settings.PlaceholderText = "Skriv ditt inlägg här, glöm inte att posta inlägget";

            return field;
        }
        private GenericDataField GetPerson()
        {
            var field = new GenericDataField();
            field.FieldId = QuizWalkConstants.Person;
            field.FieldName = QuizWalkConstants.Person;
            field.FieldType = DataFieldTypes.StringDGType;

            return field;
        }
        private GenericDataField GetOrganizaton()
        {
            var field = new GenericDataField();
            field.FieldId = QuizWalkConstants.Organization;
            field.FieldName = "Lag";
            field.FieldType = DataFieldTypes.StringDGType;

            return field;
        }
        private GenericDataField GetCreatedDateTime()
        {
            var field = new GenericDataField();
            field.FieldId = DatabaseConstants.CreatedDateTime;
            field.FieldName = "Skapa datum och tid";
            field.FieldType = DataFieldTypes.StringDGType;

            return field;
        }

        public Task<object> GetGridViewForExport(string data)
        {
            throw new NotImplementedException();
        }

        public Task<object> HandleFormData(string data)
        {
            throw new NotImplementedException();
        }

        public Task<GenericDataContainer> UpdateField(GenericDataField fieldData)
        {
            throw new NotImplementedException();
        }

        internal string GetValue(string fieldId, QuizWalkItem item)
        {
            switch (fieldId)
            {
                case QuizWalkConstants.Answer:
                    return item.Answer;
                case QuizWalkConstants.Question:
                    return item.Question;
                case QuizWalkConstants.Id:
                    return item.Id;
            }
            return string.Empty;
        }
        internal string GetValue(string fieldId, QuizWalkChatItem item)
        {
            switch (fieldId)
            {
                case QuizWalkConstants.Chat:
                    return item.Chat;
                case QuizWalkConstants.Person:
                    var person = _personService.Get(item.PersonSystemId);
                    if (person != null)
                    {
                        if (person.Fields.TryGetValue(RegisterMeConstants.UserName, out var value))
                            return value?.ToString()??string.Empty;
                    }
                    return string.Empty;
                case DatabaseConstants.CreatedDateTime:
                    return item.CreatedDateTime.ToString();
                case QuizWalkConstants.Organization:
                    return _personStorage.CurrentSelectedOrganization.Name;
            }
            return string.Empty;
        }
        internal void SetValue(string fieldId, object value, ref QuizWalkItem item)
        {
            switch (fieldId)
            {
                case QuizWalkConstants.Answer:
                    item.Answer = value.ToString();
                    break;
                case QuizWalkConstants.Question:
                    item.Question = value.ToString();
                    break;
                case QuizWalkConstants.Id:
                    item.Id = value.ToString();
                    break;
            }
        }
        internal void SetValue(string fieldId, object value, ref QuizWalkChatItem item)
        {
            switch (fieldId)
            {
                case QuizWalkConstants.Chat:
                    item.Chat = value.ToString();
                    break;
            }
        }
    }
}
