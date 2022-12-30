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
                GetPageField(Constants.GenericGridView_PageTemplateNameConstants.GenericGridView,  "General",SystemFieldDefinitionConstants.Name),
                GetPageField(Constants.GenericGridView_PageTemplateNameConstants.GenericGridView,  "General",SystemFieldDefinitionConstants.Url),
                GetPageField(Constants.GenericGridView_PageTemplateNameConstants.GenericGridView,  "General",SystemFieldDefinitionConstants.Title),
                GetPageField(Constants.GenericGridView_PageTemplateNameConstants.GenericGridView,  "General",PageFieldNameConstants.Text),
                GetPageField(Constants.GenericGridView_PageTemplateNameConstants.GenericGridView,  "General",PageFieldNameConstants.Introduction),
                GetPageField(Constants.GenericGridView_PageTemplateNameConstants.GenericGridView,  "General",Constants.GenericGridView_PageFieldNameConstants.ImageText),
                GetPageField(Constants.GenericGridView_PageTemplateNameConstants.GenericGridView,  "General",Constants.GenericGridView_PageFieldNameConstants.HasMegaMenu),
                GetPageField(Constants.GenericGridView_PageTemplateNameConstants.GenericGridView,  "DataSources",Constants.GenericGridView_PageFieldNameConstants.DataSource),
                GetPageField(Constants.GenericGridView_PageTemplateNameConstants.GenericGridView,  "DataSources",PageFieldNameConstants.PageSize),
                GetPageField(Constants.GenericGridView_PageTemplateNameConstants.GenericGridView,  "DataSources",Constants.GenericGridView_PageFieldNameConstants.EnableDropZone),
                GetWebsiteField("AcceleratorWebsite", "Quota", PNGenericGridView.Constants.GenericGridView_PageFieldNameConstants.QuotaCategory),
            };



            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new PageFieldTemplate(Constants.GenericGridView_PageTemplateNameConstants.GenericGridView)
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