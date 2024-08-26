namespace WebApp.Core.Domain.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
