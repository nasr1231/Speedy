namespace Speedy.Services.StartUps
{
    public interface IStartUpService
    {
        public Task<StartUp?> GetStartUpAsync(string startUplId);
    }
}
