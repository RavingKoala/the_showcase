namespace Web.Models.Chess {
    public class Piece  {
        public enum Color {
            White,
            Black
        }
        public enum Type {
            Pawn
        };

        private static IEnumerable<((Type, Color), string)> _typesDict = new List<((Type, Color), string)> {
            ((Type.Pawn,Color.White), "P"),

            ((Type.Pawn,Color.Black), "p")
        };

        private Color _color { get; set; }
        private Type _type { get; set; }

        public string PieceCode { 
            get => Piece.TypesToString(_type, _color);
            set => (_type, _color) = Piece.StringToTypes(value);
        }

        public Color getColor() { return _color; }
        public Type getType() { return _type; }

        public static string TypesToString(Type type, Color color) {
            Func<((Type, Color), string), bool> predicateCompareParams = item => item.Item1.Item1 == type && item.Item1.Item2 == color;

            if (_typesDict.Any(predicateCompareParams))
                return _typesDict.FirstOrDefault(predicateCompareParams).Item2;

            throw new InvalidBoardPieceException("Piece could not be identified!");
        }

        public static (Type, Color) StringToTypes(string strType) {
            Func<((Type, Color), string), bool> predicateCompareParams = item => item.Item2 == strType;

            if (_typesDict.Any(predicateCompareParams))
                return _typesDict.FirstOrDefault(predicateCompareParams).Item1;

            throw new InvalidBoardPieceException("Piece could not be identified!");
        }
    }

    public class InvalidBoardPieceException : Exception {
        public InvalidBoardPieceException(string message) : base(message) {}
        public InvalidBoardPieceException(string message, Exception inner) : base(message, inner) {}
    }
}
