﻿using AutoMapper;
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
using Litium.Accelerator.ViewModels.Article;


namespace PandoNexis.AddOns.Extensions.PNCollectionPage
{
    public class CollectionPageViewModel : IAutoMapperConfiguration, IViewModel
    {
        public Guid PageId { get; set; }
        public Dictionary<string, List<BlockModel>> Blocks { get; set; }
        public string Introduction { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ImageModel Image { get; set; }


        [Litium.Owin.UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg) => cfg.CreateMap<PageModel, CollectionPageViewModel>()
               .ForMember(x => x.Title, m => m.MapFromField(PageFieldNameConstants.Title))
               .ForMember(x => x.Introduction, m => m.MapFromField(PageFieldNameConstants.Introduction))
               .ForMember(x => x.Text, m => m.MapFromField(PageFieldNameConstants.Text))
               .ForMember(x => x.Image, m => m.MapFrom<ImageModelResolver>())
               ;

        [Litium.Owin.UsedImplicitly]
        protected class ImageModelResolver : IValueResolver<PageModel, CollectionPageViewModel, ImageModel>
        {
            public ImageModel Resolve(PageModel source, CollectionPageViewModel collectionPageViewModel, ImageModel destMember, ResolutionContext context)
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

