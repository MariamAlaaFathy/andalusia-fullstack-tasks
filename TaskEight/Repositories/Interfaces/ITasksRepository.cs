using FullStackSession6.Model;
using TaskEight.Model;

namespace FullStackSession6.Repositories.Interfaces
{
    public interface ITasksRepository
    {
        public Task<PagedResult<Tasks>> GetTasks(TaskFilterParams paginationParams);
        public Task<Tasks> GetTaskById(int id);
        public Task<Tasks> CreateTask(Tasks task);
        public Task<Tasks> UpdateTask(int id, Tasks task);
        public Task DeleteTask(int id);
    }
}
