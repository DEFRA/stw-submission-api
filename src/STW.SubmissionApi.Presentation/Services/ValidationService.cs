namespace STW.SubmissionApi.Presentation.Services;

using NJsonSchema;
using NJsonSchema.Validation;

public class ValidationService : IValidationService
{
    public async Task<List<ValidationError>> Validate(string spsCertificate)
    {
        var schema = await JsonSchema.FromFileAsync("Schemas/enotification/SPSCertificate.json");
        var errors = schema.Validate(spsCertificate);
        return FlattenErrorsList(errors);
    }

    private static List<ValidationError> FlattenErrorsList(IEnumerable<ValidationError> errors)
    {
        var result = new List<ValidationError>();

        foreach (var error in errors)
        {
            if (error is ChildSchemaValidationError childError)
            {
                foreach (var keyValuePair in childError.Errors)
                {
                    result.AddRange(FlattenErrorsList(keyValuePair.Value));
                }
            }
            else
            {
                result.Add(error);
            }
        }

        return result;
    }
}
