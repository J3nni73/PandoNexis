using Litium.Accelerator.Constants;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Customers;
using Litium.Globalization;
using Litium.Runtime.DependencyInjection;
using Litium.Sales;
using Litium.Security;

namespace PandoNexis.Accelerator.Extensions.Services
{
    [Service(ServiceType = typeof(PNOrganizationService))]
    [RequireServiceImplementation]
    public class PNOrganizationService
    {
        private readonly OrganizationService _organizationService;
        private readonly PersonStorage _personStorage;
        private readonly SecurityContextService _securityContextService;
        private readonly CountryService _countryService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly RoleService _roleService;
        public PNOrganizationService(OrganizationService organizationService, PersonStorage personStorage, SecurityContextService securityContextService, CountryService countryService, RequestModelAccessor requestModelAccessor, RoleService roleService)
        {
            _organizationService = organizationService;
            _personStorage = personStorage;
            _securityContextService = securityContextService;
            _countryService = countryService;
            _requestModelAccessor = requestModelAccessor;
            _roleService = roleService;
        }
        public async Task SetOrganizationAsync(CartContext cartContext, Guid organizationSystemId)
        {
            _personStorage.CurrentSelectedOrganization = _organizationService.Get(organizationSystemId);
            var personId = _securityContextService.GetIdentityUserSystemId().GetValueOrDefault();
            if (cartContext != null && (cartContext.OrganizationSystemId != organizationSystemId || cartContext.PersonSystemId != personId))
            {
                await cartContext.ChangeCustomerAsync(new ChangeCustomerArgs
                {
                    CustomerNumber = _personStorage.CurrentSelectedOrganization.Id,
                    CustomerType = CustomerType.Organization,
                    PersonSystemId = personId,
                    OrganizationSystemId = organizationSystemId
                });
            }
        }
        public async Task SetCountryByAddressAsync(CartContext cartContext, Litium.Customers.Address address)
        {
            //Check if the country in the address is same as channel has.
            if (address != null && !string.IsNullOrEmpty(address.Country) && !address.Country.Equals(_requestModelAccessor.RequestModel.CountryModel.Country.Id, StringComparison.CurrentCultureIgnoreCase))
            {
                var country = _countryService.Get(address.Country);
                //Check if country is connected to the channel
                if (country != null && _requestModelAccessor.RequestModel.ChannelModel.Channel.CountryLinks.Any(x => x.CountrySystemId == country.SystemId))
                {
                    // Set user's country to the channel
                    await cartContext.SelectCountryAsync(new SelectCountryArgs { CountryCode = country.Id });
                }
            }
        }

        public List<Organization> GetOrganizations(Person person)
        {            
            var organizations = new List<Organization>();
            if (person.OrganizationLinks.Count == 0) return organizations;

            var organizationIds = new List<Guid>();
            foreach (var personToOrganizationLink in person.OrganizationLinks)
            {
                foreach (var roleSystemId in personToOrganizationLink.RoleSystemIds)
                {
                    var role = _roleService.Get(roleSystemId);
                    if (role.Id != RolesConstants.RoleOrderApprover &&
                        role.Id != RolesConstants.RoleOrderPlacer)
                    {
                        continue;
                    }
                    if (organizationIds.Contains(personToOrganizationLink.OrganizationSystemId))
                    {
                        continue;
                    }
                    organizationIds.Add(personToOrganizationLink.OrganizationSystemId);
                    break;
                }
            }
            organizations.AddRange(organizationIds.Select(x => _organizationService.Get(x)));
            return organizations;
        }
    }
}
