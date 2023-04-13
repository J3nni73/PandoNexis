namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class Payment
    {
        public string Id { get; set; }
        public string PaymentProviderName { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentAccountId { get; set; }
        public string PaymentProviderReferenceId1 { get; set; }
        public string PaymentProviderReferenceId2 { get; set; }
        public double TotalAuthorizedAmount { get; set; }
        public double TotalCapturedAmount { get; set; }
        public double TotalRefundedAmount { get; set; }
        public double TotalPendingCapture { get; set; }
        public double TotalPendingRefund { get; set; }
        public double TotalPendingCancel { get; set; }
        public double TotalCancelledAmount { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
