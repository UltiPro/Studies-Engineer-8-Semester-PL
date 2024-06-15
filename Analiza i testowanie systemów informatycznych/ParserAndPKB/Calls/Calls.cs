using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.Models;
using SPA.ParserAndPKB.Tables;

namespace SPA.ParserAndPKB.Calls;

public class Calls : ICalls
{
    private static Calls? _Calls;
    public static Calls Instance
    {
        get
        {
            _Calls ??= new Calls();
            return _Calls;
        }
    }

    public List<Procedure> GetCalls(string proc)
    {
        var procedures = new List<Procedure>();
        var procNode = ProcTable.Instance.GetAstRoot(proc);
        if (procNode != null)
        {
            var stmtLstChild = AST.AST.Instance.GetFirstChild(procNode);
            GetCalls(procedures, stmtLstChild);
        }
        return procedures;
    }

    public List<Procedure> GetCalls(List<Procedure> procedures, Node stmtNode)
    {
        var procNames = AST.AST.Instance.GetLinkedNodes(stmtNode, NodeRelationType.Child)
            .Where(n => n.EntityType == QueryTokenType.Call).Select(n => n.Name).ToList();

        foreach (var procName in procNames)
        {
            var findProcedure = ProcTable.Instance.GetProcedure(procName);
            if (findProcedure != null) procedures.Add(findProcedure);
        }

        var ifOrWhileNodes = AST.AST.Instance.GetLinkedNodes(stmtNode, NodeRelationType.Child)
            .Where(n => n.EntityType == QueryTokenType.While || n.EntityType == QueryTokenType.If).ToList();

        foreach (var node in ifOrWhileNodes)
        {
            List<Node> stmtNodes = AST.AST.Instance.GetLinkedNodes(node, NodeRelationType.Child)
                .Where(i => i.EntityType == QueryTokenType.Stmtlist).ToList();
            foreach (var stmtN in stmtNodes) GetCalls(procedures, stmtN);
        }

        return procedures;
    }

    public List<Procedure> GetCallsStar(string proc) => GetCallsStar(proc, new List<Procedure>());

    private List<Procedure> GetCallsStar(string proc, List<Procedure> procedures)
    {
        foreach (var procedure in GetCalls(proc))
        {
            procedures.Add(procedure);
            GetCallsStar(procedure.Identifier, procedures);
        }
        return procedures;
    }

    public bool IsCalls(string proc1, string proc2) => GetCalls(proc1).Any(p => p.Identifier == proc2);

    public bool IsCallsStar(string proc1, string proc2) => GetCallsStar(proc1).Any(p => p.Identifier == proc2);
}