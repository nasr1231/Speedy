using Humanizer;
using Speedy.Core.Models.RelatedData;

namespace Speedy.Core.Models
{
    public class StartUp : BaseModel
    {
        #region Properties
        public int Id { get; set; }
        public string StartUpName { get; set; } = null!;
        public DateTime FoundingDate { get; set; }
        public List<string> Url { get; set; } = [];
        public string? Address { get; set; }

        public bool IsOnline { get; set; }
        #endregion

        #region Relations
        public AppUser? AppUser { get; set; }
        public string AppUserId { get; set; } = null!;
        public int CityId { get; set; }
        public City City { get; set; } = null!;
        public ICollection<Review>? Reviews { get; set; }        

        //public int PaymentMethodId { get; set; }
        //public PaymentMethod? PaymentMethod { get; set; }
        #endregion
    }
}
