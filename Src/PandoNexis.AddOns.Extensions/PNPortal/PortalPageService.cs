using Litium.Accelerator.Routing;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Web;
using Litium.Web.Models.Websites;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Builders;
using PandoNexis.Accelerator.Extensions.ViewModels;
using System.Globalization;

namespace PandoNexis.AddOns.Extensions.PNPortalPage
{
    [Service(ServiceType = typeof(PortalPageService), Lifetime = DependencyLifetime.Singleton)]
    public class PortalPageService
    {
        private readonly PageService _pageService;
        private readonly UrlService _urlService;
        private readonly RequestModelAccessor _requestModelAccessor;
        public readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;

        public PortalPageService(PageService pageService,
                                                UrlService urlService,
                                                RequestModelAccessor requestModelAccessor,
                                                ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder)
        {
            _pageService = pageService;
            _urlService = urlService;
            _requestModelAccessor = requestModelAccessor;
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
        }

        public PortalPageData BuildPortalPageData(Guid parentPageSystemId)
        {
            var result = new PortalPageData();
            if (parentPageSystemId == Guid.Empty)
                return result;

            var parentPageModel = _pageService.Get(parentPageSystemId)?.MapTo<PageModel>();
            if (parentPageModel == null)
                return result;

            var childPageModels = _pageService.GetChildPages(parentPageSystemId)?
                                    .Where(p => p.IsActive(_requestModelAccessor.RequestModel.ChannelModel.SystemId))?
                                    .Select(i => i.MapTo<PageModel>())?
                                    .ToList();

            if (childPageModels == null)
                return result;

            result.Filters = GetFilters(parentPageModel, childPageModels);
            result.Children = GetChildren(parentPageModel, childPageModels);
            return result;
        }

        private List<PortalPageFilter> GetFilters(PageModel parentPageModel, List<PageModel> childPageModels)
        {
            var filters = new List<PortalPageFilter>();
            var filter = GetFilter(PortalPageFieldNameConstants.PortalFilterField1Name, PortalPageFieldNameConstants.PortalFilterField1Value, parentPageModel, childPageModels);
            if (filter != null)
                filters.Add(filter);
            filter = GetFilter(PortalPageFieldNameConstants.PortalFilterField2Name, PortalPageFieldNameConstants.PortalFilterField2Value, parentPageModel, childPageModels);
            if (filter != null)
                filters.Add(filter);
            filter = GetFilter(PortalPageFieldNameConstants.PortalFilterField3Name, PortalPageFieldNameConstants.PortalFilterField3Value, parentPageModel, childPageModels);
            if (filter != null)
                filters.Add(filter);

            return filters;
        }

        private PortalPageFilter GetFilter(string filterNameField, string filterValueField, PageModel parentPageModel, List<PageModel> childPageModels)
        {
            var filterName = parentPageModel.Fields.GetValue<string>(filterNameField, CultureInfo.CurrentCulture);
            if (!string.IsNullOrEmpty(filterName))
            {
                var values = new List<string>();

                foreach (var childPageModel in childPageModels)
                {
                    var value = childPageModel.Fields.GetValue<string>(filterValueField, CultureInfo.CurrentCulture);
                    if (!string.IsNullOrEmpty(value))
                        values.Add(value);
                }
                if (values.Count > 0)
                {
                    return new PortalPageFilter { Title = filterName, Values = values };
                }
            }
            return null;
        }

        private List<PortalPageChild> GetChildren(PageModel parentPageModel, List<PageModel> childPageModels)
        {
            var children = new List<PortalPageChild>();

            foreach (var childPageModel in childPageModels)
            {
                var PortalPageChild = childPageModel.MapTo<PortalPageChild>();
                if (PortalPageChild == null)
                {
                    continue;
                }
                PortalPageChild.Button = BuildPortalPageLink(parentPageModel, childPageModel);
                children.Add(PortalPageChild);
            }
            return children;
        }
        private ExtendedLinkViewModel BuildPortalPageLink(PageModel parentPageModel, PageModel childPageModel)
        {
            var text = string.IsNullOrEmpty(childPageModel.Fields.GetValue<string>(PortalPageFieldNameConstants.PortalPageChildPageButtonText, CultureInfo.CurrentCulture)) ?
                                            parentPageModel.Fields.GetValue<string>(PortalPageFieldNameConstants.PortalPageParentPageButtonText, CultureInfo.CurrentCulture) :
                                            childPageModel.Fields.GetValue<string>(PortalPageFieldNameConstants.PortalPageChildPageButtonText, CultureInfo.CurrentCulture);

            var linkToPage = childPageModel.SystemId;
            var linkToCategory = Guid.Empty;
            var linkToProduct = Guid.Empty;
            var linkToFile = Guid.Empty;
            var linkToExternalUrl = string.Empty;
            var extendedClass = string.Empty;
            var buttonSubText = string.Empty;

            return _extendedLinkViewModelBuilder.Build(text, linkToPage, linkToCategory, linkToProduct, linkToFile, linkToExternalUrl, extendedClass, buttonSubText);
        }
    }
}
