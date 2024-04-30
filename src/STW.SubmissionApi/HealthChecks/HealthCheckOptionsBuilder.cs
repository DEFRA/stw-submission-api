namespace src.HealthChecks;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public class HealthCheckOptionsBuilder
{
    public static HealthCheckOptions Build()
    {
        return new HealthCheckOptions
        {
            AllowCachingResponses = false,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
            },
        };
    }
}