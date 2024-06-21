using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class RequestDetailsViewModel
    {
        public int DeliveryId { get; set; }
        public string Address { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public string CriminalStatus { get; set; } = null!;
        public string? DrivingLicsense { get; set; }
        public string? Description { get; set; }
        public bool HasWhatsApp { get; set; }
        public bool IsFirstTime { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
