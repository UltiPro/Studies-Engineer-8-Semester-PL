using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;

namespace SPA.ParserAndPKB.NodeFactory;

public static class CreateStmtList
{
    public static Node Create(Node parent)
    {
        Node newNode = new Node(QueryTokenType.Stmtlist);
        AST.AST.Instance.SetChildOfLink(parent, newNode);
        return newNode;
    }
}