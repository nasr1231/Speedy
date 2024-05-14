using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Speedy.Services.User
{
    public class DataService(ApplicationDbContext context) : IDataService
    {
        private readonly ApplicationDbContext _context = context;
       
        // Individuals Retrieve
        public async Task<IEnumerable<Individual>> GetAllIndividualsAsync()
        {
            IQueryable<Individual> IndividualsQueryable = _context.Individuals
                .Include(s => s.AppUser)
                .Include(c => c.City)
                .ThenInclude(g => g.Governorate);

            IndividualsQueryable = IndividualsQueryable.AsNoTracking();

            var Individuals = await IndividualsQueryable.ToListAsync();

            return Individuals;
        }

        // StartUp Retrieve
        public async Task<IEnumerable<StartUp>> GetAllStartUpsAsync()
        {
            IQueryable<StartUp> StartUpsQueryable = _context.StartUps
                .Include(s => s.AppUser)
                .Include(c => c.City)
                .ThenInclude(g => g.Governorate);

            StartUpsQueryable = StartUpsQueryable.AsNoTracking();

            var StartUps = await StartUpsQueryable.ToListAsync();

            return StartUps;
        }

    }
}
