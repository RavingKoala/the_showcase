namespace Web.Models.Chess;
public class Square {
    private char _x { get; set; }
    private char _y { get; set; }


    public Square(string location) {
        if (!IsValidLocation(location))
            throw new InvalidFormatException($"Value: {location} is not an available square. The valid squares range from columns a-z and rows 1-8.");

        (_x, _y) = (location[0], location[1]);
    }

    public static bool IsValidLocation(string location) {
        if (location.Length != 2) return false;
        return location[0] >= 'a' && location[0] <= 'h'
            && location[0] >= '1' && location[0] <= '8';
    }

    public override string ToString() {
        return $"{_x}{_y}";
    }
}

public class InvalidFormatException : Exception {
    public InvalidFormatException(string message) : base(message) { }
    public InvalidFormatException(String message, Exception innerException) : base(message, innerException) { }
}