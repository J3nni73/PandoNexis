using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PandoNexis.AddOns.Extensions.Pilot.Definitions;
using PandoNexis.AddOns.Extensions.Pilot.Objects;
using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal;
using System.Data;
using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.Services;
using DocumentFormat.OpenXml.Drawing;

namespace PandoNexis.AddOns.Extensions.Pilot.Services
{
    [Service(ServiceType = typeof(PilotTimeDALService))]
    public class PilotTimeDALService
    {
        private readonly IConfiguration _configuration;

        public PilotTimeDALService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Time> GetTime()
        {
            var result = new List<Time>();

            var sql = $"select * from  {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.Time}" + Environment.NewLine;

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
                            newTime.SystemId = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.SystemId)) ? reader.GetGuid(reader.GetOrdinal(PilotConstants.SystemId)) : Guid.Empty;
                            newTime.ItemSystemId = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.ItemSystemId)) ? reader.GetGuid(reader.GetOrdinal(PilotConstants.ItemSystemId)) : Guid.Empty;
                            newTime.OrganizationSystemId = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.OrganizationSystemId)) ? reader.GetGuid(reader.GetOrdinal(PilotConstants.OrganizationSystemId)) : Guid.Empty;
                            newTime.TimeType = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.TimeType)) ? reader.GetString(reader.GetOrdinal(PilotConstants.TimeType)) : string.Empty;
                            newTime.TimeComment = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.TimeComment)) ? reader.GetString(reader.GetOrdinal(PilotConstants.TimeComment)) : string.Empty;
                            newTime.TimeFrom = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.TimeFrom)) ? reader.GetDateTime(reader.GetOrdinal(PilotConstants.TimeFrom)) : DateTime.MinValue;
                            newTime.TimeTo = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.TimeTo)) ? reader.GetDateTime(reader.GetOrdinal(PilotConstants.TimeTo)) : DateTime.MinValue;
                            newTime.Amount = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.TimeAmount)) ? reader.GetInt32(reader.GetOrdinal(PilotConstants.TimeAmount)) : 0;
                            newTime.Risk = !reader.IsDBNull(reader.GetOrdinal(PilotConstants.TimeRisk)) ? reader.GetInt32(reader.GetOrdinal(PilotConstants.TimeRisk)) : 0;
                            newTime.CreatedDateTime = !reader.IsDBNull(reader.GetOrdinal(DatabaseConstants.CreatedDateTime)) ? reader.GetDateTime(reader.GetOrdinal(DatabaseConstants.CreatedDateTime)) : DateTime.MinValue;
                            newTime.CreatedBy = !reader.IsDBNull(reader.GetOrdinal(DatabaseConstants.CreatedBy)) ? reader.GetGuid(reader.GetOrdinal(DatabaseConstants.CreatedBy)) : Guid.Empty;
                            newTime.UpdatedDateTime = !reader.IsDBNull(reader.GetOrdinal(DatabaseConstants.UpdatedDateTime)) ? reader.GetDateTime(reader.GetOrdinal(DatabaseConstants.UpdatedDateTime)) : DateTime.MinValue;
                            newTime.UpdatedBy = !reader.IsDBNull(reader.GetOrdinal(DatabaseConstants.UpdatedBy)) ? reader.GetGuid(reader.GetOrdinal(DatabaseConstants.UpdatedBy)) : Guid.Empty;
                            newTime.DeletedDateTime = !reader.IsDBNull(reader.GetOrdinal(DatabaseConstants.DeletedDateTime)) ? reader.GetDateTime(reader.GetOrdinal(DatabaseConstants.DeletedDateTime)) : DateTime.MinValue;
                            newTime.DeletedBy = !reader.IsDBNull(reader.GetOrdinal(DatabaseConstants.DeletedBy)) ? reader.GetGuid(reader.GetOrdinal(DatabaseConstants.DeletedBy)) : Guid.Empty;
                            result.Add(newTime);
                        }
                    }
                }
            }
            return result;
        }
        public bool AddOrUpdateTime(Time time)
        {
            time.UpdatedDateTime = DateTime.Now;
            var sql = $"Declare @rowcount int" + Environment.NewLine;
            //update
            sql += $"Update {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.Time}" + Environment.NewLine;
            sql += $"set" + Environment.NewLine;

            sql += $"{PilotConstants.ItemSystemId}='{time.ItemSystemId}'" + Environment.NewLine;
            sql += $",{PilotConstants.OrganizationSystemId}='{time.OrganizationSystemId}'" + Environment.NewLine;

            sql += $",{PilotConstants.TimeType}='{time.TimeType}'" + Environment.NewLine;
            sql += $",{PilotConstants.TimeComment}='{time.TimeComment}'" + Environment.NewLine;
            sql += $",{PilotConstants.TimeFrom}='{time.TimeFrom}'" + Environment.NewLine;
            sql += $",{PilotConstants.TimeTo}='{time.TimeTo}'" + Environment.NewLine;

            sql += $",{PilotConstants.TimeAmount}={time.Amount}" + Environment.NewLine;
            sql += $",{PilotConstants.TimeRisk}={time.Risk}" + Environment.NewLine;

            sql += $",{DatabaseConstants.UpdatedDateTime}='{time.UpdatedDateTime}'" + Environment.NewLine;
            sql += $",{DatabaseConstants.UpdatedBy}='{time.UpdatedBy}'" + Environment.NewLine;
            if (time.DeletedDateTime != null && time.DeletedDateTime != DateTime.MinValue)
            {
                sql += $",{DatabaseConstants.DeletedDateTime}='{time.DeletedDateTime}'" + Environment.NewLine;
                sql += $",{DatabaseConstants.DeletedBy}='{time.DeletedBy}'" + Environment.NewLine;
            }
            sql += $"where {PilotConstants.SystemId}='{time.SystemId}'" + Environment.NewLine;
            sql += $"select @rowcount = @@rowcount" + Environment.NewLine;
            sql += $"if @rowcount > 0" + Environment.NewLine;
            sql += $"begin" + Environment.NewLine;
            sql += $"select @rowcount" + Environment.NewLine;
            sql += $"end" + Environment.NewLine;
            sql += $"else" + Environment.NewLine;
            //insert
            sql += $"begin" + Environment.NewLine;
            sql += $"insert into {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.Time}" + Environment.NewLine;
            sql += $"(" + Environment.NewLine;
            sql += $"{PilotConstants.SystemId}" + Environment.NewLine;
            sql += $",{PilotConstants.ItemSystemId}" + Environment.NewLine;
            sql += $",{PilotConstants.OrganizationSystemId}" + Environment.NewLine;
            sql += $",{PilotConstants.TimeType}" + Environment.NewLine;
            sql += $",{PilotConstants.TimeComment}" + Environment.NewLine;
            sql += $",{PilotConstants.TimeFrom}" + Environment.NewLine;
            sql += $",{PilotConstants.TimeTo}" + Environment.NewLine;
            sql += $",{PilotConstants.TimeAmount}" + Environment.NewLine;
            sql += $",{PilotConstants.TimeRisk}" + Environment.NewLine;
            sql += $",{DatabaseConstants.CreatedDateTime}" + Environment.NewLine;
            sql += $",{DatabaseConstants.CreatedBy}" + Environment.NewLine;
            sql += $",{DatabaseConstants.UpdatedDateTime}" + Environment.NewLine;
            sql += $",{DatabaseConstants.UpdatedBy}" + Environment.NewLine;
            if (time.DeletedDateTime != null && time.DeletedDateTime != DateTime.MinValue)
            {
                sql += $",{DatabaseConstants.DeletedDateTime}" + Environment.NewLine;
                sql += $",{DatabaseConstants.DeletedBy}" + Environment.NewLine;
            }
            sql += $")" + Environment.NewLine;
            sql += $"values(" + Environment.NewLine;
            sql += $"'{time.SystemId}'" + Environment.NewLine;
            sql += $",'{time.ItemSystemId}'" + Environment.NewLine;
            sql += $",'{time.OrganizationSystemId}'" + Environment.NewLine;
            sql += $",'{time.TimeType}'" + Environment.NewLine;
            sql += $",'{time.TimeComment}'" + Environment.NewLine;
            sql += $",'{time.TimeFrom}'" + Environment.NewLine;
            sql += $",'{time.TimeTo}'" + Environment.NewLine;
            sql += $",{time.Amount}" + Environment.NewLine;
            sql += $",{time.Risk}" + Environment.NewLine;
            sql += $",'{time.CreatedDateTime}'" + Environment.NewLine;
            sql += $",'{time.CreatedBy}'" + Environment.NewLine;
            sql += $",'{time.UpdatedDateTime}'" + Environment.NewLine;
            sql += $",'{time.UpdatedBy}'" + Environment.NewLine;
            if (time.DeletedDateTime != null && time.DeletedDateTime != DateTime.MinValue)
            {
                sql += $",'{time.DeletedDateTime}'" + Environment.NewLine;
                sql += $",'{time.DeletedBy}'" + Environment.NewLine;
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
