using TaskSeven.Model;

namespace TaskSeven.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        public Task<PagedResult<Users>> GetUsers(UserFilterParams paginationParams);
        public Task<Users> GetUserById(int id);
        public Task<Users> CreateUser(Users user);
        public Task<Users> UpdateUser(int id, Users user);
        public Task DeleteUser(int id);
    }
}
