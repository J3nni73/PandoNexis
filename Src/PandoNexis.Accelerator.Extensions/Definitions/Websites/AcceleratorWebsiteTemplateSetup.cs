using Litium.Websites;
using Litium.FieldFramework;
using Litium.Accelerator.Constants;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;

namespace PandoNexis.Accelerator.Extensions.Definitions.Websites
{
    internal class AcceleratorWebsiteTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetWebsiteField("AcceleratorWebsite", "General", SystemFieldDefinitionConstants.Name),
                GetWebsiteField("AcceleratorWebsite",  "Logotype", AcceleratorWebsiteFieldNameConstants.LogotypeMain),
                GetWebsiteField("AcceleratorWebsite",  "Logotype", AcceleratorWebsiteFieldNameConstants.LogotypeIcon),

                GetWebsiteField("AcceleratorWebsite",  "Header", AcceleratorWebsiteFieldNameConstants.HeaderLayout),
                GetWebsiteField("AcceleratorWebsite",  "Header", AcceleratorWebsiteFieldNameConstants.CheckoutPage),
                GetWebsiteField("AcceleratorWebsite",  "Header", AcceleratorWebsiteFieldNameConstants.MyPagesPage),
                GetWebsiteField("AcceleratorWebsite",  "Header", AcceleratorWebsiteFieldNameConstants.AdditionalHeaderLinks),

                GetWebsiteField("AcceleratorWebsite",  "Search", AcceleratorWebsiteFieldNameConstants.SearchResultPage),

                GetWebsiteField("AcceleratorWebsite",  "Footer", AcceleratorWebsiteFieldNameConstants.Footer),
                GetWebsiteField("AcceleratorWebsite",  "Navigation", AcceleratorWebsiteFieldNameConstants.NavigationTheme),
                GetWebsiteField("AcceleratorWebsite",  "LeftNavigation", AcceleratorWebsiteFieldNameConstants.InFirstLevelCategories),
                GetWebsiteField("AcceleratorWebsite",  "LeftNavigation", AcceleratorWebsiteFieldNameConstants.InBrandPages),
                GetWebsiteField("AcceleratorWebsite",  "LeftNavigation", AcceleratorWebsiteFieldNameConstants.InProductListPages),
                GetWebsiteField("AcceleratorWebsite",  "LeftNavigation", AcceleratorWebsiteFieldNameConstants.InArticlePages),

                GetWebsiteField("AcceleratorWebsite",  "ProductLists", AcceleratorWebsiteFieldNameConstants.ProductsPerPage),
                GetWebsiteField("AcceleratorWebsite",  "ProductLists", AcceleratorWebsiteFieldNameConstants.ShowBuyButton),
                GetWebsiteField("AcceleratorWebsite",  "ProductLists", AcceleratorWebsiteFieldNameConstants.ShowQuantityFieldProductList),

                GetWebsiteField("AcceleratorWebsite",  "Filters", AcceleratorWebsiteFieldNameConstants.FiltersOrdering),
                GetWebsiteField("AcceleratorWebsite",  "Filters", AcceleratorWebsiteFieldNameConstants.FiltersIndexedBySearchEngines),

                GetWebsiteField("AcceleratorWebsite",  "Checkout", AcceleratorWebsiteFieldNameConstants.CheckoutMode),
                GetWebsiteField("AcceleratorWebsite",  "Customers", AcceleratorWebsiteFieldNameConstants.AllowCustomersEditLogin),
                GetWebsiteField("AcceleratorWebsite",  "Emails", AcceleratorWebsiteFieldNameConstants.SenderEmailAddress),
                GetWebsiteField("AcceleratorWebsite",  "OrderConfirmationPage", AcceleratorWebsiteFieldNameConstants.OrderConfirmationPage),
            };


            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new WebsiteFieldTemplate("AcceleratorWebsite")
            {
                FieldGroups = new[]
                {
                    new FieldTemplateFieldGroup()
                    {
                        Id = "General",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "Logotype",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "Header",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "Search",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "Footer",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "Navigation",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "LeftNavigation",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "ProductLists",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "ProductPage",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "Filters",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "Checkout",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "Customers",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "Emails",
                        Collapsed = false
                    },
                    new FieldTemplateFieldGroup()
                    {
                        Id = "OrderConfirmationPage",
                        Collapsed = false
                    }
                }
            };

            return template;
        }
    }
}
