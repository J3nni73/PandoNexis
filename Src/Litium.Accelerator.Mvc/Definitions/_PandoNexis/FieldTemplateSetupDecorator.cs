using System;
using System.Collections.Generic;
using System.Linq;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using Litium.Accelerator.Mvc.Controllers._PandoNexis.ArticleWithoutLeftMeny;
using Litium.Accelerator.Mvc.Controllers.Blocks;
using Litium.Accelerator.Mvc.Controllers.PandoNexis.Blocks;
using Litium.FieldFramework;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using BlockTemplateNameConstants = PandoNexis.Accelerator.Extensions.Constants.BlockTemplateNameConstants;
using PNFieldTemplateConstants = PandoNexis.Accelerator.Extensions.Constants.PageFieldTemplateConstants;

namespace Litium.Accelerator.Mvc.Definitions.PandoNexis
{
    [ServiceDecorator(typeof(FieldTemplateSetup))]
    public class FieldTemplateSetupDecorator : FieldTemplateSetup
    {
        private readonly FieldTemplateSetup _parent;
        private readonly IDictionary<(Type areaType, string id), (Type controllerType, string action)> _controllerMapping = new Dictionary<(Type areaType, string id), (Type controllerType, string action)>
        {
            // EXAMPLES
            [(typeof(Websites.WebsiteArea), PNFieldTemplateConstants.ArticleWithoutLeftMenu)] = (typeof(ArticleWithoutLeftMenyController), nameof(ArticleWithoutLeftMenyController.Index)),
            [(typeof(Blocks.BlockArea), BlockTemplateNameConstants.TextBlock)] = (typeof(TextBlockController), nameof(TextBlockController.Invoke)),

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
