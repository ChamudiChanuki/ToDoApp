using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Todo.Api.Dtos;
using Todo.Api.Tests.Web;

namespace Todo.Api.Tests.Integration;

public class TasksControllerTests : IClassFixture<SqliteAppFactory>
{
    private readonly HttpClient _client;
    public TasksControllerTests(SqliteAppFactory factory) => _client = factory.CreateClient();

    [Fact]
    public async Task Get_returns_at_most_5_not_completed_desc()
    {
        for (var i = 0; i < 6; i++)
            await _client.PostAsJsonAsync("/api/tasks", new TaskCreateDto { Title = $"T{i}" });

        var res = await _client.GetAsync("/api/tasks");
        res.StatusCode.Should().Be(HttpStatusCode.OK);

        var list = await res.Content.ReadFromJsonAsync<List<TaskReadDto>>();
        list!.Count.Should().BeLessOrEqualTo(5);
        list.Should().OnlyContain(t => !t.IsCompleted);
        list.Should().BeInDescendingOrder(t => t.CreatedAt);
    }

    [Fact]
    public async Task Put_done_hides_item_from_get()
    {
        var made = await (await _client.PostAsJsonAsync("/api/tasks",
            new TaskCreateDto { Title = "Complete me" }))
            .Content.ReadFromJsonAsync<TaskReadDto>();

        var put = await _client.PutAsync($"/api/tasks/{made!.Id}/done", null);
        put.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var list = await (await _client.GetAsync("/api/tasks")).Content.ReadFromJsonAsync<List<TaskReadDto>>();
        list!.Any(x => x.Id == made.Id).Should().BeFalse();
    }
}
