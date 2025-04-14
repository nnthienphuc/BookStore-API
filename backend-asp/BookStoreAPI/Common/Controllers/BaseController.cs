using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Common.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public abstract class BaseController : ControllerBase
{
    protected Guid GetUserId()
    {
        var userIdClaim = User.FindFirst("id");
        return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
    }
}
