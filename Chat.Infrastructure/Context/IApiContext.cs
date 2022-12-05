using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Context
{
    public interface IApiContext
    {
        DbSet<ChatMessage> ChatMessages { get; set; }
        DbSet<ChatRoom> ChatRooms { get; set; }
        DbSet<Membership> Memberships { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
