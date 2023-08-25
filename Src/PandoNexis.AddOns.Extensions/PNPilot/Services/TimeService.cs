using Humanizer;
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
        public Time? GetTime(Guid systemId)
        {
            return GetAllTime()?.FirstOrDefault(i => i.SystemId == systemId);
        }
        public Time GetNewTime(Guid itemSystemId, Guid timeTypeSystemId, Guid organizationSystemId, Guid personSystemId)
        {
            var item = new Time();
            item.SystemId = Guid.NewGuid();
            item.ItemSystemId= itemSystemId;
            item.OrganizationSystemId = organizationSystemId;
            item.TimeTypeSystemId = timeTypeSystemId;
            item.CreatedBy = personSystemId;
            item.UpdatedBy = personSystemId;
            item.CreatedDateTime = DateTime.Now;
            item.UpdatedDateTime = DateTime.Now;
            item.TimeFrom= DateTime.Now;
            item.TimeTo= DateTime.Now;
            return item;
        }

        public bool AddOrUpdateTime(string jsonItem)
        {
            var item = JsonConvert.DeserializeObject<Time>(jsonItem);
            if (item == null) return false;
            return AddOrUpdateTime(item);

        }


        public bool AddOrUpdateTime(Time time)
        {
            if (time == null) return false;

            return _timeDALService.AddOrUpdate(time);

        }

    }
}
