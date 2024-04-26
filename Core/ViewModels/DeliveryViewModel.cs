using Speedy.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels
{
    public class DeliveryViewModel : BaseViewModel
    {        
        public int FirstName { get; set; }
        public int LastName { get; set; }
        [Range(0,5)]
        public Byte? Rate { get; set; }
        public string ServiceArea { get; set; } = null!;
        public string Address { get; set; } = null!;       
        public int ShippingMethodId { get; set; }

        public ReviewViewModel? Reviews { get; }

    }
}
