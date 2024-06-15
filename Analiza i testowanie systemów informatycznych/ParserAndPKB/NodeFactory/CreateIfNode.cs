using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.NodeFactory.Helpers;
using SPA.ParserAndPKB.Tables;

namespace SPA.ParserAndPKB.NodeFactory;

public class CreateIfNode : ACreate
{
    public new static Node Create(Node parent, Node stmtListNode, int lineNumberQuery)
    {
        StmtTable.Instance.AddStatement(QueryTokenType.If, lineNumberQuery);
        Node ifNode = new Node(QueryTokenType.If);
        StmtTable.Instance.SetAstRoot(lineNumberQuery, ifNode);
        AST.AST.Instance.SetParent(parent, ifNode);
        Utils.SettingFollows(ifNode, stmtListNode);
        AST.AST.Instance.SetChildOfLink(stmtListNode, ifNode);
        return ifNode;
    }
}