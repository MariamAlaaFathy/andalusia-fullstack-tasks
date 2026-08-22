using System.ComponentModel.DataAnnotations;

namespace TaskEight.DTOs
{
    public class UpdateTaskRequest
    {
        [Required]
        public string Title { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string TaskStatus { get; set; } = "Pending"; // Pending, In Progress, Completed
        public DateTime DueDate { get; set; }
    }
}
