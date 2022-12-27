using Litium.Websites;
using Litium.FieldFramework;
using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;

namespace PandoNexis.AddOns.Extensions.PNMediaCatalog.Definitions.Pages
{
    internal class MediaCatalogPageTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {

                GetPageField(Constants.PageTemplateNameConstants.MediaCatalog,  "General",SystemFieldDefinitionConstants.Name),
                GetPageField(Constants.PageTemplateNameConstants.MediaCatalog,  "General",SystemFieldDefinitionConstants.Url),

                GetPageField(Constants.PageTemplateNameConstants.MediaCatalog,  "Contents",PageFieldNameConstants.Title),
                GetPageField(Constants.PageTemplateNameConstants.MediaCatalog,  "Contents",PageFieldNameConstants.Introduction),
                GetPageField(Constants.PageTemplateNameConstants.MediaCatalog,  "Contents",PageFieldNameConstants.Text),

                GetPageField(Constants.PageTemplateNameConstants.MediaCatalog,  "List",PageFieldNameConstants.Links),
                GetPageField(Constants.PageTemplateNameConstants.MediaCatalog,  "Image",PageFieldNameConstants.Image),
                GetPageField(Constants.PageTemplateNameConstants.MediaCatalog,  "Image",PageFieldNameConstants.AlternativeImageDescription),
                GetPageField(Constants.PageTemplateNameConstants.MediaCatalog,  "MediaFolder",PageFieldNameConstants.AlternativeImageDescription),

                GetPageField(Constants.PageTemplateNameConstants.MediaCatalog,  "MediaFolder",Constants.PageFieldNameConstants.MediaCatalogPointer),
                GetPageField(Constants.PageTemplateNameConstants.MediaCatalog,  "MediaFolder",Constants.PageFieldNameConstants.AlternativeFirstCatalogName),
                GetPageField(Constants.PageTemplateNameConstants.MediaCatalog,  "MediaFolder",Constants.PageFieldNameConstants.AlternativeFolderView),

            };
            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new PageFieldTemplate(Constants.PageTemplateNameConstants.MediaCatalog)
            {
                IndexThePage = true,
                TemplatePath = "",
                FieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false,
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "Contents",
                            Collapsed = false,
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "List",
                            Collapsed = false,
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "Image",
                            Collapsed = false
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "MediaFolder",
                            Collapsed = false,
                        }
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
                        }
                    },
            };
            return template;
        }
    }
}

