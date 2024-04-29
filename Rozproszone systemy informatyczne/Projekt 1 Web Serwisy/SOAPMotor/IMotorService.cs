using Projekt_1_Web_Serwisy.Models;
using System.ServiceModel;

namespace Projekt_1_Web_Serwisy.SOAPMotor;

[ServiceContract]
public interface IMotorService
{
    [OperationContract]
    public Task Create(CreateMotor motor);

    [OperationContract]
    public Task Remove(int id);

    [OperationContract]
    public Task Update(UpdateMotor motor);

    [OperationContract]
    public Task<DetailMotor> Detail(int id);

    [OperationContract]
    public Task<List<DetailMotor>> GetAll();

    [OperationContract]
    public Task<string> Reserve(int id);

    [OperationContract]
    public Task<string> CancelReserve(int id);

    [OperationContract]
    public Task<string> Rent(int id, int numberOfDays);

    [OperationContract]
    public void GeneratePDF(int id);
}
