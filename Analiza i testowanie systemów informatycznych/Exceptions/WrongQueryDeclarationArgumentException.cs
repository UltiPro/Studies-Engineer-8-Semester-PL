namespace SPA.Exceptions;

public class WrongQueryDeclarationArgumentException : Exception
{
    public WrongQueryDeclarationArgumentException(string message, Exception e) : base(message, e) { }

}