#pragma warning disable

namespace Server.Models;

public class DetailMotor
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Name { get; set; }
    public string RequiredLicence { get; set; }
    public string Description { get; set; }
    public int RentPrice { get; set; }
    public string RentTo { get; set; }
    public string Reservation { get; set; }
}
