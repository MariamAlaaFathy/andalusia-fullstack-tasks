using System.ComponentModel.DataAnnotations;

namespace CinemaTicketBookingProject.DTOs
{
    public class MovieDTOV1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool AvailableInCinema { get; set; }
    }

    public class MovieDTOV2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool AvailableInCinema { get; set; }
    }

    public class CreateMovieRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required")]
        [MaxLength(200, ErrorMessage = "Genre cannot exceed 200 characters")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "ReleaseDate is required")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "AvailableInCinema is required")]
        public bool AvailableInCinema { get; set; }
    }

    public class UpdateMovieRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required")]
        [MaxLength(200, ErrorMessage = "Genre cannot exceed 200 characters")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "ReleaseDate is required")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "AvailableInCinema is required")]
        public bool AvailableInCinema { get; set; }
    }
}
