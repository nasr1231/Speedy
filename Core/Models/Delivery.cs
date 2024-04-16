using System.Reflection.Metadata.Ecma335;

namespace Speedy.Core.Models
{
    public class Delivery : BaseModel
    {
        public int Id { get; set; }
        public Byte Rate {  get; set; }
        public string ServiceArea { get; set; }
        public string ShippingMethod { get; set; }
        public bool ActiveStatus { get; set; }
        public bool IsActive { get; set; }

    }
}
