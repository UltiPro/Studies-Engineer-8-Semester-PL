using SPA.ParserAndPKB.Enums;

namespace SPA.QueryResolver;

public class QueryParser
{
    private static Dictionary<string, List<int>>? _variableIndexes;
    private static int _currentSum;
    private static bool _algorithmNotFinished;    

    public static List<string> GetData()
    {
        _algorithmNotFinished = true;
        _currentSum = -1;
        _variableIndexes = new Dictionary<string, List<int>>();
        _variableIndexes = VariableIndexer.PopulateVariableIndices();
        var queryDetails = QueryVariables.GetQueryDetails();
        var suchThatPart = QueryParserHelper.GetSuchThatPart(queryDetails);

        if (suchThatPart.Count > 0)
        {
            suchThatPart = QueryParserHelper.SortSuchThatPart(suchThatPart);
            do
            {
                foreach (var method in suchThatPart)
                    if (method.Length > 0)
                        MethodDecoder.DecodeMethod(method, _variableIndexes, suchThatPart.Count > 1);
                VerifySum();
            } while (_algorithmNotFinished);
        }
        return QueryParserHelper.GenerateOutput(_variableIndexes);
    }

    private static void VerifySum()
    {
        var tmpSum = 0;
        foreach (var (_, value) in _variableIndexes!) tmpSum += value.Count;
        if (tmpSum != _currentSum) _currentSum = tmpSum;
        else _algorithmNotFinished = false;
    }

    public static List<int> GetArgIndexes(string var, QueryTokenType type) => VariableIndexer.GetArgIndexes(var, type, _variableIndexes);

    public static void RemoveIndexesFromLists(string firstArgument, string secondArgument, List<int> firstList, List<int> secondList, bool and)
        => VariableIndexer.RemoveIndexesFromLists(firstArgument, secondArgument, firstList, secondList, _variableIndexes, and);
}