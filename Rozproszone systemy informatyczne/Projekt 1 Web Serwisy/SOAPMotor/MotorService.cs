using Microsoft.EntityFrameworkCore;
using Projekt_1_Web_Serwisy.Database;
using Projekt_1_Web_Serwisy.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Projekt_1_Web_Serwisy.SOAPMotor;

public class MotorService : IMotorService
{
    private readonly DatabaseContext _context;

    public MotorService(DatabaseContext _context)
    {
        this._context = _context;
    }

    public async Task Create(CreateMotor motor)
    {
        await _context.Motors.AddAsync(new DBMotor
        {
            Name = motor.Name,
            RentPrice = motor.RentPrice
        });

        await _context.SaveChangesAsync();
    }

    public async Task Remove(int id)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) return;

        _context.Motors.Remove(motor);

        await _context.SaveChangesAsync();
    }

    public async Task Update(UpdateMotor motor)
    {
        var motor2 = await _context.Motors.FirstOrDefaultAsync(m => m.Id == motor.Id);

        if (motor2 is null) return;

        motor2.Name = motor.Name;
        motor2.RentPrice = motor.RentPrice;

        await _context.SaveChangesAsync();
    }

    public async Task<DetailMotor> Detail(int id)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) return null;

        return new DetailMotor
        {
            Id = motor.Id,
            Name = motor.Name,
            RentPrice = motor.RentPrice,
            RentTo = motor.RentTo.ToString() ?? "",
            Reservation = motor.Reservation ? "Reserved" : "Not reserved"
        };
    }

    public async Task<List<DetailMotor>> GetAll()
    {
        var motors = await _context.Motors.ToListAsync();

        var list = new List<DetailMotor>();

        foreach (var motor in motors)
        {
            list.Add(new DetailMotor
            {
                Id = motor.Id,
                Name = motor.Name,
                RentPrice = motor.RentPrice,
                RentTo = motor.RentTo.ToString() ?? "",
                Reservation = motor.Reservation ? "Reserved" : "Not reserved"
            });
        }

        return list;
    }

    public async Task<string> Reserve(int id)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) return "Incorrect ID of motorbike.";

        if (motor.Reservation) return "This motorbike is already reserved!";

        motor.Reservation = true;

        await _context.SaveChangesAsync();

        return $"The motorbike has been reserved.";
    }

    public async Task<string> CancelReserve(int id)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) return "Incorrect ID of motorbike.";

        if (!motor.Reservation) return "This motorbike is not reserved!";

        motor.Reservation = false;

        await _context.SaveChangesAsync();

        return $"The motorbike is no longer reserved.";
    }

    public async Task<string> Rent(int id, int numberOfDays)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) return "Incorrect ID of motorbike.";

        var rentDate = DateTime.Now.AddDays(numberOfDays);

        if (motor.RentTo != null && motor.RentTo < DateTime.Now) return $"Motorbike can not be rent before {motor.RentTo}.";

        motor.RentTo = rentDate;

        await _context.SaveChangesAsync();

        return $"The motorbike has been rented to {rentDate}.";
    }

    public async Task<byte[]?> GeneratePDF(int id)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) return null;

        string invoiceName = $"Invoice {DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}" +
            $"{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}.pdf";

        QuestPDF.Settings.License = LicenseType.Community;

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(16));

                page.Header().AlignCenter().Text($"{motor.Name}").Bold().FontSize(24).FontColor(Colors.Grey.Darken4);

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(20);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });
                    table.Header(header =>
                    {
                        header.Cell().Text("#");
                        header.Cell().Text("Motorbike");
                        header.Cell().AlignRight().Text("Rent Price");
                    });
                    table.Cell().Text(motor.Id.ToString());
                    table.Cell().Text(motor.Name);
                    table.Cell().AlignRight().Text(motor.RentPrice.ToString());
                });
            });
        }).GeneratePdf(invoiceName);

        return File.ReadAllBytes(invoiceName);
    }
}
