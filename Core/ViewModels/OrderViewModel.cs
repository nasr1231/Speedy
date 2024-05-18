namespace Speedy.Core.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime RecieveDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTable { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? Description { get; set; }
        public bool IsSensitive { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }
        public string RecieverName { get; set; } = null!;
        public string RecieverAddress { get; set; } = null!;
        public string RecieverPhoneNumber { get; set; } = null!;
    }
}
