using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.Models;
using SPA.ParserAndPKB.NodeFactory;
using SPA.ParserAndPKB.Tables;
using System.Runtime.InteropServices;

namespace SPA.ParserAndPKB;

/// <summary>
/// Parser kodu simple na drzewo AST
/// </summary>
public class Parser
{
    private int _lineNumberQuery = 1;
    private int _lineNumberOld = 0;

    /// <summary>
    /// Konstruktor
    /// </summary>
    public Parser()
    {
        AST.AST.Instance.Root = null;
        VarTable.Instance.variables.Clear();
        StmtTable.Instance.statements.Clear();
        ProcTable.Instance.procedures.Clear();
    }

    /// <summary>
    /// Główna metoda parsująca
    /// </summary>
    /// <param name="lines">Lista linii programu simple</param>
    /// <param name="startIndex">Początkowy index</param>
    /// <param name="lineNum">Obecnie sprawdzana linia</param>
    /// <param name="endIndex">Ostatni index</param>
    /// <param name="procName">Nazwa procedury</param>
    /// <param name="parent">Rodzic w drzewie AST</param>
    /// <param name="stmtList">statement List</param>
    public void Parse(List<string> lines, int startIndex, ref int lineNum, out int endIndex, string procName, Node parent, [Optional] Node stmtList)
    {
        //Pobranie tokena dla danych indeksów i linii kodu
        string token = GetToken(lines, ref lineNum, startIndex, out endIndex, true);

        //na podstawie tokenu parsowanie pozostałych 
        switch (token)
        {
            case "procedure":
                ParseProcedure(lines, startIndex, ref lineNum, out endIndex, parent);
                break;
            case "{":
                ParseStmtLst(lines, startIndex, ref lineNum, out endIndex, procName, parent);
                break;
            case "while":
                ParseWhile(lines, startIndex, ref lineNum, out endIndex, procName, parent, stmtList);
                break;
            case "call":
                ParseCall(lines, startIndex, ref lineNum, out endIndex, procName, parent, stmtList);
                break;
            case "if":
                ParseIf(lines, startIndex, ref lineNum, out endIndex, procName, parent, stmtList);
                break;
            default:
                if (ParserUtils.IsVarName(token))
                {
                    ParseAssign(lines, startIndex, ref lineNum, out endIndex, procName, parent, stmtList);
                    break;
                }
                else throw new Exception("Parse: Niespodziewany token: " + token + ", linia: " + lineNum);
        }
    }

    /// <summary>
    /// Metoda inicjalizująca parser
    /// </summary>
    /// <param name="code">Kod simple</param>
    public void Start(string code)
    {
        //rozbicie kodu na linie
        List<string> lines = code.Split(new[] { '\r', '\n' }).ToList();

        if (lines.Count == 0 || (lines.Count == 1 && string.IsNullOrEmpty(lines[0])))
            throw new Exception("# StartParse: Pusty kod");

        int lineNum = 0;
        int index = 0;
        int endIndex;
        string token;
        int countToken = 0;

        //główna pętla - przejście po wszystkich liniach kodu
        while (lineNum < lines.Count)
        {
            // token jako string na podstawie linii
            token = GetToken(lines, ref lineNum, index, out endIndex, true);
            if (token != "") countToken++;
            if (token == "")
            {
                if (countToken > 0) break;
                else throw new Exception("StartParse: Pusty kod");
            }

            //init drzewa AST
            var newRoot = new Node(QueryTokenType.Program);
            AST.AST.Instance.Root = newRoot;

            // pierwszy token musi być procedure 
            if (token != "procedure") throw new Exception("StartParse: Spodziewano się słowa kluczowego procedure, linia: " + lineNum);

            //parsowanie pozostałych tokenów
            Parse(lines, index, ref lineNum, out endIndex, "", newRoot);
            index = endIndex;
        }
        ParserUpdater.UpdateModifiesAndUsesTablesInProcedures();
        ParserUpdater.UpdateModifiesAndUsesTablesInWhilesAndIfs();
    }

    /// <summary>
    /// Zwraca string będący tokenem
    /// </summary>
    /// <param name="lines">Linie do sprawdzenia</param>
    /// <param name="lineNum">Numer linii do stawdzenia</param>
    /// <param name="startIndex">Początkowy index</param>
    /// <param name="endIndex">Końcowy index</param>
    /// <param name="test">Czy test</param>
    /// <returns>Token będący stringiem</returns>

    public string GetToken(List<string> lines, ref int lineNum, int startIndex, out int endIndex, bool test)
    {
        if (startIndex == -1)
        {
            lineNum++;
            startIndex = 0;
        }
        if (lineNum >= lines.Count)
        {
            throw new Exception("ParseProcedure: Nieoczekiwany koniec pliku, linia: " + lineNum);
        }
        int startingLine = lineNum;
        string token = "";
        char character;
        while (true)
        {
            string fileLine = lines.ElementAt(lineNum);
            while (string.IsNullOrEmpty(fileLine))
            {
                lineNum++;
                if (lineNum >= lines.Count)
                {
                    endIndex = -1;
                    if (test) lineNum = startingLine;
                    return "";
                }
                fileLine = lines.ElementAt(lineNum);
            }
            while (startIndex < fileLine.Length && (character = fileLine[startIndex]) == ' ')
                startIndex++;

            if (startIndex >= fileLine.Length)
            {
                if (startIndex >= fileLine.Length)
                {
                    if (!fileLine.Contains("procedure") && !fileLine.Contains("else"))
                    {
                        if (lineNum - _lineNumberOld >= 1)
                        {
                            _lineNumberQuery++;
                            _lineNumberOld = lineNum;
                        }
                    }
                    lineNum++;
                    startIndex = 0;

                    if (lineNum >= lines.Count)
                    {
                        endIndex = -1;
                        if (test) lineNum = startingLine;
                        return "";
                    }
                }
                lineNum++;
                startIndex = 0;
                if (lineNum >= lines.Count)
                {
                    endIndex = -1;
                    if (test) lineNum = startingLine;
                    return "";
                }
            }
            else
            {
                break;
            }
        }
        string currentLine = lines[lineNum];
        int index = startIndex;
        for (; index < currentLine.Length; index++)
        {
            character = currentLine[index];

            if (char.IsLetterOrDigit(character))
            {
                token += character;
            }
            else
            {
                if (string.IsNullOrEmpty(token))
                {
                    token += character;
                    endIndex = index + 1;
                    if (endIndex > currentLine.Length) endIndex = -1;
                    if (test) lineNum = startingLine;
                    return token;
                }
                else
                {
                    endIndex = index;
                    if (test) lineNum = startingLine;
                    return token;
                }
            }
        }
        endIndex = index;
        if (endIndex > currentLine.Length) endIndex = -1;

        if (test) lineNum = startingLine;
        return token;
    }

    /// <summary>
    /// Tworzy element w drzewie AST dla typu procedure
    /// </summary>
    /// <param name="lines">Linie simple</param>
    /// <param name="startIndex">Początkowy index</param>
    /// <param name="lineNum">Numer lini do sprawdenia</param>
    /// <param name="endIndex">Końcowy index</param>
    /// <param name="parent">Rodzic w drzewie AST</param>

    public void ParseProcedure(List<string> lines, int startIndex, ref int lineNum, out int endIndex, Node parent)
    {
        string token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        ValidateToken(token, "procedure", lineNum, "ParseProcedure: Brak słowa procedure, linia: ");
        startIndex = endIndex;
        token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        Node newNode = new Node(QueryTokenType.Procedure);
        string procName = ValidateAndAddProcedure(token, lineNum, newNode, parent);
        startIndex = endIndex;
        token = GetToken(lines, ref lineNum, startIndex, out endIndex, true);
        ValidateToken(token, "{", lineNum, "ParseProcedure: Brak nawiasu { po nazwie procedury, linia: ");
        Parse(lines, startIndex, ref lineNum, out endIndex, procName, newNode);
    }

    //sprawdzanie typu tokenu 
    private void ValidateToken(string token, string expectedToken, int lineNum, string errorMessage)
    {
        if (token != expectedToken)
        {
            throw new Exception($"{errorMessage} {lineNum}");
        }
    }

    private string ValidateAndAddProcedure(string token, int lineNum, Node newNode, Node parent)
    {
        if (!ParserUtils.IsVarName(token))
        {
            throw new Exception($"ParseProcedure: Błędna nazwa procedury, {token}, linia: {lineNum}");
        }
        ProcTable.Instance.AddProcedure(token);
        ProcTable.Instance.SetAstRootNode(token, newNode);
        AST.AST.Instance.SetChildOfLink(parent, newNode);
        return token;
    }


    /// <summary>
    /// Tworzy element w drzewie AST dla typu statement list
    /// </summary>
    /// <param name="lines">Linie simple</param>
    /// <param name="startIndex">Początkowy index</param>
    /// <param name="lineNum">Numer lini do sprawdenia</param>
    /// <param name="endIndex">Końcowy index</param>
    /// <param name="parent">Rodzic w drzewie AST</param>
    /// <param name="procName">Nazwa procedury</param>

    public void ParseStmtLst(List<string> lines, int startIndex, ref int lineNum, out int endIndex, string procName, Node parent)
    {
        string token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        ValidateToken(token, "{", lineNum, "ParseStmtList: Brak znaku { linia: ");
        Node newNode = CreateStmtList.Create(parent);
        startIndex = endIndex;
        while (lineNum < lines.Count)
        {
            Parse(lines, startIndex, ref lineNum, out endIndex, procName, parent, newNode);
            startIndex = endIndex;

            token = GetToken(lines, ref lineNum, startIndex, out endIndex, true);
            if (token == "}")
            {
                token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
                break;
            }
        }
        if (lineNum == lines.Count && token != "}")
        {
            throw new Exception("ParseStmtLst: Brak znaku }, linia: " + lineNum);
        }
    }

    /// <summary>
    /// Tworzy element w drzewie AST dla typu while
    /// </summary>
    /// <param name="lines">Linie simple</param>
    /// <param name="startIndex">Początkowy index</param>
    /// <param name="lineNum">Numer lini do sprawdenia</param>
    /// <param name="endIndex">Końcowy index</param>
    /// <param name="parent">Rodzic w drzewie AST</param>
    /// <param name="procName">Nazwa procedury</param>
    /// <param name="stmtListNode">Roszic dla node while</param>

    public void ParseWhile(List<string> lines, int startIndex, ref int lineNum, out int endIndex, string procName, Node parent, Node stmtListNode)
    {
        string token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        ValidateToken(token, "while", lineNum, "ParseWhile: Brak słowa kluczowego while, linia: ");
        StmtTable.Instance.AddStatement(QueryTokenType.While, _lineNumberQuery);
        startIndex = endIndex;
        Node whileNode = CreateWhileNode.Create(parent, stmtListNode, _lineNumberQuery);
        token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        ParseVariable(token, lineNum, whileNode);
        startIndex = endIndex;
        token = GetToken(lines, ref lineNum, startIndex, out endIndex, true);
        ValidateToken(token, "{", lineNum, "ParseWhile: Brak znaku {, linia: ");
        Parse(lines, startIndex, ref lineNum, out endIndex, procName, whileNode);
    }

    private Node ParseVariable(string token, int lineNum, Node whileNode)
    {
        if (!ParserUtils.IsVarName(token))
        {
            throw new Exception($"ParseWhile: Wymagana nazwa zmiennej, {token}, linia: {lineNum}");
        }
        Node variableNode = new Node(QueryTokenType.Variable);
        AST.AST.Instance.SetChildOfLink(whileNode, variableNode);
        Variable var = new Variable(token);
        if (VarTable.Instance!.GetVarIndex(token) == -1)
        {
            VarTable.Instance.AddVariable(token);
        }
        SetUsesForFamily(whileNode, var);
        return variableNode;
    }

    public void SetUsesForFamily(Node node, Variable var)
    {
        if (node.EntityType == QueryTokenType.Procedure)
        {
            Procedure proc = ProcTable.Instance.procedures.Where(i => i.AstRoot == node).FirstOrDefault();
            var.Id = VarTable.Instance.GetVarIndex(var.Identifier);
            Uses.Uses.Instance.SetUses(proc, var);
        }
        else
        {
            Statement stmt = StmtTable.Instance.statements.Where(i => i.AstRoot == node).FirstOrDefault();
            var.Id = VarTable.Instance.GetVarIndex(var.Identifier);
            Uses.Uses.Instance.SetUses(stmt, var);
        }
        if (AST.AST.Instance.GetParent(node) != null) SetUsesForFamily(AST.AST.Instance.GetParent(node), var);
    }

    public void ParseAssign(List<string> lines, int startIndex, ref int lineNum, out int endIndex, string procName, Node parent, Node stmtListNode)
    {
        string token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        if (!ParserUtils.IsVarName(token))
            throw new Exception("ParseAssign: Wymagana nazwa zmiennej, " + token + ", linia: " + lineNum);
        StmtTable.Instance.AddStatement(QueryTokenType.Assign, _lineNumberQuery);
        startIndex = endIndex;
        Node assignNode = CreateAssignNode.Create(parent, stmtListNode, token, _lineNumberQuery);
        token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        if (token != "=")
            throw new Exception("ParseAssign: Brak znaku =, linia: " + lineNum);
        startIndex = endIndex;
        Node root;
        ParseExpr(lines, startIndex, ref lineNum, out endIndex, procName, assignNode, assignNode, false, out root);
        AST.AST.Instance.SetChildOfLink(assignNode, root);
    }

    public bool ParseExpr(List<string> lines, int startIndex, ref int lineNum, out int endIndex,
        string procName, Node assignNode, Node parent, bool inBracket,
        out Node root, string prevToken = "")
    {
        root = null;
        var endAssign = false;
        var token = "";
        endIndex = startIndex;
        var bracketsPaired = false;
        var expectedAction = false;
        var possibleBracketClose = false;
        var tokenCount = 0;
        while (lineNum < lines.Count)
        {
            token = GetToken(lines, ref lineNum, startIndex, out endIndex, true);
            tokenCount++;
            if (expectedAction)
            {
                Node oldAssignRoot;
                token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
                startIndex = endIndex;
                Dictionary<string, QueryTokenType> operationTypes = new Dictionary<string, QueryTokenType>
                {
                    { "+", QueryTokenType.Plus },
                    { "-", QueryTokenType.Minus },
                    { "*", QueryTokenType.Multiply },
                    { "/", QueryTokenType.Divide }
                };

                if (operationTypes.ContainsKey(token))
                {
                    oldAssignRoot = new Node(root);
                    root = new Node(operationTypes[token]);
                    AST.AST.Instance.SetChildOfLink(root, oldAssignRoot);
                    expectedAction = false;
                }
                else if (token == ")")
                {
                    if (!possibleBracketClose)
                        throw new Exception("ParseExpr: niespodziewany znak ), linia: " + lineNum);

                    expectedAction = true;
                    bracketsPaired = true;
                    token = GetToken(lines, ref lineNum, startIndex, out endIndex, true);
                    if (token == "*" || token == "/")
                    {
                        if (parent.EntityType == QueryTokenType.Multiply || parent.EntityType == QueryTokenType.Divide)
                        {
                            AST.AST.Instance.SetChildOfLink(root, parent);
                        }
                    }
                    else
                    {
                        AST.AST.Instance.SetChildOfLink(root, parent);
                    }
                }
                else
                    throw new Exception("ParseExpr: Nieobsługiwane działanie, " + token + ", linia: " + lineNum);
            }
            else
            {
                if (ParserUtils.IsVarName(token))
                {

                    if (root == null) root = new Node(QueryTokenType.Variable);
                    else
                    {
                        string nextToken = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
                        if (nextToken == "*" || nextToken == "/")
                        {
                            if (root.EntityType == QueryTokenType.Divide || root.EntityType == QueryTokenType.Multiply)
                            {
                                Node rightSide = new Node(QueryTokenType.Variable);
                                AST.AST.Instance.SetChildOfLink(root, rightSide);
                            }
                            else
                            {
                                Node tinyTreeRoot = null;
                                endAssign = ParseExpr(lines, startIndex, ref lineNum, out endIndex, procName, assignNode, root, false, out tinyTreeRoot, token);
                                AST.AST.Instance.SetChildOfLink(root, tinyTreeRoot);
                            }
                        }
                        else
                        {
                            Node rightSide = new Node(QueryTokenType.Variable);
                            AST.AST.Instance.SetChildOfLink(root, rightSide);
                        }
                    }
                    Variable usesVar = new Variable(token);
                    if (VarTable.Instance.GetVarIndex(token) == -1)
                    {
                        VarTable.Instance.AddVariable(token);
                    }
                    SetUsesForFamily(assignNode, usesVar);
                    startIndex = endIndex;
                    expectedAction = true;
                }
                else if (ParserUtils.IsConstValue(token))
                {
                    if (root == null) root = new Node(QueryTokenType.Constant);
                    else
                    {
                        string nextToken = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
                        if (nextToken == "*" || nextToken == "/")
                        {
                            if (root.EntityType == QueryTokenType.Divide || root.EntityType == QueryTokenType.Multiply)
                            {
                                Node rightSide = new Node(QueryTokenType.Constant);
                                AST.AST.Instance.SetChildOfLink(root, rightSide);
                            }
                            else
                            {
                                Node tinyTreeRoot = null;
                                endAssign = ParseExpr(lines, startIndex, ref lineNum, out endIndex, procName, assignNode, root, false, out tinyTreeRoot, token);
                                AST.AST.Instance.SetChildOfLink(root, tinyTreeRoot);
                            }
                        }
                        else
                        {
                            Node rightSide = new Node(QueryTokenType.Constant);
                            AST.AST.Instance.SetChildOfLink(root, rightSide);
                        }
                    }
                    startIndex = endIndex;
                    expectedAction = true;
                }
                else if (token == "(")
                {
                    if (tokenCount == 1)
                    {
                        possibleBracketClose = true;
                        bracketsPaired = false;
                        startIndex = endIndex;
                    }
                    else
                    {
                        Node tinyTreeRoot;
                        endAssign = ParseExpr(lines, startIndex, ref lineNum, out endIndex, procName, assignNode, root, true, out tinyTreeRoot);
                        if (root == null)
                            root = new Node(tinyTreeRoot);
                        startIndex = endIndex;
                        expectedAction = true;
                        if (endAssign)
                        {
                            token = ";";
                            break;
                        }
                    }


                }
                else throw new Exception("ParseExpr: Spodziewana zmienna lub stała, " + token + ", linia: " + lineNum);
            }
            token = GetToken(lines, ref lineNum, startIndex, out endIndex, true);
            if (token == ";")
            {
                if (inBracket && !bracketsPaired) throw new Exception("ParseExpr: Brak nawiasu zamykajacego, wystapil " + token + ", linia: " + lineNum);
                token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
                endAssign = true;
                break;
            }
        }
        if (lineNum == lines.Count && token != ";") throw new Exception("ParseExpr: Spodziewano się znaku ; linia: " + lineNum);
        return endAssign;
    }

    public void ParseCall(List<string> lines, int startIndex, ref int lineNum, out int endIndex, string procName, Node parent, Node stmtListNode)
    {
        string token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        ValidateToken(token, "call", lineNum, "ParseCall: Brak słowa kluczowego call, linia: ");
        startIndex = endIndex;
        Node callNode = CreateCallNode.Create(parent, stmtListNode, _lineNumberQuery);
        token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        callNode.Name = token;
        startIndex = endIndex;
        token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        ValidateToken(token, ";", lineNum, "ParseCall: Brak znaku ; linia: ");
    }

    public void ParseIf(List<string> lines, int startIndex, ref int lineNum, out int endIndex, string procName, Node parent, Node stmtListNode)
    {
        string token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        ValidateToken(token, "if", lineNum, "ParseIf: Brak słowa kluczowego if, linia: ");
        startIndex = endIndex;
        Node ifNode = CreateIfNode.Create(parent, stmtListNode, _lineNumberQuery);
        token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        if (ParserUtils.IsVarName(token))
        {
            Variable var = new Variable(token);
            if (VarTable.Instance.GetVarIndex(token) == -1)
            {
                VarTable.Instance.AddVariable(token);
            }
            SetUsesForFamily(ifNode, var);
        }
        else
        {
            throw new Exception($"ParseIf: Wymagana nazwa zmiennej, {token}, linia: {lineNum}");
        }
        startIndex = endIndex;
        token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        ValidateToken(token, "then", lineNum, "ParseIf: Brak słowa kluczowego then, linia: ");
        startIndex = endIndex;
        token = GetToken(lines, ref lineNum, startIndex, out endIndex, true);
        ValidateToken(token, "{", lineNum, "ParseIf: Brak znaku {, linia: ");
        Parse(lines, startIndex, ref lineNum, out endIndex, procName, ifNode);
        startIndex = endIndex;
        token = GetToken(lines, ref lineNum, startIndex, out endIndex, false);
        ValidateToken(token, "else", lineNum, "ParseIf: Brak słowa kluczowego else, linia: ");
        startIndex = endIndex;
        token = GetToken(lines, ref lineNum, startIndex, out endIndex, true);
        ValidateToken(token, "{", lineNum, "ParseIf: Brak znaku {, linia: ");
        Parse(lines, startIndex, ref lineNum, out endIndex, procName, ifNode);
        startIndex = endIndex;
    }
}