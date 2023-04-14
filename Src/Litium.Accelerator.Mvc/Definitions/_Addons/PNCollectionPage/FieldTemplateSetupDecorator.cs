using System;
using System.Collections.Generic;
using System.Linq;
using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Accelerator.Mvc.Controllers._Addons.CollectionPage;
using PandoNexis.AddOns.Extensions.PNCollectionPage;

namespace Litium.Accelerator.Mvc.Definitions.Addons
{
    [ServiceDecorator(typeof(FieldTemplateSetup))]
    public class CollectionPageFieldTemplateSetupDecorator : FieldTemplateSetup
    {
        private readonly FieldTemplateSetup _parent;
        private readonly IDictionary<(Type areaType, string id), (Type controllerType, string action)> _controllerMapping = new Dictionary<(Type areaType, string id), (Type controllerType, string action)>
        {
            [(typeof(Websites.WebsiteArea), CollectionPageFieldTemplateConstants.CollectionPage)] = (typeof(CollectionPageController), nameof(CollectionPageController.Index)),
        };

        public CollectionPageFieldTemplateSetupDecorator(FieldTemplateSetup parent)
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
