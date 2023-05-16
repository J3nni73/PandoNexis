using Litium.Runtime.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandoNexis.Accelerator.Extensions.Database.Objects;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using MetadataExtractor.Formats.Xmp;
using Litium.Web.Security;

namespace PandoNexis.Accelerator.Extensions.Database.Services
{

    [Service(ServiceType = typeof(DatabaseInitiator))]
    public abstract class DatabaseInitiator
    {
        private readonly IConfiguration _configuration;

        public DatabaseInitiator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public abstract void GetCheckDatabaseObjects();

        public void SyncronizeDatabaseObjects(string tableName, List<DatabaseColumns> columns)
        {

            var inDatabaseColumns = new List<DatabaseColumns>();
            if (!TableExists(tableName, out inDatabaseColumns))
            {
                CreateTable(tableName, columns);
            }
            else if (inDatabaseColumns.Any())
            {
                UpdateTableToLatestVersion(tableName, columns, inDatabaseColumns);
            }
        }

        public DatabaseColumns GetColumn(string name, string type, string attribute, bool isIdentity = false)
        {
            return new DatabaseColumns()
            {
                Name = name,
                Type = type,
                Attribute = attribute,
                IsIdentity = isIdentity
            };
        }

        public List<DatabaseColumns> GetEditedColumns()
        {
            return new List<DatabaseColumns>
            {
                GetColumn(DatabaseConstants.CreatedDateTime, DatabaseTypeConstants.DateTime, DatabaseTypeConstants.NotNull),
                GetColumn(DatabaseConstants.CreatedBy, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),

                GetColumn(DatabaseConstants.UpdatedDateTime, DatabaseTypeConstants.DateTime, DatabaseTypeConstants.Null),
                GetColumn(DatabaseConstants.UpdatedBy, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.Null),

                GetColumn(DatabaseConstants.DeletedDateTime, DatabaseTypeConstants.DateTime, DatabaseTypeConstants.Null),
                GetColumn(DatabaseConstants.DeletedBy, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.Null),
            };
        }
        public List<DatabaseColumns> GetFieldDataColumns()
        {
            var result = new List<DatabaseColumns>
            {
                GetColumn(DatabaseFieldDataConstants.OwnerSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(DatabaseFieldDataConstants.FieldDefinitionId, DatabaseTypeConstants.Varchar100, DatabaseTypeConstants.NotNull),
                GetColumn(DatabaseFieldDataConstants.Culture, DatabaseTypeConstants.Varchar50, DatabaseTypeConstants.NotNull),
                GetColumn(DatabaseFieldDataConstants.Index, DatabaseTypeConstants.Int, DatabaseTypeConstants.NotNull),
                GetColumn(DatabaseFieldDataConstants.BooleanValue, DatabaseTypeConstants.Bit, DatabaseTypeConstants.Null),
                GetColumn(DatabaseFieldDataConstants.DateTimeValue, DatabaseTypeConstants.DateTime, DatabaseTypeConstants.Null),
                GetColumn(DatabaseFieldDataConstants.DecimalValue, DatabaseTypeConstants.Decimal, DatabaseTypeConstants.Null),
                GetColumn(DatabaseFieldDataConstants.GuidValue, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.Null),
                GetColumn(DatabaseFieldDataConstants.IndexedTextValue, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.Null),
                GetColumn(DatabaseFieldDataConstants.IntValue, DatabaseTypeConstants.Int, DatabaseTypeConstants.Null),
                GetColumn(DatabaseFieldDataConstants.JsonValue, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.Null),
                GetColumn(DatabaseFieldDataConstants.LongValue, DatabaseTypeConstants.BigInt, DatabaseTypeConstants.Null),
                GetColumn(DatabaseFieldDataConstants.TextValue, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.Null),
                GetColumn(DatabaseFieldDataConstants.ChildOwnerId, DatabaseTypeConstants.Varchar100, DatabaseTypeConstants.Null),
                GetColumn(DatabaseFieldDataConstants.ChildIndex, DatabaseTypeConstants.Int, DatabaseTypeConstants.NotNull),
            };
            result.AddRange(GetEditedColumns());
            return result;
        }


        public void UpdateTableToLatestVersion(string tableName, List<DatabaseColumns> columnsToExist, List<DatabaseColumns> currentColumns)
        {
            var columnsToCreate = new List<DatabaseColumns>();

            foreach (var column in columnsToExist)
            {
                if (currentColumns.FirstOrDefault(i => i.Name == column.Name) == null)
                {
                    columnsToCreate.Add(column);
                }
            }

            var sql = $"Alter TABLE {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{tableName}" + Environment.NewLine;

            sql += $"add " + Environment.NewLine;
            if (columnsToCreate.Any())
            {
                foreach (var column in columnsToCreate)
                {
                    if (columnsToCreate.IndexOf(column) != columnsToCreate.Count - 1)
                    {
                        sql += $"{column.Name} {column.Type} {column.Attribute.Replace(DatabaseTypeConstants.NotNull, DatabaseTypeConstants.Null)}, " + Environment.NewLine;
                    }
                    else
                    {
                        sql += $"{column.Name} {column.Type} {column.Attribute.Replace(DatabaseTypeConstants.NotNull, DatabaseTypeConstants.Null)}" + Environment.NewLine;
                    }
                }
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.ConnectionString = _configuration["Litium:Data:ConnectionString"];
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                        }
                    }
                }
            }
        }

        public void CreateTable(string tableName, List<DatabaseColumns> columnsToCreate)
        {
            var sql = $"CREATE TABLE {DatabaseConstants.Schema}.{DatabaseConstants.TablePrefix}{tableName}" + Environment.NewLine;
            sql += $"(" + Environment.NewLine;
            foreach (var column in columnsToCreate)
            {
                if (columnsToCreate.IndexOf(column) != columnsToCreate.Count - 1)
                {
                    sql += $"{column.Name} {column.Type} {column.Attribute}, " + Environment.NewLine;
                }
                else
                {
                    sql += $"{column.Name} {column.Type} {column.Attribute}" + Environment.NewLine;
                }
            }

            sql += $")";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = _configuration["Litium:Data:ConnectionString"];
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                    }
                }
            }
        }

        public bool TableExists(string tableName, out List<DatabaseColumns> columns)
        {
            columns = new List<DatabaseColumns>();
            var sql = $"select c.name from sys.all_objects o inner join sys.all_columns c on c.object_id = o.object_id where o.type = 'U' and o.name like '{DatabaseConstants.TablePrefix}{tableName}'";
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
                            columns.Add(new DatabaseColumns
                            {
                                Name = reader.GetString(0)
                            });
                        }
                    }
                }
            }
            if (columns.Any())
                return true;
            else
                return false;
        }

    }
}
