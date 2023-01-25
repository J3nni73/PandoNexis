using Microsoft.Data.SqlClient;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using Microsoft.Extensions.Configuration;
using Solution.Extensions.PNPilot.Definitions;
using Solution.Extensions.PNPilot.Objects;
using Litium.Runtime.DependencyInjection;
using Solution.Extensions.PNPilot.Constants;
using PandoNexis.Accelerator.Extensions.Database.Services;
namespace Solution.Extensions.PNPilot.Services.DALServices
{
    [Service(ServiceType = typeof(WorkItemDALService))]
    public class WorkItemDALService : BaseDALService
    {
        private readonly PilotDatabaseInitiator _pilotDatabaseInitiator;
        private readonly string _dbTable = $"{DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.Item}";
        public WorkItemDALService(IConfiguration configuration) : base(configuration)
        {
            _pilotDatabaseInitiator = new PilotDatabaseInitiator(configuration);
        }

        public override IEnumerable<WorkItem> GetAll()
        {
            var result = new List<WorkItem>();

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
                            var newItem = new WorkItem();
                            newItem.SystemId = GetGuidValue(reader, PilotConstants.SystemId);
                            newItem.OrganizationSystemId = GetGuidValue(reader, PilotConstants.OrganizationSystemId);
                            newItem.ParentSystemId = GetGuidValue(reader, PilotConstants.ParentSystemId);
                            newItem.ItemTitle = GetStringValue(reader, PilotConstants.ItemTitle);
                            newItem.ItemDescription = GetStringValue(reader, PilotConstants.ItemDescription);
                            newItem.ItemTypeSystemId = GetGuidValue(reader, PilotConstants.ItemTypeSystemId);
                            newItem.ItemStatusSystemId = GetGuidValue(reader, PilotConstants.ItemStatusSystemId);
                            newItem.DueDateTime = GetDateTimeValue(reader, PilotConstants.DueDateTime);
                            newItem.CreatedDateTime = GetDateTimeValue(reader, DatabaseConstants.CreatedDateTime);
                            newItem.CreatedBy = GetGuidValue(reader, DatabaseConstants.CreatedBy);
                            newItem.UpdatedDateTime = GetDateTimeValue(reader, DatabaseConstants.UpdatedDateTime);
                            newItem.UpdatedBy = GetGuidValue(reader, DatabaseConstants.UpdatedBy);
                            newItem.DeletedDateTime = GetDateTimeValue(reader, DatabaseConstants.DeletedDateTime);
                            newItem.DeletedBy = GetGuidValue(reader, DatabaseConstants.DeletedBy);
                            result.Add(newItem);
                        }
                    }
                }
            }
            return result;
        }
     
        public override bool AddOrUpdate(object workItem)
        {
            var item = workItem as WorkItem; 
            item.UpdatedDateTime = DateTime.Now;

            var dalObject = new DALAddOrUpdate();
            dalObject.Table = $"{_dbTable}";
            dalObject.Columns = _pilotDatabaseInitiator.GetItemColumns();
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.SystemId).Value = item.SystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.OrganizationSystemId).Value = item.OrganizationSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.ParentSystemId).Value = item.ParentSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.ItemTitle).Value = item.ItemTitle;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.ItemDescription).Value = item.ItemDescription;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.ItemTypeSystemId).Value = item.ItemTypeSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.ItemStatusSystemId).Value = item.ItemStatusSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.DueDateTime).Value = item.DueDateTime;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.CreatedDateTime).Value = item.CreatedDateTime;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.CreatedBy).Value = item.CreatedBy;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.UpdatedDateTime).Value = item.UpdatedDateTime;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.UpdatedBy).Value = item.UpdatedBy;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.DeletedDateTime).Value = item.DeletedDateTime;
            dalObject.Columns.FirstOrDefault(i => i.Name == DatabaseConstants.DeletedBy).Value = item.DeletedBy;

            return base.AddOrUpdate(dalObject);
        }
    }

}
