using FullStackSession6.Model;
using FullStackSession6.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskSeven.Data;
using TaskSeven.Exceptions;
using TaskSeven.Model;

namespace FullStackSession6.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly AppDbContext _dbcontext;

        public TasksRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<PagedResult<Tasks>> GetTasks(TaskFilterParams paginationParams)
        {
            IEnumerable<Tasks> tasks = await _dbcontext.Tasks.ToListAsync();

            if (!string.IsNullOrEmpty(paginationParams.Search))
            {
                tasks = tasks.Where(t => t.Title!.Contains(paginationParams.Search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (paginationParams.IsCompleted.HasValue)
            {
                tasks = tasks.Where(t => t.IsCompleted == paginationParams.IsCompleted.Value).ToList();
            }

            if (!string.IsNullOrEmpty(paginationParams.TaskStatus))
            {
                tasks = tasks.Where(t => t.TaskStatus!.Equals(paginationParams.TaskStatus, StringComparison.OrdinalIgnoreCase)).ToList();
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
                ["taskstatus"] = t => t.TaskStatus!,
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

            IEnumerable<Tasks> filteredTasks = tasks.Skip((paginationParams.Page - 1) * paginationParams.PageSize).Take(paginationParams.PageSize).ToList();
            return new PagedResult<Tasks>
            {
                Data = filteredTasks,
                Page = paginationParams.Page,
                PageSize = paginationParams.PageSize,
                TotalCount = tasks.Count()
            };
        }

        public async Task<Tasks> GetTaskById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid task ID.");
            }
            else if (await _dbcontext.Tasks.FindAsync(id) == null)
            {
                throw new NotFoundException("The requested task could not be found.");
            }
            else
            {
                return await _dbcontext.Tasks.Include(t => t.User).Where(t => t.Id == id).SingleAsync();
            }
        }

        public async Task<Tasks> CreateTask(Tasks task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            else if (await _dbcontext.Tasks.AnyAsync(t => t.Title == task.Title))
            {
                throw new ConflictException("A task with the same title already exists.");
            }
            else if (task.DueDate < DateTime.Now)
            {
                throw new DueDateInPastException("The due date cannot be in the past.");
            }
            else
            {
                _dbcontext.Tasks.Add(task);
                await _dbcontext.SaveChangesAsync();
                return await _dbcontext.Tasks.Include(t => t.User).Where(t => t.Id == task.Id).SingleAsync();
            }
        }

        public async Task<Tasks> UpdateTask(int id, Tasks task)
        {
            var existingTask = await _dbcontext.Tasks.FindAsync(id);
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid task ID.");
            }
            else if (existingTask == null)
            {
                throw new NotFoundException("The requested task could not be found.");
            }
            else if (await _dbcontext.Tasks.AnyAsync(t => t.Title == task.Title) && (await _dbcontext.Tasks.FirstOrDefaultAsync(t => t.Title == task.Title)).Id != id)
            {
                throw new ConflictException("A task with the same title already exists.");
            }
            else if (task.DueDate < DateTime.Now)
            {
                throw new DueDateInPastException("The due date cannot be in the past.");
            }
            else
            {
                if (task.Title != null) existingTask.Title = task.Title;
                if (task.IsCompleted != false) existingTask.IsCompleted = task.IsCompleted;
                if (task.TaskStatus != null) existingTask.TaskStatus = task.TaskStatus;
                if (task.DueDate == DateTime.MinValue) existingTask.DueDate = task.DueDate;
                if (task.UserId != 0) existingTask.UserId = task.UserId;
                await _dbcontext.SaveChangesAsync();
                return await _dbcontext.Tasks.Include(t => t.User).Where(t => t.Id == id).SingleAsync();
            }
        }

        public async Task DeleteTask(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid task ID.");
            }
            else if (await _dbcontext.Tasks.FindAsync(id) == null)
            {
                throw new NotFoundException("The requested task could not be found.");
            }
            else
            {
                _dbcontext.Tasks.Remove(await _dbcontext.Tasks.FindAsync(id));
                await _dbcontext.SaveChangesAsync();
            }
        }
    }
}
