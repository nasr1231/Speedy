using Speedy.Core.Models.RelatedData;

namespace Speedy.Seeds
{
    public static class RelevantData
    {
        public static readonly List<ShippingMethod> ShippingMethods =
   [
        new ShippingMethod { Id = 1, Name = "عجلة", CreatedOn = new DateTime(day: 1, month: 1, year: 2024) },
        new ShippingMethod { Id = 2, Name = "سيارة", CreatedOn = new DateTime(day: 1, month: 1, year: 2024) },
        new ShippingMethod { Id = 3, Name = "موتوسيكل", CreatedOn = new DateTime(day: 1, month: 1, year: 2024) },
   ];
    }
}
