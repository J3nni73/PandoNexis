using Litium.Accelerator.Builders;
using Litium.Customers;
using Litium.Security;

namespace PandoNexis.AddOns.Extensions.PNLoggedOnInfoLabel.Builders
{
    using Litium.Accelerator.Utilities;
    using PandoNexis.AddOns.Extensions.PNLoggedOnInfoLabel.ViewModels;

    public class LoggedOnInfoLabelBuilder : IViewModelBuilder<PersonInfo>
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

        public async virtual Task<PersonInfo> BuildAsync()
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
                FirstName = GetCleanString(currentPerson.FirstName),
                Surname = GetCleanString(currentPerson.LastName),
                Email = GetCleanString(currentPerson.Email),
                OrgId = currentOrganization != null ? GetCleanString(currentOrganization.Id) : string.Empty,
                OrganizationName = currentOrganization != null ? GetCleanString(currentOrganization.Name) : string.Empty,
                Tel = string.Empty,
                AdditionalInfo = string.Empty,
            };
            
            return personInfo;
        }

        private string GetCleanString(string stringVal)
        {
            return stringVal ?? string.Empty;
        }
    }
}
