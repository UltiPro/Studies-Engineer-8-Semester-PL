using Server.Models;

namespace Server.Contracts;

public interface IMotorService
{
    Task Create(CreateMotor motor);
    Task Remove(int id);
    Task Update(UpdateMotor motor);
    Task<DetailMotor> Detail(int id);
    Task<List<DetailMotor>> GetAll();
    Task<List<DetailMotor>> GetSelected(string brandString);
    Task Reserve(int id);
    Task CancelReserve(int id);
    Task Rent(int id, int numberOfDays);
    Task<byte[]?> GeneratePDF(int id);
}
