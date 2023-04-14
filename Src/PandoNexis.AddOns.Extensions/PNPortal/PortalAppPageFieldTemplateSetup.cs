using Litium.Accelerator.Constants;
using Litium.FieldFramework;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNPortalPage;

namespace PandoNexis.AddOns.Extensions.PNPortalAppPage
{
    internal class PortalAppPageFieldTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetPageField(PortalPageFieldTemplateConstants.PortalAppPage, "General", SystemFieldDefinitionConstants.Name),
                GetPageField(PortalPageFieldTemplateConstants.PortalAppPage, "General", SystemFieldDefinitionConstants.Url),
                GetPageField(PortalPageFieldTemplateConstants.PortalAppPage, "Contents", PageFieldNameConstants.Title),
                GetPageField(PortalPageFieldTemplateConstants.PortalAppPage, "Contents", PageFieldNameConstants.Introduction),
                GetPageField(PortalPageFieldTemplateConstants.PortalAppPage, "Contents", PageFieldNameConstants.Text),
                GetPageField(PortalPageFieldTemplateConstants.PortalAppPage, "List", PageFieldNameConstants.Links),
                GetPageField(PortalPageFieldTemplateConstants.PortalAppPage, "Image", PageFieldNameConstants.Image),
                GetPageField(PortalPageFieldTemplateConstants.PortalAppPage, "Image", PageFieldNameConstants.AlternativeImageDescription),
                GetPageField(PortalPageFieldTemplateConstants.PortalAppPage, "FileList", PageFieldNameConstants.Files),

               
 
            };
            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new PageFieldTemplate(PortalPageFieldTemplateConstants.PortalAppPage)
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
