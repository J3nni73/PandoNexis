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

        public GenericDataViewService(PageService pageService)
        {
            _pageService = pageService;
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

            var genericButtons = new List<GenericButton>();

            //var genericButton = new GenericButton
            //{
            //    FieldId = ContactFormConstants.SavePerson,
            //    ButtonText = "Klicka här så gör vi ngt coolt",
            //    EndPointMethod = "test",
            //    FieldTooltipMessage = "testar knapp",
            //    HideButton = false,
            //    UseConfirmation = false,
            //    ClassName = "generic-data-view__btn-green"
            //};

            //var genericButton2 = new GenericButton
            //{
            //    FieldId = ContactFormConstants.RegisterMeOrganization,
            //    ButtonText = "Exportera",
            //    EndPointMethod = "test2",
            //    FieldTooltipMessage = "testar knapp2",
            //    HideButton = false,
            //    UseConfirmation = false,
            //    ClassName = "generic-data-view__btn-orange"
            //};

            //genericButtons.Add(genericButton);
            //genericButtons.Add(genericButton2);
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
    }

}
