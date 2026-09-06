using FullStackSession6.Model;
using FullStackSession6.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskTen.Data;
using TaskTen.DTOs;
using TaskTen.Exceptions;
using TaskTen.Model;
using static System.Net.WebRequestMethods;

namespace FullStackSession6.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly AppDbContext _dbcontext;

        public TasksRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<PagedResult<Tasks>> GetTasks(TaskFilterParams paginationParams, int userId)
        {
            var query = _dbcontext.Tasks
                .Where(t => t.UserId == userId)
                .AsQueryable();

            var totalCount = await query.CountAsync();

            if (!string.IsNullOrEmpty(paginationParams.Search))
            {
                query = query.Where(t => EF.Functions.Like(t.Title, $"%{paginationParams.Search}%"));
            }

            if (paginationParams.IsCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == paginationParams.IsCompleted.Value);
            }

            if (!string.IsNullOrEmpty(paginationParams.TaskStatus))
            {
                query = query.Where(t => t.TaskStatus!.ToLower() == paginationParams.TaskStatus.ToLower());
            }

            if (paginationParams.CreatedAfter.HasValue)
            {
                query = query.Where(t => t.CreatedAt > paginationParams.CreatedAfter.Value);
            }

            if (paginationParams.CreatedBefore.HasValue)
            {
                query = query.Where(t => t.CreatedAt < paginationParams.CreatedBefore.Value);
            }

            var allowedSort = new Dictionary<string, Expression<Func<Tasks, object>>>
            {
                ["id"] = t => t.Id,
                ["title"] = t => t.Title!,
                ["iscompleted"] = t => t.IsCompleted,
                ["taskstatus"] = t => t.TaskStatus!,
                ["duedate"] = t => t.DueDate,
                ["createdat"] = t => t.CreatedAt,
            };

            if (allowedSort.TryGetValue(paginationParams.SortBy ?? "createdat", out var keySelector))
            {
                query = paginationParams.Order == "desc"
                    ? query.OrderByDescending(keySelector)
                    : query.OrderBy(keySelector);
            }

            IEnumerable<Tasks> filteredTasks = await query.Skip((paginationParams.Page - 1) * paginationParams.PageSize).Take(paginationParams.PageSize).ToListAsync();
            return new PagedResult<Tasks>
            {
                Data = filteredTasks,
                Page = paginationParams.Page,
                PageSize = paginationParams.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Tasks> GetTaskById(int id)
        {
            if (await _dbcontext.Tasks.FirstOrDefaultAsync(t => t.Id == id) == null)
            {
                throw new NotFoundException("The requested task could not be found.");
            }
            return await _dbcontext.Tasks.Include(t => t.User).Where(t => t.Id == id).SingleAsync();
        }

        public async Task<Tasks> GetTaskByTitle(string title, int userId)
        {
            if (await _dbcontext.Tasks.FirstOrDefaultAsync(t => t.Title == title && t.UserId == userId) == null)
            {
                return null!;
            }
            return await _dbcontext.Tasks.Include(t => t.User).Where(t => t.Title == title && t.UserId == userId).SingleAsync();
        }

        public async Task<Tasks> CreateTask(Tasks task, int userId)
        {
            _dbcontext.Tasks.Add(task);
            await _dbcontext.SaveChangesAsync();
            return await _dbcontext.Tasks.Include(t => t.User).Where(t => t.Id == task.Id && t.UserId == userId).SingleAsync();

        }

        public async Task<Tasks> UpdateTask(int id, Tasks task)
        {
            var existingTask = await _dbcontext.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                throw new NotFoundException("The requested task could not be found.");
            }
            else if (await _dbcontext.Tasks.FirstOrDefaultAsync(t => t.Title == task.Title) != null && (await _dbcontext.Tasks.FirstOrDefaultAsync(t => t.Title == task.Title)).Id != id)
            {
                throw new ConflictException("A task with the same title already exists.");
            }
            else if (task.DueDate < DateTime.Now)
            {
                throw new DueDateInPastException("The due date cannot be in the past.");
            }

            if (task.Title != null) existingTask.Title = task.Title;
            existingTask.IsCompleted = task.IsCompleted;
            if (task.TaskStatus != null) existingTask.TaskStatus = task.TaskStatus;
            if (task.DueDate == DateTime.MinValue) existingTask.DueDate = task.DueDate;
            existingTask.UpdatedAt = DateTime.Now;

            await _dbcontext.SaveChangesAsync();
            return await _dbcontext.Tasks.Include(t => t.User).Where(t => t.Id == id).SingleAsync();
        }

        public async Task DeleteTask(int id)
        {
            _dbcontext.Tasks.Remove(await _dbcontext.Tasks.FirstOrDefaultAsync(t => t.Id == id));
            await _dbcontext.SaveChangesAsync();
        }
    }
}
