using Speedy.Core.Models.RelatedData;

namespace Speedy.Core.Models
{
    public class Individual : BaseModel
    {
        #region Properties
        public int Id { get; set; }
        public string? referralCode { get; set; }
        #endregion

        #region Relations
        public AppUser? AppUser { get; set; }
        public string AppUserId { get; set; } = null!;

        public int CityId { get; set; }
        public City City { get; set; } = null!;

        public ICollection<Review>? Reviews { get; }
        
        #endregion
    }
}
