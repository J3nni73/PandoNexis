namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Objects
{
    public class GenericDataView
    {
        public List<GenericDataContainer> DataContainers { get; set; } = new List<GenericDataContainer>();
        public GenericDataViewSettings Settings { get; set; } = new GenericDataViewSettings();
    }
}
