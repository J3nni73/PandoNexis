using Microsoft.Data.SqlClient;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using Microsoft.Extensions.Configuration;
using Solution.Extensions.PNPilot.Objects;
using Litium.Runtime.DependencyInjection;
using Solution.Extensions.PNPilot.Constants;
using PandoNexis.Accelerator.Extensions.Database.Services;
using Solution.Extensions.PNPilot.Definitions;
using DocumentFormat.OpenXml.Office2013.Excel;

namespace Solution.Extensions.PNPilot.Services.DALServices
{
    [Service(ServiceType = typeof(ItemTypeDALService))]
    public class ItemTypeDALService : BaseDALService
    {
        private readonly PilotDatabaseInitiator _pilotDatabaseInitiator;
        private readonly string _dbTable = $"{DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.ItemType}";
        public ItemTypeDALService(IConfiguration configuration) : base(configuration)
        {
            _pilotDatabaseInitiator = new PilotDatabaseInitiator(configuration);
        }

        public override IEnumerable<ItemType> GetAll()
        {
            var result = new List<ItemType>();

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
                            var newTimeType = new ItemType();
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
            var itemType = item as ItemType;

        

            var dalObject = new DALAddOrUpdate();
            dalObject.Table = $"{_dbTable}";
            dalObject.Columns = _pilotDatabaseInitiator.GetItemColumns();

            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.SystemId).Value = itemType.SystemId;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.Name).Value = itemType.Name;
            dalObject.Columns.FirstOrDefault(i => i.Name == PilotConstants.Description).Value = itemType.Description;

            return base.AddOrUpdate(dalObject);
        }
    }
}
