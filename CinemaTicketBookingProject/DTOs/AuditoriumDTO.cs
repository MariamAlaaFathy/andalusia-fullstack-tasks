using System.ComponentModel.DataAnnotations;

namespace CinemaTicketBookingProject.DTOs
{
    public class AuditoriumDTO
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
    }

    public class CreateAuditoriumRequest
    {
        [Required(ErrorMessage = "RoomNumber is required")]
        [Range(1, 20, ErrorMessage = "Capacity must be between 1 and 20")]
        public int RoomNumber { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 200, ErrorMessage = "Capacity must be between 1 and 200")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Available is required")]
        public bool Available { get; set; }
    }

    public class UpdateAuditoriumRequest
    {
        [Required(ErrorMessage = "RoomNumber is required")]
        [Range(1, 20, ErrorMessage = "Capacity must be between 1 and 20")]
        public int RoomNumber { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 200, ErrorMessage = "Capacity must be between 1 and 200")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Available is required")]
        public bool Available { get; set; }
    }
}
