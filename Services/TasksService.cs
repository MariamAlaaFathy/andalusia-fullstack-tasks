using FullStackSession6.Model;
using FullStackSession6.Repositories.Interfaces;
using FullStackSession6.Services.Interfaces;
using TaskSix.Model;

namespace FullStackSession6.Services
{
    public class TasksService : ITasksService
    {
        private ITasksRepository _repo;
        public TasksService(ITasksRepository repo)
        {
            _repo = repo;
        }

        public PagedResult<Tasks> GetTasks(TaskFilterParams paginationParams)
        {
            return _repo.GetTasks(paginationParams);
        }

        public Tasks GetTaskById(int id)
        {
            return _repo.GetTaskById(id);
        }

        public Tasks CreateTask(Tasks task)
        {
            return _repo.CreateTask(task);
        }

        public Tasks UpdateTask(int id, Tasks task)
        {
            return _repo.UpdateTask(id, task);
        }

        public void DeleteTask(int id)
        {
            _repo.DeleteTask(id);
        }
    }
}
