using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Speedy.Services.User
{
    public class DeliveryService(ApplicationDbContext context) : IDeliveryService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Delivery>> GetAllDeliveriesAsync()
        {
            IQueryable<Delivery> deliveriesQueryable = _context.Deliveries
                .Include(s => s.ShippingMethods)
                .Include(s => s.AppUser)
                .Include(c => c.City)
                .ThenInclude(g => g.Governorate);

            deliveriesQueryable = deliveriesQueryable.AsNoTracking();

            var deliveries = await deliveriesQueryable.ToListAsync();

            return deliveries;
        }
        public async Task<Delivery> GetDeliveryAsync(string deliveryId)
        {
            IQueryable<Delivery> deliveriesQueryable = _context.Deliveries
                .Include(s => s.ShippingMethods)
                .Include(s => s.AppUser)
                .Include(c => c.City)
                .ThenInclude(g => g.Governorate);

            var delivery = await deliveriesQueryable.AsNoTracking().FirstOrDefaultAsync(d => d.AppUser!.Id == deliveryId);                      

            return delivery;
        }
    }
}
