namespace Server.Exceptions;

public class NotAuthorizedException : Exception
{
    public NotAuthorizedException() : base("Incorrect login data.") { }
}
