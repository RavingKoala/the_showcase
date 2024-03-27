using System.Linq;

namespace Web.Models.Chess {
    public class Board {
        private char[][] _board { get; set; }

        public Board(string boardString) {
            if (boardString.Length != 8 * 8)
                throw new ArgumentException("board must be exactly 64 characters");
            
            _board = boardString.Chunk(8).ToArray();
        }

        public override string ToString() {
            return string.Join("", _board.ToList());
        }
    }

    public class ParseBoardException : Exception {
        public ParseBoardException(string message) : base(message) { }
        public ParseBoardException(string message, Exception inner) : base(message, inner) { }
    }
}
