namespace STW.SubmissionApi.Presentation.Extensions;

public static class StreamExtensions
{
    public static async Task<string> ReadAsStringAsync(this Stream stream)
    {
        using var streamReader = new StreamReader(stream);

        return await streamReader.ReadToEndAsync();
    }
}
