namespace Weardian.Client.Core.Services.InputValidation
{
    public sealed class InputValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<string> Errors { get; } = new List<string>();
    }
}
