using SPA.Common;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB;

namespace SPA.QueryResolver;

public class StmtKeyHandler
{
    private const string StmtKey = "stmt#";

    public List<int> GetStatementIdxList(Dictionary<string, List<string>> attributes, QueryTokenType type)
    {
        var idxList = new List<int>();
        var stmtNumberList = attributes.ContainsKey(StmtKey) ? attributes[StmtKey] : new List<string>();

        if (stmtNumberList.Count != 1 || Helpers.IsLetter(stmtNumberList[0]))
        {
            foreach (var stmt in PKB.Instance._StmtTable.GetTable())
                if (type == QueryTokenType.Statement || stmt.StmtType == type) idxList.Add(stmt.LineNumber);
        }
        else
        {
            try
            {
                var statement = PKB.Instance._StmtTable.GetStatement(int.Parse(stmtNumberList[0]));
                if (statement != null)
                {
                    idxList.Add(statement.LineNumber);
                }
            }
            catch
            {
                throw new ArgumentException($"# Wrong stmt# = {stmtNumberList[0]}");
            }
        }
        return idxList;
    }
}