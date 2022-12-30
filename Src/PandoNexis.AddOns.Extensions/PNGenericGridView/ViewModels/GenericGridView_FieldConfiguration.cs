namespace PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels
{
    public class FieldConfigurationField
    {
        private string _virtualId = null;
        public string Id { get; set; }
        public bool IsBaseProduct { get; set; }
        public string Name { get; set; }
        public bool IsEditable { get; set; }
        public string Suffix { get; set; }
        public string OrderListDate { get; set; }
        public int? ScannedQuantity { get; set; }
        public object Value { get; set; }
        public Guid DeliveryId { get; set; }
        public string FieldMessage { get; set; }
        public string ErrorFieldMessage { get; set; }
        public string BackgroundColor { get; set; }

        public string Support { get; set; }
        public string VirtualId
        {
            get => _virtualId ?? Id;
            set => _virtualId = value;
        }
    }

    public class AutocompleteFields
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public class FieldUpdateData
    {
        public string FieldId { get; set; }
        public string DataSource { get; set; }
        public string Value { get; set; }
        public string EntitySystemId { get; set; }
    }
}