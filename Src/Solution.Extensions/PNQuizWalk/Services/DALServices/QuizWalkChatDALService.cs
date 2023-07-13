using Litium.Runtime.DependencyInjection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using PandoNexis.Accelerator.Extensions.Database.Services;
using Solution.Extensions.PNPilot.Constants;
using Solution.Extensions.PNPilot.Definitions;
using Solution.Extensions.PNPilot.Objects;
using Solution.Extensions.PNQuizWalk.Constants;
using Solution.Extensions.PNQuizWalk.Definitions;
using Solution.Extensions.PNQuizWalk.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNQuizWalk.Services.DALServices
{
    [Service(ServiceType = typeof(QuizWalkChatDALService))]
    public class QuizWalkChatDALService : BaseDALService
    {
        private readonly QuizWalkDatabaseInitiator _databaseInitiator;
        private readonly string _dbTable = $"{DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{QuizWalkConstants.QuizWalkChat}";
        public QuizWalkChatDALService(IConfiguration configuration) : base(configuration)
        {
            _databaseInitiator = new QuizWalkDatabaseInitiator(configuration);
        }

        public override IEnumerable<QuizWalkChatItem> GetAll()
        {
            var result = new List<QuizWalkChatItem>();

            var sql = $"select * from  {_dbTable}" + Environment.NewLine;

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = _configuration["Litium:Data:ConnectionString"];
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var quizWalkChatItem = new QuizWalkChatItem();
                            quizWalkChatItem.SystemId = GetGuidValue(reader, QuizWalkConstants.SystemId);
                            quizWalkChatItem.OrganizationSystemId = GetGuidValue(reader, QuizWalkConstants.OrganizationSystemId);
                            quizWalkChatItem.PersonSystemId = GetGuidValue(reader, QuizWalkConstants.PersonSystemId);
                            quizWalkChatItem.Chat = GetStringValue(reader, QuizWalkConstants.Chat);
                            quizWalkChatItem.CreatedDateTime = GetDateTimeValue(reader, DatabaseConstants.CreatedDateTime);
                            quizWalkChatItem.CreatedBy = GetGuidValue(reader, DatabaseConstants.CreatedBy);
                            quizWalkChatItem.UpdatedDateTime = GetDateTimeValue(reader, DatabaseConstants.UpdatedDateTime);
                            quizWalkChatItem.UpdatedBy = GetGuidValue(reader, DatabaseConstants.UpdatedBy);
                            quizWalkChatItem.DeletedDateTime = GetDateTimeValue(reader, DatabaseConstants.DeletedDateTime);
                            quizWalkChatItem.DeletedBy = GetGuidValue(reader, DatabaseConstants.DeletedBy);
                            result.Add(quizWalkChatItem);
                        }
                    }
                }
            }
            return result;
        }
        public override bool AddOrUpdate(object item)
        {
            var quizWalkChatItem = item as QuizWalkChatItem;



            var dalObject = new DALAddOrUpdate();
            dalObject.Table = $"{_dbTable}";
            dalObject.Columns = _databaseInitiator.GetQuizWalkChatColumns();

            dalObject.Columns.FirstOrDefault(i => i.Name == QuizWalkConstants.SystemId).Value = quizWalkChatItem.SystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == QuizWalkConstants.OrganizationSystemId).Value = quizWalkChatItem.OrganizationSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == QuizWalkConstants.PersonSystemId).Value = quizWalkChatItem.PersonSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == QuizWalkConstants.Chat).Value = quizWalkChatItem.Chat;

            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.CreatedDateTime).Value = quizWalkChatItem.CreatedDateTime;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.CreatedBy).Value = quizWalkChatItem.CreatedBy;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.UpdatedDateTime).Value = quizWalkChatItem.UpdatedDateTime;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.UpdatedBy).Value = quizWalkChatItem.UpdatedBy;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.DeletedDateTime).Value = quizWalkChatItem.DeletedDateTime;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.DeletedBy).Value = quizWalkChatItem.DeletedBy;

            return base.AddOrUpdate(dalObject);
        }
        public List<QuizWalkChatItem> GetChatsForOrganization(Guid organizationSystemId)
        {
            var sql = $"select * from {_dbTable} where organizationSystemId = '{organizationSystemId}' order by CreatedDateTime desc";


            var result = new List<QuizWalkChatItem>();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = _configuration["Litium:Data:ConnectionString"];
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var quizWalkItem = new QuizWalkItem();
                            var quizWalkChatItem = new QuizWalkChatItem();
                            quizWalkChatItem.SystemId = GetGuidValue(reader, QuizWalkConstants.SystemId);
                            quizWalkChatItem.OrganizationSystemId = GetGuidValue(reader, QuizWalkConstants.OrganizationSystemId);
                            quizWalkChatItem.PersonSystemId = GetGuidValue(reader, QuizWalkConstants.PersonSystemId);
                            quizWalkChatItem.Chat = GetStringValue(reader, QuizWalkConstants.Chat);
                            quizWalkChatItem.CreatedDateTime = GetDateTimeValue(reader, DatabaseConstants.CreatedDateTime);
                            quizWalkChatItem.CreatedBy = GetGuidValue(reader, DatabaseConstants.CreatedBy);
                            quizWalkChatItem.UpdatedDateTime = GetDateTimeValue(reader, DatabaseConstants.UpdatedDateTime);
                            quizWalkChatItem.UpdatedBy = GetGuidValue(reader, DatabaseConstants.UpdatedBy);
                            quizWalkChatItem.DeletedDateTime = GetDateTimeValue(reader, DatabaseConstants.DeletedDateTime);
                            quizWalkChatItem.DeletedBy = GetGuidValue(reader, DatabaseConstants.DeletedBy);
                            result.Add(quizWalkChatItem);
                        }
                    }
                }
            }
            return result;

        }

        public void UpdateOrganizationsWalk(Guid organizationSystemId, Guid quizWalkSystemId)
        {
            var sql = $"update qwo set passed = 1 from PandoNexis_QuizWalkForOrganization qwo where OrganizationSystemId = '{organizationSystemId}'and QuizWalkSystemId  = '{quizWalkSystemId}' ";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = _configuration["Litium:Data:ConnectionString"];
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                       
                    }
                }
            }


        }
    }
}
