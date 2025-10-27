using Microsoft.EntityFrameworkCore;
using Todo.Api.Models;

namespace Todo.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        var e = b.Entity<TaskItem>();

        e.ToTable("task");

        // PK + explicit column names
        e.HasKey(x => x.Id);
        e.Property(x => x.Id).HasColumnName("id");

        e.Property(x => x.Title)
            .HasColumnName("title")
            .HasMaxLength(255)
            .IsRequired();

        e.Property(x => x.Description)
            .HasColumnName("description");

        e.Property(x => x.IsCompleted)
            .HasColumnName("is_completed")
            .HasDefaultValue(false);

        // Store UTC instants with microsecond precision.
        // Pomelo maps DateTimeOffset -> DATETIME(6) and preserves UTC value.
        e.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime(6)")
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
            .ValueGeneratedOnAdd();

        // Helpful index for your "active + latest" query
        e.HasIndex(x => new { x.IsCompleted, x.CreatedAt })
            .HasDatabaseName("IX_task_active_created");
    }
}
