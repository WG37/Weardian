using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Weardian.Server.Application.DTOs.RequestDtos;
using Weardian.Server.Application.DTOs.ResponseDtos;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/keys")]
    public class SymmetricKeyController : ControllerBase
    {
        private readonly ISymmetricKeyService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public SymmetricKeyController(ISymmetricKeyService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet("{envelopeId:guid}")]
        public async Task<ActionResult<SymmetricKeyResponseDto>> GetKeyById(Guid envelopeId)
        {
            try
            {
                var userId = _userManager.GetUserId(User)!;
                if (userId == null)
                    return Unauthorized();

                var keyRecord = await _service.GetKeyById(userId, envelopeId);
                return Ok(keyRecord);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet()]
        public async Task<ActionResult<List<SymmetricKeyResponseDto>>> GetAllKeys()
        {
        
            var userId = _userManager.GetUserId(User)!;
            if (userId == null)
                return Unauthorized();

            var keyRecords = await _service.GetKeys(userId);
            return Ok(keyRecords);
        
        }

        [HttpPost]
        public async Task<ActionResult<SymmetricKeyResponseDto>> CreateSymmetricKey([FromBody] CreateSymmetricKeyRequestDto req)
        {
            try
            {
                var userId = _userManager.GetUserId(User)!;
                if (userId == null)
                    return Unauthorized();

                var keyRecord = await _service.CreateKey(req, userId);
                return CreatedAtAction(nameof(GetKeyById), new { publicId = keyRecord.EnvelopeId }, keyRecord);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{envelopeId:guid}")]
        public async Task<ActionResult> RemoveSymmetricKey(Guid envelopeId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (userId == null)
                return Unauthorized();

            var deleted = await _service.RemoveKeyById(userId, envelopeId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
