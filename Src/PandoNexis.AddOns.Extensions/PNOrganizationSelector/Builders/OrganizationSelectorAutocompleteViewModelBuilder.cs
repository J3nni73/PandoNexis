using Litium.Accelerator.Builders;
using Litium.Customers;
using Litium.Security;

namespace PandoNexis.AddOns.Extensions.PNOrganizationSelector.Builders
{
    using Litium.Accelerator.Utilities;
    using PandoNexis.Accelerator.Extensions.Services;
    using PandoNexis.AddOns.Extensions.PNOrganizationSelector.ViewModels;

    public class OrganizationSelectorAutocompleteViewModelBuilder : IViewModelBuilder<OrganizationSelectorAutocompleteViewModel>
    {
        private readonly PersonService _personService;
        private readonly SecurityContextService _securityContextService;
        private readonly PersonStorage _personStorage;
        private readonly PNOrganizationService _PNOrganizationService;

        public OrganizationSelectorAutocompleteViewModelBuilder(PersonService personService, SecurityContextService securityContextService, PersonStorage personStorage, PNOrganizationService pNOrganizationService)
        {
            _personService = personService;
            _securityContextService = securityContextService;
            _personStorage = personStorage;
            _PNOrganizationService = pNOrganizationService;
        }

        public async virtual Task<OrganizationSelectorAutocompleteViewModel> BuildAsync()
        {
            var result = new List<OrganizationItem>();
            var organizationSelectorAutocompleteViewModel = new OrganizationSelectorAutocompleteViewModel();
            var personSystemId = _securityContextService.GetIdentityUserSystemId();
            if (!personSystemId.HasValue)
            {
                return organizationSelectorAutocompleteViewModel;
            }

            var currentPerson = _personService.Get(personSystemId.Value);
            if (currentPerson == null)
            {
                return organizationSelectorAutocompleteViewModel;
            }
            var currentOrganization = _personStorage.CurrentSelectedOrganization;
           

            if (currentOrganization == null)
            {
                return organizationSelectorAutocompleteViewModel;
            }
            var currentSelectedOrgId = currentOrganization.SystemId;
            if (personSystemId.HasValue && currentPerson != null)
            {
                var organizations = _PNOrganizationService.GetOrganizations(currentPerson);

                if (organizations != null && organizations.Count > 0)
                {
                    foreach (var organization in organizations)
                    {
                        bool isSelected = organization.SystemId == currentSelectedOrgId;
                        OrganizationItem orgItem = new OrganizationItem();
                        orgItem.Id = organization.SystemId;
                        orgItem.Name = organization.Name;
                        orgItem.IsSelected = isSelected;
                        result.Add(orgItem);
                    }
                }
            }

            if (result.Count > 0)
            {
                organizationSelectorAutocompleteViewModel.Results = result;
            }

            return organizationSelectorAutocompleteViewModel;
        }
    }
}
