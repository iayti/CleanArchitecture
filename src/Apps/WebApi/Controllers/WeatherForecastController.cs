using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Dto;
using Application.WeatherForecasts.Queries.GetCurrentWeatherForecastQuery;
using Application.WeatherForecasts.Queries.GetWeatherForecastQuery;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class WeatherForecastController : BaseApiController
    {
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await Mediator.Send(new GetWeatherForecastsQuery());
        }

        [HttpGet("current")]
        public async Task<ActionResult<ServiceResult<CurrentWeatherForecastDto>>> GetCurrentWeather([FromQuery] GetCurrentWeatherForecastQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
