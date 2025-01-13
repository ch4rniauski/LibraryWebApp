namespace Application.Exceptions.CustomExceptions
{
    public class IncorrectDataException : Exception
    {
        public IncorrectDataException(string message) : base(message)
        {
        }
    }
}
