using System.ComponentModel.DataAnnotations;
using Web.Models.Chess;

namespace Web.Models;

public class Game {

    private ChessGame _chessGame { get; set; }
    private Lobby _lobby { get; set; }

    [Required]
    public int Id { get; set; }
    [Required]
    public int LobbyId { get; set; }
    public string Turn { get; set; } // color string
    public string board { get => _chessGame.Board.ToString(); set => new ChessGame(value); }

}