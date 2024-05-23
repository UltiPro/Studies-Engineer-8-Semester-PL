namespace Server.Exceptions;

public class CouldNotCreateInvoiceException : Exception
{
    public CouldNotCreateInvoiceException(int id) : base("Server could not process the invoice, please try latter.") { }
}
