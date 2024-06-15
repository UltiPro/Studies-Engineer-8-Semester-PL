using SPA.QueryResolver.Printer;

namespace SPA.QueryResolver;

public static class QueryParserHelper
{
    public static List<string> GetSuchThatPart(Dictionary<string, List<string>> queryDetails)
    {
        try
        {
            return new List<string>(queryDetails["SUCH THAT"]);
        }
        catch (Exception e)
        {
            Console.WriteLine("#" + e.Message);
            return new List<string>();
        }
    }

    public static List<string> SortSuchThatPart(List<string> stp) => stp.OrderByDescending(x => x.Contains("\"")).ToList();

    public static List<string> GenerateOutput(Dictionary<string, List<int>> variableIndexes)
    {
        var outputIdxList = new Dictionary<string, List<int>>();
        var availableVars = QueryVariables.GetVariableToSelect();
        bool anyZero = false;
        foreach (string variable in availableVars)
        {
            string trimmedVar = variable.Trim();
            if (!variableIndexes.ContainsKey(trimmedVar)) throw new ArgumentException($"Invalid argument: \"{trimmedVar}\"");
            outputIdxList[trimmedVar] = variableIndexes[trimmedVar];
        }
        foreach (var dict in variableIndexes)
        {
            if (availableVars.Contains(dict.Key)) continue;
            if (dict.Value.Count == 0) anyZero = true;
        }
        return new QueryResultPrinter().Print(anyZero ? new Dictionary<string, List<int>>() : outputIdxList);
    }
}