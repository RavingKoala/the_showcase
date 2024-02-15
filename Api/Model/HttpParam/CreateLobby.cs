using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Model.HttpParam {
    public class CreateLobby {
        
        [Required]
        public string UserToken { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
        [DefaultValue(true)]
        public bool PreferWhite { get; set; }

    }
}
