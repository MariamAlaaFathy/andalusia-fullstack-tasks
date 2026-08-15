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
    }
}
