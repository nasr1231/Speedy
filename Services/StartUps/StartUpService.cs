using Speedy.Core.Models;

namespace Speedy.Services.StartUps
{
    public class StartUpService(ApplicationDbContext context) : IStartUpService
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<StartUp?> GetStartUpAsync(string startUplId)
        {
            IQueryable<StartUp> startUpQueryable = _context.StartUps!
                .Include(s => s.AppUser)                
                .Include(c => c.City)
                .Include(c => c.Reviews);

            startUpQueryable = startUpQueryable.AsNoTracking();

            var startUp = await startUpQueryable.FirstOrDefaultAsync(d => d.AppUser!.Id == startUplId);

            return startUp;
        }
    }
}
