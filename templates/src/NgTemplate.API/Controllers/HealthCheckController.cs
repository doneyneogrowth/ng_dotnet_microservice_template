using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NgTemplate.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [Produces("application/json")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            return await Task.FromResult(Ok("Success"));
        }
    }
}