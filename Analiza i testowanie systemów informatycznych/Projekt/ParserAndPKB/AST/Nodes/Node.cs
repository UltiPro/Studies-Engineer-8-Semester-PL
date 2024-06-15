using SPA.ParserAndPKB.Enums;

namespace SPA.ParserAndPKB.AST.Nodes;

public class Node
{
    public string Name { get; set; } = "DEFAULT";
    public List<NodeRelation> RelatedNodes { get; }
    public List<NodeRelation> PrevRelated { get; }
    public QueryTokenType EntityType { get; }

    public Node(QueryTokenType EntityType)
    {
        this.RelatedNodes = new List<NodeRelation>();
        this.PrevRelated = new List<NodeRelation>();
        this.EntityType = EntityType;
    }

    public Node(Node node)
    {
        this.Name = node.Name;
        this.RelatedNodes = node.RelatedNodes;
        this.PrevRelated = node.PrevRelated;
        this.EntityType = node.EntityType;
    }
}