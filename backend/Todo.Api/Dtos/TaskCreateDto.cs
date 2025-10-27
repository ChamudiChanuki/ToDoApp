using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Dtos;

public class TaskCreateDto
{
    [Required, StringLength(255)]
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
}
