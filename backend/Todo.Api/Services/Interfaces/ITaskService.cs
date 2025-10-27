using Todo.Api.Dtos;

namespace Todo.Api.Services.Interfaces;

public interface ITaskService
{
    Task<TaskReadDto> CreateAsync(TaskCreateDto dto, CancellationToken ct = default);
    Task<IReadOnlyList<TaskReadDto>> GetLatestActiveAsync(int take = 5, CancellationToken ct = default);
    Task<bool> MarkDoneAsync(int id, CancellationToken ct = default);
}
