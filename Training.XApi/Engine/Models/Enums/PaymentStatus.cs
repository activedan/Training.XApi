namespace Training.XApi.Engine.Enums
{
    public enum PaymentStatus
    {
        AwaitingPayment = 0,
        AwaitingPaymentComplete = 10,
        Paid = 20,
        Failed = 30,
        Cancelled = 40,
        Refunded = 50
    }
}
