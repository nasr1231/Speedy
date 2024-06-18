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
        public DateTime RecieveDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }
        public Status? OrderStatus { get; set; } = Status.Pending;
        public string? Notes { get; set; }
        public Status? PaymentStatus { get; set; } = Status.Pending;
        public string? Description { get; set; }
        public bool IsSensitive { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }
        #endregion

        #region Reciever Properties 
        public string RecieverName { get; set; } = null!;
        public string RecieverAddress { get; set; } = null!;
        public string RecieverPhoneNumber { get; set; } = null!;        

        #endregion

        #region Relations
        public string AppUserId { get; set; } = null!;
        public ICollection<AppUser>? AppUsers { get; set; } 

        public PaymentMethod PaymentMethod { get; set; } = null!;

        public int? ReviewId { get; set; }
        public Review? Review { get; set; }
        #endregion
    }
}
