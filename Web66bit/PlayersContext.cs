using Microsoft.EntityFrameworkCore;

public class PlayersContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<Player> players { get; set; }
    public PlayersContext(DbContextOptions<PlayersContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("football_players");
    }
}