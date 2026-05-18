using System.Text.Json;
using Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleDecryptionDtos;
using Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleEncryptionDtos;
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

        public async Task<string> HandleAsync(string request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request))
                    throw new ArgumentException("Request type cannot be null or empty", nameof(request));

                var requestType = JsonSerializer.Deserialize<WebViewRequestDto>(request)
                    ?? throw new InvalidOperationException("Failed to deserialized request type");

                return requestType.Type switch
                {
                    "encryption" => await HandleEncryptionRequestAsync(request),
                    "decryption" => await HandleDecryptionRequestAsync(request),
                    _ => throw new InvalidOperationException("Invalid request type")
                };
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(
                    new WebViewResponseDto<object>(
                        Type: "Error",
                        Success: false,
                        Data: null,
                        Error: ex.Message));
            }
        }

        public async Task<string> HandleEncryptionRequestAsync(string request)
        {
            if (string.IsNullOrWhiteSpace(request))
                throw new ArgumentException("Invalid request: cannot be null, empty or whitespace", nameof(request));

            var dto = JsonSerializer.Deserialize<EncryptionRequestDto>(request)
                ?? throw new InvalidOperationException("Deserialization Failed: result cannot be null");

            var encryptionResult = await _keyManagementService
                .CreateEncryptedPasswordAsync(
                    dto.KeyName, 
                    dto.Password, 
                    dto.CreateSynced);

            return JsonSerializer.Serialize(
                new WebViewResponseDto<EncryptionResponseDto>(
                    Type: "encryption",
                    Success: true,
                    Data: encryptionResult,
                    Error: null
                ));
        }

        public async Task<string> HandleDecryptionRequestAsync(string request)
        {
            if (string.IsNullOrWhiteSpace(request))
                throw new ArgumentException("Invalid request: cannot be null, empty or whitespace", nameof(request));

            var dto = JsonSerializer.Deserialize<DecryptionRequestDto>(request);

            if (dto == null || dto.KeyId == Guid.Empty)
                throw new InvalidOperationException("Deserialization Failed: null result or Guid is invalid.");

            var decryptionResult = await _keyManagementService
                .RetrieveDecryptedPasswordAsync(dto.KeyId);

            return JsonSerializer.Serialize(
                new WebViewResponseDto<string>(
                    Type: "decryption",
                    Success: true,
                    Data: decryptionResult,
                    Error: null
                    ));
        }
    }
}
