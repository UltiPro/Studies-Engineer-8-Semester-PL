namespace Server.Exceptions;

public class MotorbikeCannotBeRentException : Exception
{
    public MotorbikeCannotBeRentException(DateTime retoTo) : base($"Motorbike can not be rent before {retoTo}.") { }
}
