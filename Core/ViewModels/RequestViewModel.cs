namespace Speedy.Core.ViewModels
{
    public class RequestViewModel
    {
        public int DeliverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedOn { get; set; }
        #region Overloads
        public IEnumerable<Delivery> Deliveries { get; set; } = [];
        #endregion
    }
}
