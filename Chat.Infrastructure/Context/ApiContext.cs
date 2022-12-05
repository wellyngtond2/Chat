using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Context
{
    public class ApiContext : DbContext, IApiContext
    {
        public ApiContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
