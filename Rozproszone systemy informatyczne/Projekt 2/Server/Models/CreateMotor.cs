#pragma warning disable

namespace Server.Models;

public class CreateMotor
{
    public string Brand {  get; set; }
    public string Name { get; set; }
    public Licence RequiredLicence { get; set; }
    public string Description { get; set; }
    public int RentPrice { get; set; }
}
