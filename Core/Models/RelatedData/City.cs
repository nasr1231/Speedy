using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Speedy.Core.Models.RelatedData
{
    public class City : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int GovernorateId { get; set; }
        public Governorate? Governorate { get; set; }

        #region Relations
        public ICollection<Individual> Individuals { get; set; } = [];
        //public ICollection<Delivery> Deliveries { get; set; } = [];
        public ICollection<StartUp> StartUps { get; set; } = [];
        #endregion
    }
}
