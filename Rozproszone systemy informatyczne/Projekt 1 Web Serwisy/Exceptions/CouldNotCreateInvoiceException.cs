namespace Projekt_1_Web_Serwisy.Exceptions;

public class CouldNotCreateInvoiceException : Exception
{
    public CouldNotCreateInvoiceException(int id) : base("Server could not process the invoice, please try latter.") { }
}
