using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
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
}