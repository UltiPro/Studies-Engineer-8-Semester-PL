using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.Models;
using SPA.ParserAndPKB;

namespace SPA.QueryResolver;

public static class CheckHelpers
{
    public static void IsProcedureModifiesOrUses(string _1Arg, string _2Arg, Func<Variable, Procedure, bool> IsModifiedOrUsedByProc, bool and)
    {
        var procIdxesLimit = new List<int>();
        var varIdxesLimit = new List<int>();

        var _1ArgType = _1Arg[0] == '\"' & _1Arg[^1] == '\"' ? QueryTokenType.Procedure : QueryProcessor.GetVariableEnumType(_1Arg);
        var _1ArgIdxes = QueryParser.GetArgIndexes(_1Arg, _1ArgType);

        var _2ArgType = _2Arg[0] == '\"' & _2Arg[^1] == '\"' || 
                        _2Arg == "_" ? QueryTokenType.Variable : QueryProcessor.GetVariableEnumType(_2Arg);
        var _2ArgIdxes = QueryParser.GetArgIndexes(_2Arg, _2ArgType);

        foreach (var idx1 in _1ArgIdxes)     
            foreach (var idx2 in _2ArgIdxes)
            {
                var proc = PKB.Instance._ProcTable.GetProcedure(idx1);
                var var = PKB.Instance._VarTable!.GetVar(idx2);
                if (IsModifiedOrUsedByProc(var, proc))
                {
                    procIdxesLimit.Add(idx1);
                    varIdxesLimit.Add(idx2);
                }
            } 
        QueryParser.RemoveIndexesFromLists(_1Arg, _2Arg, procIdxesLimit, varIdxesLimit, and);
    }

    public static void IsStatementModifiesOrUses(string _1Arg, string _2Arg, Func<Variable, Statement, bool> IsModifiedOrUsedByStmt, bool and)
    {
        var stmtIdxesLimit = new List<int>();
        var varIdxesLimit = new List<int>();

        var _1ArgType =  int.TryParse(_1Arg, out _) || 
                         _1Arg == "_" ? QueryTokenType.Statement : QueryProcessor.GetVariableEnumType(_1Arg);
        var _1ArgIdxes = QueryParser.GetArgIndexes(_1Arg, _1ArgType);
        
        var _2ArgType = _2Arg[0] == '\"' & _2Arg[^1] == '\"' ||
                        _2Arg == "_" ? QueryTokenType.Variable : QueryProcessor.GetVariableEnumType(_2Arg);
        var _2ArgIdxes = QueryParser.GetArgIndexes(_2Arg, _2ArgType);
        

        foreach (var idx1 in _1ArgIdxes)     
            foreach (var idx2 in _2ArgIdxes)
            {
                var statement = PKB.Instance._StmtTable!.GetStatement(idx1);
                var variable = PKB.Instance._VarTable!.GetVar(idx2);
                if (IsModifiedOrUsedByStmt(variable, statement))
                {
                    stmtIdxesLimit.Add(idx1);
                    varIdxesLimit.Add(idx2);
                }
            }      
        QueryParser.RemoveIndexesFromLists(_1Arg, _2Arg, stmtIdxesLimit, varIdxesLimit, and);
    }

    public static void IsParentOrFollows(string _1Arg, string _2Arg, Func<Node, Node, bool> method, bool and)
    {
        var _1IdxesLimit = new List<int>();
        var _2IdxesLimit = new List<int>();

        var _1ArgType = int.TryParse(_1Arg, out _) || 
                        _1Arg == "_" ? QueryTokenType.Statement : QueryProcessor.GetVariableEnumType(_1Arg);
        var _1ArgIdxes = QueryParser.GetArgIndexes(_1Arg, _1ArgType);

        var _2ArgType = int.TryParse(_2Arg, out _) || 
                        _2Arg == "_" ? QueryTokenType.Statement : QueryProcessor.GetVariableEnumType(_2Arg);
        var _2ArgIdxes = QueryParser.GetArgIndexes(_2Arg, _2ArgType);

        foreach (int idx1 in _1ArgIdxes)
            foreach (int idx2 in _2ArgIdxes)
            {
                var first = NodeHelper.GetNodeByType(_1ArgType, idx1);
                var second = NodeHelper.GetNodeByType(_2ArgType, idx2);
                if (method(first, second))
                {
                    _1IdxesLimit.Add(idx1);
                    _2IdxesLimit.Add(idx2);
                }
            }
        QueryParser.RemoveIndexesFromLists(_1Arg, _2Arg, _1IdxesLimit, _2IdxesLimit, and);
    }

    public static void IsCalls(string _1Arg, string _2Arg, Func<string, string, bool> method, bool and)
    {
        var _1IdxesLimit = new List<int>();
        var _2IdxesLimit = new List<int>();

        var _1ArgType = _1Arg[0] == '\"' & _1Arg[^1] == '\"' || 
                                    _1Arg == "_" ? QueryTokenType.Procedure : QueryProcessor.GetVariableEnumType(_1Arg);
        var _1ArgIdxes = QueryParser.GetArgIndexes(_1Arg, _1ArgType);


        var _2ArgType = _2Arg[0] == '\"' & _2Arg[^1] == '\"' || 
                                    _2Arg == "_" ? QueryTokenType.Procedure : QueryProcessor.GetVariableEnumType(_2Arg);
        var _2ArgIdxes = QueryParser.GetArgIndexes(_2Arg, _2ArgType);

        if (_1ArgType != QueryTokenType.Procedure)
            throw new ArgumentException("Not a procedure: {0}", _1Arg);
        else if (_2ArgType != QueryTokenType.Procedure)
            throw new ArgumentException("Not a procedure: {0}", _2Arg);

        foreach (var idx1 in _1ArgIdxes)
            foreach (var idx2 in _2ArgIdxes)
            {
                var p1 = PKB.Instance._ProcTable!.GetProcedure(idx1);
                var p2 = PKB.Instance._ProcTable.GetProcedure(idx2);

                var _1st = p1 == null ? "" : p1.Identifier;
                var _2nd = p2 == null ? "" : p2.Identifier;

                if (method(_1st, _2nd))
                {
                    _1IdxesLimit.Add(idx1);
                    _2IdxesLimit.Add(idx2);
                }
            }
        QueryParser.RemoveIndexesFromLists(_1Arg, _2Arg, _1IdxesLimit, _2IdxesLimit, and);
    }

    public static void IsNext(string _1Arg, string _2Arg, Func<Node, Node, bool> method, bool and)
    {
        var _1IdxesLimit = new List<int>();
        var _2IdxesLimit = new List<int>();

        var _1ArgType = int.TryParse(_1Arg, out _) || 
                           _1Arg == "_" ? QueryTokenType.Prog_line : QueryProcessor.GetVariableEnumType(_1Arg);
        var _1ArgIdxes = QueryParser.GetArgIndexes(_1Arg, _1ArgType);
        
        var _2ArgType = int.TryParse(_2Arg, out _) || 
                            _2Arg == "_" ? QueryTokenType.Prog_line : QueryProcessor.GetVariableEnumType(_2Arg);
        var _2ArgIdxes = QueryParser.GetArgIndexes(_2Arg, _2ArgType);

        foreach (var idx1 in _1ArgIdxes)
            foreach (var idx2 in _2ArgIdxes)
            {
                var first = NodeHelper.GetNodeByType(_1ArgType, idx1);
                var second = NodeHelper.GetNodeByType(_2ArgType, idx2);
                if (method(first, second))
                {
                    _1IdxesLimit.Add(idx1);
                    _2IdxesLimit.Add(idx2);
                }
            }
        QueryParser.RemoveIndexesFromLists(_1Arg, _2Arg, _1IdxesLimit, _2IdxesLimit, and);
    }
}
