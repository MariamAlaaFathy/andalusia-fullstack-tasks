using System.ComponentModel.DataAnnotations;
using TaskTen.Model;

namespace TaskTen.DTOs
{
    public class TasksDTO
    {
        public string Title { get; set; }

        public bool IsCompleted { get; set; } = false;
        public string TaskStatus { get; set; } = "Pending"; // Pending, In Progress, Completed
        public DateTime DueDate { get; set; }

        // Navigation property
        public String UserName { get; set; }
    }
}
