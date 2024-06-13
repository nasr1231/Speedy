namespace Speedy.Services.Individuals
{
    public interface IIndividualService
    {
        public Task<Individual?> GetIndividualAsync(string individualId);
    }
}
