using Litium.Runtime.DependencyInjection;
using Newtonsoft.Json;
using Solution.Extensions.PNPilot.Objects;
using Solution.Extensions.PNPilot.Services.DALServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNPilot.Services
{
    [Service(ServiceType = typeof(WorkItemService))]
    public class WorkItemService
    {
        private readonly WorkItemDALService _pilotItemDALService;
        public WorkItemService(WorkItemDALService pilotItemDALService)
        {
            _pilotItemDALService = pilotItemDALService;
        }

        public IEnumerable<WorkItem> GetItems()
        {
            return _pilotItemDALService.GetAll();
        }

        public bool AddOrUpdateItem(string jsonItem)
        {
            var item = JsonConvert.DeserializeObject<WorkItem>(jsonItem);
            if (item == null) return false;
            return _pilotItemDALService.AddOrUpdate(item);

        }

    }
}
