using SPA.ParserAndPKB;

namespace SPA.QueryResolver.Printer;

public class VariableResultPrinter : IResultPrinter
{
    public List<string> Print(List<int> indexes)
    {
        List<string> results = new List<string>();
        foreach (int index in indexes) results.Add(PKB.Instance._VarTable!.GetVar(index)!.Identifier);
        return results;
    }
}