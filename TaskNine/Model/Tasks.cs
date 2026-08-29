using System.ComponentModel.DataAnnotations;
using TaskNine.Model;

namespace FullStackSession6.Model
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public bool IsCompleted { get; set; } = false;
        public string TaskStatus { get; set; } = "Pending"; // Pending, In Progress, Completed
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        // Navigation property
        public Users? User { get; set; }
    }
}
