using Chat.Share.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Chat.Infrastructure.Interceptos
{
    public class SendNotificationsInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator _mediator;

        public SendNotificationsInterceptor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            if (eventData is not null && eventData.Context is not null)
            {
                var entries = eventData.Context.ChangeTracker
                              .Entries()
                              .Where(e => e.Entity is BaseEntity)
                              .ToList();

                foreach (var entityEntry in entries)
                {
                    var domainEvents = ((BaseEntity)entityEntry.Entity).GetDomainEvents();

                    if (domainEvents.Any())
                        foreach (var item in domainEvents)
                        {
                            await _mediator.Publish(item);
                        }
                }
            }

            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
