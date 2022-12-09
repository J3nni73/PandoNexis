using System;
using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Extensions;
using Litium.FieldFramework;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Websites;
using Litium.Websites;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Extensions
{
    public static class PageModelExtensions
    {
        /// <summary>
        /// Get all ancestor pages of current page include itself
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
       
        /// <summary>
        /// Check if page has "ProductList" page type
        /// </summary>
        /// <param name="pageModel">The page model</param>
        /// <returns></returns>
        public static bool IsProductListPageType(this PageModel pageModel)
        {
            return pageModel.GetPageType() == PageTemplateNameConstants.ProductList;
        }

        /// <summary>
        /// Check if page has "ProductList" page type
        /// </summary>
        /// <param name="pageModel">The page model</param>
        /// <returns></returns>
        public static bool IsGenericGridViewPageType(this PageModel pageModel)
        {
            return pageModel.GetPageType() == PNGenericGridView.Constants.PageTemplateNameConstants.GenericGridView;
        }
    }
}
