using System.ComponentModel.DataAnnotations;

namespace FullStackSession6.Model
{
    public class Tasks
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }

        public bool IsCompleted { get; set; }
        public string? Status { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(1);
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Tasks(int id, string? title, bool isCompleted, string? status, DateTime dueDate)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
            Status = status;
            DueDate = dueDate;
        }
    }
}
