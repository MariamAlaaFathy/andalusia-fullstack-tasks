using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskTen.Data;
using TaskTen.DTOs;
using TaskTen.Enums;
using TaskTen.Exceptions;
using TaskTen.Model;
using TaskTen.Repositories.Interfaces;

namespace TaskTen.Repositories
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
            if (await _dbcontext.Users.FindAsync(id) == null)
            {
                throw new NotFoundException("The requested user could not be found.");
            }
            else
            {
                return await _dbcontext.Users.Where(u => u.Id == id).SingleAsync();
            }
        }

        public async Task<Users> GetUserByEmail(string email)
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task RegisterUser(Users user)
        {
            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<Users> LoginUser(LoginUserRequestDTO user)
        {
            var userFound = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (userFound is null || !BCrypt.Net.BCrypt.Verify(user.Password, userFound.HashedPassword))
                return null;

            return userFound;
        }

        public async Task<Users> UpdateUser(int id, UpdateUserRequestDTO user)
        {
            Users oldUser = await _dbcontext.Users.FindAsync(id);
            if (oldUser == null)
            {
                throw new NotFoundException("The requested user could not be found.");
            }
            else if (!BCrypt.Net.BCrypt.Verify(user.CurrentPassword, oldUser.HashedPassword))
            {
                throw new WrongPasswordException("The current password you inserted is incorrect.");
            }
            else if (user.CurrentPassword == user.NewPassword)
            {
                throw new WrongPasswordException("The new password you inserted is the same as the current password.");
            }
            else if (await GetUserByEmail(user.Email) != null && (await GetUserByEmail(user.Email)).Id != id)
            {
                throw new ConflictException("A user with the same email already exists.");
            }

            if(user.Name != null) oldUser.Name = user.Name;
            if(user.Email != null) oldUser.Email = user.Email;
            oldUser.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.NewPassword);
            oldUser.Role = Enum.Parse<Role>(user.Role, true);

            await _dbcontext.SaveChangesAsync();
            return await _dbcontext.Users.Where(u => u.Id == id).SingleAsync();

        }

        public async Task DeleteUser(int id)
        {
            if (await _dbcontext.Users.FindAsync(id) == null)
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
