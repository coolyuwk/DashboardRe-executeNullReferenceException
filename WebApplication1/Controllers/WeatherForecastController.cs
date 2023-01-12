using Consul;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using System.Xml.Linq;
using System.Text.Json;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [Route("~/send")]
        public IActionResult SendMessage([FromServices] ICapPublisher capBus)
        {
            LoginUser loginUser = new LoginUser()
            {
                Id = Guid.NewGuid(),
                Role = new string[] { },
                Extend = new Dictionary<string, object>()
            };
            Dictionary<string, string> di = new Dictionary<string, string>();
            di.Add("LoginUser", loginUser.ToJson());
            capBus.Publish("test.show.time", DateTime.Now, di);
            return Ok();
        }

        [NonAction]
        [CapSubscribe("test.show.time")]
        public void ReceiveMessage(DateTime time, [FromCap] CapHeader header)
        {
            Console.WriteLine("message time is:" + time);
        }
    }

    public class LoginUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Manager { get; set; }
        public string Mobile { get; set; }
        public Guid TenantId { get; set; }
        public string[] Role { get; set; }
        public Guid ProvidersId { get; set; }
        public string IdCard { get; set; }
        public Dictionary<string, object> Extend { get; set; }
    }



}