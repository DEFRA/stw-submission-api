namespace STW.SubmissionApi.Presentation.Services;

using NJsonSchema.Validation;

public interface IValidationService
{
    public Task<List<ValidationError>> Validate(string spsCertificate);
}
