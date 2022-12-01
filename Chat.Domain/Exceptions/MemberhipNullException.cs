namespace Chat.Domain.Exceptions
{
    public class MemberhipNullException : Exception
    {
        public MemberhipNullException(Exception ex) : base(ex.Message, ex.InnerException) { }
        public MemberhipNullException(string? message) : base(message) { }
        public MemberhipNullException() { }

    }
}
