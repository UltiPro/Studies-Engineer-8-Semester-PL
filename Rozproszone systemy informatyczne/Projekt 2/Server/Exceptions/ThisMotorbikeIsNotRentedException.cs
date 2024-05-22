namespace Server.Exceptions;

public class ThisMotorbikeIsNotRentedException : Exception
{
    public ThisMotorbikeIsNotRentedException() : base("Invoice Error: This motorbike is not rented.") { }
}
