namespace Api.Model;
public class Lobby {
    public int LobbyId { get; set; }
    public string UserToken { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Turn { get; set; }
}
