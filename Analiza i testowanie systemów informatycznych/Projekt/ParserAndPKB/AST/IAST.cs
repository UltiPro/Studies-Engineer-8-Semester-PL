using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;

namespace SPA.ParserAndPKB.AST;

public interface IAST
{
    void SetChildOfLink(Node parentNode, Node childNode);
    void SetLink(NodeRelationType linkType, Node node1, Node node2);
    void SetPrevLink(NodeRelationType linkType, Node node1, Node node2);
    Node GetFirstChild(Node parentNode);
    List<Node> GetLinkedNodes(Node node, NodeRelationType linkType);
    void SetParent(Node parentNode, Node childNode);
    Node GetParent(Node node);
    List<Node> GetParentStar(Node node);
    void SetFollows(Node node1, Node node2);
    List<Node> GetFollows(Node node);
    List<Node> GetFollowsStar(Node node);
    void SetNext(Node node1, Node node2);
    List<Node> GetNext(Node node);
    List<Node> GetNextStar(Node node);
    bool IsParent(Node parentNode, Node childNode);
    bool IsParentStar(Node parentNode, Node childNode);
    bool IsFollowed(Node node1, Node node2);
    bool IsFollowedStar(Node node1, Node node2);
    bool IsNext(Node node1, Node node2);
    bool IsNextStar(Node node1, Node node2);
    List<int> GetConstants(Node node);
}