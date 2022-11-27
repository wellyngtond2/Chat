using Chat.Domain.Interfaces.Repository;
using Chat.Infrastructure.Context;

namespace Chat.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext _dbContext;

        public UnitOfWork(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
