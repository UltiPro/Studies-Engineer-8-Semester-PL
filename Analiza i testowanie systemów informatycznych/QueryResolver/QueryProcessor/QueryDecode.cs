using SPA.Exceptions;
using SPA.ParserAndPKB.Enums;

namespace SPA.QueryResolver;

public static class QueryDecode
{
    /// <summary>
    /// Przetworzenie deklaracji i uzupełnienie słownika
    /// </summary>
    /// <param name="variableDefinition">definicja</param>
    public static void DecodeVarDefinitions(ref Dictionary<string, QueryTokenType> variables, string variableDefinition)
    {
        QueryTokenType QueryToken;
        string[] variableParts = variableDefinition.Replace(" ", ",").Replace(";", ",").Split(",");

        if (variableParts[0] == "stmt")
            QueryToken = QueryTokenType.Statement;
        else if (variableParts[0] == "assign")
            QueryToken = QueryTokenType.Assign;
        else if (variableParts[0] == "call")
            QueryToken = QueryTokenType.Call;
        else if (variableParts[0] == "while")
            QueryToken = QueryTokenType.While;
        else if (variableParts[0] == "procedure")
            QueryToken = QueryTokenType.Procedure;
        else if (variableParts[0] == "constant")
            QueryToken = QueryTokenType.Constant;
        else if (variableParts[0] == "variable")
            QueryToken = QueryTokenType.Variable;
        else if (variableParts[0] == "if")
            QueryToken = QueryTokenType.If;
        else if (variableParts[0] == "boolean")
            QueryToken = QueryTokenType.Boolean;
        else if (variableParts[0] == "prog_line")
            QueryToken = QueryTokenType.Prog_line;
        else
            throw new WrongQueryDeclarationArgumentException(string.Format("# Wrong query argument: \"{0}\"", variableParts[0]), new Exception());

        foreach (var variablePart in variableParts)
            if (variablePart == "")
                continue;
            else
                variables.Add(variablePart, QueryToken);
    }
}