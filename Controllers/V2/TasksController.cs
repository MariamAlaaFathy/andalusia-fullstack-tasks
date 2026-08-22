using Asp.Versioning;
using FullStackSession6.Model;
using FullStackSession6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TaskEight.DTOs;
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
        public async Task<ActionResult<TasksDTO>> GetTaskById(int id)
        {
            return Ok(await _taskService.GetTaskById(id));
        }

        [HttpGet]
        [Route("summary/{id}")]
        public async Task<ActionResult<TaskSummaryDTO>> GetTaskSummaryById(int id)
        {
            return Ok(await _taskService.GetTaskSummaryById(id));
        }

        [HttpPost]
        public async Task<ActionResult<TasksDTO>> CreateTask([FromBody] CreateTaskRequest task)
        {
            var createdTask = await _taskService.CreateTask(task);
            return CreatedAtAction(nameof(CreateTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<TasksDTO>> UpdateTask(int id, [FromBody] UpdateTaskRequest task)
        {
            return Ok(await _taskService.UpdateTask(id, task));
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