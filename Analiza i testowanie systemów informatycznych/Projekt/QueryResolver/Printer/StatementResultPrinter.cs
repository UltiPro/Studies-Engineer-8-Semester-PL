namespace SPA.QueryResolver.Printer;

public class StatementResultPrinter : IResultPrinter
{
    public List<string> Print(List<int> indexes)
    {
        List<string> results = new List<string>();
        foreach (int index in indexes) results.Add(index.ToString());
        return results;
    }
}