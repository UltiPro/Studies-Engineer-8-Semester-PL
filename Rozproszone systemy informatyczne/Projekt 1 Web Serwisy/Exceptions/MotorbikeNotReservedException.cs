namespace Projekt_1_Web_Serwisy.Exceptions;

public class MotorbikeNotReservedException : Exception
{
    public MotorbikeNotReservedException() : base("This motorbike is not reserved!") { }
}
