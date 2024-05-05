using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Speedy.Core.Models.RelatedData
{
    public class City : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int GovernorateId { get; set; }
        public Governorate? Governorate { get; set; }
    }
}
