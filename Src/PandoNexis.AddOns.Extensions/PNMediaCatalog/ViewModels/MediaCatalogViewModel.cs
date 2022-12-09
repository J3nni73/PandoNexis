using AutoMapper;
using JetBrains.Annotations;
using Litium.Accelerator.Constants;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models;
using Litium.Web.Models.Blocks;
using Litium.Web.Models.Websites;
using System;
using System.Collections.Generic;
using System.Linq;
using Litium.Accelerator.Builders;
using Litium.Accelerator.Extensions;
using Litium.FieldFramework.FieldTypes;
using Litium.Media;
using Litium.FieldFramework;
using Litium.Accelerator.Utilities;
using PandoNexis.Accelerator.Extensions;

namespace PandoNexis.AddOns.Extensions.PNMediaCatalog.ViewModels
{
    public class MediaCatalogViewModel : IAutoMapperConfiguration, IViewModel
    {
        public Dictionary<string, List<BlockModel>> Blocks { get; set; }
        public string Introduction { get; set; }
        public string Title { get; set; }
        public EditorString Text { get; set; }
        public ImageModel Image { get; set; }
        public string AlternativeFirstCatalogName { get; set; }
        public Guid MediaCatalogPointer { get; set; }
        public bool AlternativeFolderView { get; set; }



        [Litium.Owin.UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<PageModel, MediaCatalogViewModel>()
               .ForMember(x => x.Title, m => m.MapFromField(PageFieldNameConstants.Title))
               .ForMember(x => x.Introduction, m => m.MapFromField(PageFieldNameConstants.Introduction))
               .ForMember(x => x.Text, m => m.MapFrom(articlePage => articlePage.GetValue<string>(PageFieldNameConstants.Text)))
               .ForMember(x => x.Image, m => m.MapFrom<ImageModelResolver>())
               .ForMember(x => x.AlternativeFirstCatalogName, m => m.MapFrom(mediaCatalog => mediaCatalog.GetValue<string>(Constants.PageFieldNameConstants.AlternativeFirstCatalogName)))
               //.ForMember(x => x.MediaCatalogPointer, m => m.MapFrom(mediaCatalog => mediaCatalog.GetValue<PointerItem>(Constants.PageFieldNameConstants.MediaCatalogPointer).EntitySystemId.MapTo<Guid>()))
               .ForMember(x => x.MediaCatalogPointer, m => m.MapFrom<MediaFolderResolver>())
               .ForMember(x => x.AlternativeFolderView, m => m.MapFromField(Constants.PageFieldNameConstants.AlternativeFolderView));
        }
        [Litium.Owin.UsedImplicitly]


        protected class MediaFolderResolver : IValueResolver<PageModel, MediaCatalogViewModel, Guid>
        {
            private readonly FileService _fileService;
            public MediaFolderResolver(FileService fileService)
            {
                _fileService = fileService;
            }
            public Guid Resolve(PageModel source, MediaCatalogViewModel mediaCatalogViewModel, Guid destMember, ResolutionContext context)
            {
                var pointerItem = source.GetValue<Guid>(Constants.PageFieldNameConstants.MediaCatalogPointer).MapTo<PointerItem>();
                if (pointerItem is not null)
                {
                    var file = _fileService.Get(pointerItem.EntitySystemId);

                    return file.FolderSystemId;
                }

                return Guid.Empty;
            }
        }

        [Litium.Owin.UsedImplicitly]
        protected class ImageModelResolver : IValueResolver<PageModel, MediaCatalogViewModel, ImageModel>
        {
            public ImageModel Resolve(PageModel source, MediaCatalogViewModel mediaCatalogViewModel, ImageModel destMember, ResolutionContext context)
            {
                var imageModel = source.GetValue<Guid>(PageFieldNameConstants.Image).MapTo<ImageModel>();
                if (imageModel is not null)
                {
                    var altImage = source.GetValue<string>(PageFieldNameConstants.AlternativeImageDescription);
                    if (!string.IsNullOrEmpty(altImage))
                    {
                        imageModel.Alt = altImage;
                    }

                    return imageModel;
                }

                return null;
            }
        }
    }
}

