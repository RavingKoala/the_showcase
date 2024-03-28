namespace Web.Models.Chess;
public class Square {
    internal char _X { get; private set; }
    internal char _Y { get; private set; }


    public Square(string location) {
        if (!IsValidLocation(location))
            throw new InvalidFormatException($"Value: {location} is not an available square. The valid squares range from columns a-z and rows 1-8.");

        (_X, _Y) = (location[0], location[1]);
    }

    public static bool IsValidLocation(string location) {
        if (location.Length != 2) return false;
        return location[0] >= 'a' && location[0] <= 'h'
            && location[0] >= '1' && location[0] <= '8';
    }

    public override string ToString() {
        return $"{_X}{_Y}";
    }
}

public class InvalidFormatException : Exception {
    public InvalidFormatException(string message) : base(message) { }
    public InvalidFormatException(String message, Exception innerException) : base(message, innerException) { }
}