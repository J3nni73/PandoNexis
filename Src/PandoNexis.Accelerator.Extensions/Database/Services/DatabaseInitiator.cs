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
        public void UpdateTableToLatestVersion(string databaseName, List<DatabaseColumns> databaseColumnsToExist, List<DatabaseColumns> currentDatabaseColumns)
        {
            var columnsToCreate = new List<DatabaseColumns>();

            foreach (var column in databaseColumnsToExist)
            {
                if (currentDatabaseColumns.FirstOrDefault(i => i.Name == column.Name)==null)
                {
                    columnsToCreate.Add(column);
                }
            }

            var sql = $"Alter TABLE [{databaseName}]" + Environment.NewLine;

            sql += $"add " + Environment.NewLine;
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

        public void CreateTable(string databaseName, List<DatabaseColumns> databaseColumnsToCreate)
        {
            var sql = $"CREATE TABLE [{databaseName}]" + Environment.NewLine;
            sql += $"(" + Environment.NewLine;
            foreach (var column in databaseColumnsToCreate)
            {
                if (databaseColumnsToCreate.IndexOf(column) != databaseColumnsToCreate.Count - 1)
                {
                    sql += $"{column.Name} {column.Type} {column.Attribute}, " +  Environment.NewLine;
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

        public bool TableExists(string databaseName, out List<DatabaseColumns> databaseColumns)
        {
            databaseColumns = new List<DatabaseColumns>();
            var sql = $"select c.name from sys.all_objects o inner join sys.all_columns c on c.object_id = o.object_id where o.type = 'U' and o.name like '{databaseName}'";
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
                            databaseColumns.Add(new DatabaseColumns
                            {
                                Name = reader.GetString(0)
                            });
                        }
                    }
                }
            }
            if (databaseColumns.Any())
                return true;
            else
                return false;
        }
        public DatabaseColumns GetColumn(string name, string type, string attribute)
        {
            return new DatabaseColumns()
            {
                Name = name,
                Type = type,
                Attribute = attribute
            };
        }
    }
}
