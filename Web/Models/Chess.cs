namespace Web.Models.Chess;
public enum SideColor {
    White,
    Black
}
public enum PieceTypes {
    Pawn
}

public class ChessGame {
    public const string StartBoard = "        pppppppp                                PPPPPPPP        ";

    public Board Board { get; private set; }

    public ChessGame() {
        Board = new Board(StartBoard);
    }

    public ChessGame(string board) {
        Board = new Board(board);
    }

    public void doMove() {

    }


    public bool isValidMove() {

    }
}
