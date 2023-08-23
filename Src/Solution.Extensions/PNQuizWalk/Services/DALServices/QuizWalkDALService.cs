using Litium.Runtime.DependencyInjection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using PandoNexis.Accelerator.Extensions.Database.Services;
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
    [Service(ServiceType = typeof(QuizWalkDALService))]
    public class QuizWalkDALService : BaseDALService
    {
        private readonly QuizWalkDatabaseInitiator _databaseInitiator;
        private readonly string _dbTable = $"{DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{QuizWalkConstants.QuizWalkItem}";
        public QuizWalkDALService(IConfiguration configuration) : base(configuration)
        {
            _databaseInitiator = new QuizWalkDatabaseInitiator(configuration);
        }

        public override IEnumerable<QuizWalkItem> GetAll()
        {
            var result = new List<QuizWalkItem>();

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
                            var quizWalkItem = new QuizWalkItem();
                            quizWalkItem.SystemId = GetGuidValue(reader, QuizWalkConstants.SystemId);
                            quizWalkItem.Id = GetStringValue(reader, QuizWalkConstants.Id);
                            quizWalkItem.Question = GetStringValue(reader, QuizWalkConstants.Question);
                            quizWalkItem.Answer = GetStringValue(reader, QuizWalkConstants.Answer);
                            result.Add(quizWalkItem);
                        }
                    }
                }
            }
            return result;
        }
        public override bool AddOrUpdate(object item)
        {
            var quizWalkItem = item as QuizWalkItem;



            var dalObject = new DALAddOrUpdate();
            dalObject.Table = $"{_dbTable}";
            dalObject.Columns = _databaseInitiator.GetQuizWalkColumns();

            dalObject.Columns.FirstOrDefault(i => i.Name == QuizWalkConstants.SystemId).Value = quizWalkItem.SystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == QuizWalkConstants.Question).Value = quizWalkItem.Question;
            dalObject.Columns.FirstOrDefault(i => i.Name == QuizWalkConstants.Answer).Value = quizWalkItem.Answer;

            return base.AddOrUpdate(dalObject);
        }
        public QuizWalkItem GetNextInLine(Guid organizationSystemId)
        {
            var sql = $"select top 1 qwi.* from PandoNexis_QuizWalkItem qwi inner join PandoNexis_QuizWalkForOrganization qwo on qwo.quizwalkSystemId = qwi.systemid where organizationSystemID = '{organizationSystemId}' and passed = 0 order by itemorder";


            var result = new QuizWalkItem();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = _configuration["Litium:Data:ConnectionString"];
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var quizWalkItem = new QuizWalkItem();
                            quizWalkItem.SystemId = GetGuidValue(reader, QuizWalkConstants.SystemId);
                            quizWalkItem.Id = GetStringValue(reader, QuizWalkConstants.Id);
                            quizWalkItem.Question = GetStringValue(reader, QuizWalkConstants.Question);
                            quizWalkItem.Answer = GetStringValue(reader, QuizWalkConstants.Answer);
                            result = quizWalkItem;
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
