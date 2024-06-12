using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Speedy.Core.Models
{
    public class Review : BaseModel
    {
        #region Properties
        public int Id { get; set; }

        [Range(0, 5)]
        public int Rate { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }
        #endregion

        #region Relations
        public Delivery? Delivery { get; set; }
        public int DeliveryId { get; set; }

        public Individual Individual { get; set; } = null!;
        public int IndividualId { get; set; }

        public Order? OrderId { get; set; }
        #endregion
    }
}
