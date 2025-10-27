namespace Todo.Api.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTimeOffset CreatedAt { get; set; }
}
