using System.ServiceModel;

namespace Projekt_1_Web_Serwisy.SOAPHelloWorld;

[ServiceContract]
public interface ISOAPService
{
    [OperationContract]
    string HelloWorld(string name);
}
