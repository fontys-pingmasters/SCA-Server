using Business.Entities;
using Business.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<CompetitionMembership> CompetitionMemberships { get; set; }
    public DbSet<TeamMembership> TeamMemberships { get; set; }
    public DbSet<CompetitionMatch> CompetitionMatches { get; set; }
    public DbSet<EloHistory> EloHistories { get; set; }
    public DbSet<MatchRequest> MatchRequests { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.EloHistories)
            .WithOne(eh => eh.User)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.TeamMemberships)
            .WithOne(tm => tm.User)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.MatchesAsPlayer1)
            .WithOne(m => m.Player1)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasMany(u => u.MatchesAsPlayer2)
            .WithOne(m => m.Player2)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasMany(u => u.MatchesAsOpponent1)
            .WithOne(m => m.Opponent1)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.MatchesAsOpponent2)
            .WithOne(m => m.Opponent2)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TeamMembership>()
            .HasOne(tm => tm.User)
            .WithMany(u => u.TeamMemberships)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TeamMembership>()
            .HasOne(tm => tm.Team)
            .WithMany(t => t.TeamMemberships)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Team>()
            .HasOne(t => t.Club)
            .WithMany(c => c.Teams)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Team>()
            .HasMany(t => t.TeamMemberships)
            .WithOne(tm => tm.Team)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Team>()
            .HasMany(t => t.CompetitionMemberships)
            .WithOne(cm => cm.Team)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MatchRequest>()
            .HasOne(mr => mr.Sender)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MatchRequest>()
            .HasOne(mr => mr.Receiver)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MatchRequest>()
            .HasOne(mr => mr.Match)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        // Player1 matches
        modelBuilder.Entity<Match>()
            .HasOne(m => m.Player1)
            .WithMany(u => u.MatchesAsPlayer1)
            .HasForeignKey("Player1Id") // Using shadow property for FK
            .OnDelete(DeleteBehavior.Restrict); // Avoids cascading deletes

        // Player2 matches
        modelBuilder.Entity<Match>()
            .HasOne(m => m.Player2)
            .WithMany(u => u.MatchesAsPlayer2)
            .HasForeignKey("Player2Id")
            .OnDelete(DeleteBehavior.Restrict);

        // Opponent1 matches
        modelBuilder.Entity<Match>()
            .HasOne(m => m.Opponent1)
            .WithMany(u => u.MatchesAsOpponent1)
            .HasForeignKey("Opponent1Id")
            .OnDelete(DeleteBehavior.Restrict);

        // Opponent2 matches
        modelBuilder.Entity<Match>()
            .HasOne(m => m.Opponent2)
            .WithMany(u => u.MatchesAsOpponent2)
            .HasForeignKey("Opponent2Id")
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Match>()
            .HasMany(m => m.MatchRequests)
            .WithOne(mr => mr.Match)
            .OnDelete(DeleteBehavior.Cascade);

        // Define EloHistory relationships
        modelBuilder.Entity<EloHistory>()
            .HasOne(eh => eh.User)
            .WithMany(u => u.EloHistories)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EloHistory>()
            .HasOne(eh => eh.Match)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        // Define CompetitionMembership relationships
        modelBuilder.Entity<CompetitionMembership>()
            .HasOne(cm => cm.Team)
            .WithMany(t => t.CompetitionMemberships)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CompetitionMembership>()
            .HasOne(cm => cm.Competition)
            .WithMany(c => c.CompetitionMemberships)
            .OnDelete(DeleteBehavior.Cascade);

        // Define CompetitionMatch relationships
        modelBuilder.Entity<CompetitionMatch>()
            .HasOne(cm => cm.Match)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CompetitionMatch>()
            .HasOne(cm => cm.Competition)
            .WithMany(c => c.CompetitionMatches)
            .OnDelete(DeleteBehavior.Cascade);

        // Enum storage conversion for RequestStatus
        modelBuilder.Entity<MatchRequest>()
            .Property(mr => mr.Status)
            .HasConversion(new EnumToStringConverter<RequestStatus>());
        
    }

}