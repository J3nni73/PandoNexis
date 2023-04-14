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
    [Service(ServiceType = typeof(ItemStatusDALService))]
    public class ItemStatusDALService : BaseDALService
    {
        private readonly PilotDatabaseInitiator _pilotDatabaseInitiator;
        private readonly string _dbTable = $"{DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.ItemStatus}";
        public ItemStatusDALService(IConfiguration configuration):base(configuration) 
        {
            _pilotDatabaseInitiator = new PilotDatabaseInitiator(configuration);
        }    

        public override IEnumerable<ItemStatus> GetAll()
        {
            var result = new List<ItemStatus>();

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
                            var newItemStatus = new ItemStatus();
                            newItemStatus.SystemId = GetGuidValue(reader, PilotConstants.SystemId);
                            newItemStatus.ItemTypeSystemId = GetGuidValue(reader,PilotConstants.ItemTypeSystemId);
                            newItemStatus.Name = GetStringValue(reader, PilotConstants.Name);
                            newItemStatus.Description = GetStringValue(reader, PilotConstants.Description);
                            result.Add(newItemStatus);
                        }
                    }
                }
            }
            return result;
        }

        public override bool AddOrUpdate(object item)
        {
            var itemStatus = item as ItemStatus;



            var dalObject = new DALAddOrUpdate();
            dalObject.Table = $"{_dbTable}";
            dalObject.Columns = _pilotDatabaseInitiator.GetItemColumns();

            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.SystemId).Value = itemStatus.SystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.ItemTypeSystemId).Value = itemStatus.ItemTypeSystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.Name).Value = itemStatus.Name;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.Description).Value = itemStatus.Description;

            return base.AddOrUpdate(dalObject);
        }
    }
}
