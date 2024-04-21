using Microsoft.EntityFrameworkCore;
using Projekt_1_Web_Serwisy.Database;
using Projekt_1_Web_Serwisy.Models;

namespace Projekt_1_Web_Serwisy.SOAPMotor;

public class MotorService : IMotorService
{
    private readonly DatabaseContext _context;

    public MotorService(DatabaseContext _context)
    {
        this._context = _context;
    }

    public async Task<List<DBMotor>> GetAll()
    {
        return await _context.Motors.ToListAsync();
    }

    public async Task<string> Reserve(int id)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) return "Incorrect ID of motorbike.";

        if (motor.Reservation) return "This motorbike is already reserved!";

        motor.Reservation = true;

        motor.Reservation = true;

        return $"The motorbike has been reserved.";
    }

    public async Task<string> CancelReserve(int id)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) return "Incorrect ID of motorbike.";

        if (motor.Reservation) return "This motorbike is not reserved!";

        motor.Reservation = false;

        return $"The motorbike is no longer reserved.";
    }

    public async Task<string> Rent(int id, DateTime date)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) return "Incorrect ID of motorbike.";

        if (motor.RentTo != null && motor.RentTo > date) return $"Motorbike can not be rent before {date}.";

        motor.RentTo = date;

        return "The motorbike has been rented.";
    }
}
