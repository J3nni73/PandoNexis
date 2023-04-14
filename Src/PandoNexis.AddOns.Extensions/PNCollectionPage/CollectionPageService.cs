using Litium.Accelerator.Routing;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Web;
using Litium.Web.Models.Websites;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Builders;
using PandoNexis.Accelerator.Extensions.ViewModels;
using System.Globalization;

namespace PandoNexis.AddOns.Extensions.PNCollectionPage
{
    [Service(ServiceType = typeof(CollectionPageService), Lifetime = DependencyLifetime.Singleton)]
    public class CollectionPageService
    {
        private readonly PageService _pageService;
        private readonly UrlService _urlService;
        private readonly RequestModelAccessor _requestModelAccessor;
        public readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;

        public CollectionPageService(PageService pageService,
                                                UrlService urlService,
                                                RequestModelAccessor requestModelAccessor,
                                                ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder)
        {
            _pageService = pageService;
            _urlService = urlService;
            _requestModelAccessor = requestModelAccessor;
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
        }

        public CollectionPageData BuildCollectionPageData(Guid parentPageSystemId)
        {
            var result = new CollectionPageData();
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

        private List<CollectionPageFilter> GetFilters(PageModel parentPageModel, List<PageModel> childPageModels)
        {
            var filters = new List<CollectionPageFilter>();
            var filter = GetFilter(CollectionPageFieldNameConstants.CollectionFilterField1Name, CollectionPageFieldNameConstants.CollectionFilterField1Value, parentPageModel, childPageModels);
            if (filter != null)
                filters.Add(filter);
            filter = GetFilter(CollectionPageFieldNameConstants.CollectionFilterField2Name, CollectionPageFieldNameConstants.CollectionFilterField2Value, parentPageModel, childPageModels);
            if (filter != null)
                filters.Add(filter);
            filter = GetFilter(CollectionPageFieldNameConstants.CollectionFilterField3Name, CollectionPageFieldNameConstants.CollectionFilterField3Value, parentPageModel, childPageModels);
            if (filter != null)
                filters.Add(filter);

            return filters;
        }

        private CollectionPageFilter GetFilter(string filterNameField, string filterValueField, PageModel parentPageModel, List<PageModel> childPageModels)
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
                    return new CollectionPageFilter { Title = filterName, Values = values };
                }
            }
            return null;
        }

        private List<CollectionPageChild> GetChildren(PageModel parentPageModel, List<PageModel> childPageModels)
        {
            var children = new List<CollectionPageChild>();

            foreach (var childPageModel in childPageModels)
            {
                var collectionPageChild = childPageModel.MapTo<CollectionPageChild>();
                if (collectionPageChild == null)
                {
                    continue;
                }
                collectionPageChild.Button = BuildCollectionPageLink(parentPageModel, childPageModel);
                children.Add(collectionPageChild);
            }
            return children;
        }
        private ExtendedLinkViewModel BuildCollectionPageLink(PageModel parentPageModel, PageModel childPageModel)
        {
            var text = string.IsNullOrEmpty(childPageModel.Fields.GetValue<string>(CollectionPageFieldNameConstants.CollectionPageChildPageButtonText, CultureInfo.CurrentCulture)) ?
                                            parentPageModel.Fields.GetValue<string>(CollectionPageFieldNameConstants.CollectionPageParentPageButtonText, CultureInfo.CurrentCulture) :
                                            childPageModel.Fields.GetValue<string>(CollectionPageFieldNameConstants.CollectionPageChildPageButtonText, CultureInfo.CurrentCulture);

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
