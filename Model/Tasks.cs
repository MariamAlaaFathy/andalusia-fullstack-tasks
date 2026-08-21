using System.ComponentModel.DataAnnotations;
using TaskEight.Model;

namespace FullStackSession6.Model
{
    public class Tasks
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }

        public bool IsCompleted { get; set; } = false;
        public string? TaskStatus { get; set; } = "Pending"; // Pending, In Progress, Completed
        [Required]
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int UserId { get; set; }

        // Navigation property
        public Users? User { get; set; }
    }
}
