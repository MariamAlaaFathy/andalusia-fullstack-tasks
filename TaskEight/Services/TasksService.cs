using FullStackSession6.Model;
using FullStackSession6.Repositories.Interfaces;
using FullStackSession6.Services.Interfaces;
using TaskEight.Model;

namespace FullStackSession6.Services
{
    public class TasksService : ITasksService
    {
        private ITasksRepository _taskRepository;
        public TasksService(ITasksRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<PagedResult<Tasks>> GetTasks(TaskFilterParams paginationParams)
        {
            return await _taskRepository.GetTasks(paginationParams);
        }

        public async Task<Tasks> GetTaskById(int id)
        {
            return await _taskRepository.GetTaskById(id);
        }

        public async Task<Tasks> CreateTask(Tasks task)
        {
            return await _taskRepository.CreateTask(task);
        }

        public async Task<Tasks> UpdateTask(int id, Tasks task)
        {
            return await _taskRepository.UpdateTask(id, task);
        }

        public async Task DeleteTask(int id)
        {
            await _taskRepository.DeleteTask(id);
        }
    }
}
