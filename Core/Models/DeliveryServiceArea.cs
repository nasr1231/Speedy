using Speedy.Core.Models.RelatedData;

namespace Speedy.Core.Models
{
    public class DeliveryServiceArea
    {
        public int DeliveryId { get; set; }
        public Delivery? Delivery { get; set; }
        public int ServiceAreaId { get; set; }
        public ServiceArea? ServiceArea { get; set; }
    }
}
