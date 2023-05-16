namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Objects
{
    public class GenericDataContainer
    {
        public List<GenericDataField> Fields { get; set; }  = new List<GenericDataField>();
        public string Messages { get; set; } = string.Empty;
        public GenericDataContainerSettings Settings { get; set; } = new GenericDataContainerSettings();
    }
}
