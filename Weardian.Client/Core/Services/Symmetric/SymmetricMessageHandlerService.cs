using System.Text.Json;
using Weardian.Client.Core.DTOs.MessageHandlerDtos;
using Weardian.Client.Core.DTOs.WebViewDtos;
using Weardian.Client.Core.Interfaces.Symmetric;

namespace Weardian.Client.Core.Services.Symmetric
{
    public class SymmetricMessageHandlerService : ISymmetricMessageHandlerService
    {
        private readonly IKeyManagementService _keyManagementService;

        public SymmetricMessageHandlerService(
            IKeyManagementService keyManagementService)
        {
            _keyManagementService = keyManagementService;
        }

        public async Task<string> HandleEncryptionRequestAsync(string request)
        {
            if (string.IsNullOrWhiteSpace(request))
                throw new ArgumentException("Invalid request: cannot be null, empty or whitespace", nameof(request));

            var result = JsonSerializer.Deserialize<EncryptionRequestDto>(request)
                ?? throw new ArgumentNullException("The deserialized result cannot be null");

            var encryptionResult = await _keyManagementService
                .CreateEncryptedPasswordAsync(
                    result.KeyName, 
                    result.Password, 
                    result.CreateSynced);

            return JsonSerializer.Serialize(
                new WebViewResponseDto<EncryptionResultDto>(
                    Type: "encryptionResult",
                    Success: true,
                    Data: encryptionResult,
                    Error: null
                ));
        }
    }
}
