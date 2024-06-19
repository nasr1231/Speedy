using Microsoft.AspNetCore.Hosting;

namespace Speedy.Core.Models
{
    public class PaymentMethod : BaseModel
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? handle { get; set; }
        public string? PhoneNumber { get; set; }
        public string? HolderName { get; set; }

        #endregion

        #region 
        public string? AppUserId { get; set; }
        public AppUser? AppUser {  get; set; }
        public ICollection<Order>? Order { get; set; }
        #endregion
    }
}
