using Litium.Runtime.DependencyInjection;
using Microsoft.Data.SqlClient;
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

        public abstract IEnumerable<object> GetAll();
        public abstract bool AddOrUpdate(object item);
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
            return !reader.IsDBNull(reader.GetOrdinal(columnName)) ? reader.GetDateTime(reader.GetOrdinal(columnName)) : DateTime.MinValue;
        }


    }

}
