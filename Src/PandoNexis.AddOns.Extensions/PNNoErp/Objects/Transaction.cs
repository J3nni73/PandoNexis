namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class Transaction    
    {
        public string Id { get; set; }
        public string TransactionRef1 { get; set; }
        public string TransactionRef2 { get; set; }
        public string Status { get; set;}
        public string Type { get; set;}
        public int TotalIncludingVat { get; set; }
        public int TotalExcludingVat { get; set; }
        public int TotalVat { get; set; }
        public List<TransactionRow> Rows { get; set;}

    }
}
