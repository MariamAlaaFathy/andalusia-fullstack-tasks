using System.ComponentModel.DataAnnotations;

namespace TaskEight.DTOs
{
    public class TaskSummaryDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
