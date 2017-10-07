using System.Collections.Generic;
using Articles.Entities;
using Microsoft.AspNetCore.Mvc;
using ConsoleApplication.Infrastructure;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ConsoleApplication.Articles
{
    [Route("/api/[controller]")]
    public class HelloWorldController : Controller
    {
        public IActionResult Get()
        {
            return Ok("Hello world, from ASP.NET Core in a container!!!!!!!!!!");
        }

    }
}
