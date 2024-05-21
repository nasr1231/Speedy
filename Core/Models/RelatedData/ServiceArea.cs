namespace Speedy.Core.Models.RelatedData
{
    [Index(nameof(Name), IsUnique = true)]
    public class ServiceArea : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<DeliveryServiceArea>? Delivery { get; set; }

    }
}
