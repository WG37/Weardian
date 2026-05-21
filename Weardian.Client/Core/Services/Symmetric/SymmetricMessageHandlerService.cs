using System.Text.Json;
using Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleDecryptionDtos;
using Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleEncryptionDtos;
using Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleRetrievalDtos;
using Weardian.Client.Core.DTOs.WebViewDtos;
using Weardian.Client.Core.Interfaces.Symmetric;
using Weardian.Client.Core.Serialization;

namespace Weardian.Client.Core.Services.Symmetric
{
    public class SymmetricMessageHandlerService : ISymmetricMessageHandlerService
    {
        private readonly IKeyManagementService _keyManagementService;
        private readonly IPayloadService _payloadService;
      
        public SymmetricMessageHandlerService(
            IKeyManagementService keyManagementService,
            IPayloadService payloadService)
        {
            _keyManagementService = keyManagementService;
            _payloadService = payloadService;
        }
        
        public async Task<string> HandleAsync(string request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request))
                    throw new ArgumentException("Request type cannot be null or empty", nameof(request));

                var requestType = JsonSerializer.Deserialize<WebViewRequestDto>(request, 
                    JsonSerializeCaseHelper.CaseInsensitiveOptions)
                    ?? throw new InvalidOperationException("Failed to deserialized request type");

                return requestType.Type switch
                {
                    "encryption" => await HandleEncryptionRequestAsync(request),
                    "decryption" => await HandleDecryptionRequestAsync(request),
                    "retrieveAllKeys" => await HandleRetrieveAllKeysRequestAsync(),
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
                        Error: ex.Message
                        ), 
                    JsonSerializeCaseHelper.CamelCaseOptions);
            }
        }

        public async Task<string> HandleEncryptionRequestAsync(string request)
        {
            if (string.IsNullOrWhiteSpace(request))
                throw new ArgumentException("Invalid request: cannot be null, empty or whitespace", nameof(request));

            var dto = JsonSerializer.Deserialize<EncryptionRequestDto>(request, 
                JsonSerializeCaseHelper.CaseInsensitiveOptions)
                ?? throw new InvalidOperationException("Deserialization Failed: result cannot be null");

            var encryptionResponse = await _keyManagementService
                .CreateEncryptedPasswordAsync(
                    dto.KeyName, 
                    dto.Password, 
                    dto.CreateSynced);

            return JsonSerializer.Serialize(
                new WebViewResponseDto<EncryptionResponseDto>(
                    Type: "encryption",
                    Success: true,
                    Data: encryptionResponse,
                    Error: null
                    ), 
                JsonSerializeCaseHelper.CamelCaseOptions);
        }

        public async Task<string> HandleDecryptionRequestAsync(string request)
        {
            if (string.IsNullOrWhiteSpace(request))
                throw new ArgumentException("Invalid request: cannot be null, empty or whitespace", nameof(request));

            var dto = JsonSerializer.Deserialize<DecryptionRequestDto>(request, 
                JsonSerializeCaseHelper.CaseInsensitiveOptions);

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
                    ), 
                JsonSerializeCaseHelper.CamelCaseOptions);
        }

        public async Task<string> HandleRetrieveAllKeysRequestAsync()
        {

            var keysResult = await _payloadService.GetPayloadRecordsAsync();

            return JsonSerializer.Serialize(
                new WebViewResponseDto<IReadOnlyList<RetrieveKeyResponseDto>>(
                    Type: "retrieveAllKeys",
                    Success: true,
                    Data: keysResult,
                    Error: null
                    ),
                JsonSerializeCaseHelper.CamelCaseOptions); 
        }
    }
}
