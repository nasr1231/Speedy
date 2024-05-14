namespace Speedy.Services.User
{
    public interface IDeliveryService
    {
        public Task<IEnumerable<Delivery>> GetAllDeliveriesAsync();
        public Task<Delivery> GetDeliveryAsync(string deliveryId);
    }
}
