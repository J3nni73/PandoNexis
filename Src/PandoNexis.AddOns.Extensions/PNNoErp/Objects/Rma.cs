namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class Rma
    {
        public string Id { get; set; }
        public string ReturnSlipId { get; set; }
        public DateTime ReceivedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ReceivedByPersonId { get; set; }
        public string ApprovedByPersonId { get; set; }
        public string CustomerComments { get; set; }
        public string AdminComments { get; set; }
        public object CdditionalInfo { get; set; }
        public string State { get; set; }
        public string ApprovalCode { get; set; }
        public List<RmaRow> Rows { get; set; }
    }
}
