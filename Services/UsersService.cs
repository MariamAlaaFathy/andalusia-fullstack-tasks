using FullStackSession6.Model;
using TaskNine.Model;
using TaskNine.Repositories.Interfaces;
using TaskNine.Services.Interfaces;

namespace TaskNine.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<PagedResult<Users>> GetUsers(UserFilterParams paginationParams)
        {
            return _userRepository.GetUsers(paginationParams);
        }
        public async Task<Users> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<Users> CreateUser(Users user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task<Users> UpdateUser(int id, Users user)
        {
            return await _userRepository.UpdateUser(id, user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }
    }
}
