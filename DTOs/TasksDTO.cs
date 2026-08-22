using System.ComponentModel.DataAnnotations;
using TaskEight.Model;

namespace TaskEight.DTOs
{
    public class TasksDTO
    {
        [Required]
        public string? Title { get; set; }

        public bool IsCompleted { get; set; } = false;
        public string? TaskStatus { get; set; } = "Pending"; // Pending, In Progress, Completed
        public DateTime DueDate { get; set; }

        // Navigation property
        public String? UserName { get; set; }
    }
}
