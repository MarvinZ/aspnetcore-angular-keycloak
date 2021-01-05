namespace WebApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WebApp.Models;

  //  [Authorize] // (Roles = "role1")
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/weatherforecasts")]
    public class WeatherForecastController : ControllerBase
    {
        DogsRepository _DogsRepository;


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DogsRepository dog_repo)
        {
            this.logger = logger;
            _DogsRepository = dog_repo;
        }


        [HttpGet]
        [Route("Getme")]
        public IEnumerable<WeatherForecast> Getme()
        {
            this.logger.LogInformation("weather request");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet]
        [Route("GetDogs")]
        public IEnumerable<Dog> GetDogs()
        {
            
            return _DogsRepository.GetAllDogs();
        }



    }
}
