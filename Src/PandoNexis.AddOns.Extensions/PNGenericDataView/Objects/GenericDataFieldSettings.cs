using Litium.Application.Validations;

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
        //public bool IsRequired { get; set; } = false;
        //public string ValidationRules { get; set; } = string.Empty;
        //public string FieldMessage { get; set; } = string.Empty;
        public List<ValidationRule> ValidationRules{get;set;}
        public bool IsFilterField { get; set; } = false;
        public bool IsSortField { get; set; } = false;
        public string EndPointMethod { get; set; } = string.Empty;
        public string AutocompleteEndPointMethod { get; set; } = string.Empty;
        public string PlaceholderText { get; set; } = string.Empty;
        public string IconClass { get; set; } = string.Empty;
        public string EmptyResultText { get; set; } = string.Empty;
        public int MinimumCharsRequired { get; set; } = 9999;
        
        public List<GenericButton> GenericButtons { get; set; } = new List<GenericButton>(); //TODO: move button functionality to use this, one field can contain several buttons. 
        public string ButtonText { get; set; } = string.Empty;
        public bool HideButton { get; set; } = false;
        public bool UseConfirmation { get; set; } = false;
        public string ConfirmationText { get; set; } = string.Empty;
        public string FieldTooltipMessage { get; set; } = string.Empty; // On hover field information
        public string FieldTooltipType { get; set; } = "dark"; // options: "dark", "light", "success", "warning", "error", "info"
        public string ErrorFieldMessage { get; set; } = string.Empty;
        public bool ButtonOpenInModal { get; set; } = false;
        public Guid ModalPageSystemId { get; set; }       
       

    }
}
