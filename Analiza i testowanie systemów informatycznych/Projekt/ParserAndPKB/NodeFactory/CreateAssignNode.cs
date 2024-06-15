using SPA.ParserAndPKB.NodeFactory.Helpers;
using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.Models;
using SPA.ParserAndPKB.Tables;

namespace SPA.ParserAndPKB.NodeFactory;

public class CreateAssignNode
{
    public static Node Create(Node parent, Node stmtListNode, string variableName, int lineNumberQuery)
    {
        Node assignNode = new Node(QueryTokenType.Assign);
        Variable var = new Variable(variableName);
        VarTable.Instance.AddVariable(variableName);
        StmtTable.Instance.SetAstRoot(lineNumberQuery, assignNode);
        AST.AST.Instance.SetParent(parent, assignNode);
        Utils.SettingFollows(assignNode, stmtListNode);
        AST.AST.Instance.SetChildOfLink(stmtListNode, assignNode);
        Node variableNode = new Node(QueryTokenType.Variable);
        AST.AST.Instance.SetChildOfLink(assignNode, variableNode);
        Utils.SetModifiesForFamily(assignNode, var);
        return assignNode;
    }
}