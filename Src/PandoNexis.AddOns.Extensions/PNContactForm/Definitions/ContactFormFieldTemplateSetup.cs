using Litium.Accelerator.Constants;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Products;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;

namespace PandoNexis.AddOns.Extensions.PNContactForm.Definitions
{
    internal class ContactFormFieldTemplateSetup : FieldTemplateHelper
    {
        private readonly DisplayTemplateService _displayTemplateService;
        public ContactFormFieldTemplateSetup(DisplayTemplateService displayTemplateService)
        {
            _displayTemplateService = displayTemplateService;
        }
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>()
            {
               GetPersonField(ContactFormConstants.ContactForm, ProcessorConstants.AvailableFields, SystemFieldDefinitionConstants.FirstName),
               GetPersonField(ContactFormConstants.ContactForm, ProcessorConstants.AvailableFields, SystemFieldDefinitionConstants.LastName),
               GetPersonField(ContactFormConstants.ContactForm, ProcessorConstants.AvailableFields, SystemFieldDefinitionConstants.Email),
               GetPersonField(ContactFormConstants.ContactForm, ProcessorConstants.AvailableFields, ContactFormConstants.ContactFormCompany),
               GetPersonField(ContactFormConstants.ContactForm, ProcessorConstants.AvailableFields, ContactFormConstants.ContactFormMessage),

               GetPersonField(ContactFormConstants.ContactForm, ProcessorConstants.EditableFields, SystemFieldDefinitionConstants.FirstName),
               GetPersonField(ContactFormConstants.ContactForm, ProcessorConstants.EditableFields, SystemFieldDefinitionConstants.LastName),
               GetPersonField(ContactFormConstants.ContactForm, ProcessorConstants.EditableFields, SystemFieldDefinitionConstants.Email),
               GetPersonField(ContactFormConstants.ContactForm, ProcessorConstants.EditableFields, ContactFormConstants.ContactFormCompany),
               GetPersonField(ContactFormConstants.ContactForm, ProcessorConstants.EditableFields, ContactFormConstants.ContactFormMessage),

               GetPersonField(ContactFormConstants.ContactForm, ProcessorConstants.RequiredFields, SystemFieldDefinitionConstants.Email),

               GetPersonField(CustomerTemplateIdConstants.B2BPersonTemplate,ContactFormConstants.ContactForm, ContactFormConstants.AddedByContactForm),
               GetPersonField(CustomerTemplateIdConstants.B2BPersonTemplate,ContactFormConstants.ContactForm, ContactFormConstants.ContactFormMessage),
               GetPersonField(CustomerTemplateIdConstants.B2BPersonTemplate,ContactFormConstants.ContactForm, ContactFormConstants.ContactFormCompany),

            };


            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
           
            var template = new PersonFieldTemplate(ContactFormConstants.ContactForm)
            {
                FieldGroups = new[]
                     {
                        new FieldTemplateFieldGroup
                        {
                            Id = ProcessorConstants.AvailableFields,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = ProcessorConstants.AvailableFields },
                                ["en-US"] = { Name = ProcessorConstants.AvailableFields }
                            },
                            UseInStorefront = true,
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = ProcessorConstants.DefaultFields,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = ProcessorConstants.DefaultFields },
                                ["en-US"] = { Name = ProcessorConstants.DefaultFields }
                            },

                            UseInStorefront = true,
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = ProcessorConstants.DefaultFields,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = ProcessorConstants.EditableFields },
                                ["en-US"] = { Name = ProcessorConstants.EditableFields }
                            },

                            UseInStorefront = true,
                        }
                },
            };
            return template;
        }
    }
}
