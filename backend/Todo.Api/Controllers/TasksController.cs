using Microsoft.AspNetCore.Mvc;
using Todo.Api.Dtos;
using Todo.Api.Services.Interfaces;

namespace Todo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(ITaskService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<TaskReadDto>> Create([FromBody] TaskCreateDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var created = await service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetLatest), new { }, created);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetLatest(CancellationToken ct)
    {
        var data = await service.GetLatestActiveAsync(5, ct);
        return Ok(data);
    }

    [HttpPut("{id:int}/done")]
    public async Task<IActionResult> MarkDone(int id, CancellationToken ct)
    {
        var ok = await service.MarkDoneAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
