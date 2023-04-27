using Litium.FieldFramework;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PageTemplateNameConstants = PandoNexis.AddOns.Extensions.PNGenericDataView.Constants.PageTemplateNameConstants;
using PageFieldNameConstants = PandoNexis.AddOns.Extensions.PNGenericDataView.Constants.PageFieldNameConstants;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Definitions
{
    internal class GPageTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetPageField(PageTemplateNameConstants.GenericDataView, "General",SystemFieldDefinitionConstants.Name),
                GetPageField(PageTemplateNameConstants.GenericDataView, "General",SystemFieldDefinitionConstants.Url),
                GetPageField(PageTemplateNameConstants.GenericDataView, "General",SystemFieldDefinitionConstants.Title),
                GetPageField(PageTemplateNameConstants.GenericDataView, "General",Litium.Accelerator.Constants.PageFieldNameConstants.Text),
                GetPageField(PageTemplateNameConstants.GenericDataView, "General",Litium.Accelerator.Constants.PageFieldNameConstants.Introduction),
                GetPageField(PageTemplateNameConstants.GenericDataView, "DataSources",PageFieldNameConstants.DataArea),
                GetPageField(PageTemplateNameConstants.GenericDataView, "DataSources",PageFieldNameConstants.AreaSource),
                GetPageField(PageTemplateNameConstants.GenericDataView, "Settings",PageFieldNameConstants.DisplayTypes),
                GetPageField(PageTemplateNameConstants.GenericDataView, "Settings",PageFieldNameConstants.HasMegaMenu),
                GetPageField(PageTemplateNameConstants.GenericDataView, "Settings",PageFieldNameConstants.EnableDropZone),

                GetPageField(PageTemplateNameConstants.GenericDataView, "Settings",PageFieldNameConstants.ColumnsInsideContainerSmall),
                GetPageField(PageTemplateNameConstants.GenericDataView, "Settings",PageFieldNameConstants.ColumnsInsideContainerMedium),
                GetPageField(PageTemplateNameConstants.GenericDataView, "Settings",PageFieldNameConstants.ColumnsInsideContainerLarge),

                GetPageField(PageTemplateNameConstants.GenericDataView, "Settings",PageFieldNameConstants.ColumnsWithContainersSmall),
                GetPageField(PageTemplateNameConstants.GenericDataView, "Settings",PageFieldNameConstants.ColumnsWithContainersMedium),
                GetPageField(PageTemplateNameConstants.GenericDataView, "Settings",PageFieldNameConstants.ColumnsWithContainersLarge),
                GetPageField(PageTemplateNameConstants.GenericDataView, "Settings", PageFieldNameConstants.AlignContainers),
            };

            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new PageFieldTemplate(PageTemplateNameConstants.GenericDataView)
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