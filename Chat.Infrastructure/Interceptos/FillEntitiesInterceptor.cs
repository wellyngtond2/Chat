using Chat.Share.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Chat.Infrastructure.Interceptos
{
    public class FillEntitiesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            if (eventData is not null && eventData.Context is not null)
            {
                var entries = eventData.Context.ChangeTracker
                              .Entries()
                              .Where(e => e.Entity is BaseEntity && (
                              e.State == EntityState.Added));

                foreach (var entityEntry in entries)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
