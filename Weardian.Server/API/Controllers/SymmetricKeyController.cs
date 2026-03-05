using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Weardian.Server.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SymmetricKeyController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetKeyById(Guid publicId)
        {
            if (publicId == Guid.Empty)
                return NotFound();

        } 
    }
}
