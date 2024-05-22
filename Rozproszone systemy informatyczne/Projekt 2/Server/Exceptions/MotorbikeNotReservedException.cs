namespace Server.Exceptions;

public class MotorbikeNotReservedException : Exception
{
    public MotorbikeNotReservedException() : base("This motorbike is not reserved!") { }
}
