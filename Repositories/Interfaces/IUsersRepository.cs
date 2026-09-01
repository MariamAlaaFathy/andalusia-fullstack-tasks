using TaskTen.DTOs;
using TaskTen.Model;

namespace TaskTen.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        public Task<PagedResult<Users>> GetUsers(UserFilterParams paginationParams);
        public Task<Users> GetUserById(int id);
        public Task<Users> GetUserByEmail(string email);
        public Task RegisterUser(Users user);
        public Task<Users> LoginUser(LoginUserRequestDTO user);
        public Task<Users> UpdateUser(int id, UpdateUserRequestDTO user);
        public Task DeleteUser(int id);
    }
}
