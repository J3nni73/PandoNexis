using Litium.Accelerator.Constants;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Products;
using Microsoft.VisualBasic;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;

namespace PandoNexis.AddOns.Extensions.PNRegisterMe.Definitions
{
    internal class RegisterMeFieldTemplateSetup : FieldTemplateHelper
    {
        private readonly DisplayTemplateService _displayTemplateService;
        public RegisterMeFieldTemplateSetup(DisplayTemplateService displayTemplateService)
        {
            _displayTemplateService = displayTemplateService;
        }
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>()
            {
               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.AvailableFields, SystemFieldDefinitionConstants.FirstName),
               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.AvailableFields, SystemFieldDefinitionConstants.LastName),
               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.AvailableFields, RegisterMeConstants.UserName),
               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.AvailableFields, SystemFieldDefinitionConstants.Email),
               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.AvailableFields, RegisterMeConstants.Allergies),
               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.AvailableFields, RegisterMeConstants.PersonInfo),

               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.EditableFields, SystemFieldDefinitionConstants.FirstName),
               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.EditableFields, SystemFieldDefinitionConstants.LastName),
               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.EditableFields, SystemFieldDefinitionConstants.Email),
               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.EditableFields, RegisterMeConstants.Allergies),
               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.EditableFields, RegisterMeConstants.PersonInfo),
               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.EditableFields, RegisterMeConstants.UserName),

               GetPersonField(RegisterMeConstants.RegisterMePerson, ProcessorConstants.RequiredFields, SystemFieldDefinitionConstants.Email),

               GetPersonField(CustomerTemplateIdConstants.B2BPersonTemplate, "General", RegisterMeConstants.AddedByRegisterMeForm),
               GetPersonField(CustomerTemplateIdConstants.B2BPersonTemplate, "General", RegisterMeConstants.DateAdded),
               GetPersonField(CustomerTemplateIdConstants.B2BPersonTemplate, ProcessorConstants.EditableFields, RegisterMeConstants.Allergies),
               GetPersonField(CustomerTemplateIdConstants.B2BPersonTemplate, ProcessorConstants.EditableFields, RegisterMeConstants.PersonInfo),
               GetPersonField(CustomerTemplateIdConstants.B2BPersonTemplate, ProcessorConstants.EditableFields, RegisterMeConstants.UserName),

            };


            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
           
            var template = new PersonFieldTemplate(RegisterMeConstants.RegisterMePerson)
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
                      
                },
            };
            return template;
        }
    }
}
