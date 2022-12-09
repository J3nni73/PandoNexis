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
using Litium.Accelerator.ViewModels.Article;
using PandoNexis.Accelerator.Extensions.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNCollectionPage
{
    public class CollectionPageFilter
    {
        public string Title { get; set; }
        public List<string> Values { get; set; }
    }
}

