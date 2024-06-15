using SPA.ParserAndPKB;

namespace SPA.QueryResolver;

public class VarNameKeyHandler
{
    private const string VarNameKey = "varname";

    public List<int> GetVariableIdxList(Dictionary<string, List<string>> attributes)
    {
        var varNames = attributes.TryGetValue(VarNameKey, out var names) ? names : new List<string>();
        var idxList = new List<int>();

        foreach (var v in PKB.Instance._VarTable.GetTable())
        {
            if (varNames.Count == 1 && v.Identifier == varNames[0].Trim('"')) idxList.Add(v.Id);
            else if (varNames.Count == 0) idxList.Add(v.Id);
        }

        return idxList;
    }
}