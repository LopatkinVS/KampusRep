using Microsoft.AspNetCore.Mvc;

namespace Kampus.Api.Controllers.Abstract
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public abstract class BaseApiController : Controller 
    { }
}
