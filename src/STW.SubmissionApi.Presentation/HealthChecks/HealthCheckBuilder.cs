namespace STW.SubmissionApi.Presentation.HealthChecks;

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

[ExcludeFromCodeCoverage]
public static class HealthCheckOptionsBuilder
{
    public static HealthCheckOptions Build(string? version)
    {
        return new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                var healthCheckResponse = new HealthCheckResponse(report.Status.ToString(), version ?? "Unknown");
                await context.Response.WriteAsJsonAsync(healthCheckResponse);
            }
        };
    }
}
