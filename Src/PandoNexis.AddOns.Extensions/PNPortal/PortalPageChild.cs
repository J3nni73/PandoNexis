﻿using AutoMapper;
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
using Litium.Accelerator.ViewModels.Article;
using PandoNexis.Accelerator.Extensions.ViewModels;
using PandoNexis.Accelerator.Extensions.ViewModel.Media;
using System.Drawing;
using PandoNexis.Accelerator.Extensions;

namespace PandoNexis.AddOns.Extensions.PNPortalPage
{
    public class PortalPageChild : IAutoMapperConfiguration
    {
        public string Title { get; set; }
        public string Introduction { get; set; }
        public SimpleImageModel Image { get; set; }
        public ExtendedLinkViewModel Button { get; set; }
        public string FilterValue1 { get; set; }
        public string FilterValue2 { get; set; }
        public string FilterValue3 { get; set; }


        [Litium.Owin.UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<PageModel, PortalPageChild>()
                .ForMember(x => x.Title, m => m.MapFrom(child => string.IsNullOrEmpty(child.GetValue<string>(PortalPageFieldNameConstants.PortalPageTitle)) ? child.GetValue<string>(PageFieldNameConstants.Title) : child.GetValue<string>(PortalPageFieldNameConstants.PortalPageTitle)))
                .ForMember(x => x.Introduction, m => m.MapFrom(child => string.IsNullOrEmpty(child.GetValue<string>(PortalPageFieldNameConstants.PortalPageDescription)) ? child.GetValue<string>(PageFieldNameConstants.Introduction) : child.GetValue<string>(PortalPageFieldNameConstants.PortalPageDescription)))
                .ForMember(x => x.Image, m => m.MapFrom<ImageModelResolver>())
                .ForMember(x => x.FilterValue1, m => m.MapFromField(PortalPageFieldNameConstants.PortalFilterField1Value))
                .ForMember(x => x.FilterValue2, m => m.MapFromField(PortalPageFieldNameConstants.PortalFilterField2Value))
                .ForMember(x => x.FilterValue3, m => m.MapFromField(PortalPageFieldNameConstants.PortalFilterField3Value))
                ;
        }

        [Litium.Owin.UsedImplicitly]
        protected class ImageModelResolver : IValueResolver<PageModel, PortalPageChild, SimpleImageModel>
        {
            public SimpleImageModel Resolve(PageModel source, PortalPageChild PortalPageChildViewModel, SimpleImageModel destMember, ResolutionContext context)            
            {
                
                var imageModel = source.GetValue<Guid>(PageFieldNameConstants.Image).MapTo<ImageModel>();
                if (imageModel is not null)
                {
                    var imageScaleConstant = 1024/imageModel.Dimension.Width;
                    var simpleImageModel = new SimpleImageModel
                    {
                        Title = imageModel.Alt,
                        Width = imageModel.Dimension.Width * imageScaleConstant,
                        Height = imageModel.Dimension.Height * imageScaleConstant,
                        ImageUrl = imageModel?.GetUrlToImage(Size.Empty, new Size(1024, 550))?.Url,
                        ThumbnailUrl = imageModel?.GetUrlToImage(Size.Empty, new Size(180, 160))?.Url,
                    };
                   
                    var altImage = source.GetValue<string>(PageFieldNameConstants.AlternativeImageDescription);
                    if (!string.IsNullOrEmpty(altImage))
                    {
                        imageModel.Title = altImage;
                    }

                    return simpleImageModel;
                }

                return null;
            }
        }

    }
}