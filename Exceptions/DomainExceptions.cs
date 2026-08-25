namespace CinemaTicketBookingProject.Exceptions
{
    public class InvalidId : Exception
    {
        public InvalidId(string message) : base(message) { }
    }
    public class MovieNotFoundException : Exception
    {
        public MovieNotFoundException(string message) : base(message) { }
    }

    public class BookingNotFoundException : Exception
    {
        public BookingNotFoundException(string message) : base(message) { }
    }

    public class MovieAlreadyExistsException : Exception
    {
        public MovieAlreadyExistsException(string message) : base(message) { }
    }

    public class ShowTimeNotFoundException : Exception
    {
        public ShowTimeNotFoundException(string message) : base(message) { }
    }

    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string message) : base(message) { }
    }

    public class InvalidBookingException : Exception
    {
        public InvalidBookingException(string message) : base(message) { }
    }

    public class AuditoriumNotFoundException : Exception
    {
        public AuditoriumNotFoundException(string message) : base(message) { }
    }

    public class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException(string message) : base(message) { }
    }

    public class EntityShouldNotBeDeletedException : Exception
    {
        public EntityShouldNotBeDeletedException(string message) : base(message) { }
    }
}
