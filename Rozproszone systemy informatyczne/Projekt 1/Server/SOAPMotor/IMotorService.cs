﻿using Projekt_1_Web_Serwisy.Models;
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
    public Task<List<DetailMotor>> GetSelected(string brandString);

    [OperationContract]
    public Task Reserve(int id);

    [OperationContract]
    public Task CancelReserve(int id);

    [OperationContract]
    public Task Rent(int id, int numberOfDays);

    [OperationContract]
    public Task<byte[]?> GeneratePDF(int id);
}
