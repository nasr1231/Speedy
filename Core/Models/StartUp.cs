using Humanizer;

namespace Speedy.Core.Models
{
    public class StartUp : BaseModel
    {
        #region Properties
        public int Id { get; set; }
        public string LegalStatus { get; set; } = string.Empty;
        public DateTime FoundingDate { get; set; }
        public string Url { get; set; } = string.Empty;
        public string? Address { get; set; }

        public bool IsOnline { get; set; }
        #endregion

        #region Relations
        public AppUser? AppUser { get; set; }
        public string AppUserId { get; set; } = null!;
        #endregion
    }
}
