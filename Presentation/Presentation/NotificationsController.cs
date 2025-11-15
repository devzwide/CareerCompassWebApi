using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Core.Entities;
using WebAPI.Infrastructure.Data;

namespace WebAPI.Presentation.Presentation;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public NotificationsController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyNotifications()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var notifications = await _db.Set<Notification>()
            .Where(n => n.UserId == userId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
        return Ok(notifications);
    }
}
