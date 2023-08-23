using Litium.Accelerator;
using Litium.Accelerator.Builders;
using Litium.Accelerator.Routing;
using Litium.FieldFramework;
using Litium.Media;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Web;
using Litium.Web.Models;
using Litium.Web.Models.Blocks;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Constants;
using PandoNexis.Accelerator.Extensions.ViewModels;
using System.Globalization;

namespace PandoNexis.Accelerator.Extensions.Builders
{
    [Service(ServiceType = typeof(ExtendedLinkViewModelBuilder), Lifetime = DependencyLifetime.Singleton)]
    public class ExtendedLinkViewModelBuilder : IViewModelBuilder<ExtendedLinkViewModel>
    {
        private readonly PageService _pageService;
        private readonly CategoryService _categoryService;
        private readonly BaseProductService _baseProductService;
        private readonly VariantService _variantService;
        private readonly FileService _fileService;
        private readonly UrlService _urlService;
        private readonly RequestModelAccessor _requestModelAccessor;

        public ExtendedLinkViewModelBuilder(PageService pageService,
                                         CategoryService categoryService,
                                         BaseProductService baseProductService,
                                         VariantService variantService,
                                         FileService fileService,
                                         UrlService urlService,
                                         RequestModelAccessor requestModelAccessor)
        {
            _pageService = pageService;
            _categoryService = categoryService;
            _baseProductService = baseProductService;
            _variantService = variantService;
            _fileService = fileService;
            _urlService = urlService;
            _requestModelAccessor = requestModelAccessor;
        }
        public ExtendedLinkViewModel Build(BlockModel block)
        {

            return Build(
                        block.Fields.GetValue<string>(BlockFieldNameConstants.ExtendedLinkText, CultureInfo.CurrentCulture),
                        block.Fields.GetValue<Guid>(BlockFieldNameConstants.ExtendedLinkToPage),
                        block.Fields.GetValue<Guid>(BlockFieldNameConstants.ExtendedLinkToCategory),
                        block.Fields.GetValue<Guid>(BlockFieldNameConstants.ExtendedLinkToProduct),
                        block.Fields.GetValue<Guid>(BlockFieldNameConstants.ExtendedLinkToFile),
                        block.Fields.GetValue<string>(BlockFieldNameConstants.ExtendedLinkToExternalUrl, CultureInfo.CurrentCulture),
                        block.Fields.GetValue<string>(BlockFieldNameConstants.ExtendedClass),
                        block.Fields.GetValue<string>(BlockFieldNameConstants.ExtendedButtonSubText)
                        );

        }
        public ExtendedLinkViewModel Build(MultiFieldItem item)
        {

            return Build(
                        item.Fields.GetValue<string>(BlockFieldNameConstants.ExtendedLinkText, CultureInfo.CurrentCulture),
                        item.Fields.GetValue<Guid>(BlockFieldNameConstants.ExtendedLinkToPage),
                        item.Fields.GetValue<Guid>(BlockFieldNameConstants.ExtendedLinkToCategory),
                        item.Fields.GetValue<Guid>(BlockFieldNameConstants.ExtendedLinkToProduct),
                        item.Fields.GetValue<Guid>(BlockFieldNameConstants.ExtendedLinkToFile),
                        item.Fields.GetValue<string>(BlockFieldNameConstants.ExtendedLinkToExternalUrl, CultureInfo.CurrentCulture),
                        item.Fields.GetValue<string>(BlockFieldNameConstants.ExtendedClass),
                        item.Fields.GetValue<string>(BlockFieldNameConstants.ExtendedButtonSubText)
                        );

        }
        public ExtendedLinkViewModel Build(string linkText,
                                    Guid pageSystemId,
                                    Guid categorySystemId,
                                    Guid productSystemId,
                                    Guid fileSystemId,
                                    string externalUrl,
                                    string extendedClass,
                                    string extendedButtonSubText)
        {
            if (string.IsNullOrEmpty(extendedClass))
            {
                extendedClass = "primaryButton";
            }
            var model = new ExtendedLinkViewModel()
            {
                LinkText = linkText,
                Class = extendedClass,
                ButtonSubText = extendedButtonSubText,
            };
            var channelSystemId = _requestModelAccessor.RequestModel.ChannelModel.SystemId;
            if (pageSystemId != Guid.Empty)
            {
                var page = _pageService.Get(pageSystemId);
                if (page != null && page.IsActive(channelSystemId))
                {
                    var args = new PageUrlArgs(channelSystemId);
                    model.Href = _urlService.GetUrl(page, args);
                    return model;
                }
            }
            if (categorySystemId != Guid.Empty)
            {
                var category = _categoryService.Get(categorySystemId);
                if (category != null && category.IsPublished(channelSystemId))
                {
                    var args = new CategoryUrlArgs(channelSystemId);
                    model.Href = _urlService.GetUrl(category, args);
                }
            }
            if (productSystemId != Guid.Empty)
            {
                var variant = _variantService.Get(productSystemId);
                if (variant != null && variant.IsPublished())
                {
                    var args = new ProductUrlArgs(channelSystemId);
                    model.Href = _urlService.GetUrl(variant, args);
                }
                else
                {
                    var baseProduct = _baseProductService.Get(productSystemId);
                    if (baseProduct != null)
                    {
                        var args = new ProductUrlArgs(channelSystemId);
                        model.Href = _urlService.GetUrl(baseProduct, args);

                    }
                }
            }
            if (fileSystemId != Guid.Empty)
            {
                var file = _fileService.Get(fileSystemId);
                if (file != null)
                {
                    model.Href = file.MapTo<FileModel>().Url;
                    model.Target = "_blanc";
                    return model;
                }
            }
            if (!string.IsNullOrEmpty(externalUrl))
            {
                if (!externalUrl.StartsWith("http"))
                {
                    externalUrl = "https://" + externalUrl;
                }
                model.Href = externalUrl;
                model.Target = "_blanc";
                return model;
            }

            return model;
        }
    }
}
