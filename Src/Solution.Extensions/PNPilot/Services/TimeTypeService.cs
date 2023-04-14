using Litium.Runtime.DependencyInjection;
using Newtonsoft.Json;
using Solution.Extensions.PNPilot.Objects;
using Solution.Extensions.PNPilot.Services.DALServices;


namespace Solution.Extensions.PNPilot.Services
{
    [Service(ServiceType = typeof(TimeTypeService))]
    public class TimeTypeService
    {
        private readonly TimeTypeDALService _timeTypeDALService;
        public TimeTypeService(TimeTypeDALService timeTypeDALService)
        {
            _timeTypeDALService = timeTypeDALService;
        }

        public IEnumerable<TimeType> GetTimeTypes()
        {
            return _timeTypeDALService.GetAll();
        }

        public bool AddOrUpdateTimeType(string jsonItem)
        {
            var item = JsonConvert.DeserializeObject<TimeType>(jsonItem);
            if (item == null) return false;
            return _timeTypeDALService.AddOrUpdate(item);

        }

    }
}
