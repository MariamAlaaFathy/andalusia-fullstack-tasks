using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskEight.Data;
using TaskEight.Exceptions;
using TaskEight.Model;
using TaskEight.Repositories.Interfaces;

namespace TaskEight.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _dbcontext;

        public UsersRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<PagedResult<Users>> GetUsers(UserFilterParams paginationParams)
        {
            var query = _dbcontext.Users.AsQueryable();

            var totalCount = await query.CountAsync();

            if (!string.IsNullOrEmpty(paginationParams.Search))
            {
                query = query.Where(u => EF.Functions.Like(u.Name, $"%{paginationParams.Search}%"));
            }

            var allowedSort = new Dictionary<string, Expression<Func<Users, object>>>
            {
                ["id"] = u => u.Id,
                ["name"] = u => u.Name!,
            };

            if (allowedSort.TryGetValue(paginationParams.SortBy ?? "name", out var keySelector))
            {
                query = paginationParams.Order == "desc"
                    ? query.OrderByDescending(keySelector)
                    : query.OrderBy(keySelector);
            }

            IEnumerable<Users> filteredUsers = await query.Skip((paginationParams.Page - 1) * paginationParams.PageSize).Take(paginationParams.PageSize).ToListAsync();
            return new PagedResult<Users>
            {
                Data = filteredUsers,
                Page = paginationParams.Page,
                PageSize = paginationParams.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Users> GetUserById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid user ID.");
            }
            else if (await _dbcontext.Users.FindAsync(id) == null)
            {
                throw new NotFoundException("The requested user could not be found.");
            }
            else
            {
                return await _dbcontext.Users.FindAsync(id);
            }
        }

        public async Task<Users> CreateUser(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            else
            {
                _dbcontext.Users.Add(user);
                await _dbcontext.SaveChangesAsync();
                return user;
            }
        }

        public async Task<Users> UpdateUser(int id, Users user)
        {
            Users existingUser = await _dbcontext.Users.FindAsync(id);
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid user ID.");
            }
            else if (existingUser == null)
            {
                throw new NotFoundException("The requested user could not be found.");
            }
            else
            {
                _dbcontext.Users.Update(user);
                await _dbcontext.SaveChangesAsync();
                return user;
            }
        }

        public async Task DeleteUser(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("You have provided an invalid user ID.");
            }
            else if (await _dbcontext.Users.FindAsync(id) == null)
            {
                throw new NotFoundException("The requested user could not be found.");
            }
            else
            {
                _dbcontext.Users.Remove(await _dbcontext.Users.FindAsync(id));
                await _dbcontext.SaveChangesAsync();
            }
        }
    }
}
