using Litium.Accelerator.Constants;
using Litium.FieldFramework;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;

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
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageAppFieldGroup,PortalPageFieldNameConstants.PortalPageUseFilter),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageAppFieldGroup,PortalPageFieldNameConstants.PortalPageParentPageButtonText),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageAppFieldGroup,PortalPageFieldNameConstants.PortalPageLink),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageAppFieldGroup,PortalPageFieldNameConstants.PortalPageLinkText),


                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageAppFieldGroup,PortalPageFieldNameConstants.PortalPageTitle),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageAppFieldGroup,PortalPageFieldNameConstants.PortalPageDescription),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageAppFieldGroup,PortalPageFieldNameConstants.PortalPageImage),
                GetPageField(PortalPageFieldTemplateConstants.PortalPage,PortalPageFieldTemplateConstants.PortalPageAppFieldGroup,PortalPageFieldNameConstants.PortalPageChildPageButtonText),





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
                            Id = PortalPageFieldTemplateConstants.PortalPageAppFieldGroup,
                            Collapsed = false,
                        },
                         new FieldTemplateFieldGroup()
                        {
                            Id = PortalPageFieldTemplateConstants.PortalPageAppFieldGroup,
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
