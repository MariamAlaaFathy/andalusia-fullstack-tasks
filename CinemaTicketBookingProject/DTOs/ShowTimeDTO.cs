using System.ComponentModel.DataAnnotations;

namespace CinemaTicketBookingProject.DTOs
{
    public class ShowTimeDTO
    {
        public int Id { get; set; }
        public DateTime Show_Time { get; set; }

        public string MovieName { get; set; }

        public int RoomNumber { get; set; }
    }

    public class CreateShowTimeRequest
    {
        [Required(ErrorMessage = "Show_Time is required")]
        public DateTime Show_Time { get; set; }

        [Required(ErrorMessage = "MovieId is required")]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "AuditoriumId is required")]
        public int AuditoriumId { get; set; }
    }
    public class UpdateShowTimeRequest
    {
        [Required(ErrorMessage = "Show_Time is required")]
        public DateTime Show_Time { get; set; }

        [Required(ErrorMessage = "MovieId is required")]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "AuditoriumId is required")]
        public int AuditoriumId { get; set; }
    }
}
