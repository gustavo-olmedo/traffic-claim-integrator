using System.ComponentModel.DataAnnotations;

public class InsuranceClaim
{
  public Guid Id { get; set; } = Guid.NewGuid();

  [Required]
  public string IntersectionId { get; set; } = null!;

  public DateTime IncidentTime { get; set; }

  public string DriverName { get; set; } = null!;

  public string ClaimDetails { get; set; } = null!;

  public ICollection<ClaimTrafficCorrelation> Correlations { get; set; } = new List<ClaimTrafficCorrelation>();

}