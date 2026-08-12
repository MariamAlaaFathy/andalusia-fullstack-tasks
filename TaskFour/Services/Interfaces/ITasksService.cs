using FullStackSession6.Model;

namespace FullStackSession6.Services.Interfaces
{
    public interface ITasksService
    {
        public List<Tasks> GetTasks();
        public Tasks GetTaskById(int id);
        public Tasks CreateTask(Tasks task);
        public Tasks UpdateTask(int id, Tasks task);
        public void DeleteTask(int id);
    }
}
