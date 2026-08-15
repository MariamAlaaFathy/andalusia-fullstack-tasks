using FullStackSession6.Model;
using FullStackSession6.Repositories.Interfaces;
using TaskFour.Middleware.Exceptions;

namespace FullStackSession6.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private List<Tasks> _tasks = new List<Tasks>();

        public List<Tasks> GetTasks()
        {
            return _tasks;
        }

        public Tasks GetTaskById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid task ID.");
            }
            else if (_tasks.FirstOrDefault(p => p.Id == id) == null)
            {
                throw new NotFoundException("The requested task could not be found.");
            }
            else
            {
                return _tasks.FirstOrDefault(p => p.Id == id)!;
            }
        }

        public Tasks CreateTask(Tasks task) {
            if (task == null) {
                throw new ArgumentNullException(nameof(task));
            } else if (_tasks.FirstOrDefault(p => p.Title == task.Title) != null)
            {
                throw new ConflictException("A task with the same title already exists.");
            } else if(task.DueDate < DateTime.Now)
            {
                throw new DueDateInPastException("The due date cannot be in the past.");
            } else
            {
                for(int i = 1; i <= _tasks.Count + 1; i++)
                {
                    if (_tasks.FirstOrDefault(p => p.Id == i) == null)
                    {
                        task.Id = i;
                        break;
                    }
                }
                _tasks.Add(task);
                return task;
            }
        }

        public Tasks UpdateTask(int id, Tasks task)
        {
            Tasks existingTask = _tasks.FirstOrDefault(p => p.Id == id)!;
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid task ID.");
            }
            else if (existingTask == null)
            {
                throw new NotFoundException("The requested task could not be found.");
            }
            else if (_tasks.FirstOrDefault(p => p.Title == task.Title && p.Id != id) != null)
            {
                throw new ConflictException("A task with the same title already exists.");
            }
            else if(task.DueDate < DateTime.Now)
            {
                throw new DueDateInPastException("The due date cannot be in the past.");
            }
            else
            {
                if (task.Id == 0) task.Id = existingTask.Id;
                _tasks.Remove(existingTask);
                _tasks.Add(task);
                return task;
            }
        }

        public void DeleteTask(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid task ID.");
            }
            else if (_tasks.FirstOrDefault(p => p.Id == id) == null)
            {
                throw new NotFoundException("The requested task could not be found.");
            }
            else
            {
                _tasks.RemoveAll(p => p.Id == id);
            }
        }
    }
}
