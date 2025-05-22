using Microsoft.AspNetCore.Mvc;

namespace Kustomer.API.Controllers;

#region HealthController
[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    #region GetHealth
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
    #endregion
}
#endregion