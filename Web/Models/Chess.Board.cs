namespace Web.Models.Chess;
internal class Board {
    private char[][] _board { get; set; }

    internal Board(string boardString) {
        if (boardString.Length != 8 * 8)
            throw new ArgumentException("board must be exactly 64 characters");

        _board = boardString.Chunk(8).ToArray();
    }

    internal void setPiece(Square square, Piece? piece) {
        (int x, int y) = (square.X - 'a', square.Y - '1');

        char pieceChar = piece == null ? ' ' : piece.ToCode();

        _board[x][y] = pieceChar;
    }

    internal Piece? getPiece(Square square) {
        (int x, int y) = (square.X - 'a', square.Y - '1');

        char pieceChar = _board[x][y];

        if (pieceChar == ' ')
            return null;

        return new Piece(pieceChar);
    }

    public override string ToString() {
        return string.Join("", _board.ToList());
    }
}

public class ParseBoardException : Exception {
    public ParseBoardException(string message) : base(message) { }
    public ParseBoardException(string message, Exception inner) : base(message, inner) { }
}
