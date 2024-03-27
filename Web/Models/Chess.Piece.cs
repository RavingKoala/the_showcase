namespace Web.Models.Chess {
    public interface IPiece {
        public string Name { get; protected set; }
        public SideColor Color { get; protected set; }
        public PieceTypes Type { get; protected set; }

        public char ToCode();
        public IEnumerable<Square> ValidMoves();
    }
}
