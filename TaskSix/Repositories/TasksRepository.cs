using FullStackSession6.Model;
using FullStackSession6.Repositories.Interfaces;
using TaskFour.Middleware.Exceptions;
using TaskSix.Model;

namespace FullStackSession6.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private List<Tasks> _tasks = new List<Tasks>() {
            new Tasks(1,"Meeting 1", false, "In Progress", DateTime.Now.AddDays(7)),
            new Tasks(2,"Task 1", true, "Completed", DateTime.Now.AddDays(3)),
            new Tasks(3,"Meeting 2", false, "Pending", DateTime.Now.AddDays(5)),
            new Tasks(4,"Task 2", false, "In Progress", DateTime.Now.AddDays(10)),
            new Tasks(5,"Meeting 3", true, "Completed", DateTime.Now.AddDays(1)),
            new Tasks(6,"Meeting 4", false, "Pending", DateTime.Now.AddDays(7)),
            new Tasks(7,"Task 3", true, "Completed", DateTime.Now.AddDays(3)),
            new Tasks(8,"Meeting 5", false, "In Progress", DateTime.Now.AddDays(5)),
            new Tasks(9,"Task 4", false, "Pending", DateTime.Now.AddDays(10)),
            new Tasks(10,"Meeting 6", true, "Completed", DateTime.Now.AddDays(1)),
        };

        public PagedResult<Tasks> GetTasks(TaskFilterParams paginationParams)
        {
            IEnumerable<Tasks> tasks = _tasks;
            
            if(!string.IsNullOrEmpty(paginationParams.Search))
            {
                tasks = tasks.Where(t => t.Title!.Contains(paginationParams.Search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if(paginationParams.IsCompleted.HasValue)
            {
                tasks = tasks.Where(t => t.IsCompleted == paginationParams.IsCompleted.Value).ToList();
            }

            if (!string.IsNullOrEmpty(paginationParams.Status))
            {
                tasks = tasks.Where(t => t.Status!.Equals(paginationParams.Status, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (paginationParams.CreatedAfter.HasValue)
            {
                tasks = tasks.Where(t => t.CreatedAt > paginationParams.CreatedAfter.Value).ToList();
            }

            if (paginationParams.CreatedBefore.HasValue)
            {
                tasks = tasks.Where(t => t.CreatedAt < paginationParams.CreatedBefore.Value).ToList();
            }

            var allowedSort =
            new Dictionary<string, Func<Tasks, object>>
            {
                ["id"] = t => t.Id,
                ["title"] = t => t.Title!,
                ["iscompleted"] = t => t.IsCompleted,
                ["status"] = t => t.Status!,
                ["duedate"] = t => t.DueDate,
                ["createdat"] = t => t.CreatedAt,
            };

            if (allowedSort.TryGetValue(
                    paginationParams.SortBy ?? "createdat", out var keySelector))
            {
                tasks = paginationParams.Order == "desc"
                    ? tasks.OrderByDescending(keySelector)
                    : tasks.OrderBy(keySelector);
            }

            tasks = tasks.Skip((paginationParams.Page - 1) * paginationParams.PageSize).Take(paginationParams.PageSize).ToList();
            return new PagedResult<Tasks>
            {
                Data = tasks,
                Page = paginationParams.Page,
                PageSize = paginationParams.PageSize,
                TotalCount = _tasks.Count
            };
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
