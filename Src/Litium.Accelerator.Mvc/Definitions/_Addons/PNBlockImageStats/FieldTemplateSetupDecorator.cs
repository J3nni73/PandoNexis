﻿using System;
using System.Collections.Generic;
using System.Linq;
using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.Accelerator.Mvc.Controllers._Addons.PnBlockImageStats;

namespace Litium.Accelerator.Mvc.Definitions.Addons
{
    [ServiceDecorator(typeof(FieldTemplateSetup))]
    public class ImageStatsBlockFieldTemplateSetupDecorator : FieldTemplateSetup
    {
        private readonly FieldTemplateSetup _parent;
        private readonly IDictionary<(Type areaType, string id), (Type controllerType, string action)> _controllerMapping = new Dictionary<(Type areaType, string id), (Type controllerType, string action)>
        {
            [(typeof(Blocks.BlockArea), BlockTemplateNameConstants.ImageStatsBlock)] = (typeof(ImageStatsBlockController), nameof(ImageStatsBlockController.Invoke)),
          };

        public ImageStatsBlockFieldTemplateSetupDecorator(FieldTemplateSetup parent)
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
