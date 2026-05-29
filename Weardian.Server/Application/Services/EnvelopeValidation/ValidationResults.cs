namespace Weardian.Server.Application.Services.EnvelopeValidation
{
    public class ValidationResults
    {
        public bool IsValid => Errors.Count == 0;
        public List<string> Errors { get; } = new List<string>();
    }
}
