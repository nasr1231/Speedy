using Speedy.Core.Models.RelatedData;
using System.Reflection.Metadata.Ecma335;

namespace Speedy.Core.Models
{
    public class Delivery : AppUser
    {
        public Byte Rate {  get; set; }
        public string ServiceArea { get; set; }
        public string ShippingMethod { get; set; }
        public bool ActiveStatus { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdateOn { get; set; }
        public bool IsDeleted { get; set; }

        public int ShippingMethodId{ get; set; }
        public ShippingMethod ShippingMethods { get; set; }
    }
}
