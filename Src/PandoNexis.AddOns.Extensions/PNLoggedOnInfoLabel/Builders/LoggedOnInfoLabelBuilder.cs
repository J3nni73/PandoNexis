using Litium.Accelerator.Builders;
using Litium.Customers;
using Litium.Security;

namespace PandoNexis.AddOns.Extensions.PNLoggedOnInfoLabel.Builders
{
    using Litium.Accelerator.Utilities;
    using PandoNexis.AddOns.Extensions.PNLoggedOnInfoLabel.ViewModels;
    using PandoNexis.AddOns.Extensions.PNOrganizationSelector.ViewModels;

    public class LoggedOnInfoLabelBuilder : IViewModelBuilder<OrganizationSelectorAutocompleteViewModel>
    {
        private readonly PersonService _personService;
        private readonly SecurityContextService _securityContextService;
        private readonly PersonStorage _personStorage;

        public LoggedOnInfoLabelBuilder(PersonService personService, SecurityContextService securityContextService, PersonStorage personStorage)
        {
            _personService = personService;
            _securityContextService = securityContextService;
            _personStorage = personStorage;
        }

        public virtual PersonInfo Build()
        {
            PersonInfo personInfo = new();

            var personSystemId = _securityContextService.GetIdentityUserSystemId();
            if (!personSystemId.HasValue)
            {
                return personInfo;
            }

            var currentPerson = _personService.Get(personSystemId.Value);
            var currentOrganization = _personStorage.CurrentSelectedOrganization;
            personInfo = new() {
                FirstName = currentPerson.FirstName,
                Surname = currentPerson.LastName,
                Email = currentPerson.Email,
                OrgId = currentOrganization != null ? currentOrganization.Id : string.Empty,
                OrganizationName = currentOrganization != null ? currentOrganization.Name : string.Empty,
                Tel = string.Empty,
                AdditionalInfo = string.Empty,
            };
            
            return personInfo;
        }
    }
}
