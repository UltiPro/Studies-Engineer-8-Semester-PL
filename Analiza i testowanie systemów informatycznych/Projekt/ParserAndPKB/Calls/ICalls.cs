using SPA.ParserAndPKB.Models;

namespace SPA.ParserAndPKB.Calls;

public interface ICalls
{
    List<Procedure> GetCalls(string proc);
    List<Procedure> GetCallsStar(string proc);
    bool IsCalls(string proc1, string proc2);
    bool IsCallsStar(string proc1, string proc2);
}