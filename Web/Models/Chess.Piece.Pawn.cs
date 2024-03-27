namespace Web.Models.Chess.Pieces {

    public class Pawn : IPiece {
        private const char _whiteCode = 'p';
        private const char _blackCode = 'P';
        string IPiece.Name { get; set; }
        SideColor IPiece.Color { get; set; }
        PieceTypes IPiece.Type { get; set; }

        public Pawn(SideColor color) {

        }


        public char ToCode() {
            return 
        }
    }

}