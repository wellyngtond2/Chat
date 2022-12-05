namespace Chat.Domain.Exceptions
{
    public class PasswordNullException : Exception
    {
        public PasswordNullException(Exception ex) : base(ex.Message, ex.InnerException) { }
        public PasswordNullException(string? message) : base(message) { }
        public PasswordNullException() { }

    }
}
