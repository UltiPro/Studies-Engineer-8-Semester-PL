#pragma warning disable 8618

using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;

namespace SPA.ParserAndPKB.AST;

public class AST : IAST
{
    private static AST? _AST;

    public static AST Instance
    {
        get
        {
            _AST ??= new AST(); //_AST is null
            return _AST;
        }
    }

    public Node Root { get; set; }

    public void SetChildOfLink(Node parentNode, Node childNode) => SetLink(NodeRelationType.Child, parentNode, childNode);

    public void SetLink(NodeRelationType linkType, Node node1, Node node2)
        => node1.RelatedNodes.Add(new NodeRelation(linkType, node2));

    public void SetPrevLink(NodeRelationType linkType, Node node1, Node node2) => node1.PrevRelated.Add(new NodeRelation(linkType, node2));

    public Node GetFirstChild(Node parentNode) => GetLinkedNodes(parentNode, NodeRelationType.Child).FirstOrDefault()!;

    public List<Node> GetLinkedNodes(Node node, NodeRelationType linkType)
        => node == null ? new() : node.RelatedNodes.Where(n => n.Type == linkType).Select(n => n.RelatedNode).ToList();

    public void SetParent(Node parentNode, Node childNode)
    {
        SetLink(NodeRelationType.Parent, childNode, parentNode);
        SetPrevLink(NodeRelationType.Parent, parentNode, childNode);
    }

    public Node GetParent(Node node) => GetLinkedNodes(node, NodeRelationType.Parent).FirstOrDefault()!;

    public List<Node> GetParentStar(Node node)
    {
        var nodes = new List<Node>();
        var tempNode = node;
        while (tempNode != null)
        {
            var parentNode = GetParent(tempNode);
            if (parentNode != null)
            {
                nodes.Add(parentNode);
            }
            tempNode = parentNode!;
        }
        return nodes;
    }

    public void SetFollows(Node node1, Node node2)
    {
        SetLink(NodeRelationType.Follows, node2, node1);
        SetPrevLink(NodeRelationType.Follows, node1, node2);
    }

    public List<Node> GetFollows(Node node) => GetLinkedNodes(node, NodeRelationType.Follows);

    public List<Node> GetFollowsStar(Node node) => GetFollowsStar(node, new List<Node>());

    private List<Node> GetFollowsStar(Node node, List<Node> tempList)
    {
        foreach (var n in GetFollows(node))
        {
            tempList.Add(n);
            GetFollowsStar(n, tempList);
        }
        return tempList;
    }

    public void SetNext(Node node1, Node node2)
    {
        SetLink(NodeRelationType.Next, node1, node2);
        SetPrevLink(NodeRelationType.Next, node2, node1);
    }

    public List<Node> GetNext(Node node) => GetLinkedNodes(node, NodeRelationType.Next);

    public List<Node> GetNextStar(Node node) => GetNextStar(node, new List<Node>());

    private List<Node> GetNextStar(Node node, List<Node> tempList)
    {
        foreach (var n in GetNext(node))
        {
            tempList.Add(n);
            GetNextStar(n, tempList);
        }
        return tempList;
    }

    public bool IsParent(Node parentNode, Node childNode) => GetParent(childNode) == parentNode;

    public bool IsParentStar(Node parentNode, Node childNode) => GetParentStar(childNode).Contains(parentNode);

    public bool IsFollowed(Node node1, Node node2) => GetFollows(node2).Contains(node1);

    public bool IsFollowedStar(Node node1, Node node2) => GetFollowsStar(node2).Contains(node1);

    public bool IsNext(Node node1, Node node2) => GetNext(node1).Contains(node2);

    public bool IsNextStar(Node node1, Node node2) => GetNextStar(node1).Contains(node2);

    public List<int> GetConstants(Node node)
    {
        var constans = new List<int>();
        var children = GetLinkedNodes(node, NodeRelationType.Child);
        if (children != null)
        {
            if (children.Count > 1)
            {
                foreach (Node child in children)
                {
                    if (child.EntityType == QueryTokenType.Constant)
                    {
                        var value = int.Parse("0");
                        constans.Add(value);
                    }
                    else constans.AddRange(GetConstants(child));
                }
            }
        }
        return constans.Distinct().ToList();
    }
}