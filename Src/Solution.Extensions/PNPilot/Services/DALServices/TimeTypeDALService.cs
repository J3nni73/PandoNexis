using Microsoft.Data.SqlClient;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using Microsoft.Extensions.Configuration;
using Solution.Extensions.PNPilot.Objects;
using Litium.Runtime.DependencyInjection;
using Solution.Extensions.PNPilot.Constants;
using PandoNexis.Accelerator.Extensions.Database.Services;
using PandoNexis.Accelerator.Extensions.Database.Objects;

namespace Solution.Extensions.PNPilot.Services.DALServices
{
    [Service(ServiceType = typeof(TimeTypeDALService))]
    public class TimeTypeDALService : BaseDALService
    {
        private readonly IConfiguration _configuration;

        public TimeTypeDALService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool AddOrUpdateTimeType(TimeType timeType)
        {
            var sql = $"Declare @rowcount int" + Environment.NewLine;
            //update
            sql += $"Update {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.TimeType}" + Environment.NewLine;
            sql += $"set" + Environment.NewLine;

            sql += $",{PilotConstants.Name}='{timeType.Name}'" + Environment.NewLine;
            sql += $",{PilotConstants.Description}='{timeType.Description}'" + Environment.NewLine;
            sql += $"where {PilotConstants.SystemId}='{timeType.SystemId}'" + Environment.NewLine;
            sql += $"select @rowcount = @@rowcount" + Environment.NewLine;
            sql += $"if @rowcount > 0" + Environment.NewLine;
            sql += $"begin" + Environment.NewLine;
            sql += $"select @rowcount" + Environment.NewLine;
            sql += $"end" + Environment.NewLine;
            sql += $"else" + Environment.NewLine;
            //insert
            sql += $"begin" + Environment.NewLine;
            sql += $"insert into {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.TimeType}" + Environment.NewLine;
            sql += $"(" + Environment.NewLine;
            sql += $"{PilotConstants.SystemId}" + Environment.NewLine;
            sql += $",{PilotConstants.Name}" + Environment.NewLine;
            sql += $",{PilotConstants.Description}" + Environment.NewLine;
            sql += $")" + Environment.NewLine;
            sql += $"values(" + Environment.NewLine;
            sql += $"'{timeType.SystemId}'" + Environment.NewLine;
            sql += $",'{timeType.Name}'" + Environment.NewLine;
            sql += $",'{timeType.Description}'" + Environment.NewLine;
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

        public override IEnumerable<TimeType> GetAll()
        {
            var result = new List<TimeType>();

            var sql = $"select * from  {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.TimeType}" + Environment.NewLine;

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
            if (timeType != null)
            {

                var sql = $"Declare @rowcount int" + Environment.NewLine;
                //update
                sql += $"Update {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.TimeType}" + Environment.NewLine;
                sql += $"set" + Environment.NewLine;

                sql += $",{PilotConstants.Name}='{timeType.Name}'" + Environment.NewLine;
                sql += $",{PilotConstants.Description}='{timeType.Description}'" + Environment.NewLine;
                sql += $"where {PilotConstants.SystemId}='{timeType.SystemId}'" + Environment.NewLine;
                sql += $"select @rowcount = @@rowcount" + Environment.NewLine;
                sql += $"if @rowcount > 0" + Environment.NewLine;
                sql += $"begin" + Environment.NewLine;
                sql += $"select @rowcount" + Environment.NewLine;
                sql += $"end" + Environment.NewLine;
                sql += $"else" + Environment.NewLine;
                //insert
                sql += $"begin" + Environment.NewLine;
                sql += $"insert into {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{PilotConstants.TimeType}" + Environment.NewLine;
                sql += $"(" + Environment.NewLine;
                sql += $"{PilotConstants.SystemId}" + Environment.NewLine;
                sql += $",{PilotConstants.Name}" + Environment.NewLine;
                sql += $",{PilotConstants.Description}" + Environment.NewLine;
                sql += $")" + Environment.NewLine;
                sql += $"values(" + Environment.NewLine;
                sql += $"'{timeType.SystemId}'" + Environment.NewLine;
                sql += $",'{timeType.Name}'" + Environment.NewLine;
                sql += $",'{timeType.Description}'" + Environment.NewLine;
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
            }
            return false;
        }
    }
}
