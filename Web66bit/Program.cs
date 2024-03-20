using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class Player
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Gender { get; set; }
    public string BirthDate { get; set; }
    public string TeamName { get; set; }
    public string Country { get; set; }

    public Player(string name, string surname, string gender, string birthDate, string teamName, string country)
    {
        this.Name = name;
        this.Surname = surname;
        this.Gender = gender;
        this.BirthDate = birthDate;
        this.TeamName = teamName;
        this.Country = country;
    }

    public Player()
    {
        
    }
}

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
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("football_players");
    }
}

class Program
{
    static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<PlayersContext>(
            options => options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}