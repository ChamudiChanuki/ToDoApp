using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Dtos;
using Todo.Api.Models;
using Todo.Api.Services.Interfaces;

namespace Todo.Api.Services.Implementations;

public class TaskService(AppDbContext db) : ITaskService
{
    public async Task<TaskReadDto> CreateAsync(TaskCreateDto dto, CancellationToken ct = default)
    {
        var entity = new TaskItem
        {
            Title = dto.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description!.Trim(),
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };
        db.Tasks.Add(entity);
        await db.SaveChangesAsync(ct);

        return new TaskReadDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            IsCompleted = entity.IsCompleted,
            CreatedAt = entity.CreatedAt
        };
    }

    public async Task<IReadOnlyList<TaskReadDto>> GetLatestActiveAsync(int take = 5, CancellationToken ct = default)
    {
        return await db.Tasks
            .AsNoTracking()
            .Where(t => !t.IsCompleted)
            .OrderByDescending(t => t.CreatedAt)
            .Take(take)
            .Select(t => new TaskReadDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync(ct);
    }

    public async Task<bool> MarkDoneAsync(int id, CancellationToken ct = default)
    {
        var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id, ct);
        if (task is null) return false;
        if (!task.IsCompleted)
        {
            task.IsCompleted = true;
            await db.SaveChangesAsync(ct);
        }
        return true;
    }
}
