using Litium.Runtime.DependencyInjection;
using Newtonsoft.Json;
using PandoNexis.AddOns.PNPilot.Objects;
using PandoNexis.AddOns.PNPilot.Services.DALServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.PNPilot.Services
{
    [Service(ServiceType = typeof(TimeStatusService))]
    public class TimeStatusService
    {
        private readonly TimeStatusDALService _timeStatusDALService;
        public TimeStatusService(TimeStatusDALService timeStatusDALService)
        {
            _timeStatusDALService = timeStatusDALService;
        }

        public IEnumerable<TimeStatus> GetTimeStatuses()
        {
            return _timeStatusDALService.GetAll();
        }

        public bool AddOrUpdateTimeType(string jsonItem)
        {
            var item = JsonConvert.DeserializeObject<TimeStatus>(jsonItem);
            if (item == null) return false;
            return _timeStatusDALService.AddOrUpdate(item);

        }

    }
}
