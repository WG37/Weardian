using Microsoft.AspNetCore.Mvc;
using Weardian.Server.Application.DTOs.RequestDtos;
using Weardian.Server.Application.DTOs.ResponseDtos;
using Weardian.Server.Application.Interfaces;

namespace Weardian.Server.API.Controllers
{
    [ApiController]
    [Route("[controller]/keys")]
    public class SymmetricKeyController : ControllerBase
    {
        private readonly ISymmetricKeyService _service;

        public SymmetricKeyController(ISymmetricKeyService service)
        {
            _service = service;
        }

        [HttpGet("{publicId:guid}")]
        public async Task<ActionResult<SymmetricKeyResponseDto>> GetKeyById(Guid publicId)
        {
            try
            {    
                var key = await _service.GetKeyById(publicId);
                return Ok(key);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet()]
        public async Task<ActionResult<List<SymmetricKeyResponseDto>>> GetAllKeys()
        {
            try
            {
                var keys = await _service.GetKeys();
                return Ok(keys);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<SymmetricKeyResponseDto>> CreateSymmetricKey([FromBody] CreateSymmetricKeyRequestDto req)
        {
            try
            {
                var key = await _service.CreateKey(req);
                return CreatedAtAction(nameof(GetKeyById), new { publicId = key.PublicId }, key);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{publicId:guid}")]
        public async Task<ActionResult> RemoveSymmetricKey(Guid publicId)
        {
            var deleted = await _service.RemoveKeyById(publicId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
