using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Solution.Extensions.PNPilot.Definitions;
using Solution.Extensions.PNPilot.Objects;
using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal;
using System.Data;
using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.Services;
using Solution.Extensions.PNPilot.Constants;

namespace Solution.Extensions.PNPilot.Services
{
    [Service(ServiceType = typeof(PilotItemDALService))]
    public class PilotItemDALService
    {
        private readonly IConfiguration _configuration;

        public PilotItemDALService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Item> GetItems()
        {
            var result = new List<Item>();

            var sql = $"select * from  {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.Item}" + Environment.NewLine;

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
                            var newItem = new Item();
                            newItem.SystemId = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.SystemId)) ? reader.GetGuid(reader.GetOrdinal(PilotConstants.SystemId)) : Guid.Empty;
                            newItem.OrganizationSystemId = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.OrganizationSystemId)) ? reader.GetGuid(reader.GetOrdinal(PilotConstants.OrganizationSystemId)) : Guid.Empty;
                            newItem.ParentSystemId = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.ParentSystemId)) ? reader.GetGuid(reader.GetOrdinal(PilotConstants.ParentSystemId)) : Guid.Empty;
                            newItem.ItemTitle = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.ItemTitle)) ? reader.GetString(reader.GetOrdinal(PilotConstants.ItemTitle)) : string.Empty;
                            newItem.ItemDescription = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.ItemDescription)) ? reader.GetString(reader.GetOrdinal(PilotConstants.ItemDescription)) : string.Empty;
                            newItem.ItemType = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.ItemType)) ? reader.GetString(reader.GetOrdinal(PilotConstants.ItemType)) : string.Empty;
                            newItem.ItemStatus = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.ItemStatus)) ? reader.GetString(reader.GetOrdinal(PilotConstants.ItemStatus)) : string.Empty;
                            newItem.DueDateTime = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.DueDateTime)) ? reader.GetDateTime(reader.GetOrdinal(PilotConstants.DueDateTime)) : DateTime.MinValue;
                            newItem.CreatedDateTime = !reader.IsDBNull(reader.GetOrdinal(DatabaseConstants.CreatedDateTime)) ? reader.GetDateTime(reader.GetOrdinal(DatabaseConstants.CreatedDateTime)) : DateTime.MinValue;
                            newItem.CreatedBy = !reader.IsDBNull(reader.GetOrdinal(DatabaseConstants.CreatedBy)) ? reader.GetGuid(reader.GetOrdinal(DatabaseConstants.CreatedBy)) : Guid.Empty;
                            newItem.UpdatedDateTime = !reader.IsDBNull(reader.GetOrdinal(DatabaseConstants.UpdatedDateTime)) ? reader.GetDateTime(reader.GetOrdinal(DatabaseConstants.UpdatedDateTime)) : DateTime.MinValue;
                            newItem.UpdatedBy = !reader.IsDBNull(reader.GetOrdinal(DatabaseConstants.UpdatedBy)) ? reader.GetGuid(reader.GetOrdinal(DatabaseConstants.UpdatedBy)) : Guid.Empty;
                            newItem.DeletedDateTime = !reader.IsDBNull(reader.GetOrdinal(DatabaseConstants.DeletedDateTime)) ? reader.GetDateTime(reader.GetOrdinal(DatabaseConstants.DeletedDateTime)) : DateTime.MinValue;
                            newItem.DeletedBy = !reader.IsDBNull(reader.GetOrdinal(DatabaseConstants.DeletedBy)) ? reader.GetGuid(reader.GetOrdinal(DatabaseConstants.DeletedBy)) : Guid.Empty;
                            result.Add(newItem);
                        }
                    }
                }
            }
            return result;
        }
        public bool AddOrUpdateItem(Item item)
        {
            item.UpdatedDateTime = DateTime.Now;
            var sql = $"Declare @rowcount int" + Environment.NewLine;
            //update
            sql += $"Update {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.Item}" + Environment.NewLine;
            sql += $"set" + Environment.NewLine;
            sql += $"{PilotConstants.OrganizationSystemId}='{item.OrganizationSystemId}'" + Environment.NewLine;
            sql += $",{PilotConstants.ParentSystemId}='{item.ParentSystemId}'" + Environment.NewLine;
            sql += $",{PilotConstants.ItemTitle}='{item.ItemTitle}'" + Environment.NewLine;
            sql += $",{PilotConstants.ItemDescription}='{item.ItemDescription}'" + Environment.NewLine;
            sql += $",{PilotConstants.ItemType}='{item.ItemType}'" + Environment.NewLine;
            sql += $",{PilotConstants.ItemStatus}='{item.ItemStatus}'" + Environment.NewLine;
            sql += $",{DatabaseConstants.UpdatedDateTime}='{item.UpdatedDateTime}'" + Environment.NewLine;
            sql += $",{DatabaseConstants.UpdatedBy}='{item.UpdatedBy}'" + Environment.NewLine;
            if (item.DeletedDateTime != null && item.DeletedDateTime != DateTime.MinValue)
            {
                sql += $",{DatabaseConstants.DeletedDateTime}='{item.DeletedDateTime}'" + Environment.NewLine;
                sql += $",{DatabaseConstants.DeletedBy}='{item.DeletedBy}'" + Environment.NewLine;
            }
            sql += $"where {PilotConstants.SystemId}='{item.SystemId}'" + Environment.NewLine;
            sql += $"select @rowcount = @@rowcount" + Environment.NewLine;
            sql += $"if @rowcount > 0" + Environment.NewLine;
            sql += $"begin" + Environment.NewLine;
            sql += $"select @rowcount" + Environment.NewLine;
            sql += $"end" + Environment.NewLine;
            sql += $"else" + Environment.NewLine;
            //insert
            sql += $"begin" + Environment.NewLine;
            sql += $"insert into {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.Item}" + Environment.NewLine;
            sql += $"(" + Environment.NewLine;
            sql += $"{PilotConstants.SystemId}" + Environment.NewLine;
            sql += $",{PilotConstants.OrganizationSystemId}" + Environment.NewLine;
            sql += $",{PilotConstants.ParentSystemId}" + Environment.NewLine;
            sql += $",{PilotConstants.ItemTitle}" + Environment.NewLine;
            sql += $",{PilotConstants.ItemDescription}" + Environment.NewLine;
            sql += $",{PilotConstants.ItemType}" + Environment.NewLine;
            sql += $",{PilotConstants.ItemStatus}" + Environment.NewLine;
            sql += $",{DatabaseConstants.CreatedDateTime}" + Environment.NewLine;
            sql += $",{DatabaseConstants.CreatedBy}" + Environment.NewLine;
            sql += $",{DatabaseConstants.UpdatedDateTime}" + Environment.NewLine;
            sql += $",{DatabaseConstants.UpdatedBy}" + Environment.NewLine;
            if (item.DeletedDateTime != null && item.DeletedDateTime != DateTime.MinValue)
            {
                sql += $",{DatabaseConstants.DeletedDateTime}" + Environment.NewLine;
                sql += $",{DatabaseConstants.DeletedBy}" + Environment.NewLine;
            }
            sql += $")" + Environment.NewLine;
            sql += $"values(" + Environment.NewLine;
            sql += $"'{item.SystemId}'" + Environment.NewLine;
            sql += $",'{item.OrganizationSystemId}'" + Environment.NewLine;
            sql += $",'{item.ParentSystemId}'" + Environment.NewLine;
            sql += $",'{item.ItemTitle}'" + Environment.NewLine;
            sql += $",'{item.ItemDescription}'" + Environment.NewLine;
            sql += $",'{item.ItemType}'" + Environment.NewLine;
            sql += $",'{item.ItemStatus}'" + Environment.NewLine;
            sql += $",'{item.CreatedDateTime}'" + Environment.NewLine;
            sql += $",'{item.CreatedBy}'" + Environment.NewLine;
            sql += $",'{item.UpdatedDateTime}'" + Environment.NewLine;
            sql += $",'{item.UpdatedBy}'" + Environment.NewLine;
            if (item.DeletedDateTime != null && item.DeletedDateTime != DateTime.MinValue)
            {
                sql += $",'{item.DeletedDateTime}'" + Environment.NewLine;
                sql += $",'{item.DeletedBy}'" + Environment.NewLine;
            }
            sql += $")" + Environment.NewLine;
            sql += $"select @rowcount = @@rowcount" + Environment.NewLine;
            sql += $"select @rowcount" + Environment.NewLine;
            sql += $"end" + Environment.NewLine;

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
                            if (reader.GetInt32(0) > 0)
                                return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
