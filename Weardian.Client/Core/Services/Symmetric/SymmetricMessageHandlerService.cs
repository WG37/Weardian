using System.Text.Json;
using Weardian.Client.Core.DTOs.AuthDtos.Requests;
using Weardian.Client.Core.DTOs.AuthDtos.Responses;
using Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleDecryptionDtos;
using Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleDeleteDtos;
using Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleEncryptionDtos;
using Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleRetrievalDtos;
using Weardian.Client.Core.DTOs.WebViewDtos;
using Weardian.Client.Core.Interfaces.Auth;
using Weardian.Client.Core.Interfaces.Symmetric;
using Weardian.Client.Core.Serialization;

namespace Weardian.Client.Core.Services.Symmetric
{
    public class SymmetricMessageHandlerService : ISymmetricMessageHandlerService
    {
        private readonly IKeyManagementService _keyManagementService;
        private readonly IPayloadService _payloadService;
        private readonly IAuthService _authService;

        public SymmetricMessageHandlerService(
            IKeyManagementService keyManagementService,
            IPayloadService payloadService,
            IAuthService authService)
        {
            _keyManagementService = keyManagementService;
            _payloadService = payloadService;
            _authService = authService;
        }

        public async Task<string> HandleAsync(string request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request))
                    throw new ArgumentException("Request type cannot be null, empty or whitespace.", nameof(request));

                var requestType = JsonSerializer.Deserialize<WebViewRequestDto>(request, 
                    JsonSerializeCaseHelper.CaseInsensitiveOptions)
                    ?? throw new InvalidOperationException("Failed to deserialize request type.");

                return requestType.Type switch
                {
                    "register" => await HandleRegistrationRequestAsync(request),
                    "login" => await HandleLoginRequestAsync(request),
                    "encryption" => await HandleEncryptionRequestAsync(request),
                    "decryption" => await HandleDecryptionRequestAsync(request),
                    "retrieveAllKeys" => await HandleRetrieveAllKeysRequestAsync(),
                    "deleteKey" => HandleDeleteKeyRequest(request),
                    _ => throw new InvalidOperationException("Invalid request type.")
                };
            }
            catch (Exception)
            {
                return JsonSerializer.Serialize(
                    new WebViewResponseDto<object>(
                        Type: "Error",
                        Success: false,
                        Data: null,
                        Error: "An error has occured in the application."
                        ), 
                    JsonSerializeCaseHelper.CamelCaseOptions);
            }
        }

        public async Task<string> HandleRegistrationRequestAsync(string request)
        {
            try
            {
                var dto = JsonSerializer.Deserialize<RegisterRequestDto>(request,
                    JsonSerializeCaseHelper.CaseInsensitiveOptions)
                    ?? throw new InvalidOperationException("Deserialization Failed: results cannot be null");

                if (string.IsNullOrWhiteSpace(dto.Email))
                    throw new ArgumentException("Email cannot be null, empty or whitespace", nameof(dto.Email));
                
                if (string.IsNullOrWhiteSpace(dto.Password))
                    throw new ArgumentException("Password cannot be null, empty or whitespace", nameof(dto.Password));

                var result = await _authService.RegisterUserAsync(dto.Email, dto.Password);

                return JsonSerializer.Serialize(
                    new WebViewResponseDto<RegistrationResponseDto>(
                        Type: "register",
                        Success: true,
                        Data: result,
                        Error: result.Error
                        ), 
                    JsonSerializeCaseHelper.CamelCaseOptions);
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(
                    new WebViewResponseDto<RegistrationResponseDto>(
                        Type: "register",
                        Success: false,
                        Data: null,
                        Error: ex.Message
                        ),
                    JsonSerializeCaseHelper.CamelCaseOptions);
            }
        }

        public async Task<string> HandleLoginRequestAsync(string request)
        {
            try
            {
                var dto = JsonSerializer.Deserialize<LoginRequestDto>(request,
                    JsonSerializeCaseHelper.CaseInsensitiveOptions)
                    ?? throw new InvalidOperationException("Deserialization Failed: result cannot be null.");

                if (string.IsNullOrWhiteSpace(dto.Email))
                    throw new ArgumentException("Email cannot be null, empty or whitespace", nameof(dto.Email));

                if (string.IsNullOrWhiteSpace(dto.Password))
                    throw new ArgumentException("Password cannot be null, empty or whitespace", nameof(dto.Password));

                await _authService.LoginAsync(dto.Email, dto.Password);

                return JsonSerializer.Serialize(
                    new WebViewResponseDto<object>(
                        Type: "login",
                        Success: true,
                        Data: null,
                        Error: null
                        ),
                    JsonSerializeCaseHelper.CamelCaseOptions);
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(
                    new WebViewResponseDto<object>(
                        Type: "login",
                        Success: false,
                        Data: null,
                        Error: ex.Message
                        ),
                    JsonSerializeCaseHelper.CamelCaseOptions);
            }
        }

        public async Task<string> HandleEncryptionRequestAsync(string request)
        {
            try
            {
                var dto = JsonSerializer.Deserialize<EncryptionRequestDto>(request,
                    JsonSerializeCaseHelper.CaseInsensitiveOptions)
                    ?? throw new InvalidOperationException("Deserialization Failed: result cannot be null.");

                if (string.IsNullOrWhiteSpace(dto.KeyName))
                    throw new ArgumentException("KeyName cannot be null, empty or whitespace.", nameof(dto.KeyName));

                if (string.IsNullOrWhiteSpace(dto.Password))
                    throw new ArgumentException("Password cannot be null, empty, or whitespace.", nameof(dto.Password));

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
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(
                    new WebViewResponseDto<EncryptionResponseDto>(
                        Type: "encryption",
                        Success: false,
                        Data: null,
                        Error: ex.Message
                    ),
                    JsonSerializeCaseHelper.CamelCaseOptions);
            }
        }

        public async Task<string> HandleDecryptionRequestAsync(string request)
        {
            var dto = JsonSerializer.Deserialize<DecryptionRequestDto>(request, 
                JsonSerializeCaseHelper.CaseInsensitiveOptions);

            if (dto == null || dto.KeyId == Guid.Empty)
                throw new InvalidOperationException("Deserialization Failed: null result or GUID is invalid.");

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
                new WebViewResponseDto<IReadOnlyList<RetrievePayloadResponseDto>>(
                    Type: "retrieveAllKeys",
                    Success: true,
                    Data: keysResult,
                    Error: null
                    ),
                JsonSerializeCaseHelper.CamelCaseOptions); 
        }

        public string HandleDeleteKeyRequest(string request)
        {
            var dto = JsonSerializer.Deserialize<DeleteKeyRequestDto>(request,
                JsonSerializeCaseHelper.CaseInsensitiveOptions);

            if (dto == null || dto.KeyId == Guid.Empty)
                throw new InvalidOperationException("Deserialization Failed: null result or GUID is invalid.");

            var deleted = _payloadService.RemoveRecordsById(dto.KeyId);

            return JsonSerializer.Serialize(
                new WebViewResponseDto<bool>(
                    Type: "deleteKey",
                    Success: true,
                    Data: deleted,
                    Error: null
                    ),
                JsonSerializeCaseHelper.CamelCaseOptions);
        }
    }
}
