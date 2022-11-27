namespace Chat.Domain.Exceptions
{
    public class HandlerException : Exception
    {
        public HandlerException(Exception ex) : base(ex.Message, ex.InnerException) { }
        public HandlerException(string? message) : base(message) { }
        public HandlerException() { }

    }
}
