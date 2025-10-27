using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Dtos;
using Todo.Api.Models;
using Todo.Api.Services.Implementations;

namespace Todo.Api.Tests.Unit;

public class TaskServiceTests
{
    private static AppDbContext NewDb(out SqliteConnection c)
    {
        c = new SqliteConnection("DataSource=:memory:");
        c.Open();
        var opts = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(c).Options;
        var db = new AppDbContext(opts);
        db.Database.EnsureCreated();
        return db;
    }

    [Fact]
    public async Task Create_inserts_row_with_defaults()
    {
        using var db = NewDb(out var c);
        var svc = new TaskService(db);

        var dto = new TaskCreateDto { Title = "Unit Task", Description = "desc" };
        var created = await svc.CreateAsync(dto);

        created.Id.Should().BeGreaterThan(0);
        created.IsCompleted.Should().BeFalse();
        (await db.Tasks.CountAsync()).Should().Be(1);
        c.Dispose();
    }

    [Fact]
    public async Task GetLatestActive_returns_5_desc()
    {
        using var db = NewDb(out var c);
        var now = DateTime.UtcNow;
        for (int i = 0; i < 7; i++)
            db.Tasks.Add(new TaskItem { Title = $"T{i}", CreatedAt = now.AddMinutes(-i) });
        db.Tasks.Add(new TaskItem { Title = "done", IsCompleted = true });
        await db.SaveChangesAsync();

        var svc = new TaskService(db);
        var list = await svc.GetLatestActiveAsync(5);

        list.Count.Should().Be(5);
        list.Should().OnlyContain(t => !t.IsCompleted);
        list.Should().BeInDescendingOrder(t => t.CreatedAt);
        c.Dispose();
    }
}
