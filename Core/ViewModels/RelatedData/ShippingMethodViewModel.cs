using Speedy.Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace Speedy.Core.ViewModels.RelatedData
{
    public class ShippingMethodViewModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
