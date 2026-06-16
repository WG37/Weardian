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
    [Route("api/keys/symmetric")]
    public class SymmetricKeyController : ControllerBase
    {
        private readonly ISymmetricEnvelopeService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public SymmetricKeyController(
            ISymmetricEnvelopeService service, 
            UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet("{envelopeId:guid}")]
        public async Task<ActionResult<KeySyncResponseDto>> GetKeyById(Guid envelopeId)
        {
            try
            {
                var userId = _userManager.GetUserId(User)!;
                if (userId == null)
                    return Unauthorized();

                var envelope = await _service.GetEncryptedEnvelopeById(userId, envelopeId);
                return Ok(envelope);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet()]
        public async Task<ActionResult<IReadOnlyList<KeySyncResponseDto>>> GetAllKeys()
        {
        
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            var envelopes = await _service.GetEncryptedEnvelopes(userId);
            return Ok(envelopes);
        }

        [HttpPost]
        public async Task<ActionResult<KeySyncResponseDto>> CreateSymmetricKey([FromBody] KeyRecordRequestDto req)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                    return Unauthorized();

                var envelope = await _service.CreateEncryptedEnvelope(req, userId);
                return CreatedAtAction(nameof(GetKeyById), new { envelopeId = envelope.EnvelopeId }, envelope);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{envelopeId:guid}")]
        public async Task<ActionResult> RemoveSymmetricKey(Guid envelopeId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            var deleted = await _service.RemoveEncryptedEnvelopeById(userId, envelopeId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
