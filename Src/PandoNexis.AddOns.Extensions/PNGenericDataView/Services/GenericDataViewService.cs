using Litium.Runtime.DependencyInjection;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;

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

            settings.DisplayTypes = page.Fields.GetValue<IList<string>>(PageFieldNameConstants.DisplayTypes)?.ToList()??new List<string>();
            settings.ColumnsInsideContainerSmall =  page.Fields.GetValue<int>(PageFieldNameConstants.ColumnsInsideContainerSmall);
            settings.ColumnsInsideContainerMedium  = page.Fields.GetValue<int>(PageFieldNameConstants.ColumnsInsideContainerMedium);
            settings.ColumnsInsideContainerLarge = page.Fields.GetValue<int>(PageFieldNameConstants.ColumnsInsideContainerLarge);
            settings.ColumnsWithContainersSmall = page.Fields.GetValue<int>(PageFieldNameConstants.ColumnsWithContainersSmall);
            settings.ColumnsWithContainersMedium = page.Fields.GetValue<int>(PageFieldNameConstants.ColumnsWithContainersMedium);
            settings.ColumnsWithContainersLarge = page.Fields.GetValue<int>(PageFieldNameConstants.ColumnsWithContainersLarge);

            return settings;
        }
    }
}
