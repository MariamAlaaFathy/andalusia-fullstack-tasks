using System.ComponentModel.DataAnnotations;

namespace TaskNine.DTOs
{
    public class TaskSummaryDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
