using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB;

namespace SPA.QueryResolver;

public static class VariableIndexer
{
    public static Dictionary<string, List<int>> PopulateVariableIndices()
    {
        var variableIndexes = new Dictionary<string, List<int>>();
        var varAttributes = QueryProcessor.GetVariableAttributes();

        foreach (var (key, type) in QueryProcessor.GetQueryVariables())
        {
            var attributes = new Dictionary<string, List<string>>();

            foreach (var attribute in varAttributes)
            {
                var splitAttr = attribute.Key.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                if (key == splitAttr[0]) attributes[splitAttr[1].ToLower()] = attribute.Value;
            }

            var procHandler = new ProcNameKeyHandler();
            var varHandler = new VarNameKeyHandler();
            var valHandler = new ValKeyHandler();
            var stmtHandler = new StmtKeyHandler();

            var statementTypes = new[]
           {
                QueryTokenType.Boolean,
                QueryTokenType.Call,
                QueryTokenType.Statement,
                QueryTokenType.If,
                QueryTokenType.While,
                QueryTokenType.Assign
            };

            if (type == QueryTokenType.Procedure) variableIndexes.Add(key, procHandler.GetProcedureIdxList(attributes));
            else if (type == QueryTokenType.Variable) variableIndexes.Add(key, varHandler.GetVariableIdxList(attributes));
            else if (statementTypes.Contains(type)) variableIndexes.Add(key, stmtHandler.GetStatementIdxList(attributes, type));
            else if (type == QueryTokenType.Prog_line) variableIndexes.Add(key, valHandler.GetProglineIdxList(attributes));
            else if (type == QueryTokenType.Constant) variableIndexes.Add(key, valHandler.GetConstantIdxList(attributes));
            else { throw new ArgumentException("# Invalid type of entity!"); }
        }

        return variableIndexes;
    }

    public static List<int> GetArgIndexes(string var, QueryTokenType type, Dictionary<string, List<int>> variableIndexes)
    {
        if (var == "_") return GetAllArgIndexes(type);

        if (var[0] == '\"' && var[^1] == '\"')
        {
            string name = var.Substring(1, var.Length - 2);
            if (type == QueryTokenType.Procedure)
                return new List<int> { PKB.Instance._ProcTable.GetProcIndex(name) };
            else if (type == QueryTokenType.Variable)
                return new List<int> { PKB.Instance._VarTable.GetVarIndex(name) };
        }

        if (int.TryParse(var, out _)) return new List<int> { int.Parse(var) };

        return variableIndexes[var];
    }

    public static List<int> GetAllArgIndexes(QueryTokenType type)
    {
        var result = new List<int>();
        if (type == QueryTokenType.Variable)
            foreach (var v in PKB.Instance._VarTable.GetTable()) result.Add(v.Id);
        else if (type == QueryTokenType.Procedure)
            foreach (var p in PKB.Instance._ProcTable.GetTable()) result.Add(p.Id);
        else
            foreach (var s in PKB.Instance._StmtTable.GetTable()) result.Add(s.LineNumber);
        return result;
    }

    public static void RemoveIndexesFromLists(string firstArgument, string secondArgument, List<int> firstList, List<int> secondList, Dictionary<string, List<int>> variableIndexes, bool and = false)
    {
        if (firstArgument != "_" && !(firstArgument[0] == '\"' && firstArgument[^1] == '\"') && !int.TryParse(firstArgument, out _))
            variableIndexes[firstArgument] = variableIndexes[firstArgument].Where(i => firstList.Contains(i)).Distinct().ToList();
        if (secondArgument != "_" && !(secondArgument[0] == '\"' && secondArgument[^1] == '\"') && !int.TryParse(secondArgument, out _))
            variableIndexes[secondArgument] = variableIndexes[secondArgument].Where(i => secondList.Contains(i)).Distinct().ToList();
        
        if (and && firstList.Count == 0 && secondList.Count == 0)
        {
            variableIndexes[firstArgument] = new List<int>();
            variableIndexes[secondArgument] = new List<int>();
        }
    }
}