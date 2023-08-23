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
    [Service(ServiceType = typeof(TimeDALService))]
    public class TimeDALService : BaseDALService
    {
        private readonly PilotDatabaseInitiator _pilotDatabaseInitiator;
        private readonly string _dbTable = $"{DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.Time}";

        public TimeDALService(IConfiguration configuration):base(configuration)
        {

            _pilotDatabaseInitiator = new PilotDatabaseInitiator(configuration);
        }

        public override IEnumerable<Time> GetAll()
        {
            var result = new List<Time>();

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
                            var newTime = new Time();
                            newTime.SystemId = GetGuidValue(reader, PilotConstants.SystemId);
                            newTime.ItemSystemId = GetGuidValue(reader, PilotConstants.ItemSystemId);
                            newTime.OrganizationSystemId = GetGuidValue(reader, PilotConstants.OrganizationSystemId);
                            newTime.TimeTypeSystemId = GetGuidValue(reader, PilotConstants.TimeTypeSystemId);
                            newTime.TimeStatusSystemId = GetGuidValue(reader, PilotConstants.TimeStatusSystemId);
                            newTime.TimeComment = GetStringValue(reader, PilotConstants.TimeComment);
                            newTime.TimeFrom = GetDateTimeValue(reader, PilotConstants.TimeFrom);
                            newTime.TimeTo = GetDateTimeValue(reader, PilotConstants.TimeTo);
                            newTime.Amount = GetIntValue(reader, PilotConstants.TimeAmount);
                            newTime.Risk = GetDecimalValue(reader, PilotConstants.TimeRisk);
                            newTime.PersonSystemId = GetGuidValue(reader, PilotConstants.PersonSystemId);
                            newTime.CreatedDateTime = GetDateTimeValue(reader, DatabaseConstants.CreatedDateTime);
                            newTime.CreatedBy = GetGuidValue(reader, DatabaseConstants.CreatedBy);
                            newTime.UpdatedDateTime = GetDateTimeValue(reader, DatabaseConstants.UpdatedDateTime);
                            newTime.UpdatedBy = GetGuidValue(reader, DatabaseConstants.UpdatedBy);
                            newTime.DeletedDateTime = GetDateTimeValue(reader, DatabaseConstants.DeletedDateTime);
                            newTime.DeletedBy = GetGuidValue(reader, DatabaseConstants.DeletedBy);
                            result.Add(newTime);
                        }
                    }
                }
            }
            return result;
        }
        public override bool AddOrUpdate(object item)
        {
            var time = item as Time;

            var dalObject = new DALAddOrUpdate();
            dalObject.Table = $"{_dbTable}";
            dalObject.Columns = _pilotDatabaseInitiator.GetTimeColumns();

            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.SystemId).Value = time.SystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.ItemSystemId).Value = time.ItemSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.OrganizationSystemId).Value = time.OrganizationSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.TimeTypeSystemId).Value = time.TimeTypeSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.TimeStatusSystemId).Value = time.TimeStatusSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.PersonSystemId).Value = time.PersonSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.TimeComment).Value = time.TimeComment;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.TimeFrom).Value = time.TimeFrom;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.TimeTo).Value = time.TimeTo;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.TimeAmount).Value = time.Amount;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.TimeRisk).Value = time.Risk;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.CreatedDateTime).Value = time.CreatedDateTime;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.CreatedBy).Value = time.CreatedBy;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.UpdatedDateTime).Value = time.UpdatedDateTime;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.UpdatedBy).Value = time.UpdatedBy;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.DeletedDateTime).Value = time.DeletedDateTime;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.DeletedBy).Value = time.DeletedBy;

            return base.AddOrUpdate(dalObject);
        }
    }
}
