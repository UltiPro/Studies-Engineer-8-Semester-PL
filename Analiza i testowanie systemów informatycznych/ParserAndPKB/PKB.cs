using SPA.ParserAndPKB.Calls;
using SPA.ParserAndPKB.Modifies;
using SPA.ParserAndPKB.Tables;
using SPA.ParserAndPKB.Uses;

namespace SPA.ParserAndPKB;

public class PKB : IPKB
{
    private static PKB? _PKB;
    public static PKB Instance
    {
        get
        {
            _PKB ??= new PKB();//if not null
            return _PKB;
        }
    }

    public IUses? _Uses => Uses.Uses.Instance;
    public ICalls? _Calls => Calls.Calls.Instance;
    public IModifies? _Modifies => Modifies.Modifies.Instance;
    public IProcTable? _ProcTable => ProcTable.Instance;
    public IStmtTable? _StmtTable => StmtTable.Instance;
    public IVarTable? _VarTable => VarTable.Instance;
}