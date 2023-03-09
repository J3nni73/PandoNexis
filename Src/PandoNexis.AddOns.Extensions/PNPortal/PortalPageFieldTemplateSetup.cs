using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PNFieldTemplateConstants = PandoNexis.Accelerator.Extensions.Constants.PageFieldTemplateConstants;

namespace PandoNexis.AddOns.Extensions.PNPortalPage
{
    internal class PortalPageFieldTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetPageField(PortalPageFieldTemplateConstants.PortalPage, "General", SystemFieldDefinitionConstants.Name),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage, "General", SystemFieldDefinitionConstants.Url),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage, "Contents", PageFieldNameConstants.Title),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage, "Contents", PageFieldNameConstants.Introduction),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage, "Contents", PageFieldNameConstants.Text),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage, "List", PageFieldNameConstants.Links),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage, "Image", PageFieldNameConstants.Image),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage, "Image", PageFieldNameConstants.AlternativeImageDescription),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage, "FileList", PageFieldNameConstants.Files),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageParentFieldGroup,PortalPageFieldNameConstants.PortalPageUseFilter),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageParentFieldGroup,PortalPageFieldNameConstants.PortalFilterField1Name),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageParentFieldGroup,PortalPageFieldNameConstants.PortalFilterField2Name),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageParentFieldGroup,PortalPageFieldNameConstants.PortalFilterField3Name),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageParentFieldGroup,PortalPageFieldNameConstants.PortalPageParentPageButtonText),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageParentFieldGroup,PortalPageFieldNameConstants.PortalPageLink),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageParentFieldGroup,PortalPageFieldNameConstants.PortalPageLinkText),


                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalPageTitle),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalPageDescription),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalPageImage),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalPageChildPageButtonText),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalFilterField1Value),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalFilterField2Value),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalFilterField3Value),

                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalPageTitle),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalPageDescription),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalPageImage),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalPageChildPageButtonText),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalFilterField1Value),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalFilterField2Value),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalFilterField3Value),

                GetPageField(PageTemplateNameConstants.Article,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalPageTitle),
                GetPageField(PageTemplateNameConstants.Article,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalPageDescription),
                GetPageField(PageTemplateNameConstants.Article,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalPageImage),
                GetPageField(PageTemplateNameConstants.Article,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalPageChildPageButtonText),
                GetPageField(PageTemplateNameConstants.Article,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalFilterField1Value),
                GetPageField(PageTemplateNameConstants.Article,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalFilterField2Value),
                GetPageField(PageTemplateNameConstants.Article,PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,PortalPageFieldNameConstants.PortalFilterField3Value),



            };
            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new PageFieldTemplate(PortalPageFieldTemplateConstants.PortalPage)
            {
                IndexThePage = true,
                TemplatePath = "",
                FieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "Contents",
                            Collapsed = false,
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "Image",
                            Collapsed = false
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = PortalPageFieldTemplateConstants.PortalPageParentFieldGroup,
                            Collapsed = false,
                        },
                         new FieldTemplateFieldGroup()
                        {
                            Id = PortalPageFieldTemplateConstants.PortalPageChildFieldGroup,
                            Collapsed = false,
                        },
                    },
                Containers = new List<BlockContainerDefinition>
                    {
                        new BlockContainerDefinition()
                        {
                            Id = BlockContainerNameConstant.Header,
                            Name =
                            {
                                ["sv-SE"] = "Huvud",
                                ["en-US"] = "Header",
                            }
                        },
                         new BlockContainerDefinition()
                        {
                            Id = BlockContainerNameConstant.Main,
                            Name =
                            {
                                ["sv-SE"] = "Fot",
                                ["en-US"] = "Footer",
                            }
                        }
                },
            };
            return template;
        }
    }
}
