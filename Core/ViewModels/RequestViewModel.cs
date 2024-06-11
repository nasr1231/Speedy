namespace Speedy.Core.ViewModels
{
    public class RequestViewModel : BaseViewModel
    {

        #region Properties
        public int Id { get; set; }
        #endregion

        #region Overloads
        public IEnumerable<Delivery> Deliveries { get; set; } = [];
        #endregion
    }
}
