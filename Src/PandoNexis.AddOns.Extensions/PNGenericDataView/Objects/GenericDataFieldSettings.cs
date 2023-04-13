namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Objects
{
    public class GenericDataFieldSettings
    {
        public bool Editable { get; set; } = false;
        public bool IsVisible { get; set; } = true;
        public string BackgroundColor { get; set; } = string.Empty;
        public bool ShowForMobile { get; set; } = true;
        public bool ShowForTablet { get; set; } = true;
        public bool ShowForDesktop { get; set; } = true;
        public bool IsRequired { get; set; } = false;
        public bool IsFilterField { get; set; } = false;
        public bool IsSortField { get; set; } = false;
        public string ValidationRules { get; set; } = string.Empty;
        public string EndPointMethod { get; set; } = string.Empty;
        public string AutocompleteEndPointMethod { get; set; } = string.Empty;
        public string PlaceholderText { get; set; } = string.Empty;
        public string ButtonText { get; set; } = string.Empty;
        public string IconClass { get; set; } = string.Empty;
        public string EmptyResultText { get; set; } = string.Empty;
        public int MinimumCharsRequired { get; set; } = 9999;
        public bool HideButton { get; set; } = false;
        public bool UseConfirmation { get; set; } = false;
        public string ConfirmationText { get; set; } = string.Empty;
        public string FieldMessage { get; set; } = string.Empty;
        public string ErrorFieldMessage { get; set; } = string.Empty;
        public bool ButtonOpenInModal { get; set; } = false;
        //public string ModalDisplayTypes { get; set; } = "cards"; 
        //public string ModalDataType { get; set; } = string.Empty;
        //public string ModalAreaSource { get; set; } = string.Empty;
        //public string ModalDataArea { get; set; } = string.Empty;
        //public int CardColumnsSmall { get; set; } = 1;
        //public int CardColumnsMedium { get; set; } = 2;
        //public int CardColumnsLarge { get; set; } = 3;
        public Guid ModalPageSystemId { get; set; }       

    }
}
