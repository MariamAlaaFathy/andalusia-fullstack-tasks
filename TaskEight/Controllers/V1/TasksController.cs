using Asp.Versioning;
using FullStackSession6.Model;
using FullStackSession6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TaskEight.Model;

namespace TaskFive.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
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
            var projected = new PagedResult<object>
            {
                Data = tasks.Data.Select(t => new { id = t.Id, title = t.Title, isCompleted = t.IsCompleted }),
                Page = tasks.Page,
                PageSize = tasks.PageSize,
                TotalCount = tasks.TotalCount
            };
            return Ok(projected);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            Tasks task = await _taskService.GetTaskById(id);
            return Ok(new { id = task.Id, title = task.Title, isCompleted = task.IsCompleted });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Tasks task)
        {
            await _taskService.CreateTask(task);
            return CreatedAtAction(nameof(CreateTask), new { id = task.Id, title = task.Title, isCompleted = task.IsCompleted });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] Tasks task)
        {
            await _taskService.UpdateTask(id, task);
            return Ok(new { id = task.Id, title = task.Title, isCompleted = task.IsCompleted });
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