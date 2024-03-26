namespace Web.Models.Chess {
    public class Square {
        protected char X { get; }
        public char Y { get; }
        
        public string Location { get; set; }

        public static bool hasPiece(Chess board) {
            throw new NotImplementedException();
        }

        public static Piece getPiece(Chess board) {
            throw new NotImplementedException();
        }

        public static Piece setPiece(Chess board) {
            throw new NotImplementedException();
        }
    }
}
