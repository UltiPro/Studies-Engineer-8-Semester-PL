using SPA.ParserAndPKB.Enums;

namespace SPA.ParserAndPKB.AST.Nodes;

public class NodeRelation
{
    public NodeRelationType Type { get; set; }
    public Node RelatedNode { get; set; }

    public NodeRelation(NodeRelationType Type, Node RelatedNode)
    {
        this.Type = Type;
        this.RelatedNode = RelatedNode;
    }
}