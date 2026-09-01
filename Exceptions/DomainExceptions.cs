namespace TaskTen.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }

    public class DueDateInPastException : Exception
    {
        public DueDateInPastException(string message) : base(message) { }
    }

    public class WrongPasswordException : Exception
    {
        public WrongPasswordException(string message) : base(message) { }
    }
}
