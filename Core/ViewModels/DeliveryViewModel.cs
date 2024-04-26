using Speedy.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class DeliveryViewModel : BaseViewModel
    {
        #region Properties
        public int FirstName { get; set; }
        public int LastName { get; set; }        
        public Byte? Rate { get; set; }
        public string ServiceArea { get; set; } = null!;
        public string Address { get; set; } = null!;       
        public int ShippingMethodId { get; set; }
        public string MobileNumber { get; set; } = null!;        
        public string? Description { get; set; }
        public bool HasWhatsApp { get; set; }
        #endregion

        #region Relations
        public ReviewViewModel? Reviews { get; }
        #endregion
    }
}
