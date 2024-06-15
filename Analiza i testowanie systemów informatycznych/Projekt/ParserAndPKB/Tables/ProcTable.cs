using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Models;

namespace SPA.ParserAndPKB.Tables;

public class ProcTable : IProcTable
{
    private static ProcTable? _ProcTable;
    public static ProcTable Instance
    {
        get
        {
            _ProcTable ??= new ProcTable();//if is null
            return _ProcTable;
        }
    }

    public List<Procedure> procedures { get; set; }

    private ProcTable() => procedures = new List<Procedure>();

    public int AddProcedure(string procName)
    {
        if (procedures.Any(p => p.Identifier == procName)) return -1;
        else
        {
            procedures.Add(new Procedure(procName) { Id = procedures.Count });
            return GetProcIndex(procName);
        }
    }

    public Node? GetAstRoot(string procName) => GetProcedure(procName)?.AstRoot ?? null;

    public Node? GetAstRoot(int index) => GetProcedure(index)?.AstRoot ?? null;

    public Procedure? GetProcedure(int index) => procedures.FirstOrDefault(p => p.Id == index);

    public Procedure? GetProcedure(string procName) => procedures.FirstOrDefault(p => p.Identifier.ToLower() == procName.ToLower());

    public int GetProcIndex(string procName)
    {
        var procedure = GetProcedure(procName);
        return procedure is null ? -1 : procedure.Id;
    }

    public int SetAstRootNode(string procName, Node node)
    {
        var procedure = GetProcedure(procName);
        if (procedure is null) return -1;
        else
        {
            procedure.AstRoot = node;
            return procedure.Id;
        }
    }

    public List<Procedure> GetTable() => procedures;
}