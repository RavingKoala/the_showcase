namespace Web.Models.Chess;
internal class Square {
    internal char X { get; private set; }
    internal char Y { get; private set; }

    internal Square(string location) {
        if (!IsValidLocation(location))
            throw new InvalidFormatException($"Value: {location} is not an available square. The valid squares range from columns a-z and rows 1-8.");

        (X, Y) = (location[0], location[1]);
    }

    internal static bool IsValidLocation(string location) {
        if (location.Length != 2) return false;
        return location[0] >= 'a' && location[0] <= 'h'
            && location[0] >= '1' && location[0] <= '8';
    }

    public override string ToString() {
        return $"{X}{Y}";
    }
}

public class InvalidFormatException : Exception {
    public InvalidFormatException(string message) : base(message) { }
    public InvalidFormatException(String message, Exception innerException) : base(message, innerException) { }
}