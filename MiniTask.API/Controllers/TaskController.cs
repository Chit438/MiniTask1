
using Microsoft.AspNetCore.Mvc;
using MiniTask.API.Models;

namespace MiniTask.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private static readonly List<TaskItem> Tasks = new()
    {
        new TaskItem
        {
            Id = 1,
            Title = "Learn CI/CD",
            IsCompleted = false
        },
        new TaskItem
        {
            Id = 2,
            Title = "Learn Docker",
            IsCompleted = false
        }
    };

    // GET: api/tasks
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Tasks);
    }

    // GET: api/tasks/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var task = Tasks.FirstOrDefault(x => x.Id == id);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    // POST: api/tasks
    [HttpPost]
    public IActionResult Create(TaskItem task)
    {
        task.Id = Tasks.Count == 0
            ? 1
            : Tasks.Max(x => x.Id) + 1;

        Tasks.Add(task);

        return CreatedAtAction(
            nameof(GetById),
            new { id = task.Id },
            task);
    }

    // DELETE: api/tasks/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var task = Tasks.FirstOrDefault(x => x.Id == id);

        if (task == null)
            return NotFound();

        Tasks.Remove(task);

        return NoContent();
    }
}
