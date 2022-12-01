using Chat.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatMessage>(builder =>
            {
                builder.HasOne(msg => msg.Creator)
               .WithOne()
               .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(msg => msg.ChatRoom)
                .WithMany(x=>x.ChatMessages)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Membership> Memberships { get; set; }
    }
}
