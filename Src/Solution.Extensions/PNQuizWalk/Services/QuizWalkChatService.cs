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
    [Service(ServiceType = typeof(QuizWalkChatService))]
    public class QuizWalkChatService
    {
        private readonly QuizWalkChatDALService _quizWalkChatDALService;

        public QuizWalkChatService(QuizWalkChatDALService quizWalkChatDALService)
        {
            _quizWalkChatDALService = quizWalkChatDALService;
        }

        public List<QuizWalkChatItem> GetAll()
        {
            var result = _quizWalkChatDALService.GetAll().ToList();

            return result;
        }
        public List<QuizWalkChatItem> GetChatsForOrganization(Guid organizationSystemId)
        {
            var result = _quizWalkChatDALService.GetChatsForOrganization(organizationSystemId);

            return result;
        }
      
        public QuizWalkChatItem Get(Guid systemId)
        {
            return GetAll().FirstOrDefault(i => i.SystemId == systemId);
        }
        public bool AddOrUpdate(string jsonItem)
        {
            var item = JsonConvert.DeserializeObject<QuizWalkChatItem>(jsonItem);
            if (item == null) return false;
            return AddOrUpdate(item);

        }
        public bool AddOrUpdate(QuizWalkChatItem quizWalkChatItem)
        {
            if (quizWalkChatItem == null) return false;
            return _quizWalkChatDALService.AddOrUpdate(quizWalkChatItem);

        }

       
    }
}
