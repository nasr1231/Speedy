namespace Speedy.Core.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; } = null!;
        public AppUser User { get; set; } = null!;
    }
}
