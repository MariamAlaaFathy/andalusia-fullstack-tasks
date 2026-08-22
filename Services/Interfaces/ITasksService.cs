using FullStackSession6.Model;
using TaskEight.DTOs;
using TaskEight.Model;

namespace FullStackSession6.Services.Interfaces
{
    public interface ITasksService
    {
        public Task<PagedResult<Tasks>> GetTasks(TaskFilterParams paginationParams);
        public Task<TasksDTO> GetTaskById(int id);
        public Task<TaskSummaryDTO> GetTaskSummaryById(int id);
        public Task<Tasks> GetTaskByTitle(string title);
        public Task<TasksDTO> CreateTask(Tasks task);
        public Task<TasksDTO> UpdateTask(int id, Tasks task);
        public Task DeleteTask(int id);
    }
}
