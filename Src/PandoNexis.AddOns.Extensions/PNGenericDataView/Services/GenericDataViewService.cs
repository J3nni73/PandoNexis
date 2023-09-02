using Litium.Accelerator.Routing;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using Litium.Web;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Services
{
    [Service(ServiceType = typeof(GenericDataViewService))]
    public class GenericDataViewService
    {
        private readonly PageService _pageService;
        private readonly RequestModelAccessor _requestModelAccessor;

        public GenericDataViewService(PageService pageService,
                                      RequestModelAccessor requestModelAccessor)
        {
            _pageService = pageService;
            _requestModelAccessor = requestModelAccessor;
        }

        public GenericDataViewSettings GetDataViewSettings(Guid pageSystemId)
        {
            var settings = new GenericDataViewSettings();
            var page = _pageService.Get(pageSystemId);

            settings.DisplayTypes = page.Fields.GetValue<IList<string>>(DataViewFieldNameConstants.DisplayTypes)?.ToList() ?? new List<string>();
            settings.ColumnsInsideContainerSmall = page.Fields.GetValue<int>(DataViewFieldNameConstants.ColumnsInsideContainerSmall);
            settings.ColumnsInsideContainerMedium = page.Fields.GetValue<int>(DataViewFieldNameConstants.ColumnsInsideContainerMedium);
            settings.ColumnsInsideContainerLarge = page.Fields.GetValue<int>(DataViewFieldNameConstants.ColumnsInsideContainerLarge);
            settings.ColumnsWithContainersSmall = page.Fields.GetValue<int>(DataViewFieldNameConstants.ColumnsWithContainersSmall);
            settings.ColumnsWithContainersMedium = page.Fields.GetValue<int>(DataViewFieldNameConstants.ColumnsWithContainersMedium);
            settings.ColumnsWithContainersLarge = page.Fields.GetValue<int>(DataViewFieldNameConstants.ColumnsWithContainersLarge);
            settings.AlignContainers = page.Fields.GetValue<string>(DataViewFieldNameConstants.AlignContainers);

            //settings.DataViewMaxWidth = "90vw";

            // Response Example
            //settings.ResponseActions.UpdateTopLevel = true; 
            //settings.ResponseActions.CloseModal = true;
            //settings.ResponseActions.HardReloadPage = true;

            var genericButtons = new List<GenericButton>();

            settings.DataViewButtons = genericButtons;

            return settings;
        }
        public string GetDataViewFieldType(string dataType)
        {
            switch (dataType)
            {
                case SystemFieldTypeConstants.Boolean:
                    return DataFieldTypes.BooleanDGType;
                case SystemFieldTypeConstants.DateTime:
                    return DataFieldTypes.DateTimeDGType;
                case SystemFieldTypeConstants.MultirowText:
                    return DataFieldTypes.TextAreaDGType;
                case SystemFieldTypeConstants.Text:
                    return DataFieldTypes.StringDGType;
            }

            return "string";
        }
        public Guid GetEntitySystemIdFromQuerystring(string querystring)
        {
            if (Guid.TryParse(querystring.Replace("?entitySystemId=", ""), out Guid systemId))
            {
                return systemId;
            }
            else
            {
                return Guid.Empty;
            }
        }
        public List<ValidationRule> GetValidationRules(string fieldDefinitionId, List<string> requiredFields)
        {
            var result = new List<ValidationRule>();
            if (requiredFields?.Contains(fieldDefinitionId) ?? false)
            {
                result.Add(new ValidationRule()
                {
                    Rule = ValidationRuleConstants.IsRequired,
                    ErrorMessage = "addons.genericdataview.validationrulemessage.isrequired".AsWebsiteText(_requestModelAccessor.RequestModel.WebsiteModel.Website)
                });
            }
            if (fieldDefinitionId == SystemFieldDefinitionConstants.Email)
            {
                result.Add(new ValidationRule()
                {
                    Rule = ValidationRuleConstants.Email,
                    ErrorMessage = "addons.genericdataview.validationrulemessage.email".AsWebsiteText(_requestModelAccessor.RequestModel.WebsiteModel.Website)
                });
            }

            return result;
        }
    }

}
