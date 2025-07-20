using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TrafficEventsController : ControllerBase
{
  private readonly ApplicationDbContext _db;

  public TrafficEventsController(ApplicationDbContext db)
  {
    _db = db;
  }

  [HttpPost]
  public async Task<IActionResult> IngestTrafficEvent([FromBody] TrafficEventRequest request)
  {
    var trafficEvent = new TrafficEvent
    {
      IntersectionId = request.IntersectionId,
      EventTime = request.EventTime,
      Status = request.Status,
      RawData = request.RawData
    };

    _db.TrafficEvents.Add(trafficEvent);
    await _db.SaveChangesAsync();

    return Ok(new { trafficEvent.Id });
  }
}