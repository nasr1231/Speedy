using Humanizer;

namespace Speedy.Core.Models
{
    public class StartUp : BaseModel
    {
        #region Properties
        public int Id { get; set; }
        public string LegalStatus { get; set; } = string.Empty;
        public string StartUpName { get; set; } = null!;
        public DateTime FoundingDate { get; set; }
        public List<string> Url { get; set; } = [];
        public string? Address { get; set; }

        public bool IsOnline { get; set; }
        #endregion

        #region Relations
        public AppUser? AppUser { get; set; }
        public string AppUserId { get; set; } = null!;
        #endregion
    }
}
