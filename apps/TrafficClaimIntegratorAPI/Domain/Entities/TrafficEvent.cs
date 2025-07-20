using System.ComponentModel.DataAnnotations;

public class TrafficEvent
{
  public Guid Id { get; set; } = Guid.NewGuid();

  [Required]
  public string IntersectionId { get; set; } = null!;

  public DateTime EventTime { get; set; }

  [Required]
  public TrafficLightStatus Status { get; set; }

  public string? RawData { get; set; }

  public ICollection<ClaimTrafficCorrelation> Correlations { get; set; } = new List<ClaimTrafficCorrelation>();
}

public enum TrafficLightStatus
{
  Red,
  Yellow,
  Green
}