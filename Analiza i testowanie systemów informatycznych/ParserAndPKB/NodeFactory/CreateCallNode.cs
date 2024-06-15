using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.NodeFactory.Helpers;
using SPA.ParserAndPKB.Tables;

namespace SPA.ParserAndPKB.NodeFactory;

public class CreateCallNode : ACreate
{
    public new static Node Create(Node parent, Node stmtListNode, int lineNumberQuery)
    {
        StmtTable.Instance.AddStatement(QueryTokenType.Call, lineNumberQuery);
        Node callNode = new Node(QueryTokenType.Call);
        StmtTable.Instance.SetAstRoot(lineNumberQuery, callNode);
        AST.AST.Instance.SetParent(parent, callNode);
        Utils.SettingFollows(callNode, stmtListNode);
        AST.AST.Instance.SetChildOfLink(stmtListNode, callNode);
        return callNode;
    }
}