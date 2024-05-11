namespace Projekt_1_Web_Serwisy.Models;

public enum Licence
{
    B_A1, A2, A
}
public class DBMotor : UpdateMotor
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Name { get; set; }
    public Licence RequiredLicence { get; set; }
    public string Description { get; set; }
    public int RentPrice { get; set; }
    public DateTime? RentTo { get; set; } = null;
    public bool Reservation { get; set; } = false;
}
