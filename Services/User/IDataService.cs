namespace Speedy.Services.User
{
    public interface IDataService
    {
        public Task<IEnumerable<Delivery>> GetAllDeliveriesAsync();
        public Task<IEnumerable<Individual>> GetAllIndividualsAsync();
        public Task<IEnumerable<StartUp>> GetAllStartUpsAsync();
    }
}
