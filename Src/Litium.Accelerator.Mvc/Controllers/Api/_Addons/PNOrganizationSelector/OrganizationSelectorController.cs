using Litium.Accelerator.Constants;
using Litium.Accelerator.Services;
using Litium.Accelerator.Utilities;
using Litium.Customers;
using Litium.Sales;
using Litium.Security;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.Accelerator.Extensions.Services;
using PandoNexis.AddOns.Extensions.PNOrganizationSelector.Builders;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Litium.Accelerator.Mvc.Controllers.Api._Addons.PNOrganizationSelector
{
    [Route("api/organizationSelector")]
    public class OrganizationSelectorController : ApiControllerBase
    {
        private readonly OrganizationSelectorAutocompleteViewModelBuilder _organizationSelectorResultViewModellBuilder;
        private readonly PersonStorage _personStorage;
        private readonly PersonService _personService;
        private readonly LoginService _loginService;
        private readonly SecurityContextService _securityContextService;
        private readonly AddressTypeService _addressTypeService;
        private readonly PNOrganizationService _PNOrganizationService;

        public OrganizationSelectorController(OrganizationSelectorAutocompleteViewModelBuilder organizationSelectorResultViewModellBuilder, PersonStorage personStorage, SecurityContextService securityContextService, PersonService personService, LoginService loginService, AddressTypeService addressTypeService, PNOrganizationService pNOrganizationService)
        {
            _organizationSelectorResultViewModellBuilder = organizationSelectorResultViewModellBuilder;
            _personStorage = personStorage;
            _securityContextService = securityContextService;
            _personService = personService;
            _loginService = loginService;
            _addressTypeService = addressTypeService;
            _PNOrganizationService = pNOrganizationService;
        }

        [HttpPost]
        [Route("getall")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAll()
        {
            var result = await _organizationSelectorResultViewModellBuilder.BuildAsync();

            return Ok(result.Results);
        }

        [HttpPost]
        [Route("setcurrentorganization")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetCurrentOrganization([FromBody] Guid orgId)
        {
            var currentPersonId = _securityContextService.GetIdentityUserSystemId();
            if (orgId != Guid.Empty && currentPersonId.HasValue)
            {
                var person = _personService.Get(currentPersonId.Value);
                if (_loginService.IsBusinessCustomer(person, out var organizations))
                {
                    if (organizations.Count > 0)
                    {
                        var currentSelectedOrg = organizations.FirstOrDefault(x => x.SystemId == orgId);
                        if (currentSelectedOrg != null)
                        {
                            var cartContext = HttpContext.GetCartContext();
                            await _PNOrganizationService.SetOrganizationAsync(cartContext, orgId);
                            if (_personStorage.CurrentSelectedOrganization != null)
                            {
                                var addressType = _addressTypeService.Get(AddressTypeNameConstants.Address);
                                await _PNOrganizationService.SetCountryByAddressAsync(cartContext, _personStorage.CurrentSelectedOrganization.Addresses.FirstOrDefault(x => x.AddressTypeSystemId == addressType.SystemId));

                                return Ok();
                            }

                            _loginService.Logout();
                            return Ok();
                        }
                    }
                }
            }
            return NotFound();
        }
    }
}