using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class OrderFormViewModel
    {
        public string UserId { get; set; } = null!;
        public int DeliveryId { get; set; }
        public DateTime RecieveDate { get; set; } = DateTime.Now;
        public DateTime ShippingDate { get; set; } = DateTime.Now;        
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }

        [Display(Name = "الوصف")]
        [Required(ErrorMessage = Errors.isRequired)]
        public string Description { get; set; } = null!;
        public bool IsSensitive { get; set; }
        

        [Required(ErrorMessage = Errors.isRequired)]
        [Display(Name ="الأسم")]
        public string RecieverName { get; set; } = null!;

        [Required(ErrorMessage = Errors.isRequired)]
        [Display(Name = "العنوان")]
        public string RecieverAddress { get; set; } = null!;

        [Required(ErrorMessage = Errors.isRequired)]
        [Display(Name = "رقم الهاتف")]
        public string RecieverPhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = Errors.isRequired)]
        [Display(Name = "الأسم")]
        public string SenderName { get; set; } = null!;

        [Required(ErrorMessage = Errors.isRequired)]
        [Display(Name = "العنوان")]
        public string SenderAddress { get; set; } = null!;

        [Required(ErrorMessage = Errors.isRequired)]
        [Display(Name = "رقم الهاتف")]
        public string SenderPhoneNumber { get; set; } = null!;
        //public string Title { get; set; }
        //public string? handle { get; set; }
        //public string? WalletPhoneNumber { get; set; }
        //public string? HolderName { get; set; }        
        //public string CVV { get; set; }

        //public double OrderTotal { get; set; }
        //public string? TrackingNumber { get; set; }
    }
}
