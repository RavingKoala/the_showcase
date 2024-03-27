using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Web.Services;

namespace Web.Models.Chess;
public class Move {

    private readonly string _regexFullMoveStringCheck = "^[a-zA-Z](?:[a-h][1-8])?x?(?:[a-h][1-8])$";

    private IPiece _piece { get; set; }
    private Square? _from { get; set; }
    private Square _to { get; set; }

    [Required]
    public string Piece { get => _piece.PieceCode; set => _piece.PieceCode = value; }
    public string? From { get => _from is not null ? _from.ToString() : null ; set => _from = value is not null ? new Square(value) : null; }
    [Required]
    public bool TakesPiece { get; set; }
    [Required]
    [RegexStringValidator(@"[a-zA-Z][1-8]")]
    public string To { get => _to.ToString(); set => _to = new Square(value); }

    public const string TakesPieceSymbol = "x";

    public Move(string moveStr) {
        RegexStringValidator fullMoveRegexValidator = new RegexStringValidator(_regexFullMoveStringCheck);
        try {
            fullMoveRegexValidator.Validate(moveStr);
        } catch (Exception e) {
            throw new ParseMoveException($"Invalid move format, expected: {_regexFullMoveStringCheck} but got {moveStr}!");
        }
        // Pf1xf2 -> (pawn), (from f1), (takes a piece) (to f2)
        // Pf2 -> (pawn), (to f2)
        Piece = moveStr.Remove(0, 1);
        To = moveStr.Remove(moveStr.Length - 2, 2);
        if (moveStr.Length > 1)
            From = moveStr.Remove(0, 2);
        if (moveStr[moveStr.Length - 1].ToString() == TakesPieceSymbol)
            TakesPiece = moveStr.Remove(moveStr.Length - 1, 1) == TakesPieceSymbol;

        if (moveStr.Length != 0)
            throw new ParseMoveException($"Invalid move format, expected: {_regexFullMoveStringCheck} but got {moveStr}!");
    }

    public override string ToString() {
        return string.Format($"{0}{1}{2}{3}", Piece, From, (TakesPiece ? TakesPieceSymbol : ""), To);
    }
}

public class ParseMoveException : Exception {
    public ParseMoveException(string message) : base(message) { }
    public ParseMoveException(string message, Exception inner) : base(message, inner) { }
}
