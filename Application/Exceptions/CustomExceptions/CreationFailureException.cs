namespace Application.Exceptions.CustomExceptions
{
    public class CreationFailureException : Exception
    {
        public CreationFailureException(string message) : base(message)
        {
        }
    }
}
