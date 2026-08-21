using Asp.Versioning;
using FullStackSession6.Model;
using FullStackSession6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TaskEight.Model;

namespace TaskFive.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/tasks")]
    public class TasksController : ControllerBase
    {
        private ITasksService _taskService;

        public TasksController(ITasksService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] TaskFilterParams paginationParams)
        {
            var tasks = await _taskService.GetTasks(paginationParams);
            return Ok(tasks);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            return Ok(new
            {
                task.Id,
                task.Title,
                task.IsCompleted,
                task.TaskStatus,
                task.DueDate,
                task.CreatedAt,
                task.UserId,
                task.User?.Name,
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Tasks task)
        {
            await _taskService.CreateTask(task);
            return CreatedAtAction(nameof(CreateTask), new { id = task.Id }, task);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] Tasks task)
        {
            await _taskService.UpdateTask(id, task);
            return Ok(new
            {
                task.Id,
                task.Title,
                task.IsCompleted,
                task.TaskStatus,
                task.DueDate,
                task.CreatedAt,
                task.UserId,
                task.User?.Name,
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTask(id);
            return NoContent();
        }
    }
}