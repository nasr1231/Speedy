namespace Speedy.Core.Models.RelatedData
{
    public class Governorate : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Delivery> Deliveries { get; set; } = [];
        public ICollection<StartUp> StartUps { get; set; } = [];
        public ICollection<Individual> Individuals { get; set; } = [];
    }
}
