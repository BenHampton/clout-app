namespace clout_api.Exceptions;

public class MockDataException : Exception
{
    public MockDataException()
    {
    }

    public MockDataException(string? message) : base(message)
    {
    }

    public MockDataException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
