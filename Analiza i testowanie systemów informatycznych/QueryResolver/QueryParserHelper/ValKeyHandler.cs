using SPA.ParserAndPKB.AST;
using SPA.ParserAndPKB;

namespace SPA.QueryResolver;

public class ValKeyHandler
{
    private const string ValKey = "value";

    public List<int> GetProglineIdxList(IReadOnlyDictionary<string, List<string>> attributes)
    {
        var lines = attributes.ContainsKey(ValKey) ? attributes[ValKey] : new List<string>();
        var idxList = new List<int>();

        foreach (var stmt in PKB.Instance._StmtTable.GetTable())
        {
            if (lines.Count == 1 && stmt.LineNumber.ToString() == lines[0])
            {
                idxList.Add(stmt.LineNumber);
            }
            else if (lines.Count == 0)
            {
                idxList.Add(stmt.LineNumber);
            }
        }
        return idxList;
    }

    public List<int> GetConstantIdxList(IReadOnlyDictionary<string, List<string>> attributes)
    {
        var constants = attributes.ContainsKey(ValKey) ? attributes[ValKey] : new List<string>();
        var idxList = new List<int>();

        if (constants.Count == 1 && int.TryParse(constants[0], out _))
        {
            foreach (var stmt in PKB.Instance._StmtTable.GetTable())
            {
                var constValues = AST.Instance.GetConstants(stmt.AstRoot);
                if (constValues.Contains(int.Parse(constants[0]))) idxList.Add(int.Parse(constants[0]));
            }
        }
        else if (constants.Count == 0)
            foreach (var stmt in PKB.Instance._StmtTable.GetTable()) idxList.AddRange(AST.Instance.GetConstants(stmt.AstRoot));
        return idxList.Distinct().ToList();
    }
}