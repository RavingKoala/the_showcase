using Web.Models.Board;

namespace Web.Models.ViewModels {
    public class GameMove {
        private Move _move {  get; set; }

        public string UserId { get; set; }
        public string Move { get => _move.ToString(); set => _move.FromString(value); }
    }
}
