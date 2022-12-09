using Litium.Accelerator.Constants;
using Litium.FieldFramework;
using Litium.Websites;
using System.Collections.Generic;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Definitions.Pages
{
    internal class GenericGridViewPageTemplateSetup : Litium.Accelerator.Definitions.FieldTemplateSetup
    {
        public override IEnumerable<FieldTemplate> GetTemplates()
        {
            var templates = new List<FieldTemplate>
            {
                new PageFieldTemplate(Constants.PageTemplateNameConstants.GenericGridView)
                {
                    IndexThePage = true,
                    FieldGroups = new []
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false,
                            Fields =
                            {
                               SystemFieldDefinitionConstants.Name,
                               SystemFieldDefinitionConstants.Url,
                               PageFieldNameConstants.Title,
                               PageFieldNameConstants.Text,
                               PageFieldNameConstants.Introduction,
                               Constants.PageFieldNameConstants.ImageText,
                               Constants.PageFieldNameConstants.HasMegaMenu,
                            }
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "DataSources",
                            Collapsed = false,
                            Fields =
                            {
                                Constants.PageFieldNameConstants.DataSource,
                                PageFieldNameConstants.PageSize,
                                Constants.PageFieldNameConstants.EnableDropZone,
                            }
                        }
                    },
                },
            };
            return templates;
        }
    }
}