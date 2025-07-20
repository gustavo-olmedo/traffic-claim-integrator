public class ClaimTrafficCorrelation
{
  public Guid id { get; set; } = Guid.NewGuid();

  public Guid ClaimId { get; set; }
  public InsuranceClaim Claim { get; set; } = null!;

  public Guid TrafficEventId { get; set; }
  public TrafficEvent TrafficEvent { get; set; } = null!;

  public float MatchScore { get; set; } = 1.0f;

  public DateTime CorrelationTime { get; set; } = DateTime.UtcNow;
}