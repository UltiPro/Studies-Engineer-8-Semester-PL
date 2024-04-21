namespace Projekt_1_Web_Serwisy.SOAPHelloWorld;

public class SOAPService : ISOAPService
{
    public string HelloWorld(string name)
    {
        return $"Hello {name}!";
    }
}
