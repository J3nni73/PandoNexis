using Litium.Accelerator.Constants;
using Litium.Connect.Erp.Import;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers
{
    [Service(ServiceType = typeof(FieldTemplateHelper))]
    public abstract class FieldTemplateHelper
    {
        public abstract IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges();
        public abstract FieldTemplate GetFieldTemplateNewTemplate();
        public FieldTemplateChanges GetWebsiteField(string template, string fieldgroup, string field)
        {
            return new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.WebsiteFieldTemplate,
                DisplayTemplate = "",
                TemplateName = template,
                FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                FieldGroupName = fieldgroup,
                Field = field
            };
        }
        public FieldTemplateChanges GetChannelField(string template, string fieldgroup, string field)
        {
            return new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.ChannelFieldTemplate,
                DisplayTemplate = "",
                TemplateName = template,
                FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                FieldGroupName = fieldgroup,
                Field = field
            };
        }
        public FieldTemplateChanges GetPageField(string template, string fieldgroup, string field)
        {
            return new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.PageFieldTemplate,
                DisplayTemplate = "",
                TemplateName = template,
                FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                FieldGroupName = fieldgroup,
                Field = field
            };
        }
        public FieldTemplateChanges GetBlockField(string template, string fieldgroup, string field)
        {
            return new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                DisplayTemplate = "",
                TemplateName = template,
                FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                FieldGroupName = fieldgroup,
                Field = field
            };
        }
        public FieldTemplateChanges GetFileField(string template, string fieldgroup, string field)
        {
            return new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.FileFieldTemplate,
                DisplayTemplate = "",
                TemplateName = template,
                FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                FieldGroupName = fieldgroup,
                Field = field
            };

        }
        public FieldTemplateChanges GetOrganizationField(string template, string fieldgroup, string field)
        {

            return new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.OrganizationFieldTemplate,
                DisplayTemplate = "",
                TemplateName = template,
                FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                FieldGroupName = fieldgroup,
                Field = field
            };

        }
        public FieldTemplateChanges GetProductField(string template, string productFieldGroup, string fieldgroup, string field)
        {
            return new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.ProductFieldTemplate,
                DisplayTemplate = "",
                TemplateName = template,
                FieldGroupType = productFieldGroup,
                FieldGroupName = fieldgroup,
                Field = field
            };

        }
        public List<FieldTemplateChanges> GetChangesForBlockExtendedCTA(string blockId)
        {
            var templateChanges = new List<FieldTemplateChanges>
                {
                new FieldTemplateChanges()
                {
                    TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                    DisplayTemplate = "",
                    TemplateName = blockId,
                    FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                    FieldGroupName = BlockFieldGroupNameConstants.CTA,
                    Field = BlockFieldNameConstants.ExtendedLinkText
                },
                new FieldTemplateChanges()
                {
                    TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                    DisplayTemplate = "",
                    TemplateName = blockId,
                    FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                    FieldGroupName = BlockFieldGroupNameConstants.CTA,
                    Field = BlockFieldNameConstants.ExtendedLinkToPage
                },
                new FieldTemplateChanges()
                {
                    TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                    DisplayTemplate = "",
                    TemplateName = blockId,
                    FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                    FieldGroupName = BlockFieldGroupNameConstants.CTA,
                    Field = BlockFieldNameConstants.ExtendedLinkToCategory
                },
                new FieldTemplateChanges()
                {
                    TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                    DisplayTemplate = "",
                    TemplateName = blockId,
                    FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                    FieldGroupName = BlockFieldGroupNameConstants.CTA,
                    Field = BlockFieldNameConstants.ExtendedLinkToProduct
                },
                new FieldTemplateChanges()
                {
                    TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                    DisplayTemplate = "",
                    TemplateName = blockId,
                    FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                    FieldGroupName = BlockFieldGroupNameConstants.CTA,
                    Field = BlockFieldNameConstants.ExtendedLinkToFile
                },
                new FieldTemplateChanges()
                {
                    TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                    DisplayTemplate = "",
                    TemplateName = blockId,
                    FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                    FieldGroupName = BlockFieldGroupNameConstants.CTA,
                    Field = BlockFieldNameConstants.ExtendedLinkToExternalUrl
                },
                new FieldTemplateChanges()
                {
                    TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                    DisplayTemplate = "",
                    TemplateName = blockId,
                    FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                    FieldGroupName = BlockFieldGroupNameConstants.CTA,
                    Field = BlockFieldNameConstants.ExtendedClass
                },
                new FieldTemplateChanges()
                {
                    TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                    DisplayTemplate = "",
                    TemplateName = blockId,
                    FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                    FieldGroupName = BlockFieldGroupNameConstants.CTA,
                    Field = BlockFieldNameConstants.ExtendedButtonSubText
                },
            };

            return templateChanges;

        }
    }
}
