public class TrafficEventRequest
{
  public string IntersectionId { get; set; } = null!;
  public DateTime EventTime { get; set; }
  public TrafficLightStatus Status { get; set; }
  public string? RawData { get; set; }
}