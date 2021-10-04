using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleCRUD.Context;
using SimpleCRUD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        private readonly ILogger<WeatherForecastController> _logger;
        protected readonly LibraryContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, LibraryContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public List<Book> Get()
        {
            var teste = _context.Books.ToList();

            return teste;
        }
    }
}
