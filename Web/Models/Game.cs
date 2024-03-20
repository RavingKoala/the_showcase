using System.ComponentModel.DataAnnotations;

namespace Web.Models {

    public class Game {
        [Required]
        public int Id { get; set; }
        [Required]
        public int LobbyId { get; set; }
        public Lobby Lobby { get; set; }
        public string Turn { get; set; }
        public string BoardString { get; set; }

        public const string StartBoard = "        pppppppp                                PPPPPPPP        ";
    }

}