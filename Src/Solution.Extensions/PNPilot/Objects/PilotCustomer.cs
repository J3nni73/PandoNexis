namespace Solution.Extensions.PNPilot.Objects
{
    public class PilotCustomer
    {
        public Guid SystemId { get; set; }
        public string Name { get; set; }
        public string WorkItemPrefix { get; set; }
        public List<PilotProject> Projects { get; set; }
    }
}
