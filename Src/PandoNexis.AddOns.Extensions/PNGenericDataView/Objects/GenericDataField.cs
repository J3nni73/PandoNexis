namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Objects
{
    public class GenericDataField
    { 
        public string FieldId { get; set; } //FieldefinitionID
        public string FieldName { get; set; } //FieldTitle
        public string EntitySystemId { get; set; }
        public string FieldValue { get; set; }
        public string FieldType { get; set; }
        public string FieldSuffix { get; set; }
        public string DataSource { get; set; }
        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();
        public GenericDataFieldSettings Settings { get; set; } = new GenericDataFieldSettings();
    }
}
