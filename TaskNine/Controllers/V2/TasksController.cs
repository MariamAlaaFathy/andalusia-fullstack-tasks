using Asp.Versioning;
using FullStackSession6.Model;
using FullStackSession6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TaskNine.DTOs;
using TaskNine.Model;

namespace TaskFive.Controllers.v2
{
    /// <summary>
    /// Version 2 - manages tasks, including pagination/filtering/sorting and a lightweight summary view.
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/tasks")]
    [Produces("application/json")]
    public class TasksController : ControllerBase
    {
        private ITasksService _taskService;

        public TasksController(ITasksService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Gets a paginated, filterable, sortable list of tasks.
        /// </summary>
        /// <param name="paginationParams">Search, status/completion filters, date range, sortBy, order, page, pageSize.</param>
        /// <response code="200">The paginated list of tasks.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<TaskSummaryDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasks([FromQuery] TaskFilterParams paginationParams)
        {
            var tasks = await _taskService.GetTasks(paginationParams);
            return Ok(tasks);
        }

        /// <summary>
        /// Gets a single task by id, in full detail.
        /// </summary>
        /// <param name="id">The task id.</param>
        /// <response code="200">The requested task.</response>
        /// <response code="404">No task exists with the given id.</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(TasksDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TasksDTO>> GetTaskById(int id)
        {
            return Ok(await _taskService.GetTaskById(id));
        }

        /// <summary>
        /// Gets a lightweight summary (id, title, isCompleted) for a single task.
        /// </summary>
        /// <param name="id">The task id.</param>
        /// <response code="200">The requested task summary.</response>
        /// <response code="404">No task exists with the given id.</response>
        [HttpGet]
        [Route("summary/{id}")]
        [ProducesResponseType(typeof(TaskSummaryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskSummaryDTO>> GetTaskSummaryById(int id)
        {
            return Ok(await _taskService.GetTaskSummaryById(id));
        }

        /// <summary>
        /// Creates a new task. Duplicate titles and past due dates are rejected.
        /// </summary>
        /// <param name="task">The title, due date, isCompleted, status, and owning user id.</param>
        /// <response code="201">The task was created. The response includes a Location header pointing to it.</response>
        /// <response code="400">The request body failed validation, or the due date is in the past.</response>
        /// <response code="404">The referenced user does not exist.</response>
        /// <response code="409">A task with the same title already exists.</response>
        [HttpPost]
        [ProducesResponseType(typeof(TasksDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<TasksDTO>> CreateTask([FromBody] CreateTaskRequest task)
        {
            var createdTask = await _taskService.CreateTask(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id, version = "2.0" }, createdTask);
        }

        /// <summary>
        /// Replaces an existing task's details.
        /// </summary>
        /// <param name="id">The task id.</param>
        /// <param name="task">The full replacement title, due date, isCompleted, status</param>
        /// <response code="200">The updated task.</response>
        /// <response code="400">The request body failed validation, or the due date is in the past.</response>
        /// <response code="404">No task exists with the given id.</response>
        /// <response code="409">Another task already has the given title.</response>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(TasksDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<TasksDTO>> UpdateTask(int id, [FromBody] UpdateTaskRequest task)
        {
            return Ok(await _taskService.UpdateTask(id, task));
        }

        /// <summary>
        /// Deletes a task.
        /// </summary>
        /// <param name="id">The task id.</param>
        /// <response code="204">The task was deleted.</response>
        /// <response code="404">No task exists with the given id.</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTask(id);
            return NoContent();
        }
    }
}
