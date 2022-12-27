using System.Collections.Generic;
using Litium.Accelerator.Builders;

namespace PandoNexis.AddOns.Extensions.PNOrganizationSelector.ViewModels
{
    public class OrganizationSelectorAutocompleteViewModel : IViewModel
    {
        public IEnumerable<OrganizationItem> Results { get; set; }
    }
}
