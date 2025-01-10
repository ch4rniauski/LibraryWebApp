namespace Domain.Exceptions.CustomExceptions
{
    public class RemovalFailureException : Exception
    {
        public RemovalFailureException(string message) : base(message)
        {
        }
    }
}
