using System.ComponentModel.DataAnnotations;

namespace Api.Model.HttpParam {
    public class GameTurn {
        [Required]
        public int LobbyID { get; set; }
        [Required]
        public string UserToken { get; set; }
        [Required]
        public string Move { get; set; }
    }
}
