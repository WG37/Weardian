namespace Weardian.Server.Application.Interfaces
{
    public interface ISymmetricKeyRepository
    {
        public Task<bool> AddAsync();
        public Task GetById();
    }
}
