using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.Definitions.Products
{
    internal class ProductsFieldTemplateSetup : FieldTemplateSetup
    {
        private readonly DisplayTemplateService _displayTemplateService;

        public ProductsFieldTemplateSetup(DisplayTemplateService displayTemplateService)
        {
            _displayTemplateService = displayTemplateService;
        }
        public override IEnumerable<FieldTemplate> GetTemplates()
        {
            var categoryDisplayTemplateId = _displayTemplateService.Get<CategoryDisplayTemplate>("Category")?.SystemId ?? Guid.Empty;
            var productDisplayTemplateId = _displayTemplateService.Get<ProductDisplayTemplate>("Product")?.SystemId ?? Guid.Empty;
            var productWithVariantListDisplayTemplateId = _displayTemplateService.Get<ProductDisplayTemplate>("ProductWithVariantList")?.SystemId ?? Guid.Empty;

            if (categoryDisplayTemplateId == Guid.Empty || productDisplayTemplateId == Guid.Empty || productWithVariantListDisplayTemplateId == Guid.Empty)
            {
                return Enumerable.Empty<FieldTemplate>();
            }

            var fieldTemplates = new FieldTemplate[]
            {
                new CategoryFieldTemplate("Category", categoryDisplayTemplateId)
                {
                    CategoryFieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn }
                            },
                            Fields =
                            {
                                "_name",
                                "_description",
                                "_url",
                                "_seoTitle",
                                "_seoDescription",
                                "AcceleratorFilterFields",
                                ProductFieldNameConstants.OrganizationsPointer
                            }
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionEn,
                            Collapsed = false,
                            Localizations=
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionEn }

                            },
                            Fields =
                            {
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.Description,
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.DescriptionExtended
                            }
                        }
                    }
                },
                new ProductFieldTemplate("ProductWithOneVariant", productDisplayTemplateId)
                {
                    ProductFieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn }
                            },
                            Fields =
                            {
                                "_name",
                                ProductFieldNameConstants.OrganizationsPointer
                            }
                        }
                    },
                    VariantFieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn }
                            },
                            Fields =
                            {
                                "_name",
                                "_description",
                                "_url",
                                "_seoTitle",
                                "_seoDescription"
                            }
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = "Product information",
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductInformationSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductInformationEn }
                            },
                            Fields =
                            {
                                "News",
                                "Brand",
                                "Color",
                                "Size",
                                "ProductSheet",
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.ProductType,
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.InventoryStatus
                            }
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductSpecificationEn,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductSpecificationSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductSpecificationEn }
                            },
                            Fields =
                            {
                                "Specification",
                                "Weight"
                            }
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionEn,
                            Collapsed = false,
                            Localizations=
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionEn }

                            },
                            Fields =
                            {
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.Description,
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.DescriptionExtended
                            }
                        }
                    }
                },
                new ProductFieldTemplate("ProductWithVariants", productDisplayTemplateId)
                {
                    ProductFieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn }
                            },
                            Fields =
                            {
                                "_name",
                                "_description",
                                ProductFieldNameConstants.OrganizationsPointer
                            }
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductInformationEn,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductInformationSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductInformationEn }
                            },
                            Fields =
                            {
                                "News",
                                "Brand",
                                "ProductSheet",
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.ProductType,
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.InventoryStatus
                            }
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionEn,
                            Collapsed = false,
                            Localizations=
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionEn }

                            },
                            Fields =
                            {
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.Description,
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.DescriptionExtended
                            }
                        }
                    },
                    VariantFieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn }
                            },
                            Fields =
                            {
                                "_name",
                                "_description",
                                "_url",
                                "_seoTitle",
                                "_seoDescription",
                            }
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductInformationEn,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductInformationSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductInformationEn }
                            },
                            Fields =
                            {
                                "Color",
                                "Size"
                            }
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionEn,
                            Collapsed = false,
                            Localizations=
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionEn }

                            },
                            Fields =
                            {
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.Description,
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.DescriptionExtended
                            }
                        }
                    }
                },
                new ProductFieldTemplate("ProductWithVariantsList", productWithVariantListDisplayTemplateId)
                {
                    ProductFieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn }
                            },
                            Fields =
                            {
                                "Brand",
                                "_name",
                                "_description",
                                "_url",
                                "_seoTitle",
                                "_seoDescription",
                                ProductFieldNameConstants.OrganizationsPointer
                            }
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductSpecificationEn,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductSpecificationSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.ProductSpecificationEn }
                            },
                            Fields =
                            {
                                "Specification",
                                "ProductSheet"
                            }
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionEn,
                            Collapsed = false,
                            Localizations=
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionEn }

                            },
                            Fields =
                            {
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.Description,
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.DescriptionExtended
                            }
                        }
                    },
                    VariantFieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.GeneralEn }
                            },
                            Fields =
                            {
                                "_name",
                                "Color",
                                "Size"
                            }
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionEn,
                            Collapsed = false,
                            Localizations=
                            {
                                ["sv-SE"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionSv },
                                ["en-US"] = { Name = PandoNexis.Accelerator.Extensions.Constants.FieldFrameworkConstants.DescriptionEn }

                            },
                            Fields =
                            {
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.Description,
                                PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.DescriptionExtended
                            }
                        }
                    }
                }
            };
            return fieldTemplates;
        }
    }
}
