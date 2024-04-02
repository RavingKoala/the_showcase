namespace Web.Models.Chess;

internal interface IPieceType {
    internal char GetCode();
    internal string GetName();
    internal IEnumerable<Square> ValidMoves(Square from);
}

internal class Piece {
    private IPieceType _pieceType;

    internal string Name { get => _pieceType.GetName(); }
    internal SideColor Color { get; private set; }
    internal char Code { get; private set; }


    internal Piece(char code) {
        Color = char.IsLower(code) ? SideColor.White : SideColor.Black;
        _pieceType = Piece.CodeToPiece(code);
    }

    internal Piece(string code) {
        if (code.Length != 1)
            throw new ArgumentException("Piece code is not recognized!");

        _pieceType = Piece.CodeToPiece(code[0]);
        Color = char.IsLower(code[0]) ? SideColor.White : SideColor.Black;
    }

    private static IPieceType CodeToPiece(char code) {
        switch (char.ToLower(code)) {
            case 'p':
                return new Pawn();
            default:
                throw new ArgumentException("Code not found");
        }
    }

    internal char ToCode() {
        char code = _pieceType.GetCode();
        return Color == SideColor.White ? code : char.ToUpper(code);
    }
    internal IEnumerable<Square> ValidMoves(Square from) {
        return _pieceType.ValidMoves(from);
    }

    internal bool IsValidMove(Square from, Square to) {
        return ValidMoves(from).Any(x => x.Equals(to));
    }
}

internal class Pawn : IPieceType {
    char IPieceType.GetCode() => 'p';

    string IPieceType.GetName() => "Pawn";

    IEnumerable<Square> IPieceType.ValidMoves(Square from) {
        List<(int, int)> respectivePos = new List<(int, int)> {
            (-1,-1), ( 0,-1), ( 1,-1),
            (-1, 0),          ( 1, 0),
            (-1, 1), ( 0, 1), ( 1, 1),
        };

        string fromCode = from.ToString();
        (char, char) fromPair = (fromCode[0], fromCode[1]);

        List<Square> returnSquares = new List<Square>();

        foreach ((int, int) pos in respectivePos) {
            string newLocation = $"{pos.Item1 + fromPair.Item1}{pos.Item2 + fromPair.Item2}";
            if (Square.IsValidLocation(newLocation))
                returnSquares.Add(new Square(newLocation));
        }

        return returnSquares;
    }
}
