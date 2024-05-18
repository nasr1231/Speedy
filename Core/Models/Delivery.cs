using Speedy.Core.Models.RelatedData;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Speedy.Core.Models
{    
    public class Delivery : BaseModel
    {
        #region Properties
        public int Id { get; set; }
        [Range(0,5)]
        public Byte? Rate { get; set; }
        public List<string>? ServiceArea { get; set; } = [];
        public string Address { get; set; } = null!;
        public List<string>? AttachmentUrl { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
        public bool HasWhatsApp {  get; set; }
        public bool IsFirstTime {  get; set; }


        #endregion

        #region Relations
        public AppUser? AppUser { get; set; }
        public string AppUserId { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = [];

        public int ShippingMethodId { get; set; }
        public ShippingMethod ShippingMethods { get; set; } = null!;

        public int CityId { get; set; }
        public City City { get; set; } = null!;

        public PaymentMethod? PaymentMethod { get; set; } = null!;
        #endregion
    }
}
