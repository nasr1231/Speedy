namespace Speedy.Core.Models
{
    public class Individual : BaseModel
    {
        #region Properties
        public int Id { get; set; }
        public string referralCode { get; set; } = string.Empty;
        #endregion

        #region Relations
        public AppUser? AppUser { get; set; }
        public string AppUserId { get; set; } = null!;
        public ICollection<Review>? Reviews { get; }
        #endregion
    }
}
