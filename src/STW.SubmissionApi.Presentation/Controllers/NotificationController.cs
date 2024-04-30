namespace STW.SubmissionApi.Presentation.Controllers;

using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Services;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IValidationService _validationService;

    public NotificationController(IValidationService validationService)
    {
        _validationService = validationService;
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromServices] IOptions<ApiBehaviorOptions> apiBehaviourOptions)
    {
        var rawRequestBody = await Request.Body.ReadAsStringAsync();

        var errors = await _validationService.Validate(rawRequestBody);

        if (errors.Count > 0)
        {
            errors.ForEach(error => ModelState.AddModelError(error.Path ?? string.Empty, error.ToString()));

            return apiBehaviourOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        return Accepted(
            new SubmissionAcceptedDto
            {
                Id = Guid.NewGuid(),
            });
    }
}
