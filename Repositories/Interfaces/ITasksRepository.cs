using FullStackSession6.Model;
using TaskTen.DTOs;
using TaskTen.Model;

namespace FullStackSession6.Repositories.Interfaces
{
    public interface ITasksRepository
    {
        public Task<PagedResult<Tasks>> GetTasks(TaskFilterParams paginationParams, int userId);
        public Task<Tasks> GetTaskById(int id, int userId);
        public Task<Tasks> GetTaskByTitle(string title, int userId);
        public Task<Tasks> CreateTask(Tasks task, int userId);
        public Task<Tasks> UpdateTask(int id, Tasks task, int userId);
        public Task DeleteTask(int id, int userId);
    }
}
