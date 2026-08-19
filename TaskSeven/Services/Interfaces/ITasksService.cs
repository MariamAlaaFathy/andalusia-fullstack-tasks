using FullStackSession6.Model;
using TaskSeven.Model;

namespace FullStackSession6.Services.Interfaces
{
    public interface ITasksService
    {
        public PagedResult<Tasks> GetTasks(TaskFilterParams paginationParams);
        public Tasks GetTaskById(int id);
        public Tasks CreateTask(Tasks task);
        public Tasks UpdateTask(int id, Tasks task);
        public void DeleteTask(int id);
    }
}
