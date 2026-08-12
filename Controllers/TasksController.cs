using FullStackSession6.Model;
using FullStackSession6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FullStackSession6.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private ITasksService _taskService;

        public TasksController(ITasksService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(_taskService.GetTasks());
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