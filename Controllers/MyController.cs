using MediatR;

using Microsoft.AspNetCore.Mvc;

using TestWebApiClient.Domain;
using TestWebApiClient.Services.GetGradebook.Contracts;

namespace TestWebApiClient.Controllers;

[ApiController]
[Route("MyController")]
public class MyController : ControllerBase
{
    private readonly IMediator _mediator;
    private static readonly string[] summaries = new[]
    {
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    };

    public MyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(nameof(GetWeatherForecast))]
    public async Task<ActionResult>
        GetWeatherForecast()
    {
        WeatherForecast[] forecast = Enumerable.Range(1, 5)
            .Select(index =>
                new WeatherForecast(
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();

        return Ok(forecast);
    }

    [HttpGet(nameof(GetGradebook))]
    public async Task<ActionResult>
        GetGradebook()
    {
        var request = new GetGradebookRequest();

        GetGradebookResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}