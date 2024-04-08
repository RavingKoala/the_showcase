namespace Web.Models.Chess;
public enum SideColor {
    White,
    Black
}

internal class ChessGame {
    internal const string StartBoard = "        pppppppp                                PPPPPPPP        ";

    internal Board Board { get; private init; }

    internal ChessGame() {
        Board = new Board(StartBoard);
    }

    internal ChessGame(string board) {
        Board = new Board(board);
    }

    internal void DoMove(string move, SideColor personColor) {
        Move newMove = new Move(move);

        DoMove(newMove, personColor);
    }

    internal bool IsValidMove(string move, SideColor personColor) {
        Move newMove = new Move(move);

        return IsValidMove(newMove, personColor);
    }

    private void DoMove(Move move, SideColor personColor) {
        if (!IsValidMove(move, personColor))
            throw new MoveNotAllowedException($"Move {move} is not possible!");
        
        Piece piece = move.getPiece();
        Square from = move.getFrom();
        Square to = move.getTo();

        Board.setPiece(from, null);
        Board.setPiece(to, piece);
    }

    private bool IsValidMove(Move move, SideColor personColor) {
        Piece piece = move.getPiece();
        Square from = move.getFrom();
        Square to = move.getTo();

        if (piece.Color != personColor)
            return false;

        Piece? fromPiece = Board.getPiece(from);
        Piece? toPiece = Board.getPiece(to);
        
        if (fromPiece is null)
            return false;
        
        if (!piece.IsValidMove(from, to))
            return false;

        if (toPiece is null || toPiece.Color != personColor)
            return true;

        return false;
    }
}

public class MoveNotAllowedException : Exception {
    public MoveNotAllowedException(string message) : base(message) { }
    public MoveNotAllowedException(string message, Exception inner) : base(message, inner) { }
}