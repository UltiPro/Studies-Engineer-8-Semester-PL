using Projekt_1_Web_Serwisy.Models;
using System.ServiceModel;

namespace Projekt_1_Web_Serwisy.SOAPMotor;

[ServiceContract]
public interface IMotorService
{
    [OperationContract]
    public Task<List<DBMotor>> GetAll();
    [OperationContract]
    public Task<string> Reserve(int id);
    [OperationContract]
    public Task<string> CancelReserve(int id);
    [OperationContract]
    public Task<string> Rent(int id, DateTime date);
}
