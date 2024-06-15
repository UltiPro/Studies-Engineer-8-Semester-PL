using SPA.Common;
using SPA.Exceptions;
using SPA.ParserAndPKB;
using SPA.ParserAndPKB.Enums;

namespace SPA.QueryResolver.Printer;

public class QueryResultPrinter
{
    private readonly Dictionary<QueryTokenType, IResultPrinter> _printers;

    public QueryResultPrinter()
    {
        _printers = new Dictionary<QueryTokenType, IResultPrinter>
        {
            { QueryTokenType.Variable, new VariableResultPrinter() },
            { QueryTokenType.Procedure, new ProcedureResultPrinter() },
            { QueryTokenType.Statement, new StatementResultPrinter() }
        };
    }

    public List<string> Print(Dictionary<string, List<int>> resultToPrint)
    {
        List<string> results = new List<string>();

        foreach (KeyValuePair<string, List<int>> oneVar in resultToPrint)
        {
            try
            {
                QueryTokenType entityType = QueryProcessor.GetVariableEnumType(oneVar.Key);
                if (!_printers.ContainsKey(entityType)) entityType = QueryTokenType.Statement;

                results.AddRange(_printers[entityType].Print(oneVar.Value));
            }
            catch (Exception e)
            {
                throw new PrinterException(e.Message);
            }
        }

        if (Lists.AreListsEqual(results, PKB.Instance._ProcTable.GetTable().Select(elem => elem.Identifier).ToList())) 
            return new List<string> { "none" };

        return results;
    }
}