using SPA.ParserAndPKB.AST;
using SPA.ParserAndPKB;
using SPA.QueryResolver.Enums;

namespace SPA.QueryResolver;

public static class MethodDecoder
{
    public static void DecodeMethod(string method, Dictionary<string, List<int>> variableIndexes, bool and)
    {
        string[] splitString = [" ", "(", ")", ","];
        string[] typeAndArguments = method.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

        if (typeAndArguments[0].ToLower() == StringNameResolver.Uses)
        {
            QueryHelper.IsModifiesOrUses(typeAndArguments[1], typeAndArguments[2], PKB.Instance._Uses.IsUsed, PKB.Instance._Uses.IsUsed, and);
        }
        else if (typeAndArguments[0].ToLower() == StringNameResolver.Parent)
        {
            CheckHelpers.IsParentOrFollows(typeAndArguments[1], typeAndArguments[2], AST.Instance.IsParent, and);
        }
        else if (typeAndArguments[0].ToLower() == StringNameResolver.ParentStar)
        {
            CheckHelpers.IsParentOrFollows(typeAndArguments[1], typeAndArguments[2], AST.Instance.IsParentStar, and);
        }
        else if (typeAndArguments[0].ToLower() == StringNameResolver.Modifies)
        {
            QueryHelper.IsModifiesOrUses(typeAndArguments[1], typeAndArguments[2], PKB.Instance._Modifies.IsModified, PKB.Instance._Modifies.IsModified, and);
        }
        else if (typeAndArguments[0].ToLower() == StringNameResolver.Follows)
        {
            CheckHelpers.IsParentOrFollows(typeAndArguments[1], typeAndArguments[2], AST.Instance.IsFollowed, and);
        }
        else if (typeAndArguments[0].ToLower() == StringNameResolver.FollowsStar)
        {
            CheckHelpers.IsParentOrFollows(typeAndArguments[1], typeAndArguments[2], AST.Instance.IsFollowedStar, and);
        }
        else if (typeAndArguments[0].ToLower() == StringNameResolver.Next)
        {
            CheckHelpers.IsNext(typeAndArguments[1], typeAndArguments[2], AST.Instance.IsNext, and);
        }
        else if (typeAndArguments[0].ToLower() == StringNameResolver.Calls)
        {
            CheckHelpers.IsCalls(typeAndArguments[1], typeAndArguments[2], PKB.Instance._Calls!.IsCalls, and);
        }
        else if (typeAndArguments[0].ToLower() == StringNameResolver.CallsStar)
        {
            CheckHelpers.IsCalls(typeAndArguments[1], typeAndArguments[2], PKB.Instance._Calls!.IsCallsStar, and);
        }
        else { throw new ArgumentException($"# Niepoprawna metoda: \"{typeAndArguments[0]}\""); }
    }
}