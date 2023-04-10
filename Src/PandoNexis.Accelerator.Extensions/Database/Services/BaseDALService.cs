using IdentityModel;
using Litium.Runtime.DependencyInjection;
using Litium.Web.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using PandoNexis.Accelerator.Extensions.Database.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.Database.Services
{
    [Service(ServiceType = typeof(BaseDALService))]
    public abstract class BaseDALService
    {
        private readonly DateTime _dbDateTimeMinValue = DateTime.Parse("1900-01-01 00:00");
        protected readonly IConfiguration _configuration;
        public BaseDALService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public abstract IEnumerable<object> GetAll();
        public virtual bool AddOrUpdate(object item)
        {
            var dalObject = item as DALAddOrUpdate;

            if (dalObject != null)
            {
                var sql = $"Declare @rowcount int" + Environment.NewLine;
                //update
                sql += $"Update {dalObject.Table}" + Environment.NewLine;
                sql += $"set" + Environment.NewLine;
                var firstInstance = true;
                foreach (var column in dalObject.Columns.Where(i => !i.IsIdentity))
                {
                    switch (column.Type)
                    {
                        case DatabaseTypeConstants.Decimal:
                            var value = column?.Value?.ToString()?.Replace(",", ".") ?? "0";

                            if (firstInstance)
                            {
                                sql += $"{column.Name}={value}" + Environment.NewLine;
                            }
                            else
                            {
                                sql += $",{column.Name}={value}" + Environment.NewLine;
                            }
                            break;
                        case DatabaseTypeConstants.Int:
                        case DatabaseTypeConstants.Bit:
                        case DatabaseTypeConstants.BigInt:
                            if (firstInstance)
                            {
                                sql += $"{column.Name}={column.Value}" + Environment.NewLine;
                            }
                            else
                            {
                                sql += $",{column.Name}={column.Value}" + Environment.NewLine;
                            }
                            break;
                        case DatabaseTypeConstants.DateTime:
                            if (column.Name == DatabaseConstants.UpdatedDateTime)
                            {
                                column.Value = DateTime.Now;
                            }
                            else
                            {
                                if (column.Value != null)
                                {
                                    if (Convert.ToDateTime(column.Value) == DateTime.MinValue)
                                    {
                                        column.Value = _dbDateTimeMinValue;
                                    }
                                }
                                else
                                {
                                    column.Value = _dbDateTimeMinValue;
                                }
                            }

                            if (firstInstance)
                            {
                                sql += $"{column.Name}='{column.Value}'" + Environment.NewLine;
                                firstInstance = false;
                            }
                            else
                            {
                                sql += $",{column.Name}='{column.Value}'" + Environment.NewLine;
                            }
                            break;
                        case DatabaseTypeConstants.UniqueIdentifier:
                            if (column.Value == null)
                                column.Value = Guid.Empty;

                            if (firstInstance)
                            {
                                sql += $"{column.Name}='{column.Value}'" + Environment.NewLine;
                                firstInstance = false;
                            }
                            else
                            {
                                sql += $",{column.Name}='{column.Value}'" + Environment.NewLine;
                            }
                            break;
                        default:
                            if (firstInstance)
                            {
                                sql += $"{column.Name}='{column.Value}'" + Environment.NewLine;
                                firstInstance = false;
                            }
                            else
                            {
                                sql += $",{column.Name}='{column.Value}'" + Environment.NewLine;
                            }
                            break;
                    }
                }
                firstInstance = true;
                foreach (var column in dalObject.Columns.Where(i => i.IsIdentity))
                {
                    if (firstInstance)
                    {
                        sql += $"where {column.Name}='{column.Value}'" + Environment.NewLine;
                        firstInstance = false;
                    }
                    else
                    {
                        sql += $"and {column.Name}='{column.Value}'" + Environment.NewLine;
                    }
                }
                sql += $"select @rowcount = @@rowcount" + Environment.NewLine;
                sql += $"if @rowcount > 0" + Environment.NewLine;
                sql += $"begin" + Environment.NewLine;
                sql += $"select @rowcount" + Environment.NewLine;
                sql += $"end" + Environment.NewLine;
                sql += $"else" + Environment.NewLine;

                //insert
                sql += $"begin" + Environment.NewLine;
                sql += $"insert into {dalObject.Table}" + Environment.NewLine;
                sql += $"(" + Environment.NewLine;
                firstInstance = true;
                foreach (var column in dalObject.Columns)
                {
                    if (firstInstance)
                    {
                        sql += $"{column.Name}" + Environment.NewLine;
                        firstInstance = false;
                    }
                    else
                    {
                        sql += $",{column.Name}" + Environment.NewLine;
                    }
                }
                sql += $")" + Environment.NewLine;

                sql += $"values(" + Environment.NewLine;

                firstInstance = true;
                foreach (var column in dalObject.Columns)
                {

                    switch (column.Type)
                    {
                        case DatabaseTypeConstants.Decimal:
                            var value = column?.Value?.ToString()?.Replace(",", ".") ?? "0";
                            if (firstInstance)
                            {
                                sql += $"{value}" + Environment.NewLine;
                                firstInstance = false;
                            }
                            else
                            {

                                sql += $",{value}" + Environment.NewLine;
                            }
                            break;
                        case DatabaseTypeConstants.Int:
                        case DatabaseTypeConstants.Bit:
                        case DatabaseTypeConstants.BigInt:
                            if (firstInstance)
                            {
                                sql += $"{column.Value}" + Environment.NewLine;
                                firstInstance = false;
                            }
                            else
                            {

                                sql += $",{column.Value}" + Environment.NewLine;
                            }
                            break;
                        case DatabaseTypeConstants.DateTime:
                            if (column.Name == DatabaseConstants.CreatedDateTime || column.Name == DatabaseConstants.UpdatedDateTime)
                            {
                                column.Value = DateTime.Now;
                            }
                            else
                            {
                                if (column.Value != null)
                                {
                                    if (Convert.ToDateTime(column.Value) == DateTime.MinValue)
                                    {
                                        column.Value = _dbDateTimeMinValue;
                                    }
                                }
                                else
                                {
                                    column.Value = _dbDateTimeMinValue;
                                }
                            }
                            if (firstInstance)
                            {
                                sql += $"'{column.Value}'" + Environment.NewLine;
                                firstInstance = false;
                            }
                            else
                            {

                                sql += $",'{column.Value}'" + Environment.NewLine;
                            }
                            break;
                        case DatabaseTypeConstants.UniqueIdentifier:
                            if (column.Value == null)
                                column.Value = Guid.Empty;

                            if (firstInstance)
                            {
                                sql += $"'{column.Value}'" + Environment.NewLine;
                                firstInstance = false;
                            }
                            else
                            {

                                sql += $",'{column.Value}'" + Environment.NewLine;
                            }
                            break;
                        default:
                            if (firstInstance)
                            {
                                sql += $"'{column.Value}'" + Environment.NewLine;
                                firstInstance = false;
                            }
                            else
                            {

                                sql += $",'{column.Value}'" + Environment.NewLine;
                            }
                            break;
                    }
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
            }
            return false;


        }
        public Guid GetGuidValue(SqlDataReader reader, string columnName)
        {
            return !reader.IsDBNull(reader.GetOrdinal(columnName)) ? reader.GetGuid(reader.GetOrdinal(columnName)) : Guid.Empty;
        }
        public string GetStringValue(SqlDataReader reader, string columnName)
        {
            return !reader.IsDBNull(reader.GetOrdinal(columnName)) ? reader.GetString(reader.GetOrdinal(columnName)) : string.Empty;
        }
        public DateTime GetDateTimeValue(SqlDataReader reader, string columnName)
        {
            var result = !reader.IsDBNull(reader.GetOrdinal(columnName)) ? reader.GetDateTime(reader.GetOrdinal(columnName)) : DateTime.MinValue;
            if (result == _dbDateTimeMinValue)
                return DateTime.MinValue;
            else
                return result;
        }

        public decimal GetDecimalValue(SqlDataReader reader, string columnName)
        {
            return !reader.IsDBNull(reader.GetOrdinal(columnName)) ? reader.GetDecimal(reader.GetOrdinal(columnName)) : decimal.Zero;
        }
        public int GetIntValue(SqlDataReader reader, string columnName)
        {
            return !reader.IsDBNull(reader.GetOrdinal(columnName)) ? reader.GetInt32(reader.GetOrdinal(columnName)) : 0;
        }



    }

    public class DALAddOrUpdate
    {
        public string Table { get; set; }
        public List<DatabaseColumns> Columns { get; set; }


    }
}
