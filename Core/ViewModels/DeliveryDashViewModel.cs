using static Speedy.Core.Enums.Variables;

namespace Speedy.Core.ViewModels
{
    public class DeliveryDashViewModel
    {                
        public int OrderId { get; set; }
        public string? SenderName { get; set; }
        public string? OrderAttachment { get; set; }
        public string? SenderAddress { get; set; }
        public string? SenderPhoneNumber { get; set; }
        public DateTime RecieveDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public int? Total { get; set; }
        public Status? OrderStatus { get; set; } = Status.Pending;
        public string? Notes { get; set; }
        public Status? PaymentStatus { get; set; } = Status.Pending;
        public string? Description { get; set; }
        public bool IsSensitive { get; set; }
        public int TrackingNumber { get; set; }
        public int? Fees { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }        
        public string RecieverName { get; set; }
        public string RecieverAddress { get; set; }
        public string RecieverPhoneNumber { get; set; }
    }
}
