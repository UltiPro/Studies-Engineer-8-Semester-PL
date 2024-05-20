namespace Projekt_1_Web_Serwisy.Exceptions;

public class MotorbikeReservedException : Exception
{
    public MotorbikeReservedException() : base("This motorbike is already reserved!") { }
}
