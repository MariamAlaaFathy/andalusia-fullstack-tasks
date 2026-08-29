using System.ComponentModel.DataAnnotations;

namespace TaskNine.DTOs
{
    public class UpdateTaskRequest
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string TaskStatus { get; set; } = "Pending"; // Pending, In Progress, Completed
        public DateTime DueDate { get; set; }
    }
}
