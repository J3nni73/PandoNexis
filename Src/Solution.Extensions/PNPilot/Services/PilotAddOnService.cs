﻿using Litium.FieldFramework;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Security;

namespace Solution.Extensions.PNPilot.Services
{
    [Service(ServiceType = typeof(PilotAddOnService))]
    public class PilotAddOnService
    {
        private readonly VariantService _variantService;
        private readonly BaseProductService _baseProductService;
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly SecurityContextService _securityContextService;

        public PilotAddOnService(FieldTemplateService fieldTemplateService,
                                    BaseProductService baseProductService,
                                    VariantService variantService,
                                    SecurityContextService securityContextService)
        {
            _fieldTemplateService = fieldTemplateService;
            _baseProductService = baseProductService;
            _variantService = variantService;
            _securityContextService = securityContextService;
        }

        public bool AddOnExists(string AddOnId)
        {
            var variant = _variantService.Get(AddOnId);

            if (variant == null)
                return false;

            return true;
        }
        public bool RegisterAddOn(string AddOnId)
        {
            var variant = _variantService.Get(AddOnId);

            if (variant != null)
                return false;

            var baseProduct = _baseProductService.Get("bp_" + AddOnId);
            if (baseProduct == null)
            {
                var fieldTemplate = _fieldTemplateService.Get<ProductFieldTemplate>("ProductWithVariants");
                baseProduct = new BaseProduct("bp_" + AddOnId, fieldTemplate.SystemId);
                using (_securityContextService.ActAsSystem("Pilot"))
                {
                    _baseProductService.Create(baseProduct);
                }
            }

            variant = new Litium.Products.Variant(AddOnId, baseProduct.SystemId);
            using (_securityContextService.ActAsSystem("Pilot"))
            {
                _variantService.Create(variant);
            }

        return true;
        }
    }
}
