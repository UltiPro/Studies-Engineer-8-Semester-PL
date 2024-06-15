using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.Models;
using SPA.ParserAndPKB.Tables;

namespace SPA.ParserAndPKB.NodeFactory.Helpers;

public static class Utils
{
    public static void SettingFollows(Node node, Node stmt)
    {
        List<Node> siblingsList = AST.AST.Instance!.GetLinkedNodes(stmt, NodeRelationType.Child);
        if (siblingsList.Count() != 0)
        {
            Node prevStmt = siblingsList[siblingsList.Count() - 1];
            AST.AST.Instance.SetFollows(prevStmt, node);
        }
    }

    public static void SetModifiesForFamily(Node node, Variable var)
    {
        if (node.EntityType == QueryTokenType.Procedure)
        {
            Procedure proc = ProcTable.Instance.procedures.Where(i => i.AstRoot == node).FirstOrDefault();
            var.Id = VarTable.Instance.GetVarIndex(var.Identifier);
            Modifies.Modifies.Instance.SetModifies(proc, var);
        }
        else
        {
            Statement stmt = StmtTable.Instance.statements.Where(i => i.AstRoot == node).FirstOrDefault();
            var.Id = VarTable.Instance.GetVarIndex(var.Identifier);
            Modifies.Modifies.Instance.SetModifies(stmt, var);
        }
        if (AST.AST.Instance.GetParent(node) != null) SetModifiesForFamily(AST.AST.Instance.GetParent(node), var);
    }
}