using FullStackSession6.Model;
using TaskTen.DTOs;
using TaskTen.Model;

namespace FullStackSession6.Services.Interfaces
{
    public interface ITasksService
    {
        public Task<PagedResult<TaskSummaryDTO>> GetTasks(TaskFilterParams paginationParams, int userId);
        public Task<TasksDTO> GetTaskById(int id, int userId);
        public Task<TaskSummaryDTO> GetTaskSummaryById(int id, int userId);
        public Task<Tasks> GetTaskByTitle(string title, int userId);
        public Task<TasksDTO> CreateTask(CreateTaskRequest task, int userId);
        public Task<TasksDTO> UpdateTask(int id, UpdateTaskRequest task, int userId);
        public Task DeleteTask(int id, int userId);
    }
}
