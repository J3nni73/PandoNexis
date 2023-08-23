using Litium.Accelerator.Constants;
using Litium.Blocks;
using Litium.FieldFramework;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PNFieldTemplateConstants = PandoNexis.Accelerator.Extensions.Constants.PageFieldTemplateConstants;

namespace PandoNexis.AddOns.Extensions.PNCollectionPage
{
    internal class CollectionPageFieldTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage, "General", SystemFieldDefinitionConstants.Name),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage, "General", SystemFieldDefinitionConstants.Url),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage, "Contents", PageFieldNameConstants.Title),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage, "Contents", PageFieldNameConstants.Introduction),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage, "Contents", PageFieldNameConstants.Text),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage, "List", PageFieldNameConstants.Links),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage, "Image", PageFieldNameConstants.Image),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage, "Image", PageFieldNameConstants.AlternativeImageDescription),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage, "FileList", PageFieldNameConstants.Files),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageParentFieldGroup,CollectionPageFieldNameConstants.CollectionPageUseFilter),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageParentFieldGroup,CollectionPageFieldNameConstants.CollectionFilterField1Name),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageParentFieldGroup,CollectionPageFieldNameConstants.CollectionFilterField2Name),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageParentFieldGroup,CollectionPageFieldNameConstants.CollectionFilterField3Name),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageParentFieldGroup,CollectionPageFieldNameConstants.CollectionPageParentPageButtonText),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageParentFieldGroup,CollectionPageFieldNameConstants.CollectionPageLink),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageParentFieldGroup,CollectionPageFieldNameConstants.CollectionPageLinkText),


                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionPageTitle),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionPageDescription),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionPageImage),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionPageChildPageButtonText),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionFilterField1Value),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionFilterField2Value),
                GetPageField(CollectionPageFieldTemplateConstants.CollectionPage,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionFilterField3Value),

                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionPageTitle),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionPageDescription),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionPageImage),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionPageChildPageButtonText),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionFilterField1Value),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionFilterField2Value),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionFilterField3Value),

                GetPageField(PageTemplateNameConstants.Article,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionPageTitle),
                GetPageField(PageTemplateNameConstants.Article,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionPageDescription),
                GetPageField(PageTemplateNameConstants.Article,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionPageImage),
                GetPageField(PageTemplateNameConstants.Article,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionPageChildPageButtonText),
                GetPageField(PageTemplateNameConstants.Article,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionFilterField1Value),
                GetPageField(PageTemplateNameConstants.Article,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionFilterField2Value),
                GetPageField(PageTemplateNameConstants.Article,CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,CollectionPageFieldNameConstants.CollectionFilterField3Value),



            };
            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new PageFieldTemplate(CollectionPageFieldTemplateConstants.CollectionPage)
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
                            Id = CollectionPageFieldTemplateConstants.CollectionPageParentFieldGroup,
                            Collapsed = false,
                        },
                         new FieldTemplateFieldGroup()
                        {
                            Id = CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,
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
