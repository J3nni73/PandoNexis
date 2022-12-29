using Litium.Accelerator.Constants;
using Litium.FieldFramework;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using System.Collections.Generic;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Definitions.Pages
{
    internal class GenericGridViewPageTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetPageField(Constants.PageTemplateNameConstants.GenericGridView,  "General",SystemFieldDefinitionConstants.Name),
                GetPageField(Constants.PageTemplateNameConstants.GenericGridView,  "General",SystemFieldDefinitionConstants.Url),
                GetPageField(Constants.PageTemplateNameConstants.GenericGridView,  "General",SystemFieldDefinitionConstants.Title),
                GetPageField(Constants.PageTemplateNameConstants.GenericGridView,  "General",PageFieldNameConstants.Text),
                GetPageField(Constants.PageTemplateNameConstants.GenericGridView,  "General",PageFieldNameConstants.Introduction),
                GetPageField(Constants.PageTemplateNameConstants.GenericGridView,  "General",Constants.PageFieldNameConstants.ImageText),
                GetPageField(Constants.PageTemplateNameConstants.GenericGridView,  "General",Constants.PageFieldNameConstants.HasMegaMenu),
                GetPageField(Constants.PageTemplateNameConstants.GenericGridView,  "DataSources",Constants.PageFieldNameConstants.DataSource),
                GetPageField(Constants.PageTemplateNameConstants.GenericGridView,  "DataSources",PageFieldNameConstants.PageSize),
                GetPageField(Constants.PageTemplateNameConstants.GenericGridView,  "DataSources",Constants.PageFieldNameConstants.EnableDropZone),
                GetWebsiteField("AcceleratorWebsite", "Quota", PNGenericGridView.Constants.PageFieldNameConstants.QuotaCategory),
            };



            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new PageFieldTemplate(Constants.PageTemplateNameConstants.GenericGridView)
            {
                IndexThePage = true,
                FieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false,
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "DataSources",
                            Collapsed = false,
                        }
                    },
            };
            return template;
        }
    }
}