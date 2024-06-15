using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.NodeFactory.Helpers;
using SPA.ParserAndPKB.Tables;

namespace SPA.ParserAndPKB.NodeFactory;

public class CreateWhileNode : ACreate
{
    public new static Node Create(Node parent, Node stmtListNode, int lineNumberQuery)
    {
        Node whileNode = new Node(QueryTokenType.While);
        StmtTable.Instance.SetAstRoot(lineNumberQuery, whileNode);
        AST.AST.Instance.SetParent(parent, whileNode);
        Utils.SettingFollows(whileNode, stmtListNode);
        AST.AST.Instance.SetChildOfLink(stmtListNode, whileNode);
        return whileNode;
    }
}