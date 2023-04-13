namespace Solution.Extensions.PNPilot.Objects
{
    public class PilotProject
    {
        public Guid SystemId { get; set; }
        public string Name { get; set; }
        public string ProjectType { get; set; }
        public List<string> AddOns { get; set; }
        
    }
}
