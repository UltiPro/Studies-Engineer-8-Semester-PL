namespace Server.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(int id) : base($"Not found motorbike with ID: {id}") { }
}
