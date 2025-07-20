public class CreateClaimRequest
{
  public string IntersectionId { get; set; } = null!;
  public DateTime IncidentTime { get; set; }
  public string DriverName { get; set; } = null!;
  public string ClaimDetails { get; set; } = null!;
}