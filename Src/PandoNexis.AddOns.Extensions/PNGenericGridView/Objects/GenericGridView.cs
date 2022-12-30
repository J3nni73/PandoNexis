namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Objects
{
    public class GenericGridView
    {
        public List<GenericGridViewRow> DataRows { get; set; }
        public List<TabInfo> GridViewTabs { get; set; }
        public GenericGridViewSettings Settings { get; set; }
        //Ska vara settings för vad som ska göras ex pagesize, readonly totalt, alltså att den inte ska skicka tillbaka data, att det ska gå att köpa grejor etc
    }

    public class GenericGridViewRow
    {
        public List<GenericGridViewField> Fields { get; set; }
        public GenericGridViewRowSettings Settings { get; set; }
        public string Messages { get; set; } = string.Empty;
    }

    public enum ControllFormEnum { input, select, textarea, radio, checkbox, date, file };
    public class GenericGridViewField
    {
        public string FieldID { get; set; } //FieldefinitionID
        public string FieldName { get; set; } //FieldTitle
        public string FieldValue { get; set; }
        public string FieldType { get; set; }
        public string FieldSuffix { get; set; }
        public string EntitySystemId { get; set; }
        public List<GenericGridViewFieldSimpleList> DropDownOptions { get; set; }
        public GenericGridViewFieldSettings Settings { get; set; }

    }

    public class GenericGridViewSettings
    {
        public GenericGridViewSettings(int pageSize, int totalHits)
        {
            Filter = new List<GenericFilters>();
            SortOptions = new List<GenericSortOptions>();
            PageSize = pageSize;
            TotalHits = totalHits;
        }

        public List<GenericFilters> Filter { get; set; }
        public List<GenericSortOptions> SortOptions { get; set; }
        public int PageSize { get; set; }
        public int TotalHits { get; set; }

        public List<string> EditableFieldIds { get; set; }
        public string IdentifierFieldId { get; set; }
        public bool preventInitialLoad { get; set; }
    }

    public class GenericFilters
    {
        public string FieldID { get; set; }
        public string FieldName { get; set; }
        public string FilterType { get; set; }
        public List<string> Values { get; set; }
    }

    public class GenericSortOptions
    {
        public string FieldId { get; set; }
        public string FieldName { get; set; }
        public string SortType { get; set; }
    }

    public class GenericGridViewRowSettings
    {
    }

    public class TabInfo
    {
        public string Title { get; set; }
        public string TabId { get; set; }
        public bool IsSelected { get; set; }
    }

    public class GenericGridViewFieldSimpleList
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public class GenericGridViewFieldSettings
    {
        public bool ReadOnly { get; set; } = true;
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
        public List<GenericGridViewFieldSimpleList> FieldListData { get; set; }  // Lista till ex DropDown (?)
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
    }
}