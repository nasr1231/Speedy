using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Speedy.Core.Enums;
using System.ComponentModel.DataAnnotations;
using static Speedy.Core.Enums.Variables;

namespace Speedy.Core.Models
{
    public class Order : BaseModel
    {
        #region Properties
        [Key]
        public int OrderId { get; set; }
        public string? SenderName { get; set; }
        public string? SenderAddress { get; set; }
        public string? SenderPhoneNumber { get; set; }
        public DateTime RecieveDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public int? OrderTotal { get; set; }
        public int? Fees { get; set; }
        public Status? OrderStatus { get; set; } = Status.Pending;
        public string? Notes { get; set; }
        public Status? PaymentStatus { get; set; } = Status.Pending;
        public string? Description { get; set; }
        public bool IsSensitive { get; set; }
        public int TrackingNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }
        public string OrderAttachment { get; set; } = null!;
        #endregion

        #region Reciever Properties 
        public string RecieverName { get; set; } = null!;
        public string RecieverAddress { get; set; } = null!;
        public string RecieverPhoneNumber { get; set; } = null!;        

        #endregion

        #region Relations
        public string AppUserId { get; set; } = null!;
        public ICollection<AppUser>? AppUsers { get; set; }

        public int? DeliveryId { get; set; }
        public Delivery? Delivery { get; set; } = null!;

        public int? PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; } = null!;

        public int? ReviewId { get; set; }
        public Review? Review { get; set; }
        #endregion
    }
}
