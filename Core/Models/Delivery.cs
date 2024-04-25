using Speedy.Core.Models.RelatedData;
using System.Reflection.Metadata.Ecma335;

namespace Speedy.Core.Models
{
    public class Delivery : BaseModel
    {
        #region Properties
        public int Id { get; set; }
        public Byte Rate { get; set; }
        public string ServiceArea { get; set; }
        public string Address { get; set; } = null!;
        public string ShippingMethod { get; set; }
        public bool ActiveStatus { get; set; }
        public int ShippingMethodId { get; set; }
        public ShippingMethod ShippingMethods { get; set; } = null!;
        #endregion

        #region Relations
        public AppUser? AppUser { get; set; }
        public string AppUserId { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = [];
        #endregion
    }
}
