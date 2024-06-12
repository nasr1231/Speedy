namespace Speedy.Core.ViewModels
{
    public class OrderFormViewModel
    {
        #region Propereties
        public int DeliveryId { get; set; }
        #endregion

        #region Overloads
        public IEnumerable<Delivery> Deliveries { get; set; }
        #endregion        
    }
}
