using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data;
public class ApplicationDbContext : IdentityDbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {
        this.Database.Migrate();
    }
    public DbSet<Lobby> Lobby { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // todo: delete
        => optionsBuilder.LogTo(Console.WriteLine);
    }