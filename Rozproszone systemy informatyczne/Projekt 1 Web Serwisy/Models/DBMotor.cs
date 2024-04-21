using System.ComponentModel.DataAnnotations;

namespace Projekt_1_Web_Serwisy.Models;

public class DBMotor
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? SpriteURL { get; set; } = null;
    public int RentPrice { get; set; }
    public DateTime? RentTo { get; set; } = null;
    public bool Reservation { get; set; } = false;
}
