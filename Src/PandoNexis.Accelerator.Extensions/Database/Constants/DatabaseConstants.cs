namespace PandoNexis.Accelerator.Extensions.Database.Constants
{
    public static class DatabaseConstants
    {
        public const string CreatedDateTime = "CreatedDateTime";
        public const string CreatedBy = "CreatedByPersonSystemId";
        public const string UpdatedDateTime = "UpdatedDateTime";
        public const string UpdatedBy = "UpdatedByPersonSystemId";
        public const string DeletedDateTime = "DeletedDateTime";
        public const string DeletedBy = "DeletedByPersonSystemId";

        public const string Schema = "dbo";
        public const string TablePrefix = "PandoNexis_";
    }
    public static class DatabaseFieldDataConstants
    {
        public const string OwnerSystemId = "OwnerSystemId";
        public const string FieldDefinitionId = "FieldDefinitionId";
        public const string Culture = "Culture";
        public const string Index = "Index";
        public const string BooleanValue = "BooleanValue";
        public const string DateTimeValue = "DateTimeValue";
        public const string DecimalValue = "DecimalValue";
        public const string GuidValue = "GuidValue";
        public const string IndexedTextValue = "IndexedTextValue";
        public const string IntValue = "IntValue";
        public const string JsonValue = "JsonValue";
        public const string LongValue = "LongValue";
        public const string TextValue = "TextValue";
        public const string ChildOwnerId = "ChildOwnerId";
        public const string ChildIndex = "ChildIndex";
    }
    public static class DatabaseTypeConstants
    {
        public const string UniqueIdentifier = "uniqueidentifier";
        public const string DateTime = "datetime";
        public const string Varchar50 = "nvarchar(50)";
        public const string Varchar100 = "nvarchar(100)";
        public const string VarcharMax = "nvarchar(max)";
        public const string Decimal = "Decimal(18,8)";
        public const string Int = "Int";
        public const string Bit = "Bit";
        public const string BigInt = "BigInt";


        public const string NotNull = "NOT NULL";
        public const string Null = "NULL";
    }
}
