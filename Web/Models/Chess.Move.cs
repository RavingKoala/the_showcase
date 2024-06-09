using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Web.Models.Chess;
internal class Move {
    private readonly string _regexFullMoveStringCheck = "^[a-zA-Z](?:[a-h][1-8])?x?(?:[a-h][1-8])$";

    private Piece _piece { get; set; }
    private Square _from { get; set; }
    private Square _to { get; set; }

    [Required]
    internal string Piece { get => _piece.ToCode().ToString(); set => _piece = new Piece(value); }
    [RegexStringValidator(@"[a-zA-Z][1-8]")]
    internal string From { get => _from.ToString(); set => _from = new Square(value); }
    [Required]
    internal bool TakesPiece { get; set; }
    [Required]
    [RegexStringValidator(@"[a-zA-Z][1-8]")]
    internal string To { get => _to.ToString(); set => _to = new Square(value); }

    internal const string TakesPieceSymbol = "x";

    internal Move(string moveStr) {
        RegexStringValidator fullMoveRegexValidator = new RegexStringValidator(_regexFullMoveStringCheck);
        try {
            fullMoveRegexValidator.Validate(moveStr);
        } catch (Exception _) {
            throw new ParseMoveException($"Invalid move format, expected: {_regexFullMoveStringCheck} but got {moveStr}!");
        }
        // Pf1xf2 -> (black pawn), (from f1), (takes a piece) (to f2)
        // Pf2 -> (black pawn), (to f2)
        Piece = moveStr.Remove(0, 1);
        To = moveStr.Remove(moveStr.Length - 2, 2);
        if (moveStr.Length > 1)
            From = moveStr.Remove(0, 2);
        if (moveStr[moveStr.Length - 1].ToString() == TakesPieceSymbol)
            TakesPiece = moveStr.Remove(moveStr.Length - 1, 1) == TakesPieceSymbol;

        if (moveStr.Length != 0)
            throw new ParseMoveException($"Invalid move format, expected: {_regexFullMoveStringCheck} but got {moveStr}!");
    }

    public Piece getPiece() {
        return _piece;
    }

    public Square getFrom() {
        return _from;
    }

    public Square getTo() {
        return _to;
    }

    public override string ToString() {
        return string.Format($"{0}{1}{2}{3}", Piece, From, (TakesPiece ? TakesPieceSymbol : ""), To);
    }
}

public class ParseMoveException : Exception {
    public ParseMoveException(string message) : base(message) { }
    public ParseMoveException(string message, Exception inner) : base(message, inner) { }
}
