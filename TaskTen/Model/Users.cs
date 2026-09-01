using FullStackSession6.Model;
using TaskTen.Enums;

namespace TaskTen.Model
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public Role Role { get; set; } = Role.User;

        // Navigation property
        public ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
    }
}
