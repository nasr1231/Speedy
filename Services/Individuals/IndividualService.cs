using Microsoft.EntityFrameworkCore;
using Speedy.Services.User;

namespace Speedy.Services.Individuals
{
    public class IndividualService(ApplicationDbContext context) : IIndividualService
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<Individual?> GetIndividualAsync(string individualId)
        {
            IQueryable<Individual> individualQueryable = _context.Individuals!
                .Include(s => s.AppUser)
                .Include(c => c.City)
                .Include(c => c.Reviews);

            individualQueryable = individualQueryable.AsNoTracking();

            var individual = await individualQueryable.FirstOrDefaultAsync(d => d.AppUser!.Id == individualId);

            return individual;
        }
    }
}
