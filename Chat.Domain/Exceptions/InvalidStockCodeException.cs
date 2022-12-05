namespace Chat.Domain.Exceptions
{
    public class InvalidStockCodeException : Exception
    {
        public InvalidStockCodeException(Exception ex) : base(ex.Message, ex.InnerException) { }
        public InvalidStockCodeException(string? message) : base(message) { }
        public InvalidStockCodeException() { }

    }
}
