using AutoMapper;
using FullStackSession6.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskTen.DTOs;
using TaskTen.Exceptions;
using TaskTen.Model;
using TaskTen.Repositories;
using TaskTen.Repositories.Interfaces;
using TaskTen.Services.Interfaces;

namespace TaskTen.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UsersService(IUsersRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
        }

        public async Task<PagedResult<UserDTO>> GetUsers(UserFilterParams paginationParams)
        {
            var users = await _userRepository.GetUsers(paginationParams);
            return new PagedResult<UserDTO>
            {
                Data = _mapper.Map<List<UserDTO>>(users.Data),
                Page = users.Page,
                PageSize = users.PageSize,
                TotalCount = users.TotalCount
            };
        }
        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            var mappedUser = _mapper.Map<UserDTO>(user);
            return mappedUser;
        }

        public async Task RegisterUser(RegisterUserRequestDTO user)
        {
            if (await _userRepository.GetUserByEmail(user.Email) != null)
            {
                throw new ConflictException("A user with the same email already exists.");
            }
            var mappedUser = _mapper.Map<Users>(user);
            await _userRepository.RegisterUser(mappedUser);
        }

        public async Task<string> LoginUser(LoginUserRequestDTO user)
        {

            var userFound = await _userRepository.LoginUser(user);

            if (userFound == null) return null;

            var token = GenerateToken(userFound);
            return token;
        }

        public async Task<UserDTO> UpdateUser(int id, UpdateUserRequestDTO user)
        {
            var updatedUser = await _userRepository.UpdateUser(id, user);
            var mappedUser = _mapper.Map<UserDTO>(updatedUser);
            return mappedUser;
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        public string GenerateToken(Users user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("Department","Test")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_config["Jwt:ExpiryMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
