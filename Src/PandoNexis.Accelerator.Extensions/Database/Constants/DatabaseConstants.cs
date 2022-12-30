using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.Database.Constants
{
    public static class DatabaseConstants
    {
        public const string UniqueIdentifier = "uniqueidentifier";
        public const string DateTime = "datetime";
        public const string Varchar50 = "nvarchar(50)";
        public const string Varchar100 = "nvarchar(100)";
        public const string VarcharMax = "nvarchar(max)";
        public const string Int = "Int";


        public const string NotNull = "NOT NULL";
        public const string Null = "NULL";



        public const string CreatedDateTime = "CreatedDateTime";
        public const string CreatedBy = "CreatedByPersonSystemId";
        public const string UpdatedDateTime = "UpdatedDateTime";
        public const string UpdatedBy = "UpdatedByPersonSystemId";
        public const string DeletedDateTime = "DeletedDateTime";
        public const string DeletedBy = "DeletedByPersonSystemId";

        public const string Schema = "dbo";
        public const string TablePrefix = "PandoNexis_";
    }
}
