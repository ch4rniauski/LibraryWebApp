namespace Application.Exceptions.CustomExceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message) : base(message) 
        {
        }
    }
}
