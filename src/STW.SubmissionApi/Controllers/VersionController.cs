namespace STW.SubmissionApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/admin/version")]
public class VersionController : ControllerBase
{
    public VersionController()
    {
    }

    [HttpGet]
    public string Index()
    {
        return "Running on Version 1.0";
    }
}