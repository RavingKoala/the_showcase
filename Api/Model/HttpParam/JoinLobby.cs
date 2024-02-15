using System.ComponentModel.DataAnnotations;

namespace Api.Model.HttpParam {
    public class JoinLobby {
        [Required]
        public int LobbyId { get; set; }

        [Required]
        public string UserToken { get; set; }
    }
}
