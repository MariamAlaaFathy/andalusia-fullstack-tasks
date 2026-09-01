using FullStackSession6.Model;
using TaskTen.DTOs;
using TaskTen.Model;

namespace TaskTen.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<PagedResult<UserDTO>> GetUsers(UserFilterParams paginationParams);
        public Task<UserDTO> GetUserById(int id);
        public Task RegisterUser(RegisterUserRequestDTO user);
        public Task<string> LoginUser(LoginUserRequestDTO user);
        public Task<UserDTO> UpdateUser(int id, UpdateUserRequestDTO user);
        public Task DeleteUser(int id);
    }
}
