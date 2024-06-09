namespace ShowcaseTests;

using System.Collections.Generic;
using Web.Models.Chess;

internal class MockPiece : IPieceType {
    char IPieceType.GetCode() => 'x';

    string IPieceType.GetName() => "MockPiece";

    IEnumerable<Square> IPieceType.ValidMoves(Square from, SideColor color) {
        int respectiveDirection = color == SideColor.White ? 1 : -1;

        string fromCode = from.ToString();
        (char, char) pos = (fromCode[0], fromCode[1]);
        string newLocation = $"{pos.Item1 + pos.Item1}{pos.Item2 + pos.Item2}";

        List<Square> returnSquares = new List<Square>();

        if (Square.IsValidLocation(newLocation))
            returnSquares.Add(new Square(newLocation));

        return returnSquares;
    }
}

public class ChessTests {
    internal ChessGame _ChessGameFixture;

    [SetUp]
    public void Setup() {
        _ChessGameFixture = new ChessGame("        xxxxxxxx                                XXXXXXXX        ");
    }

    [TestCase("a1")]
    [TestCase("b1")]
    [TestCase("c1")]
    [TestCase("d1")]
    [TestCase("e1")]
    [TestCase("f1")]
    [TestCase("g1")]
    [TestCase("h1")]
    [TestCase("a2")]
    [TestCase("b2")]
    [TestCase("c2")]
    [TestCase("d2")]
    [TestCase("e2")]
    [TestCase("f2")]
    [TestCase("g2")]
    [TestCase("h2")]
    [TestCase("a3")]
    [TestCase("b3")]
    [TestCase("c3")]
    [TestCase("d3")]
    [TestCase("e3")]
    [TestCase("f3")]
    [TestCase("g3")]
    [TestCase("h3")]
    [TestCase("a4")]
    [TestCase("b4")]
    [TestCase("c4")]
    [TestCase("d4")]
    [TestCase("e4")]
    [TestCase("f4")]
    [TestCase("g4")]
    [TestCase("h4")]
    [TestCase("a5")]
    [TestCase("b5")]
    [TestCase("c5")]
    [TestCase("d5")]
    [TestCase("e5")]
    [TestCase("f5")]
    [TestCase("g5")]
    [TestCase("h5")]
    [TestCase("a6")]
    [TestCase("b6")]
    [TestCase("c6")]
    [TestCase("d6")]
    [TestCase("e6")]
    [TestCase("f6")]
    [TestCase("g6")]
    [TestCase("h6")]
    [TestCase("a7")]
    [TestCase("b7")]
    [TestCase("c7")]
    [TestCase("d7")]
    [TestCase("e7")]
    [TestCase("f7")]
    [TestCase("g7")]
    [TestCase("h7")]
    [TestCase("a8")]
    [TestCase("b8")]
    [TestCase("c8")]
    [TestCase("d8")]
    [TestCase("e8")]
    [TestCase("f8")]
    [TestCase("g8")]
    [TestCase("h8")]
    public void Square_ValidPosition_true(string pos) {
        Assert.True(Square.IsValidLocation(pos));
    }

    [TestCase("a0")]
    [TestCase("b0")]
    [TestCase("c0")]
    [TestCase("d0")]
    [TestCase("e0")]
    [TestCase("f0")]
    [TestCase("g0")]
    [TestCase("h0")]
    [TestCase("`1")]
    [TestCase("i1")]
    [TestCase("`2")]
    [TestCase("i2")]
    [TestCase("`3")]
    [TestCase("i3")]
    [TestCase("`4")]
    [TestCase("i4")]
    [TestCase("`5")]
    [TestCase("i5")]
    [TestCase("`6")]
    [TestCase("i6")]
    [TestCase("`7")]
    [TestCase("i7")]
    [TestCase("`7")]
    [TestCase("i7")]
    [TestCase("`8")]
    [TestCase("i8")]
    [TestCase("a9")]
    [TestCase("b9")]
    [TestCase("c9")]
    [TestCase("d9")]
    [TestCase("e9")]
    [TestCase("f9")]
    [TestCase("g9")]
    [TestCase("h9")]
    public void Square_ValidPosition_false(string pos) {
        Assert.False(Square.IsValidLocation(pos));
    }

    [Test]
    public void Piece_IsValidMove_Mock_white() {
        Piece mockPiece = new Piece(new MockPiece(), SideColor.White);

        Assert.True(mockPiece.IsValidMove(new Square("c7"), new Square("c8")), "c7 should be able to move to c8. But it could not.");

        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("b8")), "c7 should not be able to move to b8. But it could.");
        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("d8")), "c7 should not be able to move to d8. But it could.");
        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("b7")), "c7 should not be able to move to b7. But it could.");
        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("d7")), "c7 should not be able to move to d7. But it could.");
        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("b6")), "c7 should not be able to move to b6. But it could.");
        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("c6")), "c7 should not be able to move to c6. But it could.");
        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("d6")), "c7 should not be able to move to d6. But it could.");
    }


    [Test]
    public void Piece_IsValidMove_Mock_black() {
        Piece mockPiece = new Piece(new MockPiece(), SideColor.Black);

        Assert.True(mockPiece.IsValidMove(new Square("c7"), new Square("c6")), "c7 should be able to move to c6. But it could not.");

        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("b8")), "c7 should not be able to move to b8. But it could.");
        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("c8")), "c7 should not be able to move to c8. But it could.");
        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("d8")), "c7 should not be able to move to d8. But it could.");
        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("b7")), "c7 should not be able to move to b7. But it could.");
        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("d7")), "c7 should not be able to move to d7. But it could.");
        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("b6")), "c7 should not be able to move to b6. But it could.");
        Assert.False(mockPiece.IsValidMove(new Square("c7"), new Square("d6")), "c7 should not be able to move to d6. But it could.");
    }
}