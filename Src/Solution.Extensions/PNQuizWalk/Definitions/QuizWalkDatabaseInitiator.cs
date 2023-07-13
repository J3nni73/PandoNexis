using Microsoft.Extensions.Configuration;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using PandoNexis.Accelerator.Extensions.Database.Objects;
using PandoNexis.Accelerator.Extensions.Database.Services;
using Solution.Extensions.PNPilot.Constants;
using Solution.Extensions.PNQuizWalk.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNQuizWalk.Definitions
{
    public class QuizWalkDatabaseInitiator : DatabaseInitiator
    {
        public QuizWalkDatabaseInitiator(IConfiguration configuration) : base(configuration)
        {
        }

        public override void GetCheckDatabaseObjects()
        {
            SyncronizeDatabaseObjects(QuizWalkConstants.QuizWalkItem, GetQuizWalkColumns());
            SyncronizeDatabaseObjects(QuizWalkConstants.QuizWalkForOrganization, GetQuizWalkForOrganizationColumns());
            SyncronizeDatabaseObjects(QuizWalkConstants.QuizWalkChat, GetQuizWalkChatColumns());
        }
        public List<DatabaseColumns> GetQuizWalkColumns()
        {
            var result = new List<DatabaseColumns>
            {
                GetColumn(QuizWalkConstants.SystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull, true),
                GetColumn(QuizWalkConstants.Id, DatabaseTypeConstants.Varchar50, DatabaseTypeConstants.NotNull, false ), 
                GetColumn(QuizWalkConstants.Question, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.NotNull, false),
                GetColumn(QuizWalkConstants.Answer, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.NotNull, false),

            };
            return result;
        }
        public List<DatabaseColumns> GetQuizWalkForOrganizationColumns()
        {
            var result = new List<DatabaseColumns>
            {
                GetColumn(QuizWalkConstants.OrganizationSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull, false),
                GetColumn(QuizWalkConstants.QuizWalkSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull, false ),
                GetColumn(QuizWalkConstants.ItemOrder, DatabaseTypeConstants.Int, DatabaseTypeConstants.NotNull, false),
                GetColumn(QuizWalkConstants.Passed, DatabaseTypeConstants.Bit, DatabaseTypeConstants.Null, false),

            };
            return result;
        }
        public List<DatabaseColumns> GetQuizWalkChatColumns()
        {
            var result = new List<DatabaseColumns>
            {
                GetColumn(QuizWalkConstants.SystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull, true),
                GetColumn(QuizWalkConstants.OrganizationSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull, true),
                GetColumn(QuizWalkConstants.PersonSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull, true),
                GetColumn(QuizWalkConstants.Chat, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.NotNull, false),
            };
            result.AddRange(GetEditedColumns());
            return result;
        }
    }
}
