using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Websites;
using Litium.Globalization;
using Litium.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.Services.PNStockService
{
    [Service(ServiceType = typeof(PNAvailabilityService))]
    public class PNAvailabilityService
    {
        private readonly VariantService _variantService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly PersonStorage _personStorage;

        public PNAvailabilityService(RequestModelAccessor requestModelAccessor, VariantService variantService, PersonStorage personStorage)
        {
            _requestModelAccessor = requestModelAccessor;
            _variantService = variantService;
            _personStorage = personStorage;
        }

        public bool RequiresInventory(Guid variantSystemId)
        {
            
            return RequiresInventory(_requestModelAccessor.RequestModel.WebsiteModel.Website,
                                        _requestModelAccessor.RequestModel.ChannelModel.Channel,
                                        _personStorage.CurrentSelectedOrganization,
                                        _variantService.Get(variantSystemId));
        }

        public bool RequiresInventory(Variant variant)
        {
            return RequiresInventory(_requestModelAccessor.RequestModel.WebsiteModel.Website,
                                        _requestModelAccessor.RequestModel.ChannelModel.Channel,
                                        _personStorage.CurrentSelectedOrganization,
                                        variant);
        }

        public bool RequiresInventory(Website website, Channel channel, Organization organization, Variant variant)
        {

            if (website != null)
            {
                if (website.Fields.GetValue<bool>(PNInventoryConstants.IgnoreInventory))
                    return false;
            }

            if (channel != null)
            {
                if (channel.Fields.GetValue<bool>(PNInventoryConstants.IgnoreInventory))
                    return false;
            }

            if (organization != null)
            {
                if (organization.Fields.GetValue<bool>(PNInventoryConstants.IgnoreInventory))
                    return false;
            }

            if (variant != null)
            {
                if (variant.Fields.GetValue<bool>(PNInventoryConstants.IgnoreInventory))
                    return false;
            }


            return true;
        }
    }
}
