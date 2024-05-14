namespace STW.SubmissionApi.Presentation.Controllers;

using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SamplePost()
    {
        var requestBody = await Request.Body.ReadAsStringAsync();

        var contentResult = Content(requestBody);

        contentResult.StatusCode = StatusCodes.Status200OK;

        if (Request.Headers.TryGetValue(HeaderNames.ContentType, out var contentType))
        {
            contentResult.ContentType = contentType;
        }

        return contentResult;
    }
}
