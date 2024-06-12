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
        public string Email { get; set; } = null!;
        public string? ProfilePictureIUrl { get; set; } = string.Empty;
        public string NID { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        #endregion

        #region Delivery Properties
        public int Id { get; set; }
        public Byte? Rate { get; set; }
        public List<string>? ServiceArea { get; set; } = [];
        public string Address { get; set; } = null!;       
        public string ShippingMethodName { get; set; } = null!;
        public string GovernorateName { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;        
        public string? Description { get; set; }
        public bool HasWhatsApp { get; set; }
        public bool IsDeleted { get; set; }
        #endregion

        #region Relations
        public ICollection<Review>? Reviews { get; }
        #endregion
    }
}
