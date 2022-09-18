using ErrorHandling.Filters;
using ErrorHandling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ErrorHandling.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[CustomExceptionFilter(ErrorPage = "Error1")]
        public IActionResult Index()
        {
            var n1 = 1;
            var n2 = 0;
            var result = n1 / n2;
            return View();
        }

        //[CustomExceptionFilter(ErrorPage = "Error2")]
        public IActionResult Privacy()
        {
            throw new FileNotFoundException();
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return View(new ErrorViewModel { Path = exception.Path, Message = exception.Error.Message });
        }

        public IActionResult Error1()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return View(new ErrorViewModel { Path = exception.Path, Message = exception.Error.Message });
        }

        public IActionResult Error2()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return View(new ErrorViewModel { Path = exception.Path, Message = exception.Error.Message });
        }
    }
}