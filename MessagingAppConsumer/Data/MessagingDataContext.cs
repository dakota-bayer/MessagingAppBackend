using Microsoft.EntityFrameworkCore;
using SharedModels;
using Npgsql;

namespace MessagingAppConsumer.Data;

public class MessagingDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=messaging;Username=postgres;Password=admin");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the table name explicitly to be "message"
        modelBuilder.Entity<Message>()
            .ToTable("message");
    }
}