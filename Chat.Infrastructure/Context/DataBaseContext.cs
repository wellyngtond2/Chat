using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatRoom>(builder =>
            {
                builder.HasMany(room => room.ChatMessages)
               .WithOne()
               .HasForeignKey(message => message.ChatRoomId)
               .IsRequired();
            });
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                          .Entries()
                          .Where(e => e.Entity is BaseEntity && (
                          e.State == EntityState.Added));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
            }

            return base.SaveChanges();
        }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Membership> Memberships { get; set; }
    }
}
