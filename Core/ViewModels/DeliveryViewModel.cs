using Speedy.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class DeliveryViewModel : BaseViewModel
    {
        #region User Properties
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Email { get; set; }        
        public string? ProfilePictureIUrl { get; set; } = string.Empty;
        public string NID { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        #endregion

        #region Delivery Properties
        public string Id { get; set; } = null!;
        public Byte? Rate { get; set; }
        public string ServiceArea { get; set; } = null!;        
        public string Address { get; set; } = null!;       
        public int ShippingMethodId { get; set; }
        public string GovernorateName { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;        
        public string? Description { get; set; }
        public bool HasWhatsApp { get; set; }
        #endregion

        #region Relations
        public ReviewViewModel? Reviews { get; }
        #endregion
    }
}
