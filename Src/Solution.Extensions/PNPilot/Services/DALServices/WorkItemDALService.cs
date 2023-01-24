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
using PandoNexis.Accelerator.Extensions.Database.Services;

namespace Solution.Extensions.PNPilot.Services.DALServices
{
    [Service(ServiceType = typeof(WorkItemDALService))]
    public class WorkItemDALService : BaseDALService
    {
        private readonly IConfiguration _configuration;

        public WorkItemDALService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override IEnumerable<WorkItem> GetAll()
        {
            var result = new List<WorkItem>();

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
                            var newItem = new WorkItem();
                            newItem.SystemId = GetGuidValue(reader, PilotConstants.SystemId);
                            newItem.OrganizationSystemId = GetGuidValue(reader, PilotConstants.OrganizationSystemId);
                            newItem.ParentSystemId = GetGuidValue(reader, PilotConstants.ParentSystemId);
                            newItem.ItemTitle = GetStringValue(reader, PilotConstants.ItemTitle);
                            newItem.ItemDescription = GetStringValue(reader, PilotConstants.ItemDescription);
                            newItem.ItemType = GetStringValue(reader, PilotConstants.ItemType);
                            newItem.ItemStatus = GetStringValue(reader, PilotConstants.ItemStatus);
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
        public bool AddOrUpdateItem(WorkItem item)
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



        public override bool AddOrUpdate(object item)
        {
            throw new NotImplementedException();
        }
    }
}
