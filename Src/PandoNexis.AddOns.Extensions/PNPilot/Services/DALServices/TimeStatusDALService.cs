using Microsoft.Data.SqlClient;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using Microsoft.Extensions.Configuration;
using PandoNexis.AddOns.PNPilot.Objects;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.PNPilot.Constants;
using PandoNexis.Accelerator.Extensions.Database.Services;
using PandoNexis.AddOns.PNPilot.Definitions;

namespace PandoNexis.AddOns.PNPilot.Services.DALServices
{
    [Service(ServiceType = typeof(TimeStatusDALService))]
    public class TimeStatusDALService : BaseDALService
    {
        private readonly PilotDatabaseInitiator _pilotDatabaseInitiator;
        private readonly string _dbTable = $"{DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.TimeStatus}";
        public TimeStatusDALService(IConfiguration configuration):base(configuration) 
        {
            _pilotDatabaseInitiator = new PilotDatabaseInitiator(configuration);
        }    

        public override IEnumerable<TimeStatus> GetAll()
        {
            var result = new List<TimeStatus>();

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
                            var newTimeStatus = new TimeStatus();
                            newTimeStatus.SystemId = GetGuidValue(reader, PilotConstants.SystemId);
                            newTimeStatus.TimeTypeSystemId = GetGuidValue(reader,PilotConstants.TimeTypeSystemId);
                            newTimeStatus.Name = GetStringValue(reader, PilotConstants.Name);
                            newTimeStatus.Description = GetStringValue(reader, PilotConstants.Description);
                            result.Add(newTimeStatus);
                        }
                    }
                }
            }
            return result;
        }

        public override bool AddOrUpdate(object item)
        {
            var timeStatus = item as TimeStatus;



            var dalObject = new DALAddOrUpdate();
            dalObject.Table = $"{_dbTable}";
            dalObject.Columns = _pilotDatabaseInitiator.GetItemColumns();

            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.SystemId).Value = timeStatus.SystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.TimeTypeSystemId).Value = timeStatus.TimeTypeSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.Name).Value = timeStatus.Name;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.Description).Value = timeStatus.Description;

            return base.AddOrUpdate(dalObject);
        }
    }
}
