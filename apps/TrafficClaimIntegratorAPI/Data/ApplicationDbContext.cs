using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

  public DbSet<TrafficEvent> TrafficEvents => Set<TrafficEvent>();
  public DbSet<InsuranceClaim> InsuranceClaims => Set<InsuranceClaim>();
  public DbSet<ClaimTrafficCorrelation> ClaimTrafficCorrelations => Set<ClaimTrafficCorrelation>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<ClaimTrafficCorrelation>()
      .HasOne(c => c.Claim)
      .WithMany(c => c.Correlations)
      .HasForeignKey(c => c.ClaimId);

    modelBuilder.Entity<ClaimTrafficCorrelation>()
      .HasOne(c => c.TrafficEvent)
      .WithMany(t => t.Correlations)
      .HasForeignKey(c => c.TrafficEventId);
  }
}