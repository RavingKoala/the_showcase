using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestParam {
    public class JoinLobby {
        [Required]
        public int LobbyId { get; set; }

        [Required]
        public string UserToken { get; set; }
    }
}
