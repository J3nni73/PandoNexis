using System;
using System.Collections.Generic;
using System.Linq;
using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using PageTemplateNameConstants = PandoNexis.AddOns.Extensions.Constants.PageTemplateNameConstants;
using Litium.Accelerator.Mvc.Controllers.Addons.MediaCatalog;
using Litium.Accelerator.Mvc.Controllers.Addons.CollectionPage;
using Litium.Accelerator.Mvc.Controllers.Addons.PortalPage;
using Litium.Accelerator.Mvc.Controllers._Addons.GenericGridView;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.Accelerator.Mvc.Controllers.Addons.Blocks;
using PandoNexis.AddOns.Extensions.PNCollectionPage;
using PandoNexis.AddOns.Extensions.PNPortalPage;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericGridView;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Constants;

namespace Litium.Accelerator.Mvc.Definitions.Addons
{
    [ServiceDecorator(typeof(FieldTemplateSetup))]
    public class FieldTemplateSetupDecorator : FieldTemplateSetup
    {
        private readonly FieldTemplateSetup _parent;
        private readonly IDictionary<(Type areaType, string id), (Type controllerType, string action)> _controllerMapping = new Dictionary<(Type areaType, string id), (Type controllerType, string action)>
        {
            [(typeof(Websites.WebsiteArea), CollectionPageFieldTemplateConstants.CollectionPage)] = (typeof(CollectionPageController), nameof(CollectionPageController.Index)),
            [(typeof(Websites.WebsiteArea), PortalPageFieldTemplateConstants.PortalPage)] = (typeof(PortalPageController), nameof(PortalPageController.Index)),
            [(typeof(Websites.WebsiteArea), PageTemplateNameConstants.MediaCatalog)] = (typeof(MediaCatalogController), nameof(MediaCatalogController.Index)),
            [(typeof(Websites.WebsiteArea), GenericGridView_PageTemplateNameConstants.GenericGridView)] = (typeof(GenericGridViewController), nameof(GenericGridViewController.Index)),
            [(typeof(Blocks.BlockArea), BlockTemplateNameConstants.InspirationalBlock)] = (typeof(InspirationalBlockController), nameof(InspirationalBlockController.Invoke)),
            [(typeof(Blocks.BlockArea), BlockTemplateNameConstants.ColumnBlock)] = (typeof(ColumnBlockController), nameof(ColumnBlockController.Invoke)),
            [(typeof(Blocks.BlockArea), BlockTemplateNameConstants.InfoTileBlock)] = (typeof(InfoTileBlockController), nameof(InfoTileBlockController.Invoke)),
            [(typeof(Blocks.BlockArea), BlockTemplateNameConstants.ImageStatsBlock)] = (typeof(ImageStatsBlockController), nameof(ImageStatsBlockController.Invoke)),
            [(typeof(Blocks.BlockArea), BlockTemplateNameConstants.HeroBlock)] = (typeof(HeroBlockController), nameof(HeroBlockController.Invoke)),
            [(typeof(Blocks.BlockArea), BlockTemplateNameConstants.UspBlock)] = (typeof(UspBlockController), nameof(UspBlockController.Invoke))
        };

        public FieldTemplateSetupDecorator(FieldTemplateSetup parent)
        {
            _parent = parent;
        }

        public override IEnumerable<FieldTemplate> GetTemplates()
        {
            return _parent.GetTemplates().Select(x =>
            {
                var prop = x.GetType().GetProperty("TemplatePath");
                if (prop != null && _controllerMapping.TryGetValue((x.AreaType, x.Id), out var map))
                {
                    prop.SetValue(x, "~/MVC:" + map.controllerType.MapTo<string>() + ":" + map.action);
                }
                return x;
            });
        }
    }
}
