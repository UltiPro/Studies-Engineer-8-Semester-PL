namespace Server.Exceptions;

public class MotorbikeReservedException : Exception
{
    public MotorbikeReservedException() : base("This motorbike is already reserved!") { }
}
