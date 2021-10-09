using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Dto;
using CleanArchitecture.Application.WeatherForecasts.Queries.GetCurrentWeatherForecastQuery;
using CleanArchitecture.Application.WeatherForecasts.Queries.GetWeatherForecastQuery;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    public class WeatherForecastController : BaseApiController
    {
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await Mediator.Send(new GetWeatherForecastsQuery());
        }

        [HttpGet("current")]
        public async Task<ActionResult<ServiceResult<CurrentWeatherForecastDto>>> GetCurrentWeather([FromQuery] GetCurrentWeatherForecastQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.Send(query, cancellationToken);
        }
    }
}
