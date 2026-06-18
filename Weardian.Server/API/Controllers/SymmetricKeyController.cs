using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.RequestDtos.Symmetric;
using Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.ResponseDtos.Symmetric;
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
        public async Task<ActionResult<EncryptedEnvelopeSyncResponseDto>> GetKeyById(Guid envelopeId)
        {  
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            var envelope = await _service.GetEncryptedEnvelopeById(userId, envelopeId);

            if (!envelope.Success)
                return NotFound();

            return Ok(envelope);
        }

        [HttpGet()]
        public async Task<ActionResult<IReadOnlyList<EncryptedEnvelopeSyncResponseDto>>> GetAllKeys()
        { 
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            var envelopes = await _service.GetEncryptedEnvelopes(userId);
            return Ok(envelopes);
        }

        [HttpPost]
        public async Task<ActionResult<EncryptedEnvelopeSyncResponseDto>> CreateSymmetricKey([FromBody] EncryptedEnvelopeSyncRequestDto req)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            var envelope = await _service.CreateEncryptedEnvelope(req, userId);

            if (!envelope.Success)
                return BadRequest(envelope);

            return CreatedAtAction(nameof(GetKeyById), new { envelopeId = envelope.EnvelopeId }, envelope);
        }

        [HttpDelete("{envelopeId:guid}")]
        public async Task<ActionResult<EncryptedEnvelopeSyncResponseDto>> RemoveSymmetricKey(Guid envelopeId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            var deleted = await _service.RemoveEncryptedEnvelopeById(userId, envelopeId);

            return Ok(deleted);
        }
    }
}
