using System.ComponentModel.DataAnnotations;
using Web.Models.Chess;

namespace Web.Models {

    public class Game {
        [Required]
        public int Id { get; set; }
        [Required]
        public int LobbyId { get; set; }
        public Lobby Lobby { get; set; }
        public string Turn { get; set; }
        public string Board { get => _board.FromString(); set; }
        private Board _board { get; set; }

        public const string StartBoard = "        pppppppp                                PPPPPPPP        ";
    }

}