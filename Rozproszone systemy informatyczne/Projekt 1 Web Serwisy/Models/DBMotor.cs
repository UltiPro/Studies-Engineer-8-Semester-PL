namespace Projekt_1_Web_Serwisy.Models;

public class DBMotor : UpdateMotor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RentPrice { get; set; }
    public DateTime? RentTo { get; set; } = null;
    public bool Reservation { get; set; } = false;
}
