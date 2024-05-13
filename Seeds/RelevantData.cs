using Speedy.Core.Models.RelatedData;

namespace Speedy.Seeds
{
    public static class RelevantData
    {
        public static readonly List<ShippingMethod> ShippingMethods =
        [
            new ShippingMethod { Id = 1, Name = "عجلة", CreatedOn = new DateTime(day: 25, month: 7, year: 2024) },
            new ShippingMethod { Id = 2, Name = "سيارة", CreatedOn = new DateTime(day: 25, month: 7, year: 2024) },
            new ShippingMethod { Id = 3, Name = "موتوسيكل", CreatedOn = new DateTime(day: 25, month: 7, year: 2024) },
        ];

        public static readonly List<Governorate> Governorates =
        [
            new Governorate { Id = 1, Name = "القاهرة", CreatedOn = new DateTime(day: 1, month: 1, year: 2024) },
            new Governorate { Id = 2, Name = "الجيزة", CreatedOn = new DateTime(day: 1, month: 1, year: 2024) },
            new Governorate { Id = 3, Name = "القليوبية", CreatedOn = new DateTime(day: 1, month: 1, year: 2024) },
        ];

         public static readonly List<City> Cities =
        [
            new City { Id = 5, Name = "وسط البلد", GovernorateId = 1,CreatedOn = new DateTime(day: 1, month: 1, year: 2024)},            
        ];
    }
}
