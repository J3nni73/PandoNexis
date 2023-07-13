using Litium.Runtime.DependencyInjection;
using Newtonsoft.Json;
using Solution.Extensions.PNPilot.Objects;
using Solution.Extensions.PNPilot.Services.DALServices;
using Solution.Extensions.PNQuizWalk.Objects;
using Solution.Extensions.PNQuizWalk.Services.DALServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNQuizWalk.Services
{   
    [Service(ServiceType = typeof(QuizWalkService))]
    public class QuizWalkService
    {
        private readonly QuizWalkDALService _quizWalkDALService;

        public QuizWalkService(QuizWalkDALService quizWalkDALService)
        {
            _quizWalkDALService = quizWalkDALService;
        }

        public List<QuizWalkItem> GetAll()
        {
            var result = _quizWalkDALService.GetAll().ToList();

            return result;
        }
        public QuizWalkItem GetNext(Guid organizationSystemId)
        {
            var result = _quizWalkDALService.GetNextInLine(organizationSystemId);

            return result;
        }
        public QuizWalkItem Get(string id)
        {
            return GetAll().FirstOrDefault(i => i.Id == id);
        }
        public QuizWalkItem Get(Guid systemId)
        {
            return GetAll().FirstOrDefault(i => i.SystemId == systemId);
        }
        public bool AddOrUpdate(string jsonItem)
        {
            var item = JsonConvert.DeserializeObject<QuizWalkItem>(jsonItem);
            if (item == null) return false;
            return AddOrUpdate(item);

        }
        public bool AddOrUpdate(QuizWalkItem quizWalkItem)
        {
            if (quizWalkItem == null) return false;
            return _quizWalkDALService.AddOrUpdate(quizWalkItem);

        }

        public QuizWalkItem UpdateOrganizationsWalk(Guid organizationSystemId, Guid systemId)
        {
            _quizWalkDALService.UpdateOrganizationsWalk(organizationSystemId, systemId);
            return _quizWalkDALService.GetNextInLine(organizationSystemId);
        }
    }
}
