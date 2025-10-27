namespace Todo.Api.Dtos;

public class TaskReadDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
}
