using SPA.ParserAndPKB;

namespace SPA.QueryResolver;

public class ProcNameKeyHandler
{
    private const string ProcNameKey = "procname";

    public List<int> GetProcedureIdxList(Dictionary<string, List<string>> attributes)
    {
        var procNames = attributes.ContainsKey(ProcNameKey) ? attributes[ProcNameKey] : new List<string>();
        var idxList = new List<int>();

        foreach (var proc in PKB.Instance._ProcTable.GetTable())
        {
            if (procNames.Count == 1 && proc.Identifier == procNames[0].Trim('"'))
            {
                idxList.Add(proc.Id);
            }
            else if (procNames.Count == 0)
            {
                idxList.Add(proc.Id);
            }
        }
        return idxList;
    }
}