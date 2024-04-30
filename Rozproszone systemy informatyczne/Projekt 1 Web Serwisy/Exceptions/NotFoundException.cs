namespace Projekt_1_Web_Serwisy.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(int id) : base($"Not found motorbike with ID: {id}") { }
}
