using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.Models;

namespace SPA.ParserAndPKB.Tables;

public class StmtTable : IStmtTable
{
    private static StmtTable? _StmtTable;
    public static StmtTable Instance
    {
        get
        {
            _StmtTable ??= new StmtTable();//if is null
            return _StmtTable;
        }
    }

    public List<Statement> statements { get; set; }

    public StmtTable() => statements = new List<Statement>();

    public int AddStatement(QueryTokenType entityType, int codeLine)
    {
        if (statements.Any(s => s.LineNumber == codeLine)) return -1;
        else
        {
            statements.Add(new Statement(entityType, codeLine));
            return 0;
        }
    }

    public Node? GetAstRoot(int codeLine) => GetStatement(codeLine)?.AstRoot ?? null;

    public Statement? GetStatement(int codeLine) => statements.FirstOrDefault(i => i.LineNumber == codeLine);

    public int SetAstRoot(int codeLine, Node node)
    {
        var procedure = GetStatement(codeLine);
        if (procedure == null) return -1;
        else
        {
            procedure.AstRoot = node;
            return 0;
        }
    }

    public List<Statement> GetTable() => statements;

    public void PrintStatements()
    {
        foreach (var statement in statements)
        {
            Console.WriteLine($"Statement LineNumber: {statement.LineNumber}, StmtType: {statement.StmtType}");

            Console.WriteLine("ModifiesList:");
            foreach (var modify in statement.ModifiesList)
            {
                Console.WriteLine($"  Variable: {modify.Key}, Modified: {modify.Value}");
            }

            Console.WriteLine("UsesList:");
            foreach (var use in statement.UsesList)
            {
                Console.WriteLine($"  Variable: {use.Key}, Used: {use.Value}");
            }

            Console.WriteLine();
        }
    }
}