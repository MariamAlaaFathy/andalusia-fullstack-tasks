using TaskTen.Enums;

namespace TaskTen.DTOs
{
    public class RegisterUserRequestDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; } = Role.User.ToString();
    }
}
