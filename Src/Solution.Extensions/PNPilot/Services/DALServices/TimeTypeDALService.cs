using Microsoft.Data.SqlClient;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using Microsoft.Extensions.Configuration;
using Solution.Extensions.PNPilot.Objects;
using Litium.Runtime.DependencyInjection;
using Solution.Extensions.PNPilot.Constants;
using PandoNexis.Accelerator.Extensions.Database.Services;
using Solution.Extensions.PNPilot.Definitions;

namespace Solution.Extensions.PNPilot.Services.DALServices
{
    [Service(ServiceType = typeof(TimeTypeDALService))]
    public class TimeTypeDALService : BaseDALService
    {
        private readonly PilotDatabaseInitiator _pilotDatabaseInitiator;
        private readonly string _dbTable = $"{DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.TimeType}";
        public TimeTypeDALService(IConfiguration configuration) : base(configuration)
        {
            _pilotDatabaseInitiator = new PilotDatabaseInitiator(configuration);
        }

        public override IEnumerable<TimeType> GetAll()
        {
            var result = new List<TimeType>();

            var sql = $"select * from {_dbTable}" + Environment.NewLine;

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
                            var newTimeType = new TimeType();
                            newTimeType.SystemId = GetGuidValue(reader, PilotConstants.SystemId);
                            newTimeType.Name = GetStringValue(reader, PilotConstants.Name);
                            newTimeType.Description = GetStringValue(reader, PilotConstants.Description);
                            result.Add(newTimeType);
                        }
                    }
                }
            }
            return result;
        }

        public override bool AddOrUpdate(object item)
        {
            var timeType = item as TimeType;

        

            var dalObject = new DALAddOrUpdate();
            dalObject.Table = $"{_dbTable}";
            dalObject.Columns = _pilotDatabaseInitiator.GetItemColumns();

            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.SystemId).Value = timeType.SystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.Name).Value = timeType.Name;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.Description).Value = timeType.Description;

            return base.AddOrUpdate(dalObject);
        }
    }
}
