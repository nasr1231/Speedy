namespace Speedy.Core.ViewModels.RelatedData
{
    public class ServiceAreaViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
