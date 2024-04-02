namespace Web.Models.Chess;
public enum SideColor {
    White,
    Black
}

public class ChessGame {
    public const string StartBoard = "        pppppppp                                PPPPPPPP        ";

    private Board _board;

    public ChessGame() {
        _board = new Board(StartBoard);
    }

    public ChessGame(string board) {
        _board = new Board(board);
    }

    public void DoMove(string move, SideColor personColor) {
        Move newMove = new Move(move);

        DoMove(newMove, personColor);
    }

    public bool IsValidMove(string move, SideColor personColor) {
        Move newMove = new Move(move);

        return IsValidMove(newMove, personColor);
    }

    private void DoMove(Move move, SideColor personColor) {
        if (!IsValidMove(move, personColor))
            throw new MoveNotAllowedException($"Move {move} is not possible!");
        
        Piece piece = move.getPiece();
        Square from = move.getFrom();
        Square to = move.getTo();

        _board.setPiece(from, null);
        _board.setPiece(to, piece);
    }

    private bool IsValidMove(Move move, SideColor personColor) {
        Piece piece = move.getPiece();
        Square from = move.getFrom();
        Square to = move.getTo();

        if (piece.Color != personColor)
            return false;

        Piece? fromPiece = _board.getPiece(from);
        Piece? toPiece = _board.getPiece(to);
        
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