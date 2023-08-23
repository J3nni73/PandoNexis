using Litium.Runtime.DependencyInjection;
using Newtonsoft.Json;
using PandoNexis.AddOns.PNPilot.Objects;
using PandoNexis.AddOns.PNPilot.Services.DALServices;


namespace PandoNexis.AddOns.PNPilot.Services
{
    [Service(ServiceType = typeof(ItemTypeService))]
    public class ItemTypeService
    {
        private readonly ItemTypeDALService _itemTypeDALService;
        public ItemTypeService(ItemTypeDALService itemTypeDALService)
        {
            _itemTypeDALService = itemTypeDALService;
        }

        public IEnumerable<ItemType> GetItemTypes()
        {
            return _itemTypeDALService.GetAll();
        }

        public bool AddOrUpdateItemType(string jsonItem)
        {
            var item = JsonConvert.DeserializeObject<ItemType>(jsonItem);
            if (item == null) return false;
            return _itemTypeDALService.AddOrUpdate(item);

        }

    }
}
