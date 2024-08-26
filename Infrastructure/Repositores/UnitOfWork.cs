using WebApp.Core.Domain.Repositories;
using WebApp.Core.Infrastructure.Database;
namespace WebApp.Core.Infrastructure.Repositores
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly Context _dbContext;
        public UnitOfWork(Context dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        => await _dbContext.SaveChangesAsync(); 

        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();

    }
}
