using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Web.Models.Chess {
    public class Move {

        private readonly string _regexFullMoveStringCheck = "^[a-zA-Z](?:[a-h][1-8])?x?(?:[a-h][1-8])$";

        private Piece _piece{ get; set; }
        private (char, char)? _from { get; set; }
        private (char, char) _to { get; set; }
        
        [Required]
        public string Piece { get => _piece.PieceCode; set => _piece.PieceCode = value; }

        [RegexStringValidator(@"[a-zA-Z]\d")]
        public string? From { get => _from is not null ? $"{_from.Value.Item1}{_from?.Item2}" : null; set => _from = value is not null ? (value[0], value[1]) : null; }
        [Required]
        public bool TakesPiece { get; set; }
        [Required]
        [RegexStringValidator(@"[a-zA-Z]\d")]
        public string To { get; set; }

        public const string TakesPieceSymbol = "x";


        public void FromString(string moveStr) {
            RegexStringValidator fullMoveRegexValidator = new RegexStringValidator(_regexFullMoveStringCheck);
            try {
                fullMoveRegexValidator.Validate(moveStr);
            } 
            catch (Exception e) {
                throw new FailedToParseMoveException($"Invalid move format, expected: {_regexFullMoveStringCheck} but got {moveStr}!");
            }
            // Pf1xf2 -> (pawn), (from f1), (takes a piece) (to f2)
            // Pf2 -> (pawn), (to f2)
            Piece = moveStr.Remove(0, 1);
            To = moveStr.Remove(moveStr.Length-2, 2);
            if (moveStr.Length > 1)
                From = moveStr.Remove(0, 2);
            if (moveStr[moveStr.Length-1].ToString() == TakesPieceSymbol)
                TakesPiece = moveStr.Remove(moveStr.Length-1, 1) == TakesPieceSymbol;
            
            throw new FailedToParseMoveException($"Invalid move format, expected: {_regexFullMoveStringCheck} but got {moveStr}!");
        }

        public override string ToString() {
            return string.Format($"{0}{1}{2}{3}", Piece, From, (TakesPiece? TakesPieceSymbol : ""), To);
        }
    }

    public class FailedToParseMoveException : Exception {
        public FailedToParseMoveException(string message) : base(message) { }
        public FailedToParseMoveException(string message, Exception inner) : base(message, inner) { }
    }
}
