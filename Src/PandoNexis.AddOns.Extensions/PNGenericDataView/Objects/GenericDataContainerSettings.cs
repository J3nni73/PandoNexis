namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Objects
{
    public class GenericDataContainerSettings
    {
        public bool PostContainer { get; set; } = false;
        public string PostContainerButtonText { get; set; } = string.Empty;
        public Guid PostContainerPageSystemId { get; set; }
        public KanbanDataContainerSettings KanbanContainerData { get; set; }
        public GenericDataResponseSettings ResponseActions { get; set; } = new GenericDataResponseSettings();
    }

    public class KanbanDataContainerSettings
    {
        public string TaskName { get; set; } = string.Empty;
        public GenericDataContainerState ContainerState { get; set; }
        public List<string> PossibleContainerStateTransitions { get; set; }
    }
}
