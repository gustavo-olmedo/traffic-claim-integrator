public class TrafficPollerJob
{
  private readonly ApplicationDbContext _db;
  private readonly IHttpClientFactory _httpClientFactory;

  public TrafficPollerJob(ApplicationDbContext db, IHttpClientFactory httpClientFactory)
  {
    _db = db;
    _httpClientFactory = httpClientFactory;
  }

  public async Task ExecuteAsync()
  {
    var client = _httpClientFactory.CreateClient("TrafficApi");
    // TODO: add real api endpoint
    var response = await client.GetAsync("https://traffic.vendor.com/api/events");
    response.EnsureSuccessStatusCode();

    var events = await response.Content.ReadFromJsonAsync<List<TrafficEventRequest>>();

    if (events == null || events.Count == 0)
    {
      Console.WriteLine("No traffic events received or JSON deserialization failed.");
      return;
    }

    foreach (var e in events)
    {
      var ev = new TrafficEvent
      {
        IntersectionId = e.IntersectionId,
        EventTime = e.EventTime,
        Status = e.Status,
        RawData = e.RawData
      };

      _db.TrafficEvents.Add(ev);
    }
    await _db.SaveChangesAsync();
  }
}