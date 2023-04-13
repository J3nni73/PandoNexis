namespace Solution.Extensions.PNPilot.Objects
{
    public class Time
    {
        public Guid SystemId { get; set; }
        public Guid ItemSystemId { get; set; }
        public Guid OrganizationSystemId { get; set; }
        public Guid TimeTypeSystemId { get; set; }
        public Guid TimeStatusSystemId { get; set; }
        public string TimeComment { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public int Amount { get; set; }
        public decimal Risk { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime DeletedDateTime { get; set; }
        public Guid DeletedBy { get; set; }
    }
}
