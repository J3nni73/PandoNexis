namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Objects
{
    public class GenericDataViewSettings
    {
        public List<GenericFilter> Filter { get; set; } = new List<GenericFilter>();
        public List<GenericSortOption> SortOptions { get; set; } = new List<GenericSortOption>();
        public int PageSize { get; set; } = 50;
        public int TotalHits { get; set; }
        public List<string> EditableFieldIds { get; set; }
        public string IdentifierFieldId { get; set; }
        public bool preventInitialLoad { get; set; }
        public List<string> DisplayTypes { get; set; }


        public int ColumnsInsideContainerSmall { get; set; } = 1;
        public int ColumnsInsideContainerMedium { get; set; } = 2;
        public int ColumnsInsideContainerLarge { get; set; } = 3;
        public int ColumnsWithContainersSmall { get; set; } = 1;
        public int ColumnsWithContainersMedium { get; set; } = 2;
        public int ColumnsWithContainersLarge { get; set; } = 3;
        public string AlignContainers { get; set; }  = string.Empty;    
    }
}
