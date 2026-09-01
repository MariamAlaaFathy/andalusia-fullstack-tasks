using System.ComponentModel.DataAnnotations;
using TaskTen.Enums;

namespace TaskTen.DTOs
{
    public class UpdateUserRequestDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string Role { get; set; }
    }
}
