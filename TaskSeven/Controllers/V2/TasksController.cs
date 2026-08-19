using Asp.Versioning;
using FullStackSession6.Model;
using FullStackSession6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TaskSeven.Model;

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
        public IActionResult GetTasks([FromQuery] TaskFilterParams paginationParams)
        {
            return Ok(_taskService.GetTasks(paginationParams));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTaskById(int id)
        {
            return Ok(_taskService.GetTaskById(id));
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] Tasks task)
        {
            _taskService.CreateTask(task);
            return CreatedAtAction(nameof(CreateTask), new { id = task.Id }, task);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] Tasks task)
        {
            _taskService.UpdateTask(id, task);
            return Ok(task);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTask(int id)
        {
            _taskService.DeleteTask(id);
            return NoContent();
        }
    }
}