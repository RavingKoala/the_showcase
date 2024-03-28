using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models;

public class Lobby {
    [Key]
    [Required]
    [HiddenInput]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Player1Id { get; set; } // host
    public string? Player2Id { get; set; }
}