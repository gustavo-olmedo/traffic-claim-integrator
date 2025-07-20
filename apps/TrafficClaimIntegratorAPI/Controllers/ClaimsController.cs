using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClaimsController : ControllerBase
{
  private readonly ApplicationDbContext _db;

  public ClaimsController(ApplicationDbContext db)
  {
    _db = db;
  }

  [HttpPost]
  public async Task<IActionResult> CreateClaim([FromBody] CreateClaimRequest request)
  {
    var claim = new InsuranceClaim
    {
      IntersectionId = request.IntersectionId,
      IncidentTime = request.IncidentTime,
      DriverName = request.DriverName,
      ClaimDetails = request.ClaimDetails,
    };

    _db.InsuranceClaims.Add(claim);
    await _db.SaveChangesAsync();

    return CreatedAtAction(nameof(GetClaim), new { id = claim.Id }, claim);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetClaim(Guid id)
  {
    var claim = await _db.InsuranceClaims.FindAsync(id);
    if (claim == null) return NotFound();
    return Ok(claim);
  }
}