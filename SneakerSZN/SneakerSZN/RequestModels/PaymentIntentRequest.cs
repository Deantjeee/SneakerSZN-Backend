namespace SneakerSZN.RequestModels
{
    public class PaymentIntentRequest
    {
        public long Amount { get; set; } // Amount in cents
        public string Currency { get; set; } = "eur"; 
        public string? Description { get; set; } 
    }
}
