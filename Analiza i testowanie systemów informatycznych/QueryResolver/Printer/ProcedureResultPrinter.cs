using SPA.ParserAndPKB;

namespace SPA.QueryResolver.Printer;

public class ProcedureResultPrinter : IResultPrinter
{
    public List<string> Print(List<int> indexes)
    {
        List<string> results = new List<string>();
        foreach (int index in indexes) results.Add(PKB.Instance._ProcTable!.GetProcedure(index)!.Identifier);
        return results;
    }
}