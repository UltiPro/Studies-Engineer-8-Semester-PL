using Microsoft.EntityFrameworkCore;
using Projekt_1_Web_Serwisy.Database;
using Projekt_1_Web_Serwisy.Exceptions;
using Projekt_1_Web_Serwisy.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ServiceModel;

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
            Brand = motor.Brand,
            Name = motor.Name,
            Description = motor.Description,
            RequiredLicence = motor.RequiredLicence,
            RentPrice = motor.RentPrice
        });

        await _context.SaveChangesAsync();
    }

    public async Task Remove(int id)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) throw new NotFoundException(id);

        _context.Motors.Remove(motor);

        await _context.SaveChangesAsync();
    }

    public async Task Update(UpdateMotor motor)
    {
        var motor2 = await _context.Motors.FirstOrDefaultAsync(m => m.Id == motor.Id);

        if (motor2 is null) throw new NotFoundException(motor.Id);

        motor2.Brand = motor.Brand;
        motor2.Name = motor.Name;
        motor2.Description = motor.Description;
        motor2.RequiredLicence = motor.RequiredLicence;
        motor2.RentPrice = motor.RentPrice;

        await _context.SaveChangesAsync();
    }

    public async Task<DetailMotor> Detail(int id)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) throw new NotFoundException(id);

        return new DetailMotor
        {
            Id = motor.Id,
            Brand = motor.Brand,
            Name = motor.Name,
            RentPrice = motor.RentPrice,
            Description = motor.Description,
            RequiredLicence = motor.RequiredLicence.ToString(),
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
                Brand = motor.Brand,
                Name = motor.Name,
                RentPrice = motor.RentPrice,
                Description = motor.Description,
                RequiredLicence = motor.RequiredLicence.ToString(),
                RentTo = motor.RentTo.ToString() ?? "",
                Reservation = motor.Reservation ? "Reserved" : "Not reserved"
            });
        }

        return list;
    }

    public async Task<List<DetailMotor>> GetSelected(string brandString)
    {
        var motors = await _context.Motors.Where(motor => motor.Brand.Contains(brandString)).ToListAsync();

        var list = new List<DetailMotor>();

        foreach (var motor in motors)
        {
            list.Add(new DetailMotor
            {
                Id = motor.Id,
                Brand = motor.Brand,
                Name = motor.Name,
                RentPrice = motor.RentPrice,
                Description = motor.Description,
                RequiredLicence = motor.RequiredLicence.ToString(),
                RentTo = motor.RentTo.ToString() ?? "",
                Reservation = motor.Reservation ? "Reserved" : "Not reserved"
            });
        }

        return list;
    }

    public async Task Reserve(int id)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) throw new NotFoundException(id);

        if (motor.Reservation) throw new MotorbikeReservedException();

        motor.Reservation = true;

        await _context.SaveChangesAsync();
    }

    public async Task CancelReserve(int id)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) throw new NotFoundException(id);

        if (!motor.Reservation) throw new MotorbikeNotReservedException();

        motor.Reservation = false;

        await _context.SaveChangesAsync();
    }

    public async Task Rent(int id, int numberOfDays)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) throw new NotFoundException(id);

        var rentDate = DateTime.Now.AddDays(numberOfDays);

        if (motor.RentTo != null && motor.RentTo > DateTime.Now)
            throw new MotorbikeCannotBeRentException(motor.RentTo ?? DateTime.Now);

        motor.RentTo = rentDate;

        await _context.SaveChangesAsync();
    }

    public async Task<byte[]?> GeneratePDF(int id)
    {
        var motor = await _context.Motors.FirstOrDefaultAsync(motor => motor.Id == id);

        if (motor is null) throw new NotFoundException(id);

        if (motor.RentTo is null || motor.RentTo < DateTime.Now) throw new ThisMotorbikeIsNotRentedException();

        string invoiceName = $"Invoice {DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}" +
            $"{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}.pdf";

        try
        {
            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(16));

                    page.Header().AlignCenter().Text($"{motor.Brand}").Bold().FontSize(24).FontColor(Colors.Grey.Darken4);

                    page.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(30);
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
                            table.Cell().Text($"{motor.Name}");
                            table.Cell().AlignRight().Text($"{motor.RentPrice} zł");
                        });

                        col.Item().TranslateY(4).LineHorizontal(2);

                        col.Item().TranslateY(20).Text($"{motor.Description}");
                    });
                });
            }).GeneratePdf(invoiceName);

            return File.ReadAllBytes(invoiceName);
        }
        catch
        {
            throw new CouldNotCreateInvoiceException(id);
        }
    }
}
