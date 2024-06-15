using SPA.ParserAndPKB.Calls;
using SPA.ParserAndPKB.Modifies;
using SPA.ParserAndPKB.Tables;
using SPA.ParserAndPKB.Uses;

namespace SPA.ParserAndPKB;

public interface IPKB
{
    IUses? _Uses { get; }
    ICalls? _Calls { get; }
    IModifies? _Modifies { get; }
    IProcTable? _ProcTable { get; }
    IStmtTable? _StmtTable { get; }
    IVarTable? _VarTable { get; }
}