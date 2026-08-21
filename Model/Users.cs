using FullStackSession6.Model;
using System.ComponentModel.DataAnnotations;

namespace TaskSeven.Model
{
    public class Users
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        // Navigation property
        public ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
    }
}
