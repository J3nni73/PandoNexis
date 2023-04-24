using Litium.Websites;
using Litium.FieldFramework;
using Litium.Accelerator.Definitions;
using Litium.Products;
using Litium.Security;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using Litium.Blocks;
using CategoryService = Litium.Blocks.CategoryService;
using Litium.Globalization;
using Litium.Media;
using Litium.Customers;

namespace Solution.Extensions.Definitions
{
    internal class TemplateSetup : FieldTemplateSetup
    {
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly SecurityContextService _securityContextService;
        private readonly CategoryService _categoryService;
        private readonly DisplayTemplateService _displayTemplateService;
        private readonly IEnumerable<FieldTemplateHelper> _fieldTemplateHelper;


        public TemplateSetup(FieldTemplateService fieldTemplateService,
            SecurityContextService securityContextService,
            CategoryService categoryService,
            DisplayTemplateService displayTemplateService,
            IEnumerable<FieldTemplateHelper> fieldTemplateHelper)
        {
            _fieldTemplateService = fieldTemplateService;
            _securityContextService = securityContextService;
            _categoryService = categoryService;
            _displayTemplateService = displayTemplateService;
            _fieldTemplateHelper = fieldTemplateHelper;
        }

        public override IEnumerable<FieldTemplate> GetTemplates()
        {

            var fieldTemplates = new List<FieldTemplate>();
            var templateChanges = new List<FieldTemplateChanges>();
            var newTemplates = new List<FieldTemplate>();

            foreach (var item in _fieldTemplateHelper)
            {
                var changes = item.GetFieldTemplateFieldChanges();
                if (changes != null)
                {
                    templateChanges.AddRange(changes);
                }
                var templates = item.GetFieldTemplateNewTemplate();
                if (templates != null)
                {
                    newTemplates.Add(templates);
                }
            }



            if (templateChanges.Any())
            {
                foreach (var template in templateChanges.GroupBy(i => i.TemplateName))
                {
                    var changes = templateChanges.Where(i => i.TemplateName == template.Key)?.ToList();
                    if (changes != null && changes.Any())
                    {
                        var templateType = template.FirstOrDefault().TemplateType;
                        FieldTemplate? fieldTemplate = null;
                        switch (templateType)
                        {
                            case FieldTemplateHelperConstants.ProductFieldTemplate:
                                fieldTemplate = GetProductFieldTemplate(template.Key, changes, newTemplates);
                                break;
                            case FieldTemplateHelperConstants.CategoryFieldTemplate:
                                fieldTemplate = GetCategoryFieldTemplate(template.Key, changes, newTemplates);
                                break;
                            case FieldTemplateHelperConstants.PageFieldTemplate:
                                fieldTemplate = GetPageFieldTemplate(template.Key, changes, newTemplates);
                                break;
                            case FieldTemplateHelperConstants.BlockFieldTemplate:
                                fieldTemplate = GetBlockFieldTemplate(template.Key, changes, newTemplates);
                                break;
                            case FieldTemplateHelperConstants.ChannelFieldTemplate:
                                fieldTemplate = GetChannelFieldTemplate(template.Key, changes, newTemplates);
                                break;
                            case FieldTemplateHelperConstants.WebsiteFieldTemplate:
                                fieldTemplate = GetWebsiteFieldTemplate(template.Key, changes, newTemplates);
                                break;
                            case FieldTemplateHelperConstants.FileFieldTemplate:
                                fieldTemplate = GetFileFieldTemplate(template.Key, changes, newTemplates);
                                break;
                            case FieldTemplateHelperConstants.OrganizationFieldTemplate:
                                fieldTemplate = GetOrganizationFieldTemplate(template.Key, changes, newTemplates);
                                break;
                        }
                        if (fieldTemplate != null)
                            fieldTemplates.Add(fieldTemplate);
                    }
                }
            }
            return fieldTemplates;
        }
        private FieldTemplate? GetOrganizationFieldTemplate(string templateId, List<FieldTemplateChanges> changes, List<FieldTemplate> newTemplates)
        {
            var groups = changes.GroupBy(i => i.FieldGroupName);
            var customerFieldTemplate = newTemplates.FirstOrDefault(i => i.Id == templateId) as OrganizationFieldTemplate;
            if (customerFieldTemplate == null)
                return null;

            var newcustomerFieldTemplate = new OrganizationFieldTemplate(templateId);
            newcustomerFieldTemplate.FieldGroups = new List<FieldTemplateFieldGroup>();
            foreach (var group in customerFieldTemplate.FieldGroups)
            {
                var newGroup = new FieldTemplateFieldGroup()
                {
                    Id = group.Id,
                    Collapsed = group.Collapsed,
                };
                foreach (var field in group.Fields)
                {
                    newGroup.Fields.Add(field);
                }
                newcustomerFieldTemplate.FieldGroups.Add(newGroup);
            }

            foreach (var change in changes)
            {

                if (newcustomerFieldTemplate.FieldGroups?.FirstOrDefault(i => i.Id == change.FieldGroupName) == null)
                {
                    newcustomerFieldTemplate.FieldGroups.Add(new FieldTemplateFieldGroup()
                    {
                        Id = change.FieldGroupName,
                        Collapsed = false,
                        Localizations =  {
                                            ["sv-SE"] = { Name = change.FieldGroupName },
                                            ["en-US"] = { Name = change.FieldGroupName }
                                         },
                    });
                }

                if (newcustomerFieldTemplate.FieldGroups?.FirstOrDefault(i => i.Id == change.FieldGroupName) != null)
                {
                    newcustomerFieldTemplate.FieldGroups.FirstOrDefault(i => i.Id == change.FieldGroupName)?.Fields.Add(change.Field);
                }
            }

            return newcustomerFieldTemplate;
        }
        private FieldTemplate? GetFileFieldTemplate(string templateId, List<FieldTemplateChanges> changes, List<FieldTemplate> newTemplates)
        {
            var groups = changes.GroupBy(i => i.FieldGroupName);
            var fileFieldTemplate = newTemplates.FirstOrDefault(i => i.Id == templateId) as FileFieldTemplate;
            if (fileFieldTemplate == null)
                return null;

            var newFileFieldTemplate = new FileFieldTemplate(templateId);
            newFileFieldTemplate.FieldGroups = new List<FieldTemplateFieldGroup>();
            foreach (var group in fileFieldTemplate.FieldGroups)
            {
                var newGroup = new FieldTemplateFieldGroup()
                {
                    Id = group.Id,
                    Collapsed = group.Collapsed,
                };
                foreach (var field in group.Fields)
                {
                    newGroup.Fields.Add(field);
                }
                newFileFieldTemplate.FieldGroups.Add(newGroup);
            }

            newFileFieldTemplate.FileExtensions = fileFieldTemplate.FileExtensions;
            newFileFieldTemplate.TemplateType = fileFieldTemplate.TemplateType;

            foreach (var change in changes)
            {

                if (newFileFieldTemplate.FieldGroups?.FirstOrDefault(i => i.Id == change.FieldGroupName) == null)
                {
                    newFileFieldTemplate.FieldGroups.Add(new FieldTemplateFieldGroup()
                    {
                        Id = change.FieldGroupName,
                        Collapsed = false,
                        Localizations =  {
                                            ["sv-SE"] = { Name = change.FieldGroupName },
                                            ["en-US"] = { Name = change.FieldGroupName }
                                         },
                    });
                }

                if (newFileFieldTemplate.FieldGroups?.FirstOrDefault(i => i.Id == change.FieldGroupName) != null)
                {
                    newFileFieldTemplate.FieldGroups.FirstOrDefault(i => i.Id == change.FieldGroupName)?.Fields.Add(change.Field);
                }
            }

            return newFileFieldTemplate;
        }

        private FieldTemplate GetProductFieldTemplate(string templateId, List<FieldTemplateChanges> changes, List<FieldTemplate> newTemplates)
        {
            var productFieldTemplate = _fieldTemplateService.Get<ProductFieldTemplate>(templateId);
            if (productFieldTemplate == null)
            {
                productFieldTemplate = newTemplates.FirstOrDefault(i => i.Id == templateId) as ProductFieldTemplate;
            }
            if (productFieldTemplate == null)
                return null;


            var newProductFieldTemplate = new ProductFieldTemplate(productFieldTemplate.Id, productFieldTemplate.DisplayTemplateSystemId);
            newProductFieldTemplate.ProductFieldGroups = new List<FieldTemplateFieldGroup>();
            foreach (var group in productFieldTemplate.ProductFieldGroups)
            {
                var newGroup = new FieldTemplateFieldGroup()
                {
                    Id = group.Id,
                    Collapsed = group.Collapsed,
                };
                foreach (var field in group.Fields)
                {
                    if (!newGroup.Fields.Contains(field))
                        newGroup.Fields.Add(field);
                }
                newProductFieldTemplate.ProductFieldGroups.Add(newGroup);
            }
            newProductFieldTemplate.VariantFieldGroups = new List<FieldTemplateFieldGroup>();
            foreach (var group in productFieldTemplate.VariantFieldGroups)
            {
                var newGroup = new FieldTemplateFieldGroup()
                {
                    Id = group.Id,
                    Collapsed = group.Collapsed,
                };
                foreach (var field in group.Fields)
                {
                    if (!newGroup.Fields.Contains(field))
                        newGroup.Fields.Add(field);
                }
                newProductFieldTemplate.VariantFieldGroups.Add(newGroup);
            }

            foreach (var change in changes)
            {
                switch (change.FieldGroupType)
                {
                    case FieldTemplateHelperConstants.ProductFieldGroups:
                        if (newProductFieldTemplate?.ProductFieldGroups?.FirstOrDefault(i => i.Id == change.FieldGroupName) != null)
                        {
                            newProductFieldTemplate.ProductFieldGroups.FirstOrDefault(i => i.Id == change.FieldGroupName)?.Fields.Add(change.Field);
                        }
                        else
                        {
                            newProductFieldTemplate?.ProductFieldGroups.Add(
                                                                        new FieldTemplateFieldGroup()
                                                                        {
                                                                            Id = change.FieldGroupName,
                                                                            Collapsed = false,
                                                                            Localizations =
                                                                                                {
                                                                                                    ["sv-SE"] = { Name = change.FieldGroupName },
                                                                                                    ["en-US"] = { Name = change.FieldGroupName }
                                                                                                },
                                                                            Fields = { change.Field }
                                                                        });
                        }
                        break;
                    case FieldTemplateHelperConstants.VariantFieldGroups:
                        if (newProductFieldTemplate?.VariantFieldGroups?.FirstOrDefault(i => i.Id == change.FieldGroupName) != null)
                        {
                            if (!newProductFieldTemplate?.VariantFieldGroups?.FirstOrDefault(i => i.Id == change.FieldGroupName)?.Fields?.Contains(change.Field)??false)
                                newProductFieldTemplate.VariantFieldGroups.FirstOrDefault(i => i.Id == change.FieldGroupName)?.Fields.Add(change.Field);
                        }
                        else
                        {
                            newProductFieldTemplate?.VariantFieldGroups.Add(
                                                                        new FieldTemplateFieldGroup()
                                                                        {
                                                                            Id = change.FieldGroupName,
                                                                            Collapsed = false,
                                                                            Localizations =
                                                                                                {
                                                                                                    ["sv-SE"] = { Name = change.FieldGroupName },
                                                                                                    ["en-US"] = { Name = change.FieldGroupName }
                                                                                                },
                                                                            Fields = { change.Field }
                                                                        });
                        }
                        break;
                }

            }
            return newProductFieldTemplate;
        }
        private FieldTemplate GetCategoryFieldTemplate(string templateId, List<FieldTemplateChanges> changes, List<FieldTemplate> newTemplates)
        {
            var categoryFieldTemplate = _fieldTemplateService.Get<CategoryFieldTemplate>(templateId)?.MakeWritableClone();
            if (categoryFieldTemplate == null)
            {
                var categoryDisplayTemplateId = _displayTemplateService.Get<CategoryDisplayTemplate>(changes.FirstOrDefault().DisplayTemplate)?.SystemId ?? Guid.Empty;
                categoryFieldTemplate = new CategoryFieldTemplate(templateId, categoryDisplayTemplateId);
                categoryFieldTemplate.CategoryFieldGroups = new List<FieldTemplateFieldGroup>();
            }
            foreach (var change in changes)
            {


                if (categoryFieldTemplate?.CategoryFieldGroups?.FirstOrDefault(i => i.Id == change.FieldGroupName) != null)
                {
                    categoryFieldTemplate.CategoryFieldGroups.FirstOrDefault(i => i.Id == change.FieldGroupName)?.Fields.Add(change.Field);
                }
                else
                {
                    categoryFieldTemplate?.CategoryFieldGroups.Add(
                                                                        new FieldTemplateFieldGroup()
                                                                        {
                                                                            Id = change.FieldGroupName,
                                                                            Collapsed = false,
                                                                            Localizations =
                                                                                                {
                                                                                                                        ["sv-SE"] = { Name = change.FieldGroupName },
                                                                                                                        ["en-US"] = { Name = change.FieldGroupName }
                                                                                                },
                                                                            Fields = { change.Field }
                                                                        });
                }



            }
            return categoryFieldTemplate;
        }
        private FieldTemplate GetBlockFieldTemplate(string templateId, List<FieldTemplateChanges> changes, List<FieldTemplate> newTemplates)
        {
            var groups = changes.GroupBy(i => i.FieldGroupName);
            var blockFieldTemplate = newTemplates.FirstOrDefault(i => i.Id == templateId) as BlockFieldTemplate;
            if (blockFieldTemplate == null)
                return null;

            var newBlockFieldTemplate = new BlockFieldTemplate(templateId);
            newBlockFieldTemplate.FieldGroups = new List<FieldTemplateFieldGroup>();
            foreach (var group in blockFieldTemplate.FieldGroups)
            {
                var newGroup = new FieldTemplateFieldGroup()
                {
                    Id = group.Id,
                    Collapsed = group.Collapsed,
                };
                foreach (var field in group.Fields)
                {
                    newGroup.Fields.Add(field);
                }
                newBlockFieldTemplate.FieldGroups.Add(newGroup);
            }

            foreach (var change in changes)
            {

                if (newBlockFieldTemplate.FieldGroups?.FirstOrDefault(i => i.Id == change.FieldGroupName) == null)
                {
                    newBlockFieldTemplate.FieldGroups.Add(new FieldTemplateFieldGroup()
                    {
                        Id = change.FieldGroupName,
                        Collapsed = false,
                        Localizations =  {
                                            ["sv-SE"] = { Name = change.FieldGroupName },
                                            ["en-US"] = { Name = change.FieldGroupName }
                                         },
                    });
                }

                if (newBlockFieldTemplate.FieldGroups?.FirstOrDefault(i => i.Id == change.FieldGroupName) != null)
                {
                    newBlockFieldTemplate.FieldGroups.FirstOrDefault(i => i.Id == change.FieldGroupName)?.Fields.Add(change.Field);
                }
            }

            return newBlockFieldTemplate;
        }

        private FieldTemplate GetPageFieldTemplate(string templateId, List<FieldTemplateChanges> changes, List<FieldTemplate> newTemplates)
        {
            var pageFieldTemplate = newTemplates.FirstOrDefault(i => i.Id == templateId) as PageFieldTemplate;
            if (pageFieldTemplate == null)
                return null;
            var newPagefieldTemplate = new PageFieldTemplate(templateId);
            newPagefieldTemplate.FieldGroups = new List<FieldTemplateFieldGroup>();
            newPagefieldTemplate.Containers = pageFieldTemplate.Containers;
            foreach (var group in pageFieldTemplate.FieldGroups)
            {
                var newGroup = new FieldTemplateFieldGroup()
                {
                    Id = group.Id,
                    Collapsed = group.Collapsed,
                };
                foreach (var field in group.Fields)
                {
                    newGroup.Fields.Add(field);
                }
                newPagefieldTemplate.FieldGroups.Add(newGroup);
            }

            foreach (var change in changes)
            {

                if (newPagefieldTemplate.FieldGroups?.FirstOrDefault(i => i.Id == change.FieldGroupName) == null)
                {
                    newPagefieldTemplate.FieldGroups.Add(new FieldTemplateFieldGroup()
                    {
                        Id = change.FieldGroupName,
                        Collapsed = false,
                        Localizations =  {
                                            ["sv-SE"] = { Name = change.FieldGroupName },
                                            ["en-US"] = { Name = change.FieldGroupName }
                                         },
                    });
                }

                if (newPagefieldTemplate.FieldGroups?.FirstOrDefault(i => i.Id == change.FieldGroupName) != null)
                {
                    newPagefieldTemplate.FieldGroups.FirstOrDefault(i => i.Id == change.FieldGroupName)?.Fields.Add(change.Field);
                }
            }
            return newPagefieldTemplate;
        }
        private FieldTemplate GetChannelFieldTemplate(string templateId, List<FieldTemplateChanges> changes, List<FieldTemplate> newTemplates)
        {

            var groups = changes.GroupBy(i => i.FieldGroupName);
            var channelFieldTemplate = new ChannelFieldTemplate(templateId);
            channelFieldTemplate.FieldGroups = new List<FieldTemplateFieldGroup>();

            foreach (var group in groups)
            {
                var templateFieldGroup = new FieldTemplateFieldGroup();
                templateFieldGroup.Collapsed = false;

                foreach (var field in group)
                {
                    templateFieldGroup.Fields.Add(field.Field);
                }
                channelFieldTemplate.FieldGroups.Add(templateFieldGroup);
            }
            return channelFieldTemplate;

        }
        private FieldTemplate GetWebsiteFieldTemplate(string templateId, List<FieldTemplateChanges> changes, List<FieldTemplate> newTemplates)
        {

            var groups = changes.GroupBy(i => i.FieldGroupName);
            var websiteFieldTemplate = new WebsiteFieldTemplate(templateId);
            websiteFieldTemplate.FieldGroups = new List<FieldTemplateFieldGroup>();

            foreach (var group in groups)
            {
                var templateFieldGroup = new FieldTemplateFieldGroup();
                templateFieldGroup.Collapsed = false;

                foreach (var field in group)
                {
                    templateFieldGroup.Fields.Add(field.Field);
                }
                websiteFieldTemplate.FieldGroups.Add(templateFieldGroup);
            }
            return websiteFieldTemplate;
        }
    }
}