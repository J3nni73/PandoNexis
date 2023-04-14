using Litium.Runtime.DependencyInjection;
using Newtonsoft.Json;
using Solution.Extensions.PNPilot.Objects;
using Solution.Extensions.PNPilot.Services.DALServices;

namespace Solution.Extensions.PNPilot.Services
{
    [Service(ServiceType = typeof(TimeService))]
    public class TimeService
    {
        private readonly TimeDALService _timeDALService;
        public TimeService(TimeDALService timeDALService)
        {
            _timeDALService = timeDALService;
        }

        public IEnumerable<Time> GetAllTime()
        {
            return _timeDALService.GetAll();
        }

        public bool AddOrUpdateTime(string jsonItem)
        {
            var item = JsonConvert.DeserializeObject<Time>(jsonItem);
            if (item == null) return false;
            return _timeDALService.AddOrUpdate(item);

        }

    }
}
